using System.Collections.Generic;
using System.Collections.Immutable;

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
    }
}
