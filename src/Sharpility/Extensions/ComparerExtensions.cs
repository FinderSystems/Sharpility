using System.Collections.Generic;

namespace Sharpility.Extensions
{
    /// <summary>
    /// Extensions of IComparer class.
    /// </summary>
    public static class ComparerExtensions
    {
        /// <summary>
        /// Returns comparer in reversed order.
        /// </summary>
        /// <typeparam name="T">Type of compared item</typeparam>
        /// <param name="comparer">this</param>
        /// <returns>Reversed order comparer</returns>
        public static IComparer<T> Reverse<T>(this IComparer<T> comparer)
        {
            return new ReverseComparer<T>(comparer);
        }

        private sealed class ReverseComparer<T> : IComparer<T>
        {
            private readonly IComparer<T> comparer;

            internal ReverseComparer(IComparer<T> comparer)
            {
                this.comparer = comparer;
            }

            public int Compare(T first, T second)
            {
                return -comparer.Compare(first, second);
            }
        }
    }
}
