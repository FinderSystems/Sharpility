using System;
using System.Collections.Generic;
using Sharpility.Util;

namespace Sharpility.Collections
{
    public sealed class SortedSetMappedQueue<TKey, TValue> : AbstractSortedMappedQueue<TKey, TValue, SortedSet<TValue>>
        where TValue : class
    {
        private SortedSetMappedQueue(Converter<TValue, TKey> keyExtractor, IComparer<TValue> comparer) :
            base(keyExtractor, comparer, new SortedSet<TValue>(comparer))
        {
        }

        public static SortedMappedQueue<TValue> Create(Converter<TValue, TKey> keyExtractor, IComparer<TValue> comparer)
        {
            return new SortedSetMappedQueue<TKey, TValue>(keyExtractor, comparer);
        }

        public static SortedMappedQueue<T> Create<T>(IComparer<T> comparer)
            where T : class
        {
            Converter<T, T> keyExtractor = element => element;
            return new SortedSetMappedQueue<T, T>(keyExtractor, comparer);
        }

        public static SortedMappedQueue<T> Create<T>()
            where T : class, IComparable<T>
        {
            var comparer = Comparers.OfComparables<T>();
            return Create(comparer);
        }

        public static SortedMappedQueue<TV> Create<T, TV>(Converter<TV, T> keyExtractor)
            where TV : class, IComparable<TV>
        {
            var comparer = Comparers.OfComparables<TV>();
            return new SortedSetMappedQueue<T, TV>(keyExtractor, comparer);
        }

        protected override void SortQueue(SortedSet<TValue> queue, IComparer<TValue> comparer)
        {
        }
    }
}
