using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Sharpility.Base;
using Sharpility.Collections;
using Sharpility.Collections.Concurrent;
using Sharpility.Util;

namespace Sharpility.Extensions
{
    /// <summary>
    /// Collections extensions.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds items to this collection.
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
        /// Adds items to this collection.
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
        /// Remove items from this collection.
        /// </summary>
        /// <typeparam name="T">Type of item</typeparam>
        /// <param name="collection">this</param>
        /// <param name="items">removed items</param>
        public static void RemoveAll<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var value in items)
            {
                while (collection.Remove(value))
                {
                }
            }
        }

        /// <summary>
        /// Remove items from this collection.
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
        /// Convert collection items to another type and returns new collection.
        /// </summary>
        /// <typeparam name="T">Type of source collection item</typeparam>
        /// <typeparam name="TV">Type of destination collection item</typeparam>
        /// <param name="list">Source collection</param>
        /// <param name="converter">Item converter</param>
        /// <returns>Converted collection</returns>
        public static IList<TV> ConvertAll<T, TV>(this IList<T> list, Converter<T, TV> converter)
        {
            var results = new List<TV>(list.Count);
            results.AddRange(list.Select(element => converter(element)));
            return results;
        }

        /// <summary>
        /// Convert collection items to another type and returns new collection.
        /// </summary>
        /// <typeparam name="T">Type of source collection item</typeparam>
        /// <typeparam name="TV">Type of destination collection item</typeparam>
        /// <param name="set">Source collection</param>
        /// <param name="converter">Item converter</param>
        /// <returns>Converted collection</returns>
        public static ISet<TV> ConvertAll<T, TV>(this ISet<T> set, Converter<T, TV> converter)
        {
            var results = new HashSet<TV>();
            results.AddAll(set.Select(element => converter(element)));
            return results;
        }

        /// <summary>
        /// Checks is collection contains all of given items.
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="enumerable">this</param>
        /// <param name="items">checked items</param>
        /// <returns>true if collection contais all of given items</returns>
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
        /// Checks is collection contais item.
        /// </summary>
        /// <param name="enumerable">this</param>
        /// <param name="item">checked item</param>
        /// <returns>true if collection contais item</returns>
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
                var containsMethod = type.GetMethod(name: "Contains", types: new [] { genericType });
                return item.GetType().IsAssignableFrom(genericType) && (bool) containsMethod.Invoke(obj: enumerable, parameters: new[] { item });
            }
            else
            {
                return ContainsElement(enumerable, item);
            }
        }

        /// <summary>
        /// Returns number of items in collection.
        /// </summary>
        /// <param name="enumerable">this</param>
        /// <returns>number if items in collection</returns>
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
        /// Returns type of item accepted by this collection.
        /// </summary>
        /// <param name="enumerable">this</param>
        /// <returns>type of item accepted by this collection</returns>
        public static Type ItemType(this IEnumerable enumerable)
        {
            if (enumerable.GetType().IsArray)
            {
                var elementType = enumerable.GetType().GetElementType();
                return elementType ?? typeof(object);
            }
            if (Lists.IsGenericCollection(enumerable))
            {
                return GenericTypeOf(enumerable);
            }
            return typeof (object);
        }

        /// <summary>
        /// Checks is collection contains all of given items.
        /// </summary>
        /// <param name="enumerable">this</param>
        /// <param name="items">checked items</param>
        /// <returns>true if collection contais all of given items</returns>
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
        /// Checks is collection does not contains any items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="enumerable">this</param>
        /// <returns>true if collection is empty</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }

        /// <summary>
        /// Checks is collection does not contains any items.
        /// </summary>
        /// <param name="enumerable">this</param>
        /// <returns>true if collection is empty</returns>
        public static bool IsEmpty(this IEnumerable enumerable)
        {
            var enumerator = enumerable.GetEnumerator();
            return !enumerator.MoveNext();
        }

        /// <summary>
        /// Checks is collection contains any items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="enumerable">this</param>
        /// <returns>true if collection is not empty</returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Any();
        }

        /// <summary>
        /// Checks is collection contains any items.
        /// </summary>
        /// <param name="enumerable">this</param>
        /// <returns>true if collection is not empty</returns>
        public static bool IsNotEmpty(this IEnumerable enumerable)
        {
            return !IsEmpty(enumerable);
        }

        /// <summary>
        /// Checks is collection contains only one item.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="enumerable">this</param>
        /// <returns>true if collection contais only one item</returns>
        public static bool IsSingleton<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Count() == 1;
        }

        /// <summary>
        /// Checks is collection contains only one item.
        /// </summary>
        /// <param name="enumerable">this</param>
        /// <returns>true if collection contais only one item</returns>
        public static bool IsSingleton(this IEnumerable enumerable)
        {
            var count = 0;
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                count++;
                if (count > 1)
                {
                    return false;
                }
            }
            return count == 1;
        }

        /// <summary>
        /// Returns collection with removed items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="set">this</param>
        /// <param name="items">removed items</param>
        /// <returns>new collection with removed items</returns>
        public static ISet<T> Minus<T>(this ISet<T> set, IEnumerable<T> items)
        {
            var result = new HashSet<T>(set);
            result.RemoveAll(items);
            return result;
        }

        /// <summary>
        /// Returns immutable collection with removed items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="set">this</param>
        /// <param name="items">removed items</param>
        /// <returns>new collection with removed items</returns>
        public static IImmutableSet<T> Minus<T>(this IImmutableSet<T> set, IEnumerable<T> items)
        {
            var result = ImmutableHashSet.CreateBuilder<T>();
            result.AddAll(set);
            result.RemoveAll(items);
            return result.ToImmutableHashSet();
        }

        /// <summary>
        /// Returns collection with removed items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="list">this</param>
        /// <param name="items">removed items</param>
        /// <returns>new collection with removed items</returns>
        public static IList<T> Minus<T>(this IList<T> list, IEnumerable<T> items)
        {
            var result = new List<T>(list);
            result.RemoveAll(items);
            return result;
        }

        /// <summary>
        /// Returns immutable collection with removed items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="list">this</param>
        /// <param name="items">removed items</param>
        /// <returns>new collection with removed items</returns>
        public static IImmutableList<T> Minus<T>(this IImmutableList<T> list, IEnumerable<T> items)
        {
            var result = ImmutableList.CreateBuilder<T>();
            result.AddAll(list);
            result.RemoveAll(items);
            return result.ToImmutableList();
        }

        /// <summary>
        /// Returns collection with added items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="set">this</param>
        /// <param name="items">added items</param>
        /// <returns>new collection with added items</returns>
        public static ISet<T> Plus<T>(this ISet<T> set, IEnumerable<T> items)
        {
            var result = new HashSet<T>(set);
            result.AddAll(items);
            return result;
        }

        /// <summary>
        /// Returns immutable collection with added items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="set">this</param>
        /// <param name="items">added items</param>
        /// <returns>new collection with added items</returns>
        public static IImmutableSet<T> Plus<T>(this IImmutableSet<T> set, IEnumerable<T> items)
        {
            var result = ImmutableHashSet.CreateBuilder<T>();
            result.AddAll(set);
            result.AddAll(items);
            return result.ToImmutableHashSet();
        }

        /// <summary>
        /// Returns collection with added items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="list">this</param>
        /// <param name="items">added items</param>
        /// <returns>new collection with added items</returns>
        public static IList<T> Plus<T>(this IList<T> list, IEnumerable<T> items)
        {
            var result = new List<T>(list);
            result.AddAll(items);
            return result;
        }

        /// <summary>
        /// Returns immutable collection with added items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="list">this</param>
        /// <param name="items">added items</param>
        /// <returns>new collection with added items</returns>
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
        /// Removes first item from this collection.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="collection">this</param>
        /// <returns>Removed item</returns>
        public static T RemoveFirst<T>(this ICollection<T> collection)
        {
            var value = collection.First();
            if (collection is LinkedList<T>)
            {
                ((LinkedList<T>)collection).RemoveFirst();
            }
            Preconditions.IsNotNull(value, () =>
                new InvalidOperationException("Could not remove first item from empty collection"));
            if (collection is IList<T>)
            {
                ((IList<T>)collection).RemoveAt(0);
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
        public static IQueue<T> ToQueue<T>(this Queue<T> queue)
        {
            return new DefaultQueue<T>(queue);
        }

        /// <summary>
        /// Wraps this queue in IQueue contract.
        /// </summary>
        /// <typeparam name="T">Type of queue element</typeparam>
        /// <param name="queue">this</param>
        /// <returns>IQueue</returns>
        public static IQueue<T> ToQueue<T>(this ConcurrentQueue<T> queue)
        {
            return new DefaultConcurrentQueue<T>(queue);
        }

        /// <summary>
        /// Returns filtered collection items by given predicate.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="list">this</param>
        /// <param name="filter">Item filter</param>
        /// <returns>Filtered collection</returns>
        public static IList<T> FindAll<T>(this IList<T> list, Predicate<T> filter)
        {
            if (list is List<T>)
            {
                return ((List<T>) list).FindAll(filter);
            }
            else
            {
                return list.Where(item => filter(item)).ToList();
            }
        }

        /// <summary>
        /// Returns filtered collection items by given predicate.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="enumerable">this</param>
        /// <param name="filter">Item filter</param>
        /// <returns>Filtered collection</returns>
        public static IEnumerable<T> FindAll<T>(this IEnumerable<T> enumerable, Predicate<T> filter)
        {
            if (enumerable is List<T>)
            {
                return ((List<T>)enumerable).FindAll(filter);
            } else if (enumerable is ISet<T>)
            {
                return FindAll((ISet<T>) enumerable, filter);
            }
            var filtered =  enumerable.Where(item => filter(item)).ToList();
            if (enumerable is LinkedList<T>)
            {
                return Lists.AsLinkedList(filtered);
            }
            return filtered;
        }

        /// <summary>
        /// Converts collection to collection.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="enumberable">this</param>
        /// <returns>Set</returns>
        public static ISet<T> ToSet<T>(this IEnumerable<T> enumberable)
        {
            return new HashSet<T>(enumberable);
        } 

        /// <summary>
        /// Returns filtered collection items by given predicate.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="set">this</param>
        /// <param name="filter">Item filter</param>
        /// <returns>Filtered collection</returns>
        public static ISet<T> FindAll<T>(this ISet<T> set, Predicate<T> filter)
        {
            return set.Where(item => filter(item)).ToSet();
        }

        /// <summary>
        /// Converts collection to MultiDictionary
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="enumerable">collection</param>
        /// <param name="keyConverter">Key converter</param>
        /// <param name="valueConverter">Value converter</param>
        /// <returns>MultiDictionary</returns>
        public static MultiDictionary<TKey, TValue> ToMultiDictionary<T, TKey, TValue>(this IEnumerable<T> enumerable,
            Converter<T, TKey> keyConverter, Converter<T, TValue> valueConverter)
        {
            var result = new ArrayListMultiDictionary<TKey, TValue>();
            foreach (var element in enumerable)
            {
                var key = keyConverter(element);
                var value = valueConverter(element);
                result.Put(key, value);
            }
            return result;
        }

        /// <summary>
        /// Maps collection values by keys and returns MultiDictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of collection value</typeparam>
        /// <param name="enumerable">collection</param>
        /// <param name="keyConverter">Key converter</param>
        /// <returns>MultiDictionary</returns>
        public static MultiDictionary<TKey, TValue> ToMultiDictionary<TKey, TValue>(this IEnumerable<TValue> enumerable,
            Converter<TValue, TKey> keyConverter)
        {
            Converter<TValue, TValue> valueConverter = value => value;
            return ToMultiDictionary(enumerable, keyConverter, valueConverter);
        }

        /// <summary>
        /// Maps collection items to number of occurrences.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="enumerable">this</param>
        /// <returns>Dictionary (item, itemCount)</returns>
        public static IDictionary<T, int> DistinctElementCount<T>(this IEnumerable<T> enumerable)
        {
            var result = new Dictionary<T, int>();
            foreach (var element in enumerable)
            {
                if (result.ContainsKey(element))
                {
                    result[element] = result[element] + 1;
                }
                else
                {
                    result[element] = 1;
                }
            }
            return ComparableDictionary<T, int>.Of(result);
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
            return genericTypes.IsNotEmpty() ? genericTypes[0] : typeof(object);
        }

        private static bool ContainsElement(IEnumerable enumerable, object element)
        {
            return enumerable.Cast<object>().Any(e => Objects.Equal(e, element));
        }

        /// <summary>
        /// Returns slice of collection items from top.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="list">this</param>
        /// <param name="size">Slice size</param>
        /// <returns>Slice of top items</returns>
        public static IList<T> TopSlice<T>(this IList<T> list, int size)
        {
            Preconditions.Evaluate(size >= 0, string.Format("Invalid size: {0} expected >= 0", size));
            if (size <= 0)
            {
                return Lists.EmptyList<T>();
            }
            var count = list.Count;
            if (count <= size)
            {
                return list;
            }
            var result = new List<T>(size);
            var skip = count - size;
            var i = 0;
            foreach (var element in list)
            {
                if (i >= skip)
                {
                    result.Add(element);
                }
                i++;
            }
            return result;
        }

        /// <summary>
        /// Returns slice of collection items from bottom.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="list">this</param>
        /// <param name="size">Slice size</param>
        /// <returns>Slice of bottom items</returns>
        public static IList<T> BottomSlice<T>(this IList<T> list, int size)
        {
            Preconditions.Evaluate(size >= 0, string.Format("Invalid size: {0} expected >= 0", size));
            if (size == 0)
            {
                return Lists.EmptyList<T>();
            }
            var count = list.Count;
            if (count <= size)
            {
                return list;
            }
            var result = new List<T>(size);
            for (var i = 0; i < size; i++)
            {
                var element = list[i];
                result.Add(element);
            }
            return result;
        }

        /// <summary>
        /// Puts element as last item in collection. If collection element is added.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="list">this</param>
        /// <param name="element">Element</param>
        public static void SetLast<T>(this IList<T> list, T element)
        {
            if (list.IsEmpty())
            {
                list.Add(element);
            }
            else
            {
                list[list.Count - 1] = element;
            }
        }

        /// <summary>
        /// Puts element as first item in collection. If collection element is added.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="list">this</param>
        /// <param name="element">Element</param>
        public static void SetFirst<T>(this IList<T> list, T element)
        {
            if (list.IsEmpty())
            {
                list.Add(element);
            }
            else
            {
                list[0] = element;
            }
        }

        /// <summary>
        /// Checks is collection contains any of given elements.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="enumerable">Enumerable</param>
        /// <param name="elements">Checked elements</param>
        /// <returns>true if any element is in collection, false otherwise</returns>
        public static bool ContainsAny<T>(this IEnumerable<T> enumerable, params T[] elements)
        {
            return enumerable.Any(it => Contains(elements, it));
        }

        /// <summary>
        /// Sort collection using given comparer.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="enumerable">this</param>
        /// <param name="comparer">Comparer used for collection</param>
        /// <returns>Sorted collection as collection</returns>
        public static IList<T> SortBy<T>(this IEnumerable<T> enumerable, IComparer<T> comparer)
        {
            return enumerable.OrderBy(item => item, comparer).ToList();
        }

        /// <summary>
        /// Converts collection to comparable collection with equals/hashCode/toString implementations.
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="enumerable">this</param>
        /// <returns>Comparable collection</returns>
        public static IEnumerable<T> ToComparable<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is IList<T>)
            {
                return ComparableList<T>.Of((IList<T>) enumerable);
            }
            else if (enumerable is ISet<T>)
            {
                return ComparableSet<T>.Of((ISet<T>) enumerable);
            }
            else if (enumerable is ICollection<T>)
            {
                return ComparableCollection<T>.Of((ICollection<T>) enumerable);
            }
            return ComparableEnumerable<T>.Of(enumerable);
        }

        /// <summary>
        /// Converts collection to comparable collection with equals/hashCode/toString implementations.
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="set">this</param>
        /// <returns>Comparable collection</returns>
        public static ISet<T> ToComparable<T>(this ISet<T> set)
        {
            return ComparableSet<T>.Of(set);
        }

        /// <summary>
        /// Converts collection to comparable collection with equals/hashCode/toString implementations.
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="list">this</param>
        /// <returns>Comparable collection</returns>
        public static IList<T> ToComparable<T>(this IList<T> list)
        {
            return ComparableList<T>.Of(list);
        }

        /// <summary>
        /// Converts collection to comparable collection with equals/hashCode/toString implementations.
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="collection">this</param>
        /// <returns>Comparable collection</returns>
        public static ICollection<T> ToComparable<T>(this ICollection<T> collection)
        {
            return ComparableCollection<T>.Of(collection);
        }
    }
}
