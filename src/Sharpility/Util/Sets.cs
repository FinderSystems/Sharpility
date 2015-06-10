using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Sharpility.Util
{
    /// <summary>
    /// Sets utilis.
    /// </summary>
    public static class Sets
    {
        /// <summary>
        /// Creates empty immutable set.
        /// </summary>
        /// <typeparam name="T">Type of set item</typeparam>
        /// <returns>empty set</returns>
        public static ISet<T> EmptySet<T>()
        {
            return ImmutableHashSet.Create<T>();
        }

        /// <summary>
        /// Creates empty mutable set.
        /// </summary>
        /// <typeparam name="T">Type of set</typeparam>
        /// <returns>empty set</returns>
        public static ISet<T> EmptyMutableSet<T>()
        {
            return new HashSet<T>();
        }

        /// <summary>
        /// Creates single element immutable set.
        /// </summary>
        /// <typeparam name="T">Type of set item</typeparam>
        /// <param name="element">set element</param>
        /// <returns>single element set</returns>
        public static ISet<T> Singleton<T>(T element)
        {
            return ImmutableHashSet.Create(element);
        }

        /// <summary>
        /// If given set is null returns empty set.
        /// </summary>
        /// <typeparam name="T">Type of set item</typeparam>
        /// <param name="set">set</param>
        /// <returns>null safe set</returns>
        public static ISet<T> NullSafeSet<T>(ISet<T> set)
        {
            return set ?? EmptyMutableSet<T>();
        }

        /// <summary>
        /// Creates set from given values.
        /// </summary>
        /// <typeparam name="T">Type of set item</typeparam>
        /// <param name="values">Set values</param>
        /// <returns>set</returns>
        public static ISet<T> AsSet<T>(params T[] values)
        {
            return new HashSet<T>(values);
        }

        /// <summary>
        /// Creates set from given values.
        /// </summary>
        /// <typeparam name="T">Type of set item</typeparam>
        /// <param name="values">Set values</param>
        /// <returns>set</returns>
        public static ISet<T> AsSet<T>(IEnumerable<T> values)
        {
            return new HashSet<T>(values);
        }

        /// <summary>
        /// Chceks is object implements generic ISet interface.
        /// </summary>
        /// <param name="obj">Checked object</param>
        /// <returns>True if object is generic ISet</returns>
        public static bool IsGenericSet(object obj)
        {
            return obj != null && obj.GetType().GetInterfaces().Any(
               interfaceType => interfaceType.IsGenericType &&
                   interfaceType.GetGenericTypeDefinition() == typeof(ISet<>));
        }
    }
}
