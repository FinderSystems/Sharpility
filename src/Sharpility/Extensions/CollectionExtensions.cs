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
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds items to this enumerable.
        /// </summary>
        /// <typeparam name="T">Type of item</typeparam>
        /// <param name="collection">this</param>
        /// <param name="items">added items</param>
        public static void AddAll<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var value in items)
            {
                collection.Add(value);
            }
        }

        /// <summary>
        /// Adds items to this enumerable.
        /// </summary>
        /// <typeparam name="T">Type of item</typeparam>
        /// <param name="collection">this</param>
        /// <param name="items">added items</param>
        public static void AddAll<T>(this ICollection<T> collection, params T[] items)
        {
            foreach (var value in items)
            {
                collection.Add(value);
            }
        }

        /// <summary>
        /// Remove items from this enumerable.
        /// </summary>
        /// <typeparam name="T">Type of item</typeparam>
        /// <param name="collection">this</param>
        /// <param name="items">removed items</param>
        public static void RemoveAll<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var value in items)
            {
                collection.Remove(value);
            }
        }

        /// <summary>
        /// Remove items from this enumerable.
        /// </summary>
        /// <typeparam name="T">Type of item</typeparam>
        /// <param name="collection">this</param>
        /// <param name="items">removed items</param>
        public static void RemoveAll<T>(this ICollection<T> collection, params T[] items)
        {
            foreach (var value in items)
            {
                collection.Remove(value);
            }
        }

        /// <summary>
        /// Convert list items to another type and returns new set.
        /// </summary>
        /// <typeparam name="T">Type of source list item</typeparam>
        /// <typeparam name="TV">Type of destination list item</typeparam>
        /// <param name="list">Source set</param>
        /// <param name="converter">Item converter</param>
        /// <returns>Converted set</returns>
        public static IList<TV> ConvertAll<T, TV>(this IList<T> list, Converter<T, TV> converter)
        {
            var results = new List<TV>(list.Count);
            results.AddRange(list.Select(element => converter(element)));
            return results;
        }

        /// <summary>
        /// Convert set items to another type and returns new set.
        /// </summary>
        /// <typeparam name="T">Type of source set item</typeparam>
        /// <typeparam name="TV">Type of destination set item</typeparam>
        /// <param name="set">Source set</param>
        /// <param name="converter">Item converter</param>
        /// <returns>Converted set</returns>
        public static ISet<TV> ConvertAll<T, TV>(this ISet<T> set, Converter<T, TV> converter)
        {
            var results = new HashSet<TV>();
            results.AddAll(set.Select(element => converter(element)));
            return results;
        }

        /// <summary>
        /// Checks is enumerable contains all of given items.
        /// </summary>
        /// <typeparam name="T">Type of enumerable</typeparam>
        /// <param name="enumerable">this</param>
        /// <param name="items">checked items</param>
        /// <returns>true if enumerable contais all of given items</returns>
        public static bool ContainsAll<T>(this IEnumerable<T> enumerable, IEnumerable<T> items)
        {
            foreach (var element in items)
            {
                if (!enumerable.Contains(element))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks is enumerable contais item.
        /// </summary>
        /// <param name="enumerable">this</param>
        /// <param name="item">checked item</param>
        /// <returns>true if enumerable contais item</returns>
        public static bool Contains(this IEnumerable enumerable, object item)
        {
            if (enumerable is IList)
            {
                return ((IList) enumerable).Contains(item);
            }
            else if (item != null && Lists.IsGenericCollection(enumerable))
            {
                var type = enumerable.GetType();
                var genericType = enumerable.ItemType();
                var containsMethod = type.GetMethod(name: "Contains", types: new Type[] { genericType });
                return item.GetType().IsAssignableFrom(genericType) && (bool) containsMethod.Invoke(obj: enumerable, parameters: new[] { item });
            }
            else
            {
                return ContainsElement(enumerable, item);
            }
        }

        /// <summary>
        /// Counts number of items in enumerable.
        /// </summary>
        /// <param name="enumerable">this</param>
        /// <returns>number if items in enumerable</returns>
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

        /// <summary>
        /// Returns type of item accepted by this enumerable.
        /// </summary>
        /// <param name="enumerable">this</param>
        /// <returns>type of item accepted by this enumerable</returns>
        public static Type ItemType(this IEnumerable enumerable)
        {
            if (Lists.IsGenericCollection(enumerable))
            {
                return GenericTypeOf(enumerable);
            }
            return typeof (object);
        }

        /// <summary>
        /// Checks is enumerable contains all of given items.
        /// </summary>
        /// <param name="enumerable">this</param>
        /// <param name="items">checked items</param>
        /// <returns>true if enumerable contais all of given items</returns>
        public static bool ContainsAll(this IEnumerable enumerable, IEnumerable items)
        {
            foreach (var element in items)
            {
                if (!enumerable.Contains(element))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks is enumerable does not contains any items.
        /// </summary>
        /// <typeparam name="T">Type of enumerable item</typeparam>
        /// <param name="enumerable">this</param>
        /// <returns>true if enumerable is empty</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }

        /// <summary>
        /// Checks is enumerable contains any items.
        /// </summary>
        /// <typeparam name="T">Type of enumerable item</typeparam>
        /// <param name="enumerable">this</param>
        /// <returns>true if enumerable is not empty</returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Any();
        }

        /// <summary>
        /// Checks is enumerable contains only one item.
        /// </summary>
        /// <typeparam name="T">Type of enumerable item</typeparam>
        /// <param name="enumerable">this</param>
        /// <returns>true if enumerable contais only one item</returns>
        public static bool IsSingleton<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Count() == 1;
        }

        /// <summary>
        /// Returns set with removed items.
        /// </summary>
        /// <typeparam name="T">Type of set item</typeparam>
        /// <param name="set">this</param>
        /// <param name="items">removed items</param>
        /// <returns>new set with removed items</returns>
        public static ISet<T> Minus<T>(this ISet<T> set, IEnumerable<T> items)
        {
            var result = new HashSet<T>(set);
            result.RemoveAll(items);
            return result;
        }

        /// <summary>
        /// Returns immutable set with removed items.
        /// </summary>
        /// <typeparam name="T">Type of set item</typeparam>
        /// <param name="set">this</param>
        /// <param name="items">removed items</param>
        /// <returns>new set with removed items</returns>
        public static IImmutableSet<T> Minus<T>(this IImmutableSet<T> set, IEnumerable<T> items)
        {
            var result = ImmutableHashSet.CreateBuilder<T>();
            result.AddAll(set);
            result.RemoveAll(items);
            return result.ToImmutableHashSet();
        }

        /// <summary>
        /// Returns list with removed items.
        /// </summary>
        /// <typeparam name="T">Type of list item</typeparam>
        /// <param name="list">this</param>
        /// <param name="items">removed items</param>
        /// <returns>new list with removed items</returns>
        public static IList<T> Minus<T>(this IList<T> list, IEnumerable<T> items)
        {
            var result = new List<T>(list);
            result.RemoveAll(items);
            return result;
        }

        /// <summary>
        /// Returns immutable list with removed items.
        /// </summary>
        /// <typeparam name="T">Type of list item</typeparam>
        /// <param name="list">this</param>
        /// <param name="items">removed items</param>
        /// <returns>new list with removed items</returns>
        public static IImmutableList<T> Minus<T>(this IImmutableList<T> list, IEnumerable<T> items)
        {
            var result = ImmutableList.CreateBuilder<T>();
            result.AddAll(list);
            result.RemoveAll(items);
            return result.ToImmutableList();
        }

        /// <summary>
        /// Returns set with added items.
        /// </summary>
        /// <typeparam name="T">Type of set item</typeparam>
        /// <param name="set">this</param>
        /// <param name="items">added items</param>
        /// <returns>new set with added items</returns>
        public static ISet<T> Plus<T>(this ISet<T> set, IEnumerable<T> items)
        {
            var result = new HashSet<T>(set);
            result.AddAll(items);
            return set;
        }

        /// <summary>
        /// Returns immutable set with added items.
        /// </summary>
        /// <typeparam name="T">Type of set item</typeparam>
        /// <param name="set">this</param>
        /// <param name="items">added items</param>
        /// <returns>new set with added items</returns>
        public static IImmutableSet<T> Plus<T>(this IImmutableSet<T> set, IEnumerable<T> items)
        {
            var result = ImmutableHashSet.CreateBuilder<T>();
            result.AddAll(set);
            result.AddAll(items);
            return set.ToImmutableHashSet();
        }

        /// <summary>
        /// Returns list with added items.
        /// </summary>
        /// <typeparam name="T">Type of list item</typeparam>
        /// <param name="list">this</param>
        /// <param name="items">added items</param>
        /// <returns>new list with added items</returns>
        public static IList<T> Plus<T>(this IList<T> list, IEnumerable<T> items)
        {
            var result = new List<T>(list);
            result.AddAll(items);
            return result;
        }

        /// <summary>
        /// Returns immutable list with added items.
        /// </summary>
        /// <typeparam name="T">Type of list item</typeparam>
        /// <param name="list">this</param>
        /// <param name="items">added items</param>
        /// <returns>new list with added items</returns>
        public static IImmutableList<T> Plus<T>(this IImmutableList<T> list, IEnumerable<T> items)
        {
            var result = ImmutableList.CreateBuilder<T>();
            result.AddAll(list);
            result.AddAll(items);
            return result.ToImmutableList();
        }

        /// <summary>
        /// Sorts this collection.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="collection">this</param>
        /// <param name="comparer">optional comparer</param>
        public static void Sort<T>(this ICollection<T> collection, IComparer<T> comparer = null)
        {
            if (collection is List<T>)
            {
                ((List<T>)collection).Sort(comparer);
            }
            else
            {
                SortCollection(collection, comparer);
            }
        }

        /// <summary>
        /// Returns first item from this collection.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="collection">this</param>
        /// <returns>First item</returns>
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

        /// <summary>
        /// Returns last item from this collection.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="collection">this</param>
        /// <returns>Last item</returns>
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

        /// <summary>
        /// Removes first item from this collection.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="collection">this</param>
        /// <returns>Removed item</returns>
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

        /// <summary>
        /// Wraps this queue in IQueue contract.
        /// </summary>
        /// <typeparam name="T">Type of queue element</typeparam>
        /// <param name="queue">this</param>
        /// <returns>IQueue</returns>
        public static IQueue<T> ToIQueue<T>(this Queue<T> queue)
        {
            return new DefaultQueue<T>(queue);
        }

        /// <summary>
        /// Wraps this queue in IQueue contract.
        /// </summary>
        /// <typeparam name="T">Type of queue element</typeparam>
        /// <param name="queue">this</param>
        /// <returns>IQueue</returns>
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
