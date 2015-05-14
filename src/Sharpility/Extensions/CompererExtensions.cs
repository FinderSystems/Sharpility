using System.Collections.Generic;

namespace Sharpility.Extensions
{
    public static class CompererExtensions
    {
        public static IComparer<T> Reverse<T>(this IComparer<T> comperer)
        {
            return new ReverseComparer<T>(comperer);
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
