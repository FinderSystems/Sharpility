using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Sharpility.Base;

namespace Sharpility.Util
{
    /// <summary>
    /// Enums utility.
    /// </summary>
    public static class Enums
    {
        /// <summary>
        /// Parse enum value by name.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">Parsed value</param>
        /// <returns>Type of enum</returns>
        public static T ValueOf<T>(string value)
            where T : struct, IConvertible
        {
            Preconditions.Evaluate(typeof(T).IsEnum,
                () => new ArgumentException("Expected enum type"));
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Tries to parse enum value by name.
        /// Returns defaultValue if fails to parse.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">parsed value</param>
        /// <param name="defaultValue">default enum value</param>
        /// <returns>Enum value or defaultValue</returns>
        public static T ValueOf<T>(string value, T defaultValue)
          where T : struct, IConvertible
        {
            Preconditions.Evaluate(typeof(T).IsEnum,
                () => new ArgumentException("Expected enum type"));
            T result;
            return Enum.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// Tries to parse enum value by name.
        /// Returns Null if fails.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">parsed value</param>
        /// <returns>Enum value or Null</returns>
        public static T? TryValueOf<T>(string value)
            where T : struct, IConvertible
        {
            Preconditions.Evaluate(typeof(T).IsEnum,
                () => new ArgumentException("Expected enum type"));
            T result;
            return Enum.TryParse(value, out result) ? result : (T?)null;
        }

        /// <summary>
        /// Return set of enum values.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <returns>set of enum values</returns>
        public static ISet<T> Values<T>()
             where T : struct, IConvertible
        {
            Preconditions.Evaluate(typeof(T).IsEnum,
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

        /// <summary>
        /// Returns set of enum int values.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <returns>set of enum values</returns>
        public static ISet<int> Ordinals<T>()
             where T : struct, IConvertible
        {
            Preconditions.Evaluate(typeof(T).IsEnum,
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

        /// <summary>
        /// Returns set of enum names.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <returns>set of enum names</returns>
        public static ISet<string> Names<T>()
             where T : struct, IConvertible
        {
            Preconditions.Evaluate(typeof(T).IsEnum,
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
