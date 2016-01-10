using System.Collections.Generic;

namespace Sharpility.Collections
{
    /// <summary>
    /// Implementation of MultiDictionary using ArrayList for key values.
    /// </summary>
    /// <typeparam name="TKey">Type of dictionary key</typeparam>
    /// <typeparam name="TValue">Type of dictionary value</typeparam>
    public sealed class ArrayListMultiDictionary<TKey, TValue>: AbstractMultiDictionary<TKey, TValue>
    {
        /// <summary>
        /// Creates MultiDictionary with array list as values holder.
        /// </summary>
        public ArrayListMultiDictionary()
        {
        }

        /// <summary>
        /// Creates MultiDictionary with array list as values holder.
        /// </summary>
        /// <param name="keysCapacity">Capacity of keys</param>
        /// <param name="valuesCapacity">Capacity of values</param>
        public ArrayListMultiDictionary(int keysCapacity, int valuesCapacity)
            : base(keysCapacity, valuesCapacity)
        {
        }

        /// <summary>
        /// Returns empty ArrayListMultiDictionary.
        /// </summary>
        /// <returns></returns>
        public static ArrayListMultiDictionary<TKey, TValue> Empty()
        {
            return new ArrayListMultiDictionary<TKey, TValue>(0, 0);
        }

        #region QuickCreate

        public static ArrayListMultiDictionary<TKey, TValue> Of(TKey key, TValue value)
        {
            var result = new ArrayListMultiDictionary<TKey, TValue>();
            result.Put(key, value);
            return result;
        }

        public static ArrayListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2)
        {
            var result = new ArrayListMultiDictionary<TKey, TValue>();
            result.Put(key1, value1);
            result.Put(key2, value2);
            return result;
        }

        public static ArrayListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3)
        {
            var result = new ArrayListMultiDictionary<TKey, TValue>();
            result.Put(key1, value1);
            result.Put(key2, value2);
            result.Put(key3, value3);
            return result;
        }

        public static ArrayListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4)
        {
            var result = new ArrayListMultiDictionary<TKey, TValue>();
            result.Put(key1, value1);
            result.Put(key2, value2);
            result.Put(key3, value3);
            result.Put(key4, value4);
            return result;
        }

        public static ArrayListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5)
        {
            var result = new ArrayListMultiDictionary<TKey, TValue>();
            result.Put(key1, value1);
            result.Put(key2, value2);
            result.Put(key3, value3);
            result.Put(key4, value4);
            result.Put(key5, value5);
            return result;
        }

        public static ArrayListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5, TKey key6, TValue value6)
        {
            var result = new ArrayListMultiDictionary<TKey, TValue>();
            result.Put(key1, value1);
            result.Put(key2, value2);
            result.Put(key3, value3);
            result.Put(key4, value4);
            result.Put(key5, value5);
            result.Put(key6, value6);
            return result;
        }

        public static ArrayListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5, TKey key6, TValue value6, TKey key7, TValue value7)
        {
            var result = new ArrayListMultiDictionary<TKey, TValue>();
            result.Put(key1, value1);
            result.Put(key2, value2);
            result.Put(key3, value3);
            result.Put(key4, value4);
            result.Put(key5, value5);
            result.Put(key6, value6);
            result.Put(key7, value7);
            return result;
        }

        public static ArrayListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5, TKey key6, TValue value6, TKey key7, TValue value7, TKey key8, TValue value8)
        {
            var result = new ArrayListMultiDictionary<TKey, TValue>();
            result.Put(key1, value1);
            result.Put(key2, value2);
            result.Put(key3, value3);
            result.Put(key4, value4);
            result.Put(key5, value5);
            result.Put(key6, value6);
            result.Put(key7, value7);
            result.Put(key8, value8);
            return result;
        }

        public static ArrayListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5, TKey key6, TValue value6, TKey key7, TValue value7, TKey key8, TValue value8,
            TKey key9, TValue value9)
        {
            var result = new ArrayListMultiDictionary<TKey, TValue>();
            result.Put(key1, value1);
            result.Put(key2, value2);
            result.Put(key3, value3);
            result.Put(key4, value4);
            result.Put(key5, value5);
            result.Put(key6, value6);
            result.Put(key7, value7);
            result.Put(key8, value8);
            result.Put(key9, value9);
            return result;
        }

        public static ArrayListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5, TKey key6, TValue value6, TKey key7, TValue value7, TKey key8, TValue value8,
            TKey key9, TValue value9, TKey key10, TValue value10)
        {
            var result = new ArrayListMultiDictionary<TKey, TValue>();
            result.Put(key1, value1);
            result.Put(key2, value2);
            result.Put(key3, value3);
            result.Put(key4, value4);
            result.Put(key5, value5);
            result.Put(key6, value6);
            result.Put(key7, value7);
            result.Put(key8, value8);
            result.Put(key9, value9);
            result.Put(key10, value10);
            return result;
        }

        #endregion

        protected override ICollection<TValue> CreateCollection(int capacity)
        {
            return capacity == 0 ? new List<TValue>() : new List<TValue>(capacity);
        }

        protected override ICollection<TValue> ResultCollection(ICollection<TValue> collection)
        {
            return new List<TValue>(collection);
        }

        protected override ICollection<TValue> ComparableCollection(ICollection<TValue> collection)
        {
            var list = new List<TValue>(collection);
            return ComparableList<TValue>.Of(list);
        }
    }
}
