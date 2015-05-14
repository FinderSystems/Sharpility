using System;
using System.Collections.Generic;

namespace Sharpility.Util
{
    public static class Comparers
    {
        public static IComparer<T> OfComparables<T>()
            where T : IComparable<T>
        {
            return new ComparableComparer<T>();
        }

        private sealed class ComparableComparer<T> : IComparer<T>
            where T : IComparable<T>
        {
            public int Compare(T first, T second)
            {
                if (first == null && second == null)
                {
                    return 0;
                }
                if (first == null)
                {
                    return 1;
                }
                if (second == null)
                {
                    return -1;
                }
                return first.CompareTo(second);
            }
        }
    }
}
