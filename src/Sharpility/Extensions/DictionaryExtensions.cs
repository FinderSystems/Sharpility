using System.Collections.Generic;
using System.Collections.Immutable;

namespace Sharpility.Extensions
{
    public static class DictionaryExtensions
    {
        public static void Put<T, TV>(this IDictionary<T, TV> map, T key, TV value)
        {
            map[key] = value;
        }

        public static void Put<T, TV>(this IDictionary<T, TV> map, KeyValuePair<T, TV> entry)
        {
            map[entry.Key] = entry.Value;
        }

        public static void PutAll<T, TV>(this IDictionary<T, TV> map, IDictionary<T, TV> values)
        {
            foreach (var key in values.Keys)
            {
                map.Put(key, values[key]);
            }
        }

        public static void PutAll<T, TV>(this IDictionary<T, TV> map, params KeyValuePair<T, TV>[] entries)
        {
            foreach (var entry in entries)
            {
                map.Put(entry);
            }
        }

        public static void PutAll<T, TV>(this IDictionary<T, TV> map, IEnumerable<KeyValuePair<T, TV>> entries)
        {
            foreach (var entry in entries)
            {
                map.Put(entry);
            }
        }

        public static TV Get<T, TV>(this IDictionary<T, TV> map, T key)
        {
            return map[key];
        }

        public static TV Get<T, TV>(this IReadOnlyDictionary<T, TV> map, T key)
        {
            return map[key];
        }

        public static TV GetIfPresent<T, TV>(this IDictionary<T, TV> map, T key)
            where TV : class
        {
            return map.ContainsKey(key) ? map.Get(key) : null;
        }

        public static TV GetIfPresent<T, TV>(this IReadOnlyDictionary<T, TV> map, T key)
            where TV : class
        {
            return map.ContainsKey(key) ? map.Get(key) : null;
        }

        public static TV? GetIfPresent<T, TV>(this IDictionary<T, TV?> map, T key)
           where TV : struct
        {
            return map.ContainsKey(key) ? map.Get(key) : null;
        }

        public static TV? GetIfPresent<T, TV>(this IReadOnlyDictionary<T, TV?> map, T key)
           where TV : struct
        {
            return map.ContainsKey(key) ? map.Get(key) : null;
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
