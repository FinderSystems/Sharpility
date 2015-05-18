using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Sharpility.Collections;
using Sharpility.Collections.Concurrent;
using Sharpility.Util;

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

        public static IEnumerable<TV> ConvertAll<T, TV>(this IEnumerable<T> enumerable, Converter<T, TV> converter)
        {
            var results = new List<TV>(enumerable.Count());
            results.AddRange(enumerable.Select(element => converter(element)));
            return results;
        }

        public static bool ContainsAll<T>(this IEnumerable<T> enumerable, IEnumerable<T> elements)
        {
            foreach (var element in elements)
            {
                if (!enumerable.Contains(element))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Contains(this IEnumerable enumerable, object element)
        {
            if (enumerable is IList)
            {
                return ((IList) enumerable).Contains(element);
            }
            else if (element != null && Lists.IsGenericCollection(enumerable))
            {
                var type = enumerable.GetType();
                var genericType = enumerable.ElementType();
                var containsMethod = type.GetMethod(name: "Contains", types: new Type[] { genericType });
                return element.GetType().IsAssignableFrom(genericType) && (bool) containsMethod.Invoke(obj: enumerable, parameters: new[] { element });
            }
            else
            {
                return ContainsElement(enumerable, element);
            }
        }

        public static int Count(this IEnumerable enumerable)
        {
            if (enumerable is ICollection)
            {
                return ((ICollection) enumerable).Count;
            }
            else if (Lists.IsGenericCollection(enumerable))
            {
                var type = enumerable.GetType();
                var countProperty = type.GetProperty(name: "Count");
                return (int) countProperty.GetValue(obj: enumerable);
            }
            else
            {
                int count = 0;
                foreach (var element in enumerable)
                {
                    count ++;
                }
                return count;
            }
        }

        public static Type ElementType(this IEnumerable enumerable)
        {
            if (Lists.IsGenericCollection(enumerable))
            {
                return GenericTypeOf(enumerable);
            }
            return typeof (object);
        }

        public static bool ContainsAll(this IEnumerable collection, IEnumerable elements)
        {
            foreach (var element in elements)
            {
                if (!collection.Contains(element))
                {
                    return false;
                }
            }
            return true;
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

        public static void Sort<T>(this ICollection<T> collection, IComparer<T> comparator = null)
        {
            if (collection is List<T>)
            {
                ((List<T>)collection).Sort(comparator);
            }
            else
            {
                SortCollection(collection, comparator);
            }
        }

        public static T First<T>(this ICollection<T> collection)
        {
            if (collection is LinkedList<T>)
            {
                return ((LinkedList<T>)collection).First.Value;
            }
            else
            {
                return collection.ElementAt(0);
            }
        }

        public static T Last<T>(this ICollection<T> collection)
        {
            if (collection is LinkedList<T>)
            {
                return ((LinkedList<T>)collection).Last.Value;
            }
            else
            {
                return collection.ElementAt(collection.Count - 1);
            }
        }

        public static T RemoveFirst<T>(this ICollection<T> collection)
        {
            var value = collection.First();
            if (collection is IList<T>)
            {
                ((IList<T>)collection).RemoveAt(0);
            }
            else if (collection is LinkedList<T>)
            {
                ((LinkedList<T>)collection).RemoveFirst();
            }
            else
            {
                collection.Remove(value);
                return value;
            }
            return value;
        }

        public static IQueue<T> ToIQueue<T>(this Queue<T> queue)
        {
            return new DefaultQueue<T>(queue);
        }

        public static IQueue<T> ToIQueue<T>(this ConcurrentQueue<T> queue)
        {
            return new DefaultConcurrentQueue<T>(queue);
        }

        private static void SortCollection<T>(ICollection<T> list, IComparer<T> comparator)
        {
            var elements = list.ToArray();
            Array.Sort(elements, 0, elements.Length, comparator);
            list.Clear();
            list.AddAll(elements);
        }

        private static Type GenericTypeOf(IEnumerable enumerable)
        {
            if (enumerable == null)
            {
                return null;
            }
            var type = enumerable.GetType();
            var genericTypes = type.GetGenericArguments();
            return genericTypes.IsNotEmpty() ? genericTypes[0] : null;
        }

        private static bool ContainsElement(IEnumerable enumerable, object element)
        {
            return enumerable.Cast<object>().Any(e => Objects.Equal(e, element));
        }
    }
}
