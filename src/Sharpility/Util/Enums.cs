using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Sharpility.Base;
using Sharpility.Extensions;

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
        /// <param name="ignoreCase">Disabled by default. When set to true ignores enum name case</param>
        /// <returns>Type of enum</returns>
        public static T ValueOf<T>(string value, bool ignoreCase = false)
        {
            Preconditions.Evaluate(typeof(T).IsEnum,
                () => new ArgumentException("Expected enum type"));
            Preconditions.IsNotNull(value, () => new ArgumentException(string.Format("Value: null is not supported by {0} enum", typeof(T))));
            if (ignoreCase)
            {
                foreach (var enumValue in Values<T>())
                {
                    if (enumValue.ToString().EqualsIgnoreCases(value))
                    {
                        return enumValue;
                    }
                }
                throw new ArgumentException(string.Format("Value: '{0}' is not supported by {1} enum", value, typeof(T)));
            }
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Tries to parse enum value by name.
        /// Returns defaultValue if fails to parse.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">parsed value</param>
        /// <param name="defaultValue">default enum value</param>
        /// <param name="ignoreCase">Disabled by default. When set to true ignores enum name case</param>
        /// <returns>Enum value or defaultValue</returns>
        public static T ValueOf<T>(string value, T defaultValue, bool ignoreCase = false)
        {
            Preconditions.Evaluate(typeof(T).IsEnum,
                () => new ArgumentException("Expected enum type"));
            foreach (var enumValue in Values<T>())
            {
                var comparationType = ignoreCase
                    ? StringComparison.InvariantCultureIgnoreCase
                    : StringComparison.InvariantCulture;
                if (enumValue.ToString().Equals(value, comparationType))
                {
                    return enumValue;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Tries to parse enum value by name.
        /// Returns Null if fails.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">parsed value</param>
        /// <param name="ignoreCase">Disabled by default. When set to true ignores enum name case</param>
        /// <returns>Enum value or Null</returns>
        public static T? TryValueOf<T>(string value, bool ignoreCase = false)
            where T: struct 
        {
            Preconditions.Evaluate(typeof(T).IsEnum,
                () => new ArgumentException("Expected enum type"));
            if (ignoreCase)
            {
                foreach (var enumValue in Values<T>())
                {
                    if (enumValue.ToString().EqualsIgnoreCases(value))
                    {
                        return enumValue;
                    }
                }
                return null;
            }
            T result;
            return Enum.TryParse(value, out result) ? result : (T?)null;
        }

        /// <summary>
        /// Return set of enum values.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <returns>set of enum values</returns>
        public static ISet<T> Values<T>()
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
