using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Sharpility.Extensions;

namespace Sharpility.Util
{
    /// <summary>
    /// Lists utils.
    /// </summary>
    public static class Lists
    {
        /// <summary>
        /// Creates empty immutable list.
        /// </summary>
        /// <typeparam name="T">Type of list item</typeparam>
        /// <returns>empty list</returns>
        public static IList<T> EmptyList<T>()
        {
            return ImmutableList.Create<T>();
        }

        /// <summary>
        /// Creates empty mutable list.
        /// </summary>
        /// <typeparam name="T">Type of list item</typeparam>
        /// <returns>empty list</returns>
        public static IList<T> EmptyMutableList<T>()
        {
            return new List<T>();
        }

        /// <summary>
        /// Create single element immutable list.
        /// </summary>
        /// <typeparam name="T">Type of list item</typeparam>
        /// <param name="element">list element</param>
        /// <returns>single element list</returns>
        public static IList<T> Singleton<T>(T element)
        {
            return ImmutableList.Create(element);
        }

        /// <summary>
        /// If given list is null returns empty list.
        /// </summary>
        /// <typeparam name="T">Type of list item</typeparam>
        /// <param name="list">list</param>
        /// <returns>null safe list</returns>
        public static IList<T> NullSafeList<T>(IList<T> list)
        {
            return list ?? EmptyMutableList<T>();
        }

        /// <summary>
        /// Creates new list from given values.
        /// </summary>
        /// <typeparam name="T">Type of list item</typeparam>
        /// <param name="values">values</param>
        /// <returns>list</returns>
        public static IList<T> AsList<T>(params T[] values)
        {
            return new List<T>(values);
        }

        /// <summary>
        /// Creates new linked list from given values.
        /// </summary>
        /// <typeparam name="T">Type of list item</typeparam>
        /// <param name="values">values</param>
        /// <returns>linked list</returns>
        public static LinkedList<T> AsLinkedList<T>(IEnumerable<T> values)
        {
            var result = new LinkedList<T>();
            result.AddAll(values);
            return result;
        } 

        /// <summary>
        /// Checks is object is implements generic IEnumerable interface.
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>true if object is generic IEnumerable</returns>
        public static bool IsGenericEnumerable(object obj)
        {
            return obj != null && obj.GetType().GetInterfaces().Any(
                interfaceType => interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }

        /// <summary>
        /// Checks is object is implements generic ICollection interface.
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>true if object is generic ICollection</returns>
        public static bool IsGenericCollection(object obj)
        {
            return obj != null && obj.GetType().GetInterfaces().Any(
                interfaceType => interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>)); 
        }

        /// <summary>
        /// Checks is object is implements generic IList interface.
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>true if object is generic IList</returns>
        public static bool IsGenericList(object obj)
        {
            return obj != null && obj.GetType().GetInterfaces().Any(
               interfaceType => interfaceType.IsGenericType &&
                   interfaceType.GetGenericTypeDefinition() == typeof(IList<>));
        }
    }
}
