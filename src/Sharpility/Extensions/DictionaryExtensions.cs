using System.Collections.Generic;
using System.Collections.Immutable;
using Sharpility.Collections;

namespace Sharpility.Extensions
{
    /// <summary>
    /// Dictionary extensions.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Puts value into this dictionary by given key.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="dictionary">this</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>this</returns>
        public static IDictionary<T, TV> Put<T, TV>(this IDictionary<T, TV> dictionary, T key, TV value)
        {
            dictionary[key] = value;
            return dictionary;
        }

        /// <summary>
        /// Puts key-value entry into this dictionary.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="dictionary">this</param>
        /// <param name="entry">entry</param>
        /// <returns>this</returns>
        public static IDictionary<T, TV> Put<T, TV>(this IDictionary<T, TV> dictionary, KeyValuePair<T, TV> entry)
        {
            dictionary[entry.Key] = entry.Value;
            return dictionary;
        }

        /// <summary>
        /// Puts all entries from given dictionary into this dictionary.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="dictionary">this</param>
        /// <param name="values">entires</param>
        /// <returns>this</returns>
        public static IDictionary<T, TV> PutAll<T, TV>(this IDictionary<T, TV> dictionary, IDictionary<T, TV> values)
        {
            foreach (var key in values.Keys)
            {
                dictionary.Put(key, values[key]);
            }
            return dictionary;
        }

        /// <summary>
        /// Puts key-value entries into this dictionary.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="dictionary">this</param>
        /// <param name="entries">entry</param>
        /// <returns>this</returns>
        public static IDictionary<T, TV> PutAll<T, TV>(this IDictionary<T, TV> dictionary, params KeyValuePair<T, TV>[] entries)
        {
            foreach (var entry in entries)
            {
                dictionary.Put(entry);
            }
            return dictionary;
        }

        /// <summary>
        /// Puts key-value entries into this dictionary.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="dictionary">this</param>
        /// <param name="entries">entry</param>
        /// <returns>this</returns>
        public static IDictionary<T, TV> PutAll<T, TV>(this IDictionary<T, TV> dictionary, IEnumerable<KeyValuePair<T, TV>> entries)
        {
            foreach (var entry in entries)
            {
                dictionary.Put(entry);
            }
            return dictionary;
        }

        /// <summary>
        /// Returns dictionary value by given key.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="dictionary">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV Get<T, TV>(this IDictionary<T, TV> dictionary, T key)
        {
            return dictionary[key];
        }

        /// <summary>
        /// Returns dictionary value by given key.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="dictionary">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV Get<T, TV>(this IReadOnlyDictionary<T, TV> dictionary, T key)
        {
            return dictionary[key];
        }

        /// <summary>
        /// Returns dictionary value by given key.
        /// If key not present returns null.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="dictionary">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV GetIfPresent<T, TV>(this IDictionary<T, TV> dictionary, T key)
            where TV : class
        {
            return dictionary.ContainsKey(key) ? dictionary.Get(key) : null;
        }

        /// <summary>
        /// Returns dictionary value by given key.
        /// If key not present returns null.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="dictionary">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV GetIfPresent<T, TV>(this IReadOnlyDictionary<T, TV> dictionary, T key)
            where TV : class
        {
            return dictionary.ContainsKey(key) ? dictionary.Get(key) : null;
        }

        /// <summary>
        /// Returns dictionary value by given key.
        /// If key not present returns null.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="dictionary">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV? GetIfPresent<T, TV>(this IDictionary<T, TV?> dictionary, T key)
           where TV : struct
        {
            return dictionary.ContainsKey(key) ? dictionary.Get(key) : null;
        }

        /// <summary>
        /// Returns dictionary value by given key.
        /// If key not present returns null.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="dictionary">this</param>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static TV? GetIfPresent<T, TV>(this IReadOnlyDictionary<T, TV?> dictionary, T key)
           where TV : struct
        {
            return dictionary.ContainsKey(key) ? dictionary.Get(key) : null;
        }

        /// <summary>
        /// Returns key-value entries set.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="dictionary">this</param>
        /// <returns>Set of dictionary entries</returns>
        public static ISet<KeyValuePair<T, TV>> Entries<T, TV>(this IDictionary<T, TV> dictionary)
        {
            var entries = new HashSet<KeyValuePair<T, TV>>();
            var iterator = dictionary.GetEnumerator();
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
        /// <param name="dictionary">this</param>
        /// <returns>Set of dictionary entries</returns>
        public static ISet<KeyValuePair<T, TV>> Entries<T, TV>(this IReadOnlyDictionary<T, TV> dictionary)
        {
            var entries = ImmutableHashSet.CreateBuilder<KeyValuePair<T, TV>>();
            var iterator = dictionary.GetEnumerator();
            while (iterator.MoveNext())
            {
                entries.Add(iterator.Current);
            }
            return entries.ToImmutable();
        }

        /// <summary>
        /// Converts dictionary to ArrayListMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="dictionary">this</param>
        /// <returns>ArrayListMultiDictionary</returns>
        public static ArrayListMultiDictionary<TKey, TValue> ToArrayListMultiDictionary<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary)
        {
            var result = new ArrayListMultiDictionary<TKey, TValue>();
            result.PutAll(dictionary);
            return result;
        }

        /// <summary>
        /// Converts dictionary to LinkedListMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="dictionary">this</param>
        /// <returns>LinkedListMultiDictionary</returns>
        public static LinkedListMultiDictionary<TKey, TValue> ToLinkedListMultiDictionary<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary)
        {
            var result = new LinkedListMultiDictionary<TKey, TValue>();
            result.PutAll(dictionary);
            return result;
        }

        /// <summary>
        /// Converts dictionary to HashSetMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="dictionary">this</param>
        /// <returns>HashSetMultiDictionary</returns>
        public static HashSetMultiDictionary<TKey, TValue> ToHashSetMultiDictionary<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary)
        {
            var result = new HashSetMultiDictionary<TKey, TValue>();
            result.PutAll(dictionary);
            return result;
        }

        /// <summary>
        /// Converts dictionary to ImmutableListMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="dictionary">this</param>
        /// <returns>ImmutableListMultiDictionary</returns>
        public static ImmutableListMultiDictionary<TKey, TValue> ToImmutableListMultiDictionary<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary)
        {
            var builder = ImmutableListMultiDictionary<TKey, TValue>.Builder();
            builder.PutAll(dictionary);
            return builder.Build();
        }

        /// <summary>
        /// Converts dictionary to ImmutableSetMultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="dictionary">this</param>
        /// <returns>ImmutableSetMultiDictionary</returns>
        public static ImmutableSetMultiDictionary<TKey, TValue> ToImmutableSetMultiDictionary<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary)
        {
            var builder = ImmutableSetMultiDictionary<TKey, TValue>.Builder();
            builder.PutAll(dictionary);
            return builder.Build();
        }
    }
}
