using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Collections
{
    public sealed class ImmutableCompositeDictionary<TPrimaryKey, TSecondaryKey, TValue> : CompositeDictionary<TPrimaryKey, TSecondaryKey, TValue>
    {
        private readonly int count;
        private readonly IImmutableDictionary<TPrimaryKey, IImmutableDictionary<TSecondaryKey, TValue>> dictionary;

        internal ImmutableCompositeDictionary(IImmutableDictionary<TPrimaryKey, IImmutableDictionary<TSecondaryKey, TValue>> dictionary)
        {
            this.dictionary = dictionary;
            count = dictionary.Sum(entry => entry.Value.Count);
        }

        public static ImmutableCompositeDictionaryBuilder<TPrimaryKey, TSecondaryKey, TValue> Builder()
        {
            return new ImmutableCompositeDictionaryBuilder<TPrimaryKey, TSecondaryKey, TValue>();
        }

        #region UnsupportedOperations
        public void Put(TPrimaryKey primaryKey, TSecondaryKey secondaryKey, TValue value)
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        public void Put(CompositeKey<TPrimaryKey, TSecondaryKey> key, TValue value)
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        public void PutAll(CompositeDictionary<TPrimaryKey, TSecondaryKey, TValue> compositeDictionary)
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        public void PutAll(IDictionary<TPrimaryKey, IDictionary<TSecondaryKey, TValue>> dictionary)
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        public void Clear()
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        public bool Remove(TPrimaryKey primaryKey, TSecondaryKey secondaryKey)
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        public bool Remove(CompositeKey<TPrimaryKey, TSecondaryKey> key)
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        public ICollection<TValue> RemoveByPrimaryKey(TPrimaryKey primaryKey)
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        public ICollection<TValue> RemoveBySecondaryKey(TSecondaryKey secondaryKey)
        {
            throw new InvalidOperationException("Operation is not supported");
        }
        #endregion

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
                var result = ImmutableList.CreateBuilder<TValue>();
                foreach (var entry in dictionary)
                {
                    result.AddAll(entry.Value.Values);
                }
                return result.ToImmutable();
            }
        }

        public ICollection<TValue> PrimaryKeyValues(TPrimaryKey primaryKey)
        {
            var result = ImmutableList.CreateBuilder<TValue>();
            if (dictionary.ContainsKey(primaryKey))
            {
                result.AddAll(dictionary[primaryKey].Values);
            }
            return result.ToImmutable();
        }

        public ICollection<TValue> SecondaryKeyValues(TSecondaryKey secondaryKey)
        {
            var result = ImmutableList.CreateBuilder<TValue>();
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
                var result = ImmutableHashSet.CreateBuilder<CompositeKey<TPrimaryKey, TSecondaryKey>>();
                foreach (var entry in dictionary)
                {
                    result.AddAll(SecondaryKeysOf(entry.Key, entry.Value));
                }
                return result.ToImmutable();
            }
        }

        private static IEnumerable<CompositeKey<TPrimaryKey, TSecondaryKey>> SecondaryKeysOf(TPrimaryKey primaryKey,
            IReadOnlyDictionary<TSecondaryKey, TValue> seondaryKeyDictionary)
        {
            var result = ImmutableHashSet.CreateBuilder<CompositeKey<TPrimaryKey, TSecondaryKey>>();
            foreach (var secondaryKey in seondaryKeyDictionary.Keys)
            {
                result.Add(CompositeKeys.Of(primaryKey, secondaryKey));
            }
            return result.ToImmutable();
        } 

        public ISet<TPrimaryKey> PrimaryKeys
        {
            get { return dictionary.Keys.ToImmutableHashSet(); }
        }

        public ISet<TSecondaryKey> SecondaryKeys
        {
            get
            {
                var result = ImmutableHashSet.CreateBuilder<TSecondaryKey>();
                foreach (var entry in dictionary)
                {
                    result.AddAll(entry.Value.Keys);   
                }
                return result.ToImmutable();
            }
        }

        public int Count
        {
            get { return count; }
        }

        public int PrimaryKeysCount
        {
            get { return dictionary.Count; }
        }

        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        public ICollection<KeyValuePair<TPrimaryKey, KeyValuePair<TSecondaryKey, TValue>>> Entries
        {
            get
            {
                var result = ImmutableList.CreateBuilder<KeyValuePair<TPrimaryKey, KeyValuePair<TSecondaryKey, TValue>>>();
                foreach (var entry in dictionary)
                {
                    foreach (var secondaryEntry in entry.Value)
                    {
                        result.Add(KeyValuePairs.Of(entry.Key, secondaryEntry));
                    }
                }
                return result.ToImmutable();
            }
        }

        public ICollection<KeyValuePair<TSecondaryKey, TValue>> PrimaryKeyEntries(TPrimaryKey primaryKey)
        {
            return dictionary.ContainsKey(primaryKey)
                ? dictionary[primaryKey].ToImmutableHashSet()
                : Sets.EmptySet<KeyValuePair<TSecondaryKey, TValue>>();
        }

        public ICollection<KeyValuePair<TPrimaryKey, TValue>> SecondaryKeyEntries(TSecondaryKey secondaryKey)
        {
            var result = ImmutableList.CreateBuilder<KeyValuePair<TPrimaryKey, TValue>>();
            foreach (var entry in dictionary)
            {
                if (entry.Value.ContainsKey(secondaryKey))
                {
                    foreach (var secondaryEntry in entry.Value)
                    {
                        result.Add(KeyValuePairs.Of(entry.Key, secondaryEntry.Value));
                    }
                }
            }
            return result.ToImmutable();
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
            return dictionary.SelectMany(entry => entry.Value).Any(secondaryEntry => Objects.Equal(secondaryEntry.Value, value));
        }

        public bool ContainsPrimaryKeyValue(TPrimaryKey primaryKey, TValue value)
        {
            return dictionary.ContainsKey(primaryKey) && 
                dictionary[primaryKey].Any(entry => Objects.Equal(entry.Value, value));
        }

        public bool ContainsSecondaryKeyValue(TSecondaryKey secondaryKey, TValue value)
        {
            return dictionary.Any(entry => entry.Value.ContainsKey(secondaryKey) && Objects.Equal(entry.Value[secondaryKey], value));
        }

        public bool ContainsEntry(TPrimaryKey primaryKey, TSecondaryKey secondaryKey, TValue value)
        {
            return ContainsKey(primaryKey, secondaryKey) && Objects.Equal(dictionary[primaryKey][secondaryKey], value);
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
                secondaryEntry.AddAll(entry.Value);
                result.Put(entry.Key, secondaryEntry);
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj is ImmutableCompositeDictionary<TPrimaryKey, TSecondaryKey, TValue>)
            {
                return Objects.Equal(dictionary,
                    ((ImmutableCompositeDictionary<TPrimaryKey, TSecondaryKey, TValue>) obj).dictionary);
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

        public sealed class ImmutableCompositeDictionaryBuilder<TPk, TSk, TV>
        {
            private readonly IDictionary<TPk, ImmutableDictionary<TSk, TV>.Builder> values = 
                new Dictionary<TPk, ImmutableDictionary<TSk, TV>.Builder>();

            internal ImmutableCompositeDictionaryBuilder()
            {
            }

            public ImmutableCompositeDictionaryBuilder<TPk, TSk, TV> Put(TPk primaryKey, TSk secondaryKey, TV value)
            {
                if (!values.ContainsKey(primaryKey))
                {
                    values.Put(primaryKey, ImmutableDictionary.CreateBuilder<TSk, TV>());
                }
                values[primaryKey].Put(secondaryKey, value);
                return this;
            }

            public ImmutableCompositeDictionaryBuilder<TPk, TSk, TV> Put(CompositeKey<TPk, TSk> key, TV value)
            {
                return Put(key.Primary, key.Secondary, value);
            }

            public ImmutableCompositeDictionaryBuilder<TPk, TSk, TV> PutAll(CompositeDictionary<TPk, TSk, TV> compositeDictionary)
            {
                foreach (var entry in compositeDictionary.Entries)
                {
                    Put(entry.Key, entry.Value.Key, entry.Value.Value);
                }
                return this;
            }

            public ImmutableCompositeDictionaryBuilder<TPk, TSk, TV> PutAll(IDictionary<TPk, IDictionary<TSk, TV>> dictionary)
            {
                foreach (var entry in dictionary)
                {
                    foreach (var secondaryEntry in entry.Value)
                    {
                        Put(entry.Key, secondaryEntry.Key, secondaryEntry.Value);
                    }
                }
                return this;
            }

            public ImmutableCompositeDictionary<TPk, TSk, TV> Build()
            {
                var result = ImmutableDictionary.CreateBuilder<TPk, IImmutableDictionary<TSk, TV>>();
                foreach (var entry in values)
                {
                    result.Put(entry.Key, entry.Value.ToImmutable());
                }
                return new ImmutableCompositeDictionary<TPk, TSk, TV>(result.ToImmutable());
            } 
        } 
    }
}
