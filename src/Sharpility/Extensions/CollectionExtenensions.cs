using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Sharpility.Extensions
{
    public static class CollectionExtenensions
    {
        public static void AddAll<T>(this ICollection<T> collection, IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                collection.Add(value);
            }
        }

        public static void AddAll<T>(this ICollection<T> collection, params T[] values)
        {
            foreach (var value in values)
            {
                collection.Add(value);
            }
        }

        public static void RemoveAll<T>(this ICollection<T> collection, IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                collection.Remove(value);
            }
        }

        public static void RemoveAll<T>(this ICollection<T> collection, params T[] values)
        {
            foreach (var value in values)
            {
                collection.Remove(value);
            }
        }

        public static IList<TV> ConvertAll<T, TV>(this IList<T> list, Converter<T, TV> converter)
        {
            var results = new List<TV>(list.Count);
            results.AddRange(list.Select(element => converter(element)));
            return results;
        }

        public static ISet<TV> ConvertAll<T, TV>(this ISet<T> list, Converter<T, TV> converter)
        {
            var results = new HashSet<TV>();
            results.AddAll(list.Select(element => converter(element)));
            return results;
        }

        public static IEnumerable<TV> ConvertAll<T, TV>(this IEnumerable<T> list, Converter<T, TV> converter)
        {
            var results = new List<TV>(list.Count());
            results.AddRange(list.Select(element => converter(element)));
            return results;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Any();
        }

        public static bool IsSingleton<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Count() == 1;
        }

        public static ISet<T> Minus<T>(this ISet<T> set, IEnumerable<T> values)
        {
            var result = new HashSet<T>(set);
            result.RemoveAll(values);
            return result;
        }

        public static IList<T> Minus<T>(this IList<T> list, IEnumerable<T> values)
        {
            var result = new List<T>(list);
            result.RemoveAll(values);
            return result;
        }

        public static IImmutableList<T> Minus<T>(this IImmutableList<T> list, IEnumerable<T> values)
        {
            var result = new List<T>(list);
            result.RemoveAll(values);
            return result.ToImmutableList();
        }

        public static IList<T> Plus<T>(this IList<T> list, IEnumerable<T> values)
        {
            var result = new List<T>(list);
            result.AddAll(values);
            return result;
        }

        public static ISet<T> Plus<T>(this ISet<T> set, IEnumerable<T> values)
        {
            var result = new HashSet<T>(set);
            result.AddAll(values);
            return set;
        }

        public static IImmutableList<T> Plus<T>(this IImmutableList<T> list, IEnumerable<T> values)
        {
            var result = new List<T>(list);
            result.AddAll(values);
            return result.ToImmutableList();
        }
    }
}
