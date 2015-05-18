using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Sharpility.Util
{
    public static class Lists
    {
        public static IList<T> EmptyList<T>()
        {
            return ImmutableList.Create<T>();
        }

        public static IList<T> EmptyMutableList<T>()
        {
            return new List<T>();
        }

        public static IList<T> Singleton<T>(T element)
        {
            return ImmutableList.Create(element);
        }

        public static IList<T> NullSafeList<T>(IList<T> list)
        {
            return list ?? EmptyMutableList<T>();
        }

        public static IList<T> AsList<T>(params T[] values)
        {
            return new List<T>(values);
        }

        public static bool IsGenericEnumerable(object obj)
        {
            return obj != null && obj.GetType().GetInterfaces().Any(
                interfaceType => interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }

        public static bool IsGenericCollection(object obj)
        {
            return obj != null && obj.GetType().GetInterfaces().Any(
                interfaceType => interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>)); 
        }

        public static bool IsGenericList(object obj)
        {
            return obj != null && obj.GetType().GetInterfaces().Any(
               interfaceType => interfaceType.IsGenericType &&
                   interfaceType.GetGenericTypeDefinition() == typeof(IList<>));
        }
    }
}
