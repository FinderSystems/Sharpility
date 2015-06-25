using System.Collections.Generic;
using System.Collections.Immutable;

namespace Sharpility.Collections
{
    /// <summary>
    /// Implementation of ImmutableMultiDictionary using ImmutableList for key values.
    /// </summary>
    /// <typeparam name="TKey">Type of dictionary key</typeparam>
    /// <typeparam name="TValue">Type of dictionary value</typeparam>
    public sealed class ImmutableListMultiDictionary<TKey, TValue>: ImmutableMultiDictionary<TKey, TValue>
    {
        internal ImmutableListMultiDictionary(IImmutableDictionary<TKey, ICollection<TValue>> dictionary) : 
            base(dictionary)
        {
        }

        /// <summary>
        /// Returns builder of ImmutableListMultiDictionary.
        /// </summary>
        /// <returns></returns>
        public static ImmutableListMultiDictionaryBuilder Builder()
        {
            return new ImmutableListMultiDictionaryBuilder();
        }

        /// <summary>
        /// Returns builder of ImmutableListMultiDictionary.
        /// </summary>
        /// <param name="keysCapacity">Initial keys capacity</param>
        /// <param name="valuesCapacity">Intial values capacity</param>
        /// <returns></returns>
        public static ImmutableListMultiDictionaryBuilder Builder(int keysCapacity, int valuesCapacity)
        {
            return new ImmutableListMultiDictionaryBuilder(keysCapacity, valuesCapacity);
        }

        /// <summary>
        /// Returns empty ImmutableListMultiDictionary.
        /// </summary>
        /// <returns></returns>
        public static ImmutableListMultiDictionary<TKey, TValue> Empty()
        {
            return Builder().Build();
        }

        #region QuickCreate

        public static ImmutableListMultiDictionary<TKey, TValue> Of(TKey key, TValue value)
        {
            return Builder()
                .Put(key, value)
                .Build();
        }

        public static ImmutableListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2)
        {
            return Builder()
                .Put(key1, value1)
                .Put(key2, value2)
                .Build();
        }

        public static ImmutableListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3)
        {
            return Builder()
                .Put(key1, value1)
                .Put(key2, value2)
                .Put(key3, value3)
                .Build();
        }

        public static ImmutableListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3, 
            TKey key4, TValue value4)
        {
            return Builder()
                .Put(key1, value1)
                .Put(key2, value2)
                .Put(key3, value3)
                .Put(key4, value4)
                .Build();
        }

        public static ImmutableListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5)
        {
            return Builder()
                .Put(key1, value1)
                .Put(key2, value2)
                .Put(key3, value3)
                .Put(key4, value4)
                .Put(key5, value5)
                .Build();
        }

        public static ImmutableListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5, TKey key6, TValue value6)
        {
            return Builder()
                .Put(key1, value1)
                .Put(key2, value2)
                .Put(key3, value3)
                .Put(key4, value4)
                .Put(key5, value5)
                .Put(key6, value6)
                .Build();
        }

        public static ImmutableListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5, TKey key6, TValue value6, TKey key7, TValue value7)
        {
            return Builder()
                .Put(key1, value1)
                .Put(key2, value2)
                .Put(key3, value3)
                .Put(key4, value4)
                .Put(key5, value5)
                .Put(key6, value6)
                .Put(key7, value7)
                .Build();
        }

        public static ImmutableListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5, TKey key6, TValue value6, TKey key7, TValue value7, TKey key8, TValue value8)
        {
            return Builder()
                .Put(key1, value1)
                .Put(key2, value2)
                .Put(key3, value3)
                .Put(key4, value4)
                .Put(key5, value5)
                .Put(key6, value6)
                .Put(key7, value7)
                .Put(key8, value8)
                .Build();
        }

        public static ImmutableListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5, TKey key6, TValue value6, TKey key7, TValue value7, TKey key8, TValue value8,
            TKey key9, TValue value9)
        {
            return Builder()
                .Put(key1, value1)
                .Put(key2, value2)
                .Put(key3, value3)
                .Put(key4, value4)
                .Put(key5, value5)
                .Put(key6, value6)
                .Put(key7, value7)
                .Put(key8, value8)
                .Put(key9, value9)
                .Build();
        }

        public static ImmutableListMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5, TKey key6, TValue value6, TKey key7, TValue value7, TKey key8, TValue value8,
            TKey key9, TValue value9, TKey key10, TValue value10)
        {
            return Builder()
                .Put(key1, value1)
                .Put(key2, value2)
                .Put(key3, value3)
                .Put(key4, value4)
                .Put(key5, value5)
                .Put(key6, value6)
                .Put(key7, value7)
                .Put(key8, value8)
                .Put(key9, value9)
                .Put(key10, value10)
                .Build();
        }

        #endregion

        protected override ICollection<TValue> EmptyCollection()
        {
            return ImmutableList<TValue>.Empty;
        }

        protected override ICollection<TValue> MutableCopy(ICollection<TValue> values)
        {
            return new List<TValue>(values);
        }

        /// <summary>
        /// Builder of ImmutableListMultiDictionary.
        /// </summary>
        public sealed class ImmutableListMultiDictionaryBuilder : AbstractImmutableListMultiDictionaryBuilder<TKey, TValue, ImmutableListMultiDictionary<TKey, TValue>>
        {
            internal ImmutableListMultiDictionaryBuilder(int keysCapacity, int valuesCapacity)
                :base(new ArrayListMultiDictionary<TKey, TValue>(keysCapacity, valuesCapacity))
            {
            }

            internal ImmutableListMultiDictionaryBuilder()
                : base(new ArrayListMultiDictionary<TKey, TValue>())
            {
            }

            protected override ICollection<TValue> Immutable(ICollection<TValue> values)
            {
                return values.ToImmutableList();
            }

            protected override ImmutableListMultiDictionary<TKey, TValue> Crete(IImmutableDictionary<TKey, ICollection<TValue>> dictionary)
            {
                return new ImmutableListMultiDictionary<TKey, TValue>(dictionary);
            }
        }
    }
}
