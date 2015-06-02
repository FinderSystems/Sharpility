

using System;
using System.Collections;
using System.Text;

namespace Sharpility.Util
{
    /// <summary>
    /// Strings Utility.
    /// </summary>
    public static class Strings
    {
        /// <summary>
        /// Returns length of given string.
        /// If string is NULL returns 0.
        /// </summary>
        /// <param name="value">given string</param>
        /// <returns>string length</returns>
        public static int Length(string value)
        {
            return value != null ? value.Length : 0;
        }

        /// <summary>
        /// Trims string to given length.
        /// </summary>
        /// <param name="value">string</param>
        /// <param name="length">maximum string length</param>
        /// <returns>string limtited to given length</returns>
        public static string LimitedString(string value, int length)
        {
            if (Length(value) > length)
            {
                return value.Substring(0, length);
            }
            return value;
        }

        /// <summary>
        /// Converts object to string with collections and dictinoaries support.
        /// </summary>
        /// <param name="value">object converted string</param>
        /// <returns>generated string</returns>
        public static string ToString(object value)
        {
            if (value is string)
            {
                return (string) value;
            }
            else if (value is IDictionary)
            {
                return ToString((IDictionary) value);
            }
            else if (value is IEnumerator)
            {
                return ToString((IEnumerator) value);
            }
            else if (value is IEnumerable)
            {
                return ToString((IEnumerable) value);
            }
            return value != null ? value.ToString() : "null";
        }

        private static string ToString(IEnumerable values)
        {
            var builder = new StringBuilder("[");
            foreach (var value in values)
            {
                if (builder.Length > 1)
                {
                    builder.Append(", ");
                }
                builder.Append(ToString(value));
            }
            builder.Append("]");
            return builder.ToString();
        }

        private static string ToString(IEnumerator iterator)
        {
            var builder = new StringBuilder("[");
            while (iterator.MoveNext())
            {
                var value = iterator.Current;
                if (builder.Length > 1)
                {
                    builder.Append(", ");
                }
                builder.Append(ToString(value));
            }
            builder.Append("]");
            return builder.ToString();
        }

        private static string ToString(IDictionary map)
        {
            var builder = new StringBuilder("[");
            var iterator = map.GetEnumerator();
            while (iterator.MoveNext())
            {
                var entry = iterator.Entry;
                if (builder.Length > 1)
                {
                    builder.Append(", ");
                }
                builder.Append(String.Format("({0}, {1})", ToString(entry.Key), ToString(entry.Value)));
            }
            builder.Append("]");
            return builder.ToString();
        }
    }
}
