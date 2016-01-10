using System.Collections.Generic;
using System.Linq;

namespace Sharpility.Util
{
    /// <summary>
    /// Utility for building for converting comparers into one.
    /// </summary>
    public static class MultiComparer
    {
        /// <summary>
        /// Creates comparer consisting of given comparers.
        /// </summary>
        /// <typeparam name="T">Type of compared item</typeparam>
        /// <param name="comparers">Comparers</param>
        /// <returns>Multi comparer</returns>
        public static IComparer<T> Of<T>(params IComparer<T>[] comparers)
        {
            return new MultiComparerInstance<T>(comparers);
        }

        /// <summary>
        /// Creates comparer consisting of given comparers.
        /// </summary>
        /// <typeparam name="T">Type of compared item</typeparam>
        /// <param name="comparers">Comparers</param>
        /// <returns>Multi comparer</returns>
        public static IComparer<T> Of<T>(IEnumerable<IComparer<T>> comparers)
        {
            return new MultiComparerInstance<T>(comparers);
        }

        private class MultiComparerInstance<T> : IComparer<T>
        {
            private readonly IEnumerable<IComparer<T>> comparers;

            internal MultiComparerInstance(IEnumerable<IComparer<T>> comparers)
            {
                this.comparers = comparers;
            }

            public int Compare(T first, T second)
            {
                return comparers.Select(comparer => comparer.Compare(first, second))
                    .FirstOrDefault(result => result != 0);
            }
        }
    }
}
