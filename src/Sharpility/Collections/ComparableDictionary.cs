using System;
using System.Collections;
using System.Collections.Generic;
using Sharpility.Util;

namespace Sharpility.Collections
{
    internal sealed class ComparableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary
    {
        private readonly IDictionary<TKey, TValue> dictionary;

        private ComparableDictionary(IDictionary<TKey, TValue> dictionary)
        {
            this.dictionary = dictionary;
        }

        internal static ComparableDictionary<TKey, TValue> Of(IDictionary<TKey, TValue> dictionary)
        {
            return new ComparableDictionary<TKey, TValue>(dictionary);
        }

        void IDictionary.Clear()
        {
            dictionary.Clear();
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary) dictionary).GetEnumerator();
        }

        public void Remove(object key)
        {
            ((IDictionary)dictionary).Remove(key);
        }

        object IDictionary.this[object key]
        {
            get { return ((IDictionary)dictionary)[key]; }
            set { ((IDictionary) dictionary)[key] = value; }
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            dictionary.Add(item);
        }

        public bool Contains(object key)
        {
            return ((IDictionary) dictionary).Contains(key);
        }

        public void Add(object key, object value)
        {
            ((IDictionary) dictionary).Add(key, value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            dictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return dictionary.Remove(item);
        }

        public void CopyTo(Array array, int index)
        {
            ((IDictionary) dictionary).CopyTo(array, index);
        }

        int ICollection.Count
        {
            get { return ((ICollection) dictionary).Count; }
        }

        public object SyncRoot
        {
            get { return ((IDictionary) dictionary).SyncRoot; }
        }

        public bool IsSynchronized
        {
            get { return ((IDictionary) dictionary).IsSynchronized; }
        }

        int ICollection<KeyValuePair<TKey, TValue>>.Count
        {
            get { return dictionary.Count; }
        }

        ICollection IDictionary.Values
        {
            get { return ((IDictionary) dictionary).Values; }
        }

        bool IDictionary.IsReadOnly
        {
            get { return dictionary.IsReadOnly; }
        }

        public bool IsFixedSize
        {
            get { return ((IDictionary)dictionary).IsFixedSize; }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return dictionary.IsReadOnly; }
        }

        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            dictionary.Add(key, value);
        }

        public bool Remove(TKey key)
        {
            return dictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get { return dictionary[key]; }
            set { dictionary[key] = value; }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get { return dictionary.Keys; }
        }

        ICollection IDictionary.Keys
        {
            get { return ((IDictionary) dictionary).Keys; }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get { return dictionary.Values; }
        }

        public override bool Equals(object obj)
        {
            return Objects.Equal(dictionary, obj);
        }

        public override int GetHashCode()
        {
            return Objects.HashCode(dictionary);
        }

        public override string ToString()
        {
            return Strings.ToString(dictionary);
        }
    }
}
