
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Sharpility.Collections
{
    public static class Sets
    {
        public static ISet<T> EmptySet<T>()
        {
            return ImmutableHashSet.Create<T>();
        }

        public static ISet<T> EmptyMutableSet<T>()
        {
            return new HashSet<T>();
        }

        public static ISet<T> Singleton<T>(T element)
        {
            return ImmutableHashSet.Create(element);
        }

        public static ISet<T> NullSafeSet<T>(ISet<T> set)
        {
            return set ?? EmptyMutableSet<T>();
        }

        public static ISet<T> AsSet<T>(params T[] values)
        {
            return new HashSet<T>(values);
        }
    }
}
