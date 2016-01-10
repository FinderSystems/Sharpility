using System.Collections.Generic;
using System.Linq;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Collections
{
    /// <summary>
    /// Abstraction of CompositeDictionary.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key</typeparam>
    /// <typeparam name="TSecondaryKey">Type of secondary key</typeparam>
    /// <typeparam name="TValue">Type of value</typeparam>
    public abstract class AbstractCompositeDictionary<TPrimaryKey, TSecondaryKey, TValue> : CompositeDictionary<TPrimaryKey, TSecondaryKey, TValue>
    {
        private readonly IDictionary<TPrimaryKey, IDictionary<TSecondaryKey, TValue>> dictionary;
        private readonly int? secondaryKeyCapacity;
        public int Count { get; private set; }

        internal AbstractCompositeDictionary(IDictionary<TPrimaryKey, IDictionary<TSecondaryKey, TValue>> dictionary, int? secondaryKeyCapacity)
        {
            this.dictionary = dictionary;
            this.secondaryKeyCapacity = secondaryKeyCapacity;
            RecalculateSize();
        }

        internal AbstractCompositeDictionary(IDictionary<TPrimaryKey, IDictionary<TSecondaryKey, TValue>> dictionary)
            :this(dictionary, null)
        {
        }

        public void Put(TPrimaryKey primaryKey, TSecondaryKey secondaryKey, TValue value)
        {
            FastPut(primaryKey, secondaryKey, value);
            RecalculateSize();
        }

        private void FastPut(TPrimaryKey primaryKey, TSecondaryKey secondaryKey, TValue value)
        {
            var seondaryKeyDictionary = SeondaryKeyDictionary(primaryKey);
            seondaryKeyDictionary.Put(secondaryKey, value);
        }

        public void Put(CompositeKey<TPrimaryKey, TSecondaryKey> key, TValue value)
        {
            Put(key.Primary, key.Secondary, value);
        }

        public void PutAll(CompositeDictionary<TPrimaryKey, TSecondaryKey, TValue> compositeDictionary)
        {
            foreach (var entry in compositeDictionary.Entries)
            {
                FastPut(entry.Key, entry.Value.Key, entry.Value.Value);
            }
            RecalculateSize();
        }

        public void PutAll(IDictionary<TPrimaryKey, IDictionary<TSecondaryKey, TValue>> dictionary)
        {
            foreach (var entry in dictionary)
            {
                PutEntry(entry);
            }
            RecalculateSize();
        }

        private void PutEntry(KeyValuePair<TPrimaryKey, IDictionary<TSecondaryKey, TValue>> entry)
        {
            foreach (var secondaryEntry in entry.Value)
            {
                FastPut(entry.Key, secondaryEntry.Key, secondaryEntry.Value);
            }
        }

        public TValue Get(CompositeKey<TPrimaryKey, TSecondaryKey> key)
        {
            return Get(key.Primary, key.Secondary);
        }

        public TValue Get(TPrimaryKey primaryKey, TSecondaryKey secondaryKey)
        {
            return ContainsKey(primaryKey, secondaryKey) ? 
                dictionary[primaryKey][secondaryKey] : default(TValue);
        }

        public TValue this[CompositeKey<TPrimaryKey, TSecondaryKey> key]
        {
            get { return Get(key); }
        }

        public ICollection<TValue> Values
        {
            get
            {
                var result = CreateList<TValue>();
                foreach (var entry in dictionary)
                {
                    result.AddAll(entry.Value.Values);
                }
                return result;
            }
        }

        public ICollection<TValue> PrimaryKeyValues(TPrimaryKey primaryKey)
        {
            var result = CreateList<TValue>();
            if (dictionary.ContainsKey(primaryKey))
            {
                result.AddAll(dictionary[primaryKey].Values);
            }
            return result;
        }

        public ICollection<TValue> SecondaryKeyValues(TSecondaryKey secondaryKey)
        {
            var result = CreateList<TValue>();
            foreach (var entry in dictionary.Where(entry => entry.Value.ContainsKey(secondaryKey)))
            {
                result.Add(entry.Value[secondaryKey]);
            }
            return result;
        }

        public ISet<CompositeKey<TPrimaryKey, TSecondaryKey>> Keys
        {
            get
            {
                var result = CreateSet<CompositeKey<TPrimaryKey, TSecondaryKey>>();
                foreach (var entry in dictionary)
                {
                    AppendEntries(result, entry);
                }
                return result; 
            }
        }

        private static void AppendEntries(ISet<CompositeKey<TPrimaryKey, TSecondaryKey>> result, KeyValuePair<TPrimaryKey, IDictionary<TSecondaryKey, TValue>> entry)
        {
            foreach (var secondaryKey in entry.Value.Keys)
            {
                result.Add(new CompositeKey<TPrimaryKey, TSecondaryKey>(entry.Key, secondaryKey));
            }
        }

        public ISet<TPrimaryKey> PrimaryKeys
        {
            get { return dictionary.Keys.ToSet(); }
        }

        public ISet<TSecondaryKey> SecondaryKeys
        {
            get
            {
                var result = CreateSet<TSecondaryKey>();
                foreach (var entry in dictionary)
                {
                    result.AddAll(entry.Value.Keys);
                }
                return result;
            }
        }

        public int PrimaryKeysCount
        {
            get { return dictionary.Count; }
        }

        public bool IsEmpty
        {
            get { return dictionary.IsEmpty(); }
        }

        public void Clear()
        {
            dictionary.Clear();
            Count = 0;
        }

        public ICollection<KeyValuePair<TPrimaryKey, KeyValuePair<TSecondaryKey, TValue>>> Entries
        {
            get
            {
                var result = CreateSet<KeyValuePair<TPrimaryKey, KeyValuePair<TSecondaryKey, TValue>>>();
                foreach (var entry in dictionary)
                {
                    AppendEntries(result, entry);
                }
                return result;
            }
        }

        private static void AppendEntries(ISet<KeyValuePair<TPrimaryKey, KeyValuePair<TSecondaryKey, TValue>>> result, KeyValuePair<TPrimaryKey, IDictionary<TSecondaryKey, TValue>> entry)
        {
            foreach (var secondaryEntry in entry.Value)
            {
                result.Add(new KeyValuePair<TPrimaryKey, KeyValuePair<TSecondaryKey, TValue>>(entry.Key, secondaryEntry));
            }
        }

        public ICollection<KeyValuePair<TSecondaryKey, TValue>> PrimaryKeyEntries(TPrimaryKey primaryKey)
        {
            var secondaryDictionary = dictionary.ContainsKey(primaryKey)
                ? dictionary[primaryKey]
                : Dictionaries.Empty<TSecondaryKey, TValue>();
            return secondaryDictionary.Entries();
        }

        public ICollection<KeyValuePair<TPrimaryKey, TValue>> SecondaryKeyEntries(TSecondaryKey secondaryKey)
        {
            var result = CreateList<KeyValuePair<TPrimaryKey, TValue>>();
            foreach (var entry in dictionary.Where(entry => entry.Value.ContainsKey(secondaryKey)))
            {
                result.Add(new KeyValuePair<TPrimaryKey, TValue>(entry.Key, entry.Value[secondaryKey]));
            }
            return result;
        }

        public bool Remove(TPrimaryKey primaryKey, TSecondaryKey secondaryKey)
        {
            if (ContainsKey(primaryKey, secondaryKey))
            {
                var secondaryKeyDictionary = dictionary[primaryKey];
                var result = secondaryKeyDictionary.Remove(secondaryKey);
                if (secondaryKeyDictionary.IsEmpty())
                {
                    dictionary.Remove(primaryKey);
                }
                return result;
            }
            return false;
        }

        public bool Remove(CompositeKey<TPrimaryKey, TSecondaryKey> key)
        {
            var result = Remove(key.Primary, key.Secondary);
            RecalculateSize();
            return result;
        }

        public ICollection<TValue> RemoveByPrimaryKey(TPrimaryKey primaryKey)
        {
            var removed = dictionary.ContainsKey(primaryKey)
                ? dictionary[primaryKey]
                : Dictionaries.Empty<TSecondaryKey, TValue>();
            dictionary.Remove(primaryKey);
            RecalculateSize();
            return removed.Values;
        }

        public ICollection<TValue> RemoveBySecondaryKey(TSecondaryKey secondaryKey)
        {
            var removed = CreateList<TValue>();
            var primaryKeysToRemove = new HashSet<TPrimaryKey>();
            foreach (var entry in dictionary.Where(entry => entry.Value.ContainsKey(secondaryKey)))
            {
                removed.Add(entry.Value[secondaryKey]);
                entry.Value.Remove(secondaryKey);
                if (entry.Value.IsEmpty())
                {
                    primaryKeysToRemove.Add(entry.Key);
                }
            }
            foreach (var primaryKey in primaryKeysToRemove)
            {
                dictionary.Remove(primaryKey);
            }
            RecalculateSize();
            return removed;
        }

        public bool ContainsKey(TPrimaryKey primaryKey, TSecondaryKey secondaryKey)
        {
            return dictionary.ContainsKey(primaryKey) && dictionary[primaryKey].ContainsKey(secondaryKey);
        }

        public bool ContainsKey(CompositeKey<TPrimaryKey, TSecondaryKey> key)
        {
            return ContainsKey(key.Primary, key.Secondary);
        }

        public bool ContainsValue(TValue value)
        {
            return dictionary.Any(entry => entry.Value.Values.Contains(value));
        }

        public bool ContainsPrimaryKeyValue(TPrimaryKey primaryKey, TValue value)
        {
            var secondaryEntry = dictionary.ContainsKey(primaryKey)
                ? dictionary[primaryKey]
                : Dictionaries.Empty<TSecondaryKey, TValue>();
            return secondaryEntry.Any(entry => Objects.Equal(entry.Value, value));
        }

        public bool ContainsSecondaryKeyValue(TSecondaryKey secondaryKey, TValue value)
        {
            return dictionary.Any(entry => entry.Value.ContainsKey(secondaryKey) && Objects.Equal(entry.Value[secondaryKey], value));
        }

        public bool ContainsEntry(TPrimaryKey primaryKey, TSecondaryKey secondaryKey, TValue value)
        {
            return dictionary.ContainsKey(primaryKey) && dictionary[primaryKey].ContainsKey(secondaryKey) &&
                   Objects.Equal(dictionary[primaryKey][secondaryKey], value);
        }

        public bool ContainsEntry(CompositeKey<TPrimaryKey, TSecondaryKey> key, TValue value)
        {
            return ContainsEntry(key.Primary, key.Secondary, value);
        }

        public IDictionary<TPrimaryKey, IDictionary<TSecondaryKey, TValue>> ToDictionary()
        {
            var result = new Dictionary<TPrimaryKey, IDictionary<TSecondaryKey, TValue>>(dictionary.Count);
            foreach (var entry in dictionary)
            {
                var secondaryEntry = new Dictionary<TSecondaryKey, TValue>(entry.Value.Count);
                secondaryEntry.PutAll(entry.Value);
                result.Put(entry.Key, secondaryEntry);
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj is AbstractCompositeDictionary<TPrimaryKey, TSecondaryKey, TValue>)
            {
                return Objects.Equal(dictionary,
                    ((AbstractCompositeDictionary<TPrimaryKey, TSecondaryKey, TValue>) obj).dictionary);
            }
            var that = obj as CompositeDictionary<TPrimaryKey, TSecondaryKey, TValue>;
            return that != null && Objects.Equal(dictionary, that.ToDictionary());
        }

        public override int GetHashCode()
        {
            return Objects.HashCode(dictionary);
        }

        public override string ToString()
        {
            return Strings.ToString(dictionary);
        }

        private IDictionary<TSecondaryKey, TValue> SeondaryKeyDictionary(TPrimaryKey primaryKey)
        {
            if (!dictionary.ContainsKey(primaryKey))
            {
                var secondaryDictionary = secondaryKeyCapacity != null
                    ? CreateDictionary<TSecondaryKey, TValue>((int) secondaryKeyCapacity)
                    : CreateDictionary<TSecondaryKey, TValue>();
                dictionary.Put(primaryKey, secondaryDictionary);
            }
            return dictionary[primaryKey];
        }

        private void RecalculateSize()
        {
            Count = dictionary.Sum(entry => entry.Value.Count);
        }

        /// <summary>
        /// Creates dictionary with given capacity.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="capacity">Dictionary capacity</param>
        /// <returns>created dictionary</returns>
        protected abstract IDictionary<T, TV> CreateDictionary<T, TV>(int capacity);

        /// <summary>
        /// Creates dictionary.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <returns>created dictionary</returns>
        protected abstract IDictionary<T, TV> CreateDictionary<T, TV>();

        /// <summary>
        /// Creates list matching CompositDictionary implementatio with given capacity.
        /// </summary>
        /// <typeparam name="T">Type of list item</typeparam>
        /// <param name="capacity">List capacity</param>
        /// <returns>created list</returns>
        protected abstract ICollection<T> CreateList<T>(int capacity);

        /// <summary>
        /// Creates list matching CompositDictionary implementation.
        /// </summary>
        /// <typeparam name="T">Type of list item</typeparam>
        /// <returns>created list</returns>
        protected abstract ICollection<T> CreateList<T>();

        /// <summary>
        /// Creates set.
        /// </summary>
        /// <typeparam name="T">Type of set</typeparam>
        /// <returns>created set</returns>
        protected abstract ISet<T> CreateSet<T>();
    }
}
