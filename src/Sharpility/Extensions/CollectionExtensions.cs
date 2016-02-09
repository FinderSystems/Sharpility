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
        /// <param name="source">this</param>
        /// <param name="items">added items</param>
        public static void AddAll<T>(this ICollection<T> source, IEnumerable<T> items)
        {
            Preconditions.IsNotNull(source, ()=> new ArgumentNullException("source"));
            foreach (var value in items)
            {
                source.Add(value);
            }
        }

        /// <summary>
        /// Adds items to this collection.
        /// </summary>
        /// <typeparam name="T">Type of item</typeparam>
        /// <param name="source">this</param>
        /// <param name="items">added items</param>
        public static void AddAll<T>(this ICollection<T> source, params T[] items)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            foreach (var value in items)
            {
                source.Add(value);
            }
        }

        /// <summary>
        /// Remove items from this collection.
        /// </summary>
        /// <typeparam name="T">Type of item</typeparam>
        /// <param name="source">this</param>
        /// <param name="items">removed items</param>
        public static void RemoveAll<T>(this ICollection<T> source, IEnumerable<T> items)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            foreach (var value in items)
            {
                while (source.Remove(value))
                {
                }
            }
        }

        /// <summary>
        /// Remove items from this collection.
        /// </summary>
        /// <typeparam name="T">Type of item</typeparam>
        /// <param name="source">this</param>
        /// <param name="items">removed items</param>
        public static void RemoveAll<T>(this ICollection<T> source, params T[] items)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            foreach (var value in items)
            {
                source.Remove(value);
            }
        }

        /// <summary>
        /// Convert collection items to another type and returns new collection.
        /// </summary>
        /// <typeparam name="T">Type of source collection item</typeparam>
        /// <typeparam name="TV">Type of destination collection item</typeparam>
        /// <param name="source">Source collection</param>
        /// <param name="converter">Item converter</param>
        /// <returns>Converted collection</returns>
        public static IList<TV> ConvertAll<T, TV>(this IList<T> source, Converter<T, TV> converter)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var results = new List<TV>(source.Count);
            results.AddRange(source.Select(element => converter(element)));
            return results;
        }

        /// <summary>
        /// Convert collection items to another type and returns new collection.
        /// </summary>
        /// <typeparam name="T">Type of source collection item</typeparam>
        /// <typeparam name="TV">Type of destination collection item</typeparam>
        /// <param name="source">Source collection</param>
        /// <param name="converter">Item converter</param>
        /// <returns>Converted collection</returns>
        public static ISet<TV> ConvertAll<T, TV>(this ISet<T> source, Converter<T, TV> converter)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var results = new HashSet<TV>();
            results.AddAll(source.Select(element => converter(element)));
            return results;
        }

        /// <summary>
        /// Checks is collection contains all of given items.
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="source">this</param>
        /// <param name="items">checked items</param>
        /// <returns>true if collection contais all of given items</returns>
        public static bool ContainsAll<T>(this IEnumerable<T> source, IEnumerable<T> items)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            foreach (var element in items)
            {
                if (!source.Contains(element))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks is collection contais item.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="item">checked item</param>
        /// <returns>true if collection contais item</returns>
        public static bool Contains(this IEnumerable source, object item)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            if (source is IList)
            {
                return ((IList) source).Contains(item);
            }
            else if (item != null && Lists.IsGenericCollection(source))
            {
                var type = source.GetType();
                var genericType = source.ItemType();
                var containsMethod = type.GetMethod(name: "Contains", types: new [] { genericType });
                return item.GetType().IsAssignableFrom(genericType) && (bool) containsMethod.Invoke(obj: source, parameters: new[] { item });
            }
            else
            {
                return ContainsElement(source, item);
            }
        }

        /// <summary>
        /// Returns number of items in collection.
        /// </summary>
        /// <param name="source">this</param>
        /// <returns>number if items in collection</returns>
        public static int Count(this IEnumerable source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            if (source is ICollection)
            {
                return ((ICollection) source).Count;
            }
            if (Lists.IsGenericCollection(source))
            {
                var type = source.GetType();
                var countProperty = type.GetProperty(name: "Count");
                return (int) countProperty.GetValue(obj: source);
            }
            return Enumerable.Count(source.Cast<object>());
        }

        /// <summary>
        /// Returns type of item accepted by this collection.
        /// </summary>
        /// <param name="source">this</param>
        /// <returns>type of item accepted by this collection</returns>
        public static Type ItemType(this IEnumerable source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            if (source.GetType().IsArray)
            {
                var elementType = source.GetType().GetElementType();
                return elementType ?? typeof(object);
            }
            if (Lists.IsGenericCollection(source))
            {
                return GenericTypeOf(source);
            }
            return typeof (object);
        }

        /// <summary>
        /// Checks is collection contains all of given items.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="items">checked items</param>
        /// <returns>true if collection contais all of given items</returns>
        public static bool ContainsAll(this IEnumerable source, IEnumerable items)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            foreach (var element in items)
            {
                if (!source.Contains(element))
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
        /// <param name="source">this</param>
        /// <returns>true if collection is empty</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return !source.Any();
        }

        /// <summary>
        /// Checks is collection does not contains any items.
        /// </summary>
        /// <param name="source">this</param>
        /// <returns>true if collection is empty</returns>
        public static bool IsEmpty(this IEnumerable source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var enumerator = source.GetEnumerator();
            return !enumerator.MoveNext();
        }

        /// <summary>
        /// Checks is collection contains any items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <returns>true if collection is not empty</returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return source.Any();
        }

        /// <summary>
        /// Checks is collection contains any items.
        /// </summary>
        /// <param name="source">this</param>
        /// <returns>true if collection is not empty</returns>
        public static bool IsNotEmpty(this IEnumerable source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return !IsEmpty(source);
        }

        /// <summary>
        /// Checks is collection contains only one item.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <returns>true if collection contais only one item</returns>
        public static bool IsSingleton<T>(this IEnumerable<T> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return source.Count() == 1;
        }

        /// <summary>
        /// Checks is collection contains only one item.
        /// </summary>
        /// <param name="source">this</param>
        /// <returns>true if collection contais only one item</returns>
        public static bool IsSingleton(this IEnumerable source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var count = 0;
            var enumerator = source.GetEnumerator();
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
        /// <param name="source">this</param>
        /// <param name="items">removed items</param>
        /// <returns>new collection with removed items</returns>
        public static ISet<T> Minus<T>(this ISet<T> source, IEnumerable<T> items)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var result = new HashSet<T>(source);
            result.RemoveAll(items);
            return result;
        }

        /// <summary>
        /// Returns immutable collection with removed items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <param name="items">removed items</param>
        /// <returns>new collection with removed items</returns>
        public static IImmutableSet<T> Minus<T>(this IImmutableSet<T> source, IEnumerable<T> items)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var result = ImmutableHashSet.CreateBuilder<T>();
            result.AddAll(source);
            result.RemoveAll(items);
            return result.ToImmutableHashSet();
        }

        /// <summary>
        /// Returns collection with removed items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <param name="items">removed items</param>
        /// <returns>new collection with removed items</returns>
        public static IList<T> Minus<T>(this IList<T> source, IEnumerable<T> items)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var result = new List<T>(source);
            result.RemoveAll(items);
            return result;
        }

        /// <summary>
        /// Returns immutable collection with removed items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <param name="items">removed items</param>
        /// <returns>new collection with removed items</returns>
        public static IImmutableList<T> Minus<T>(this IImmutableList<T> source, IEnumerable<T> items)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var result = ImmutableList.CreateBuilder<T>();
            result.AddAll(source);
            result.RemoveAll(items);
            return result.ToImmutableList();
        }

        /// <summary>
        /// Returns collection with added items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <param name="items">added items</param>
        /// <returns>new collection with added items</returns>
        public static ISet<T> Plus<T>(this ISet<T> source, IEnumerable<T> items)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var result = new HashSet<T>(source);
            result.AddAll(items);
            return result;
        }

        /// <summary>
        /// Returns immutable collection with added items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <param name="items">added items</param>
        /// <returns>new collection with added items</returns>
        public static IImmutableSet<T> Plus<T>(this IImmutableSet<T> source, IEnumerable<T> items)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var result = ImmutableHashSet.CreateBuilder<T>();
            result.AddAll(source);
            result.AddAll(items);
            return result.ToImmutableHashSet();
        }

        /// <summary>
        /// Returns collection with added items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <param name="items">added items</param>
        /// <returns>new collection with added items</returns>
        public static IList<T> Plus<T>(this IList<T> source, IEnumerable<T> items)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var result = new List<T>(source);
            result.AddAll(items);
            return result;
        }

        /// <summary>
        /// Returns immutable collection with added items.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <param name="items">added items</param>
        /// <returns>new collection with added items</returns>
        public static IImmutableList<T> Plus<T>(this IImmutableList<T> source, IEnumerable<T> items)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var result = ImmutableList.CreateBuilder<T>();
            result.AddAll(source);
            result.AddAll(items);
            return result.ToImmutableList();
        }

        /// <summary>
        /// Sorts this collection.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <param name="comparer">optional comparer</param>
        public static void Sort<T>(this ICollection<T> source, IComparer<T> comparer = null)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            if (source is List<T>)
            {
                ((List<T>)source).Sort(comparer);
            }
            else
            {
                SortCollection(source, comparer);
            }
        }

        /// <summary>
        /// Removes first item from this collection.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <returns>Removed item</returns>
        public static T RemoveFirst<T>(this ICollection<T> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var value = source.First();
            if (source is LinkedList<T>)
            {
                ((LinkedList<T>)source).RemoveFirst();
            }
            Preconditions.IsNotNull(value, () =>
                new InvalidOperationException("Could not remove first item from empty collection"));
            if (source is IList<T>)
            {
                ((IList<T>)source).RemoveAt(0);
            }
            else
            {
                source.Remove(value);
                return value;
            }
            return value;
        }

        /// <summary>
        /// Wraps this queue in IQueue contract.
        /// </summary>
        /// <typeparam name="T">Type of queue element</typeparam>
        /// <param name="source">this</param>
        /// <returns>IQueue</returns>
        public static IQueue<T> ToQueue<T>(this Queue<T> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return new DefaultQueue<T>(source);
        }

        /// <summary>
        /// Wraps this queue in IQueue contract.
        /// </summary>
        /// <typeparam name="T">Type of queue element</typeparam>
        /// <param name="source">this</param>
        /// <returns>IQueue</returns>
        public static IQueue<T> ToQueue<T>(this ConcurrentQueue<T> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return new DefaultConcurrentQueue<T>(source);
        }

        /// <summary>
        /// Returns filtered collection items by given predicate.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <param name="filter">Item filter</param>
        /// <returns>Filtered collection</returns>
        public static IList<T> FindAll<T>(this IList<T> source, Predicate<T> filter)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            if (source is List<T>)
            {
                return ((List<T>) source).FindAll(filter);
            }
            return source.Where(item => filter(item)).ToList();
        }

        /// <summary>
        /// Returns filtered collection items by given predicate.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <param name="filter">Item filter</param>
        /// <returns>Filtered collection</returns>
        public static IEnumerable<T> FindAll<T>(this IEnumerable<T> source, Predicate<T> filter)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            if (source is List<T>)
            {
                return ((List<T>)source).FindAll(filter);
            }
            if (source is ISet<T>)
            {
                return FindAll((ISet<T>) source, filter);
            }
            var filtered =  source.Where(item => filter(item)).ToList();
            if (source is LinkedList<T>)
            {
                return Lists.AsLinkedList(filtered);
            }
            return filtered;
        }

        /// <summary>
        /// Converts collection to collection.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <returns>Set</returns>
        public static ISet<T> ToSet<T>(this IEnumerable<T> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return new HashSet<T>(source);
        } 

        /// <summary>
        /// Returns filtered collection items by given predicate.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <param name="filter">Item filter</param>
        /// <returns>Filtered collection</returns>
        public static ISet<T> FindAll<T>(this ISet<T> source, Predicate<T> filter)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return source.Where(item => filter(item)).ToSet();
        }

        /// <summary>
        /// Converts collection to MultiDictionary
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TValue">Type of dictionary value</typeparam>
        /// <param name="source">collection</param>
        /// <param name="keyConverter">Key converter</param>
        /// <param name="valueConverter">Value converter</param>
        /// <returns>MultiDictionary</returns>
        public static MultiDictionary<TKey, TValue> ToMultiDictionary<T, TKey, TValue>(this IEnumerable<T> source,
            Converter<T, TKey> keyConverter, Converter<T, TValue> valueConverter)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var result = new ArrayListMultiDictionary<TKey, TValue>();
            foreach (var element in source)
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
        /// <param name="source">collection</param>
        /// <param name="keyConverter">Key converter</param>
        /// <returns>MultiDictionary</returns>
        public static MultiDictionary<TKey, TValue> ToMultiDictionary<TKey, TValue>(this IEnumerable<TValue> source,
            Converter<TValue, TKey> keyConverter)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            Converter<TValue, TValue> valueConverter = value => value;
            return ToMultiDictionary(source, keyConverter, valueConverter);
        }

        /// <summary>
        /// Maps collection items to number of occurrences.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <returns>Dictionary (item, itemCount)</returns>
        public static IDictionary<T, int> DistinctElementCount<T>(this IEnumerable<T> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var result = new Dictionary<T, int>();
            foreach (var element in source)
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
        /// <param name="source">this</param>
        /// <param name="size">Slice size</param>
        /// <returns>Slice of top items</returns>
        public static IList<T> TopSlice<T>(this IList<T> source, int size)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            Preconditions.Evaluate(size >= 0, string.Format("Invalid size: {0} expected >= 0", size));
            if (size <= 0)
            {
                return Lists.EmptyList<T>();
            }
            var count = source.Count;
            if (count <= size)
            {
                return source;
            }
            var result = new List<T>(size);
            var skip = count - size;
            var i = 0;
            foreach (var element in source)
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
        /// <param name="source">this</param>
        /// <param name="size">Slice size</param>
        /// <returns>Slice of bottom items</returns>
        public static IList<T> BottomSlice<T>(this IList<T> source, int size)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            Preconditions.Evaluate(size >= 0, string.Format("Invalid size: {0} expected >= 0", size));
            if (size == 0)
            {
                return Lists.EmptyList<T>();
            }
            var count = source.Count;
            if (count <= size)
            {
                return source;
            }
            var result = new List<T>(size);
            for (var i = 0; i < size; i++)
            {
                var element = source[i];
                result.Add(element);
            }
            return result;
        }

        /// <summary>
        /// Puts element as last item in collection. If collection element is added.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <param name="element">Element</param>
        public static void SetLast<T>(this IList<T> source, T element)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            if (source.IsEmpty())
            {
                source.Add(element);
            }
            else
            {
                source[source.Count - 1] = element;
            }
        }

        /// <summary>
        /// Puts element as first item in collection. If collection element is added.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <param name="element">Element</param>
        public static void SetFirst<T>(this IList<T> source, T element)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            if (source.IsEmpty())
            {
                source.Add(element);
            }
            else
            {
                source[0] = element;
            }
        }

        /// <summary>
        /// Checks is collection contains any of given elements.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">Enumerable</param>
        /// <param name="elements">Checked elements</param>
        /// <returns>true if any element is in collection, false otherwise</returns>
        public static bool ContainsAny<T>(this IEnumerable<T> source, params T[] elements)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return source.Any(it => Contains(elements, it));
        }

        /// <summary>
        /// Sort collection using given comparer.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="source">this</param>
        /// <param name="comparer">Comparer used for collection</param>
        /// <returns>Sorted collection as collection</returns>
        public static IList<T> SortBy<T>(this IEnumerable<T> source, IComparer<T> comparer)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return source.OrderBy(item => item, comparer).ToList();
        }

        /// <summary>
        /// Converts collection to comparable collection with equals/hashCode/toString implementations.
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="source">this</param>
        /// <returns>Comparable collection</returns>
        public static IEnumerable<T> ToComparable<T>(this IEnumerable<T> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            if (source is IList<T>)
            {
                return ComparableList<T>.Of((IList<T>) source);
            }
            if (source is ISet<T>)
            {
                return ComparableSet<T>.Of((ISet<T>) source);
            }
            if (source is ICollection<T>)
            {
                return ComparableCollection<T>.Of((ICollection<T>) source);
            }
            return ComparableEnumerable<T>.Of(source);
        }

        /// <summary>
        /// Converts collection to comparable collection with equals/hashCode/toString implementations.
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="source">this</param>
        /// <returns>Comparable collection</returns>
        public static ISet<T> ToComparable<T>(this ISet<T> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return ComparableSet<T>.Of(source);
        }

        /// <summary>
        /// Converts collection to comparable collection with equals/hashCode/toString implementations.
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="source">this</param>
        /// <returns>Comparable collection</returns>
        public static IList<T> ToComparable<T>(this IList<T> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return ComparableList<T>.Of(source);
        }

        /// <summary>
        /// Converts collection to comparable collection with equals/hashCode/toString implementations.
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="source">this</param>
        /// <returns>Comparable collection</returns>
        public static ICollection<T> ToComparable<T>(this ICollection<T> source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return ComparableCollection<T>.Of(source);
        }
    }
}
