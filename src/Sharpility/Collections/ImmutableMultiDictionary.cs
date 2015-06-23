using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Collections
{
    public abstract class ImmutableMultiDictionary<TKey, TValue>: MultiDictionary<TKey, TValue>
    {
        private readonly IImmutableDictionary<TKey, ICollection<TValue>> dictionary;
        public int Count { get; private set; }

        protected ImmutableMultiDictionary(IImmutableDictionary<TKey, ICollection<TValue>> dictionary)
        {
            this.dictionary = dictionary;
            Count = dictionary.Sum(entry => entry.Value.Count);
        }

        #region UnsupportedOperations
        public void Put(TKey key, TValue value)
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        public void PutAll(TKey key, IEnumerable<TValue> values)
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        public void PutAll(MultiDictionary<TKey, TValue> multiDictionary)
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        public void PutAll(IDictionary<TKey, TValue> dictionary)
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        public bool Remove(TKey key)
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        public bool Remove(TKey key, TValue value)
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        public ICollection<TValue> RemoveAll(TKey key)
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        public ICollection<TValue> ReplaceValues(TKey key, IEnumerable<TValue> values)
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        public void Clear()
        {
            throw new InvalidOperationException("Operation is not supported");
        }

        #endregion

        public ICollection<TValue> Get(TKey key)
        {
            return dictionary.ContainsKey(key) ? dictionary[key] : EmptyCollection();
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
            return dictionary.ContainsKey(key) ? dictionary[key].Count : 0;
        }

        public ISet<TKey> Keys
        {
            get { return dictionary.Keys.ToImmutableHashSet(); }
        }

        public ICollection<TValue> Values
        {
            get
            {
                var result = ImmutableList.CreateBuilder<TValue>();
                foreach (var entry in dictionary)
                {
                    result.AddAll(entry.Value);
                }
                return result.ToImmutableList();
            }
        }

        public ICollection<KeyValuePair<TKey, TValue>> Entries
        {
            get
            {
                var result = ImmutableList.CreateBuilder<KeyValuePair<TKey, TValue>>();
                foreach (var entry in dictionary)
                {
                    result.AddAll(EntriesOf(entry));
                }
                return result.ToImmutableList();
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
            get { return dictionary.Entries().ToImmutableHashSet(); }
        }

        public bool IsEmpty
        {
            get { return dictionary.IsEmpty(); }
        }

        public bool ContainsValue(TValue value)
        {
            return dictionary.Any(entry => entry.Value.Contains(value));
        }

        public bool ContainsEntry(TKey key, TValue value)
        {
            return dictionary.ContainsKey(key) && dictionary[key].Contains(value);
        }

        public IDictionary<TKey, ICollection<TValue>> ToDictionary()
        {
            var result = new Dictionary<TKey, ICollection<TValue>>(dictionary.Count);
            foreach (var entry in dictionary)
            {
                result.Put(entry.Key, MutableCopy(entry.Value));
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            var that = obj as MultiDictionary<TKey, TValue>;
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

        protected abstract ICollection<TValue> EmptyCollection();

        protected abstract ICollection<TValue> MutableCopy(ICollection<TValue> values);

        public abstract class AbstractImmutableListMultiDictionaryBuilder<T, TV, TR>
            where TR: ImmutableMultiDictionary<T, TV>
        {
            private readonly MultiDictionary<T, TV> dictionary;

            internal AbstractImmutableListMultiDictionaryBuilder(MultiDictionary<T, TV> dictionary)
            {
                this.dictionary = dictionary;
            }

            public AbstractImmutableListMultiDictionaryBuilder<T, TV, TR> Put(T key, TV value)
            {
                dictionary.Put(key, value);
                return this;
            }

            public AbstractImmutableListMultiDictionaryBuilder<T, TV, TR> PutAll(T key, IEnumerable<TV> values)
            {
                dictionary.PutAll(key, values);
                return this;
            }

            public AbstractImmutableListMultiDictionaryBuilder<T, TV, TR> PutAll(MultiDictionary<T, TV> multiDictionary)
            {
                dictionary.PutAll(multiDictionary);
                return this;
            }

            public AbstractImmutableListMultiDictionaryBuilder<T, TV, TR> PutAll(IDictionary<T, TV> dictionary)
            {
                this.dictionary.PutAll(dictionary);
                return this;
            }

            public TR Build()
            {
                var builder = ImmutableDictionary.CreateBuilder<T, ICollection<TV>>();
                foreach (var entry in dictionary.MultiEntries)
                {
                    builder.Put(entry.Key, Immutable(entry.Value));
                }
                var immutableDictionary = builder.ToImmutableDictionary();
                return Crete(immutableDictionary);
            }

            protected abstract ICollection<TV> Immutable(ICollection<TV> values);

            protected abstract TR Crete(IImmutableDictionary<T, ICollection<TV>> dictionary);
        }
    }
}
