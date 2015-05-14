using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Sharpility.Base;

namespace Sharpility.Util
{
    public static class Enums
    {
        public static T ValueOf<T>(string value)
            where T : struct, IConvertible
        {
            Precognitions.Evaluate(typeof(T).IsEnum,
                () => new ArgumentException("Expected enum type"));
            return (T)Enum.Parse(typeof(T), value);
        }

        public static T ValueOf<T>(string value, T defaultValue)
          where T : struct, IConvertible
        {
            Precognitions.Evaluate(typeof(T).IsEnum,
                () => new ArgumentException("Expected enum type"));
            T result;
            return Enum.TryParse(value, out result) ? result : defaultValue;
        }

        public static T? TryValueOf<T>(string value)
            where T : struct, IConvertible
        {
            Precognitions.Evaluate(typeof(T).IsEnum,
                () => new ArgumentException("Expected enum type"));
            T result;
            return Enum.TryParse(value, out result) ? result : (T?)null;
        }

        public static ISet<T> Values<T>()
             where T : struct, IConvertible
        {
            Precognitions.Evaluate(typeof(T).IsEnum,
                () => new ArgumentException("Expected enum type"));
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
            Precognitions.Evaluate(typeof(T).IsEnum,
                () => new ArgumentException("Expected enum type"));
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
            Precognitions.Evaluate(typeof(T).IsEnum,
                () => new ArgumentException("Expected enum type"));
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
