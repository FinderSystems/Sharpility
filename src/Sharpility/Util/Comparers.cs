using System;
using System.Collections.Generic;

namespace Sharpility.Util
{
    /// <summary>
    /// Comparers utils.
    /// </summary>
    public static class Comparers
    {
        /// <summary>
        /// Creates comparer for comparable objects.
        /// </summary>
        /// <typeparam name="T">Type of comparable object</typeparam>
        /// <returns>Comparer</returns>
        public static IComparer<T> OfComparables<T>()
            where T : IComparable<T>
        {
            return new ComparableComparer<T>();
        }

        /// <summary>
        /// Creates comparer with comparable extractor.
        /// </summary>
        /// <typeparam name="T">Type of compared object</typeparam>
        /// <typeparam name="TC">Type of comparable object</typeparam>
        /// <param name="comparableConverter">Converter of comparable object</param>
        /// <returns>Comparer</returns>
        public static IComparer<T> CompareBy<T, TC>(Converter<T, TC> comparableConverter)
            where TC: IComparable<TC>
        {
            return new ExtractedComparableComparer<T, TC>(comparableConverter);
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

        private sealed class ExtractedComparableComparer<T, TC> : IComparer<T>
            where TC: IComparable<TC>
        {
            private readonly Converter<T, TC> comparableConverter;

            internal ExtractedComparableComparer(Converter<T, TC> comparableConverter)
            {
                this.comparableConverter = comparableConverter;
            }

            public int Compare(T first, T second)
            {
                var firstValue = comparableConverter(first);
                var secondValue = comparableConverter(second);

                if (firstValue == null && secondValue == null)
                {
                    return 0;
                }
                if (firstValue == null)
                {
                    return 1;
                }
                if (secondValue == null)
                {
                    return -1;
                }
                return firstValue.CompareTo(secondValue);
            }
        }
    }
}
