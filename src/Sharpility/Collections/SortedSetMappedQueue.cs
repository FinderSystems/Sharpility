using System;
using System.Collections.Generic;
using Sharpility.Util;

namespace Sharpility.Collections
{
    /// <summary>
    /// SortedSet implementation of SortedMappedQueue.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public sealed class SortedSetMappedQueue<TKey, TValue> : AbstractSortedMappedQueue<TKey, TValue, SortedSet<TValue>>
        where TValue : class
    {
        private SortedSetMappedQueue(Converter<TValue, TKey> keyExtractor, IComparer<TValue> comparer) :
            base(keyExtractor, comparer, new SortedSet<TValue>(comparer))
        {
        }

        /// <summary>
        /// Creates SortedMappedQueue.
        /// </summary>
        /// <param name="keyExtractor">Extracts key from value</param>
        /// <param name="comparer">Queue items comparer</param>
        /// <returns>SortedMappedQueue</returns>
        public static SortedMappedQueue<TValue> Create(Converter<TValue, TKey> keyExtractor, IComparer<TValue> comparer)
        {
            return new SortedSetMappedQueue<TKey, TValue>(keyExtractor, comparer);
        }

        /// <summary>
        /// Creates SortedMappedQueue with same type for key and values.
        /// </summary>
        /// <typeparam name="T">Type of key and queue items</typeparam>
        /// <param name="comparer">Comparer of queue items</param>
        /// <returns>SortedMappedQueue</returns>
        public static SortedMappedQueue<T> Create<T>(IComparer<T> comparer)
            where T : class
        {
            Converter<T, T> keyExtractor = element => element;
            return new SortedSetMappedQueue<T, T>(keyExtractor, comparer);
        }

        /// <summary>
        /// Creates SortedMappedQueue with same type for key and values that are comparable.
        /// </summary>
        /// <typeparam name="T">Type of queue and key items</typeparam>
        /// <returns>SortedMappedQueue</returns>
        public static SortedMappedQueue<T> Create<T>()
            where T : class, IComparable<T>
        {
            var comparer = Comparers.OfComparables<T>();
            return Create(comparer);
        }

        /// <summary>
        /// Creates SortedMappedQueue with comparable queue items.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="keyExtractor">Extracts key from value</param>
        /// <returns>SortedMappedQueue</returns>
        public static SortedMappedQueue<TV> Create<T, TV>(Converter<TV, T> keyExtractor)
            where TV : class, IComparable<TV>
        {
            var comparer = Comparers.OfComparables<TV>();
            return new SortedSetMappedQueue<T, TV>(keyExtractor, comparer);
        }

        protected override void SortQueue(SortedSet<TValue> queue, IComparer<TValue> comparer)
        {
            // not needed
        }
    }
}
