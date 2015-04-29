using System.Collections.Generic;
using System.Collections.Immutable;

namespace Sharpility.Extensions
{
    public static class DictionaryExtensions
    {
        public static IDictionary<T, TV> Put<T, TV>(this IDictionary<T, TV> dictionary, T key, TV value)
        {
            dictionary[key] = value;
            return dictionary;
        }

        public static IDictionary<T, TV> Put<T, TV>(this IDictionary<T, TV> dictionary, KeyValuePair<T, TV> entry)
        {
            dictionary[entry.Key] = entry.Value;
            return dictionary;
        }

        public static IDictionary<T, TV> PutAll<T, TV>(this IDictionary<T, TV> dictionary, IDictionary<T, TV> values)
        {
            foreach (var key in values.Keys)
            {
                dictionary.Put(key, values[key]);
            }
            return dictionary;
        }

        public static IDictionary<T, TV> PutAll<T, TV>(this IDictionary<T, TV> dictionary, params KeyValuePair<T, TV>[] entries)
        {
            foreach (var entry in entries)
            {
                dictionary.Put(entry);
            }
            return dictionary;
        }

        public static IDictionary<T, TV> PutAll<T, TV>(this IDictionary<T, TV> dictionary, IEnumerable<KeyValuePair<T, TV>> entries)
        {
            foreach (var entry in entries)
            {
                dictionary.Put(entry);
            }
            return dictionary;
        }

        public static TV Get<T, TV>(this IDictionary<T, TV> dictionary, T key)
        {
            return dictionary[key];
        }

        public static TV Get<T, TV>(this IReadOnlyDictionary<T, TV> dictionary, T key)
        {
            return dictionary[key];
        }

        public static TV GetIfPresent<T, TV>(this IDictionary<T, TV> dictionary, T key)
            where TV : class
        {
            return dictionary.ContainsKey(key) ? dictionary.Get(key) : null;
        }

        public static TV GetIfPresent<T, TV>(this IReadOnlyDictionary<T, TV> dictionary, T key)
            where TV : class
        {
            return dictionary.ContainsKey(key) ? dictionary.Get(key) : null;
        }

        public static TV? GetIfPresent<T, TV>(this IDictionary<T, TV?> dictionary, T key)
           where TV : struct
        {
            return dictionary.ContainsKey(key) ? dictionary.Get(key) : null;
        }

        public static TV? GetIfPresent<T, TV>(this IReadOnlyDictionary<T, TV?> dictionary, T key)
           where TV : struct
        {
            return dictionary.ContainsKey(key) ? dictionary.Get(key) : null;
        }

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

        public static IImmutableDictionary<T, TV> ToImmutable<T, TV>(this IDictionary<T, TV> dictionary)
        {
            return dictionary.ToImmutableDictionary();
        }
    }
}
