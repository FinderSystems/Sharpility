using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Sharpility.Util
{
    public static class Enums
    {
        public static T ValueOf<T>(string value)
            where T : struct, IConvertible
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static ISet<T> Values<T>()
             where T : struct, IConvertible
        {
            var values = Enum.GetValues(typeof(T));
            var result = ImmutableHashSet.CreateBuilder<T>();
            var iterator = values.GetEnumerator();
            while (iterator.MoveNext())
            {
                var value = iterator.Current;
                result.Add((T)value);
            }
            return result.ToImmutable();
        }

        public static ISet<int> Ordinals<T>()
             where T : struct, IConvertible
        {
            var values = Enum.GetValues(typeof(T));
            var result = ImmutableHashSet.CreateBuilder<int>();
            var iterator = values.GetEnumerator();
            while (iterator.MoveNext())
            {
                var value = iterator.Current;
                result.Add((int)value);
            }
            return result.ToImmutable();
        }

        public static ISet<string> Names<T>()
             where T : struct, IConvertible
        {
            var values = Enum.GetValues(typeof(T));
            var result = ImmutableHashSet.CreateBuilder<string>();
            var iterator = values.GetEnumerator();
            while (iterator.MoveNext())
            {
                var value = iterator.Current;
                result.Add(value.ToString());
            }
            return result.ToImmutable();
        }
    }
}
