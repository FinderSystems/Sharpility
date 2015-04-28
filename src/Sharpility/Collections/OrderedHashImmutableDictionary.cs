using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Collections
{
    public sealed class OrderedHashImmutableDictionary<T, TV>: OrderedImmutableDictionary<T, TV>
    {
        private readonly IImmutableList<T> orderedKeys;
        private readonly IImmutableDictionary<T, TV> dictionary;

        private OrderedHashImmutableDictionary(IImmutableList<T> orderedKeys, IImmutableDictionary<T, TV> dictionary)
        {
            this.orderedKeys = orderedKeys;
            this.dictionary = dictionary;
        }

        public static Builder CreateBuilder()
        {
            return new Builder();
        }

        public IEnumerator<KeyValuePair<T, TV>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        public int Count
        {
            get { return dictionary.Count; }
        }

        public bool ContainsKey(T key)
        {
            return dictionary.ContainsKey(key);
        }

        public bool TryGetValue(T key, out TV value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        public TV this[T key]
        {
            get { return dictionary[key]; }
        }

        public IEnumerable<T> Keys
        {
            get { return dictionary.Keys; }
        }

        public IEnumerable<TV> Values
        {
            get { return dictionary.Values; }
        }

        public IImmutableDictionary<T, TV> Clear()
        {
            return dictionary.Clear();
        }

        public IImmutableDictionary<T, TV> Add(T key, TV value)
        {
            var newOrderedKeys = orderedKeys.Contains(key) ? orderedKeys : orderedKeys.Add(key);
            return new OrderedHashImmutableDictionary<T, TV>(newOrderedKeys, dictionary.Add(key, value));
        }

        public IImmutableDictionary<T, TV> AddRange(IEnumerable<KeyValuePair<T, TV>> pairs)
        {
            var newOrderedKeys = ImmutableList.CreateBuilder<T>();
            newOrderedKeys.AddAll(orderedKeys);
            foreach (var entry in pairs)
            {
                if (!newOrderedKeys.Contains(entry.Key))
                {
                    newOrderedKeys.Add(entry.Key);
                }
            }
            return new OrderedHashImmutableDictionary<T, TV>(newOrderedKeys.ToImmutable(), dictionary.AddRange(pairs));
        }

        public IImmutableDictionary<T, TV> SetItem(T key, TV value)
        {
            return new OrderedHashImmutableDictionary<T, TV>(orderedKeys, dictionary.SetItem(key, value));
        }

        public IImmutableDictionary<T, TV> SetItems(IEnumerable<KeyValuePair<T, TV>> items)
        {
            return new OrderedHashImmutableDictionary<T, TV>(orderedKeys, dictionary.SetItems(items));
        }

        public IImmutableDictionary<T, TV> RemoveRange(IEnumerable<T> keys)
        {
            var newOrderedKeys = orderedKeys.Minus(keys);
            return new OrderedHashImmutableDictionary<T, TV>(newOrderedKeys, dictionary.RemoveRange(keys));
        }

        public IImmutableDictionary<T, TV> Remove(T key)
        {
            var newOrderedKeys = orderedKeys.Remove(key);
            return new OrderedHashImmutableDictionary<T, TV>(newOrderedKeys, dictionary.Remove(key));
        }

        public bool Contains(KeyValuePair<T, TV> pair)
        {
            return dictionary.Contains(pair);
        }

        public bool TryGetKey(T equalKey, out T actualKey)
        {
            return dictionary.TryGetKey(equalKey, out actualKey);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        public IImmutableList<T> OrderedKeys
        {
            get { return orderedKeys; }
        }

        public IImmutableList<KeyValuePair<T, TV>> OrderedEntries
        {
            get
            {
                var builder = ImmutableList.CreateBuilder<KeyValuePair<T, TV>>() ;
                foreach (var key in orderedKeys)
                {
                    builder.Add(Dictionaries.Entry(key, this.Get(key)));
                }
                return builder.ToImmutable();
            }
        }

        public sealed class Builder
        {
            private readonly ImmutableList<T>.Builder keysOrder = ImmutableList.CreateBuilder<T>();
            private readonly ImmutableDictionary<T, TV>.Builder dictionary = ImmutableDictionary.CreateBuilder<T, TV>();

            internal Builder()
            {
            }

            public Builder Put(T key, TV value)
            {
                if (!keysOrder.Contains(key))
                {
                    keysOrder.Add(key);
                }
                dictionary.Put(key, value);
                return this;
            }

            public Builder Put(KeyValuePair<T, TV> entry)
            {
                return Put(entry.Key, entry.Value);
            }

            public Builder PutAll(IEnumerable<KeyValuePair<T, TV>> entries)
            {
                foreach (var entry in entries)
                {
                    Put(entry);
                }
                return this;
            }

            public OrderedImmutableDictionary<T, TV> Build()
            {
                return new OrderedHashImmutableDictionary<T, TV>(keysOrder.ToImmutable(), dictionary.ToImmutable());
            } 
        }
    }
}
