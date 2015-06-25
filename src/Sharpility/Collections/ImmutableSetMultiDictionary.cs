using System.Collections.Generic;
using System.Collections.Immutable;

namespace Sharpility.Collections
{
    /// <summary>
    /// Implementation of ImmutableMultiDictionary using ImmutableSet for key values.
    /// </summary>
    /// <typeparam name="TKey">Type of dictionary key</typeparam>
    /// <typeparam name="TValue">Type of dictionary value</typeparam>
    public sealed class ImmutableSetMultiDictionary<TKey, TValue> : ImmutableMultiDictionary<TKey, TValue>
    {
        internal ImmutableSetMultiDictionary(IImmutableDictionary<TKey, ICollection<TValue>> dictionary) : 
            base(dictionary)
        {
        }

        /// <summary>
        /// Returns builder of ImmutableSetMultiDictionary.
        /// </summary>
        /// <returns>Builder</returns>
        public static ImmutableSetMultiDictionaryBuilder Builder()
        {
            return new ImmutableSetMultiDictionaryBuilder();
        }

        /// <summary>
        /// Returns builder of ImmutableSetMultiDictionary.
        /// </summary>
        /// <param name="keysCapacity">Initial keys capacity.</param>
        /// <returns>Builder</returns>
        public static ImmutableSetMultiDictionaryBuilder Builder(int keysCapacity)
        {
            return new ImmutableSetMultiDictionaryBuilder(keysCapacity);
        }

        /// <summary>
        /// Returns empty ImmutableSetMultiDictionary.
        /// </summary>
        /// <returns>Empty ImmutableSetMultiDictionary</returns>
        public static ImmutableSetMultiDictionary<TKey, TValue> Empty()
        {
            return Builder().Build();
        }

        #region QuickCreate

        public static ImmutableSetMultiDictionary<TKey, TValue> Of(TKey key, TValue value)
        {
            return Builder(1)
                .Put(key, value)
                .Build();
        }

        public static ImmutableSetMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2)
        {
            return Builder(2)
                .Put(key1, value1)
                .Put(key2, value2)
                .Build();
        }

        public static ImmutableSetMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3)
        {
            return Builder(3)
                .Put(key1, value1)
                .Put(key2, value2)
                .Put(key3, value3)
                .Build();
        }

        public static ImmutableSetMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3, 
            TKey key4, TValue value4)
        {
            return Builder(4)
                .Put(key1, value1)
                .Put(key2, value2)
                .Put(key3, value3)
                .Put(key4, value4)
                .Build();
        }

        public static ImmutableSetMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5)
        {
            return Builder(5)
                .Put(key1, value1)
                .Put(key2, value2)
                .Put(key3, value3)
                .Put(key4, value4)
                .Put(key5, value5)
                .Build();
        }

        public static ImmutableSetMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5, TKey key6, TValue value6)
        {
            return Builder(6)
                .Put(key1, value1)
                .Put(key2, value2)
                .Put(key3, value3)
                .Put(key4, value4)
                .Put(key5, value5)
                .Put(key6, value6)
                .Build();
        }

        public static ImmutableSetMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5, TKey key6, TValue value6, TKey key7, TValue value7)
        {
            return Builder(7)
                .Put(key1, value1)
                .Put(key2, value2)
                .Put(key3, value3)
                .Put(key4, value4)
                .Put(key5, value5)
                .Put(key6, value6)
                .Put(key7, value7)
                .Build();
        }

        public static ImmutableSetMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5, TKey key6, TValue value6, TKey key7, TValue value7, TKey key8, TValue value8)
        {
            return Builder(8)
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

        public static ImmutableSetMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5, TKey key6, TValue value6, TKey key7, TValue value7, TKey key8, TValue value8,
            TKey key9, TValue value9)
        {
            return Builder(9)
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

        public static ImmutableSetMultiDictionary<TKey, TValue> Of(TKey key1, TValue value1, TKey key2, TValue value2, TKey key3, TValue value3,
            TKey key4, TValue value4, TKey key5, TValue value5, TKey key6, TValue value6, TKey key7, TValue value7, TKey key8, TValue value8,
            TKey key9, TValue value9, TKey key10, TValue value10)
        {
            return Builder(10)
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
        /// Builder of ImmutableSetMultiDictionary.
        /// </summary>
        public sealed class ImmutableSetMultiDictionaryBuilder : AbstractImmutableListMultiDictionaryBuilder<TKey, TValue, ImmutableSetMultiDictionary<TKey, TValue>>
        {
            internal ImmutableSetMultiDictionaryBuilder(int keysCapacity)
                :base(new HashSetMultiDictionary<TKey, TValue>(keysCapacity))
            {
            }

            internal ImmutableSetMultiDictionaryBuilder()
                : base(new HashSetMultiDictionary<TKey, TValue>())
            {
            }

            protected override ICollection<TValue> Immutable(ICollection<TValue> values)
            {
                return values.ToImmutableHashSet();
            }

            protected override ImmutableSetMultiDictionary<TKey, TValue> Crete(IImmutableDictionary<TKey, ICollection<TValue>> dictionary)
            {
                return new ImmutableSetMultiDictionary<TKey, TValue>(dictionary);
            }
        }
    }
}
