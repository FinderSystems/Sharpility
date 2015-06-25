using System.Collections.Generic;
using System.Linq;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Collections
{
    public abstract class AbstractMultiDictionary<TKey, TValue>: MultiDictionary<TKey, TValue>
    {
        public int Count { get; private set; }

        private readonly IDictionary<TKey, ICollection<TValue>> dictionary;
        private readonly int valuesCapacity;

        internal AbstractMultiDictionary()
        {
            Count = 0;
            valuesCapacity = 0;
            dictionary = new Dictionary<TKey, ICollection<TValue>>();
        }

        internal AbstractMultiDictionary(int keysCapacity, int valuesCapacity)
        {
            Count = 0;
            this.valuesCapacity = valuesCapacity;
            dictionary = new Dictionary<TKey, ICollection<TValue>>(keysCapacity);
        }

        public void Put(TKey key, TValue value)
        {
            var keyValues = ValuesOf(key);
            var previousCount = keyValues.Count;
            keyValues.Add(value);
            Count += (keyValues.Count - previousCount);
        }

        public void PutAll(TKey key, IEnumerable<TValue> values)
        {
            var keyValues = ValuesOf(key);
            var previousCount = keyValues.Count;
            keyValues.AddAll(values);
            Count += (keyValues.Count - previousCount);
        }

        public void PutAll(MultiDictionary<TKey, TValue> multiDictionary)
        {
            foreach (var entry in multiDictionary.MultiEntries)
            {
                PutAll(entry.Key, entry.Value);
            }
        }

        public void PutAll(IEnumerable<KeyValuePair<TKey, TValue>> entries)
        {
            foreach (var entry in entries)
            {
                Put(entry.Key, entry.Value);
            }
        }

        public bool Remove(TKey key)
        {
            var removedCount = dictionary.ContainsKey(key) ? dictionary[key].Count : 0;
            var result = dictionary.Remove(key);
            Count -= removedCount;
            return result;
        }

        public bool Remove(TKey key, TValue value)
        {
            var keyValues = dictionary.GetIfPresent(key);
            var result = keyValues != null && keyValues.Remove(value);
            if (result)
            {
                Count -= 1;
            }
            if (keyValues != null && keyValues.IsEmpty() && dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);
            }
            return result;
        }

        public ICollection<TValue> RemoveAll(TKey key)
        {
            var values = Get(key);
            dictionary.Remove(key);
            Count -= values.Count;
            return values;
        }

        public ICollection<TValue> ReplaceValues(TKey key, IEnumerable<TValue> values)
        {
            var replacedValues = Get(key);
            var keyValues = ValuesOf(key);
            keyValues.Clear();
            keyValues.AddAll(values);
            Count -= replacedValues.Count;
            Count += keyValues.Count;
            if (keyValues.IsEmpty())
            {
                dictionary.Remove(key);
            }
            return replacedValues;
        }

        public void Clear()
        {
            dictionary.Clear();
            Count = 0;
        }

        public ICollection<TValue> Get(TKey key)
        {
            var values = dictionary.GetIfPresent(key) ?? CreateCollection(valuesCapacity);
            return ResultCollection(values);
        }

        public ICollection<TValue> this[TKey key]
        {
            get { return Get(key); }
        }

        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public int ValuesCount(TKey key)
        {
            var keyValues = dictionary.GetIfPresent(key);
            return keyValues != null ? keyValues.Count : 0;
        }

        public ISet<TKey> Keys
        {
            get
            {
                return dictionary.Keys.ToSet();
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                var result = new List<TValue>(valuesCapacity);
                foreach (var entry in MultiEntries)
                {
                    result.AddAll(entry.Value);
                }
                return result;
            }
        }

        public ICollection<KeyValuePair<TKey, TValue>> Entries
        {
            get
            {
                var entries = new List<KeyValuePair<TKey, TValue>>();
                foreach (var multiEntry in dictionary)
                {
                    entries.AddAll(EntriesOf(multiEntry));
                }
                return entries;
            }
        }

        private static IEnumerable<KeyValuePair<TKey, TValue>> EntriesOf(KeyValuePair<TKey, ICollection<TValue>> multiEntry)
        {
            return multiEntry.Value
                .Select(value => new KeyValuePair<TKey, TValue>(multiEntry.Key, value))
                .ToList();
        }

        public ICollection<KeyValuePair<TKey, ICollection<TValue>>> MultiEntries
        {
            get
            {
                var multiEntries = new HashSet<KeyValuePair<TKey, ICollection<TValue>>>();
                foreach (var multiEntry in dictionary)
                {
                    var valuesCopy = ComparableCollection(multiEntry.Value);
                    multiEntries.Add(new KeyValuePair<TKey, ICollection<TValue>>(multiEntry.Key, valuesCopy));
                }
                return multiEntries;
            }
        }

        public bool IsEmpty
        {
            get { return dictionary.IsEmpty(); }
        }

        public bool ContainsValue(TValue value)
        {
            return dictionary.Any(multiEntry => multiEntry.Value.Contains(value));
        }

        public bool ContainsEntry(TKey key, TValue value)
        {
            var keyValues = dictionary.GetIfPresent(key);
            return keyValues != null && keyValues.Contains(value);
        }

        public IDictionary<TKey, ICollection<TValue>> ToDictionary()
        {
            var result = new Dictionary<TKey, ICollection<TValue>>(dictionary.Count);
            result.PutAll(dictionary);
            return ComparableDictionary<TKey, ICollection<TValue>>.Of(result);
        }

        private ICollection<TValue> ValuesOf(TKey key)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary[key] = CreateCollection(valuesCapacity);
            }
            return dictionary[key];
        }

        public override bool Equals(object obj)
        {
            var that = obj as MultiDictionary<TKey, TValue>;
            return that != null && Objects.Equal(dictionary, that.ToDictionary());
        }

        public override int GetHashCode()
        {
            return Objects.Hash(dictionary);
        }

        public override string ToString()
        {
            return Strings.ToString(dictionary);
        }

        protected abstract ICollection<TValue> CreateCollection(int capacity);

        protected abstract ICollection<TValue> ResultCollection(ICollection<TValue> collection);

        protected abstract ICollection<TValue> ComparableCollection(ICollection<TValue> collection);
    }
}
