using System;
using Sharpility.Base;
using Sharpility.Collections;

namespace Sharpility.Extensions
{
    /// <summary>
    /// Extensions for MultiDictionary.
    /// </summary>
    public static class MultiDictionaryExtensions
    {
        /// <summary>
        /// Converts MultiDictionary to ImmutableMultiDictionary.
        /// If MultiDictionary is already immutable returns it.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">Converted MultiDictionary</param>
        /// <returns>ImmutableMultiDictionary</returns>
        public static ImmutableMultiDictionary<TKey, TValue> ToImmutableMultiDictionary<TKey, TValue>(
            this MultiDictionary<TKey, TValue> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            if (source is ImmutableMultiDictionary<TKey, TValue>)
            {
                return (ImmutableMultiDictionary<TKey, TValue>) source;
            }
            if (source is HashSetMultiDictionary<TKey, TValue>)
            {
                return ToImmutableSetMultiDictionary(source);
            }
            return ToImmutableListMultiDictionary(source);
        }

        /// <summary>
        /// Converts MultiDictionary to ImmutableListMultiDictionary.
        /// If MultiDictionary is already ImmutableListMultiDictionary returns it.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">Converted MultiDictionary</param>
        /// <returns>ImmutableListMultiDictionary</returns>
        public static ImmutableListMultiDictionary<TKey, TValue> ToImmutableListMultiDictionary<TKey, TValue>(
            this MultiDictionary<TKey, TValue> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            if (source is ImmutableListMultiDictionary<TKey, TValue>)
            {
                return (ImmutableListMultiDictionary<TKey, TValue>)source;
            }
            var result = ImmutableListMultiDictionary<TKey, TValue>.Builder();
            result.PutAll(source);
            return result.Build();
        }

        /// <summary>
        /// Converts MultiDictionary to ImmutableSetMultiDictionary.
        /// If MultiDictionary is already ImmutableSetMultiDictionary returns it.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">Converted MultiDictionary</param>
        /// <returns>ImmutableSetMultiDictionary</returns>
        public static ImmutableSetMultiDictionary<TKey, TValue> ToImmutableSetMultiDictionary<TKey, TValue>(
            this MultiDictionary<TKey, TValue> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            if (source is ImmutableSetMultiDictionary<TKey, TValue>)
            {
                return (ImmutableSetMultiDictionary<TKey, TValue>)source;
            }
            var result = ImmutableSetMultiDictionary<TKey, TValue>.Builder();
            result.PutAll(source);
            return result.Build();
        }

        /// <summary>
        /// Checks is multi dictionary is not empty.
        /// </summary>
        /// <typeparam name="TKey">Type of MultiDictionary key</typeparam>
        /// <typeparam name="TValue">Type of MultiDictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>True if not empty, False otherwise</returns>
        public static bool IsNotEmpty<TKey, TValue>(this MultiDictionary<TKey, TValue> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return !source.IsEmpty;
        }
    }
}
