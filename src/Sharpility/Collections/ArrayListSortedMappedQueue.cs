using System;
using System.Collections.Generic;
using Sharpility.Util;

namespace Sharpility.Collections
{
    public sealed class ArrayListSortedMappedQueue<TKey, TValue> : AbstractSortedMappedQueue<TKey, TValue, List<TValue>>
        where TValue : class
    {
        private ArrayListSortedMappedQueue(Converter<TValue, TKey> keyExtractor, IComparer<TValue> comparer) :
            base(keyExtractor, comparer, new List<TValue>())
        {
        }

        public static SortedMappedQueue<TValue> Create(Converter<TValue, TKey> keyExtractor, IComparer<TValue> comparer)
        {
            return new ArrayListSortedMappedQueue<TKey, TValue>(keyExtractor, comparer);
        }

        public static SortedMappedQueue<T> Create<T>(IComparer<T> comparer)
            where T : class
        {
            Converter<T, T> keyExtractor = element => element;
            return new ArrayListSortedMappedQueue<T, T>(keyExtractor, comparer);
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
            return new ArrayListSortedMappedQueue<T, TV>(keyExtractor, comparer);
        }

        protected override void SortQueue(List<TValue> queue, IComparer<TValue> comparer)
        {
            queue.Sort(comparer);
        }
    }
}
