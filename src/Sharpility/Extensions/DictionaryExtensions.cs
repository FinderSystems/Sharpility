using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Sharpility.Base;
using Sharpility.Collections;

namespace Sharpility.Extensions
{
    /// <summary>
    /// Dictionary extensions.
    /// </summary>
    public static class DictionaryExtensions
    {

        #region PutAll

        /// <summary>
        /// Puts value into this dictionary by given key.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>this</returns>
        public static IDictionary<T, TV> Put<T, TV>(this IDictionary<T, TV> source, T key, TV value)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            source[key] = value;
            return source;
        }

        /// <summary>
        /// Puts key-value entry into this dictionary.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="entry">entry</param>
        /// <returns>this</returns>
        public static IDictionary<T, TV> Put<T, TV>(this IDictionary<T, TV> source, KeyValuePair<T, TV> entry)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            source[entry.Key] = entry.Value;
            return source;
        }

        /// <summary>
        /// Puts all entries from given dictionary into this dictionary.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="values">entires</param>
        /// <returns>this</returns>
        public static IDictionary<T, TV> PutAll<T, TV>(this IDictionary<T, TV> source, IDictionary<T, TV> values)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            foreach (var key in values.Keys)
            {
                source.Put(key, values[key]);
            }
            return source;
        }

        /// <summary>
        /// Puts key-value entries into this dictionary.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="entries">entry</param>
        /// <returns>this</returns>
        public static IDictionary<T, TV> PutAll<T, TV>(this IDictionary<T, TV> source, params KeyValuePair<T, TV>[] entries)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            foreach (var entry in entries)
            {
                source.Put(entry);
            }
            return source;
        }

        /// <summary>
        /// Puts key-value entries into this dictionary.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="entries">entry</param>
        /// <returns>this</returns>
        public static IDictionary<T, TV> PutAll<T, TV>(this IDictionary<T, TV> source, IEnumerable<KeyValuePair<T, TV>> entries)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            foreach (var entry in entries)
            {
                source.Put(entry);
            }
            return source;
        }

        #endregion

        #region Get

        /// <summary>
        /// Returns dictionary value by given key.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV Get<T, TV>(this IDictionary<T, TV> source, T key)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return source[key];
        }

        /// <summary>
        /// Returns dictionary value by given key.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV Get<T, TV>(this IReadOnlyDictionary<T, TV> source, T key)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return source[key];
        }

        /// <summary>
        /// Returns dictionary value by given key.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV Get<T, TV>(this Dictionary<T, TV> source, T key)
        {
            return Get((IDictionary<T, TV>) source, key);
        }

        /// <summary>
        /// Returns dictionary value by given key.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV Get<T, TV>(this ImmutableDictionary<T, TV> source, T key)
        {
            return Get((IReadOnlyDictionary<T, TV>)source, key);
        }

        #endregion

        #region GetIfPresent

        /// <summary>
        /// Returns dictionary value by given key.
        /// If key not present returns null.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV GetIfPresent<T, TV>(this IDictionary<T, TV> source, T key)
            where TV : class
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return source.ContainsKey(key) ? source.Get(key) : null;
        }

        /// <summary>
        /// Returns dictionary value by given key.
        /// If key not present returns null.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV GetIfPresent<T, TV>(this IReadOnlyDictionary<T, TV> source, T key)
            where TV : class
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return source.ContainsKey(key) ? source.Get(key) : null;
        }

        /// <summary>
        /// Returns dictionary value by given key.
        /// If key not present returns null.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV GetIfPresent<T, TV>(this Dictionary<T, TV> source, T key)
            where TV : class
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return GetIfPresent((IDictionary<T, TV>) source, key);
        }

        /// <summary>
        /// Returns dictionary value by given key.
        /// If key not present returns null.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV GetIfPresent<T, TV>(this ImmutableDictionary<T, TV> source, T key)
            where TV : class
        {
            return GetIfPresent((IReadOnlyDictionary<T, TV>) source, key);
        }

        #endregion

        #region NullableStructGetIfPresent

        /// <summary>
        /// Returns dictionary value by given key.
        /// If key not present returns null.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV? GetIfPresent<T, TV>(this IDictionary<T, TV?> source, T key)
           where TV : struct
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return source.ContainsKey(key) ? source.Get(key) : null;
        }

        /// <summary>
        /// Returns dictionary value by given key.
        /// If key not present returns null.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV? GetIfPresent<T, TV>(this IReadOnlyDictionary<T, TV?> source, T key)
           where TV : struct
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return source.ContainsKey(key) ? source.Get(key) : null;
        }

        /// <summary>
        /// Returns dictionary value by given key.
        /// If key not present returns null.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV? GetIfPresent<T, TV>(this Dictionary<T, TV?> source, T key)
           where TV : struct
        {
            return GetIfPresent((IDictionary<T, TV?>) source, key);
        }

        /// <summary>
        /// Returns dictionary value by given key.
        /// If key not present returns null.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV? GetIfPresent<T, TV>(this ImmutableDictionary<T, TV?> source, T key)
           where TV : struct
        {
            return GetIfPresent((IReadOnlyDictionary<T, TV?>)source, key);
        }

        #endregion

        #region Entries

        /// <summary>
        /// Returns key-value entries set.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <returns>Set of dictionary entries</returns>
        public static ISet<KeyValuePair<T, TV>> Entries<T, TV>(this IDictionary<T, TV> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var entries = new HashSet<KeyValuePair<T, TV>>();
            var iterator = source.GetEnumerator();
            while (iterator.MoveNext())
            {
                entries.Add(iterator.Current);
            }
            return entries;
        }

        /// <summary>
        /// Returns key-value entries set.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <returns>Set of dictionary entries</returns>
        public static ISet<KeyValuePair<T, TV>> Entries<T, TV>(this IReadOnlyDictionary<T, TV> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var entries = ImmutableHashSet.CreateBuilder<KeyValuePair<T, TV>>();
            var iterator = source.GetEnumerator();
            while (iterator.MoveNext())
            {
                entries.Add(iterator.Current);
            }
            return entries.ToImmutable();
        }

        /// <summary>
        /// Returns key-value entries set.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <returns>Set of dictionary entries</returns>
        public static ISet<KeyValuePair<T, TV>> Entries<T, TV>(this Dictionary<T, TV> source)
        {
            return Entries((IDictionary<T, TV>) source);
        }

        /// <summary>
        /// Returns key-value entries set.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="source">this</param>
        /// <returns>Set of dictionary entries</returns>
        public static ISet<KeyValuePair<T, TV>> Entries<T, TV>(this ImmutableDictionary<T, TV> source)
        {
            return Entries((IReadOnlyDictionary<T, TV>)source);
        }

        #endregion

        #region ToArrayListMultiDictionary

        /// <summary>
        /// Converts dictionary to ArrayListMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>ArrayListMultiDictionary</returns>
        public static ArrayListMultiDictionary<TKey, TValue> ToArrayListMultiDictionary<TKey, TValue>(
            this IDictionary<TKey, TValue> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var result = new ArrayListMultiDictionary<TKey, TValue>();
            result.PutAll(source);
            return result;
        }

        /// <summary>
        /// Converts dictionary to ArrayListMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>ArrayListMultiDictionary</returns>
        public static ArrayListMultiDictionary<TKey, TValue> ToArrayListMultiDictionary<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var result = new ArrayListMultiDictionary<TKey, TValue>();
            result.PutAll(source);
            return result;
        }

        /// <summary>
        /// Converts dictionary to ArrayListMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>ArrayListMultiDictionary</returns>
        public static ArrayListMultiDictionary<TKey, TValue> ToArrayListMultiDictionary<TKey, TValue>(
            this Dictionary<TKey, TValue> source)
        {
            return ToArrayListMultiDictionary((IDictionary<TKey, TValue>) source);
        }

        /// <summary>
        /// Converts dictionary to ArrayListMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>ArrayListMultiDictionary</returns>
        public static ArrayListMultiDictionary<TKey, TValue> ToArrayListMultiDictionary<TKey, TValue>(
            this ImmutableDictionary<TKey, TValue> source)
        {
            return ToArrayListMultiDictionary((IReadOnlyDictionary<TKey, TValue>)source);
        }

        #endregion

        #region ToLinkedListMultiDictionary

        /// <summary>
        /// Converts dictionary to LinkedListMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>LinkedListMultiDictionary</returns>
        public static LinkedListMultiDictionary<TKey, TValue> ToLinkedListMultiDictionary<TKey, TValue>(
            this IDictionary<TKey, TValue> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var result = new LinkedListMultiDictionary<TKey, TValue>();
            result.PutAll(source);
            return result;
        }

        /// <summary>
        /// Converts dictionary to LinkedListMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>LinkedListMultiDictionary</returns>
        public static LinkedListMultiDictionary<TKey, TValue> ToLinkedListMultiDictionary<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var result = new LinkedListMultiDictionary<TKey, TValue>();
            result.PutAll(source);
            return result;
        }

        /// <summary>
        /// Converts dictionary to LinkedListMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>LinkedListMultiDictionary</returns>
        public static LinkedListMultiDictionary<TKey, TValue> ToLinkedListMultiDictionary<TKey, TValue>(
            this Dictionary<TKey, TValue> source)
        {
            return ToLinkedListMultiDictionary((IDictionary<TKey, TValue>) source);
        }

        /// <summary>
        /// Converts dictionary to LinkedListMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>LinkedListMultiDictionary</returns>
        public static LinkedListMultiDictionary<TKey, TValue> ToLinkedListMultiDictionary<TKey, TValue>(
            this ImmutableDictionary<TKey, TValue> source)
        {
            return ToLinkedListMultiDictionary((IReadOnlyDictionary<TKey, TValue>)source);
        }

        #endregion

        #region ToHashSetMultiDictionary

        /// <summary>
        /// Converts dictionary to HashSetMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>HashSetMultiDictionary</returns>
        public static HashSetMultiDictionary<TKey, TValue> ToHashSetMultiDictionary<TKey, TValue>(
            this IDictionary<TKey, TValue> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var result = new HashSetMultiDictionary<TKey, TValue>();
            result.PutAll(source);
            return result;
        }

        /// <summary>
        /// Converts dictionary to HashSetMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>HashSetMultiDictionary</returns>
        public static HashSetMultiDictionary<TKey, TValue> ToHashSetMultiDictionary<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var result = new HashSetMultiDictionary<TKey, TValue>();
            result.PutAll(source);
            return result;
        }

        /// <summary>
        /// Converts dictionary to HashSetMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>HashSetMultiDictionary</returns>
        public static HashSetMultiDictionary<TKey, TValue> ToHashSetMultiDictionary<TKey, TValue>(
            this Dictionary<TKey, TValue> source)
        {
            return ToHashSetMultiDictionary((IDictionary<TKey, TValue>) source);
        }

        /// <summary>
        /// Converts dictionary to HashSetMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>HashSetMultiDictionary</returns>
        public static HashSetMultiDictionary<TKey, TValue> ToHashSetMultiDictionary<TKey, TValue>(
            this ImmutableDictionary<TKey, TValue> source)
        {
            return ToHashSetMultiDictionary((IReadOnlyDictionary<TKey, TValue>)source);
        }

        #endregion

        #region ToImmutableListMultiDictionary

        /// <summary>
        /// Converts dictionary to ImmutableListMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>ImmutableListMultiDictionary</returns>
        public static ImmutableListMultiDictionary<TKey, TValue> ToImmutableListMultiDictionary<TKey, TValue>(
            this IDictionary<TKey, TValue> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var builder = ImmutableListMultiDictionary<TKey, TValue>.Builder();
            builder.PutAll(source);
            return builder.Build();
        }

        /// <summary>
        /// Converts dictionary to ImmutableListMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>ImmutableListMultiDictionary</returns>
        public static ImmutableListMultiDictionary<TKey, TValue> ToImmutableListMultiDictionary<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var builder = ImmutableListMultiDictionary<TKey, TValue>.Builder();
            builder.PutAll(source);
            return builder.Build();
        }

        /// <summary>
        /// Converts dictionary to ImmutableListMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>ImmutableListMultiDictionary</returns>
        public static ImmutableListMultiDictionary<TKey, TValue> ToImmutableListMultiDictionary<TKey, TValue>(
            this Dictionary<TKey, TValue> source)
        {
            return ToImmutableListMultiDictionary((IDictionary<TKey, TValue>) source);
        }

        /// <summary>
        /// Converts dictionary to ImmutableListMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>ImmutableListMultiDictionary</returns>
        public static ImmutableListMultiDictionary<TKey, TValue> ToImmutableListMultiDictionary<TKey, TValue>(
            this ImmutableDictionary<TKey, TValue> source)
        {
            return ToImmutableListMultiDictionary((IReadOnlyDictionary<TKey, TValue>)source);
        }

        #endregion

        #region ToImmutableSetMultiDictionary

        /// <summary>
        /// Converts dictionary to ImmutableSetMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>ImmutableSetMultiDictionary</returns>
        public static ImmutableSetMultiDictionary<TKey, TValue> ToImmutableSetMultiDictionary<TKey, TValue>(
            this IDictionary<TKey, TValue> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var builder = ImmutableSetMultiDictionary<TKey, TValue>.Builder();
            builder.PutAll(source);
            return builder.Build();
        }

        /// <summary>
        /// Converts dictionary to ImmutableSetMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>ImmutableSetMultiDictionary</returns>
        public static ImmutableSetMultiDictionary<TKey, TValue> ToImmutableSetMultiDictionary<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var builder = ImmutableSetMultiDictionary<TKey, TValue>.Builder();
            builder.PutAll(source);
            return builder.Build();
        }

        /// <summary>
        /// Converts dictionary to ImmutableSetMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>ImmutableSetMultiDictionary</returns>
        public static ImmutableSetMultiDictionary<TKey, TValue> ToImmutableSetMultiDictionary<TKey, TValue>(
            this Dictionary<TKey, TValue> source)
        {
            return ToImmutableSetMultiDictionary((IDictionary<TKey, TValue>) source);
        }

        /// <summary>
        /// Converts dictionary to ImmutableSetMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>ImmutableSetMultiDictionary</returns>
        public static ImmutableSetMultiDictionary<TKey, TValue> ToImmutableSetMultiDictionary<TKey, TValue>(
            this ImmutableDictionary<TKey, TValue> source)
        {
            return ToImmutableSetMultiDictionary((IReadOnlyDictionary<TKey, TValue>)source);
        }

        #endregion

        #region ToComparable

        /// <summary>
        /// Converts dictionary into comparable dictionary with equals/hashCode/toString implementations.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">this</param>
        /// <returns>Comparable dictionary</returns>
        public static IDictionary<TKey, TValue> ToComparable<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return ComparableDictionary<TKey, TValue>.Of(source);
        }

        #endregion
    }
}
