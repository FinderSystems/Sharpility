

using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Sharpility.Extensions;

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
            if (value is byte[])
            {
                return Encoding.Default.GetString((byte[])value);
            }
            if (value is IDictionary)
            {
                return ToString((IDictionary) value);
            }
            if (value is IEnumerator)
            {
                return ToString((IEnumerator) value);
            }
            if (value is IEnumerable)
            {
                return ToString((IEnumerable) value);
            }
            if (value is DateTime)
            {
                return ToString((DateTime) value);
            }
            return value != null ? value.ToString() : "null";    
        }

        private static string ToString(IEnumerable enumerable)
        {
            var builder = new StringBuilder("[");
            foreach (var value in enumerable)
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

        private static string ToString(IDictionary dictionary)
        {
            var builder = new StringBuilder("[");
            var iterator = dictionary.GetEnumerator();
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

        private static string ToString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + Kind(dateTime);
        }

        private static string Kind(DateTime dateTime)
        {
            switch (dateTime.Kind)
            {
                case DateTimeKind.Utc:
                    return "Z";
                case DateTimeKind.Local:
                    var offset = TimeZone.CurrentTimeZone.GetUtcOffset(dateTime);
                    var hours = offset.ToHours();
                    var minutes = offset.Minutes;
                    var prefix = offset < TimeSpan.Zero ? "-" : "+";
                    return string.Format("{0}{1:D2}:{2:D2}",
                        prefix, Math.Abs(hours), Math.Abs(minutes));
                default:
                    return "";
            }
        }

        /// <summary>
        /// Decorated String.Format by pre formatting paramters.
        /// </summary>
        /// <param name="value">Formated string</param>
        /// <param name="parameters">Format parameters</param>
        /// <returns>Formatted string</returns>
        public static string Format(string value, params object[] parameters)
        {
            var preFormattedParameters = new object[parameters.Length];
            var i = 0;
            foreach (var param in parameters)
            {
                preFormattedParameters[i ++] = ToString(param);
            }
            return String.Format(value, preFormattedParameters);
        }
    }
}
