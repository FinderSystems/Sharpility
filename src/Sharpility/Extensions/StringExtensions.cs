using System;
using System.Linq;
using System.Text;

namespace Sharpility.Extensions
{
    /// <summary>
    /// String extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns repeated string by given times.
        /// </summary>
        /// <param name="value">this</param>
        /// <param name="count">number of repetitions</param>
        /// <returns>repeated string</returns>
        public static string Repeat(this string value, int count)
        {
            return count > 0 ? string.Concat(Enumerable.Repeat(value, count)) : "";
        }

        /// <summary>
        /// Convert string to byte array in given encoding.
        /// </summary>
        /// <param name="value">this</param>
        /// <param name="encoding">encoding, if not provided Encoding.Default</param>
        /// <returns>byte array</returns>
        public static byte[] ToBytes(this string value, Encoding encoding = null)
        {
            return (encoding ?? Encoding.Default).GetBytes(value);
        }

        /// <summary>
        /// Converts string to byte array in ASCII encoding.
        /// </summary>
        /// <param name="value">this</param>
        /// <returns>ASCII byte array</returns>
        public static byte[] ToAsciiBytes(this string value)
        {
            return ToBytes(value, Encoding.ASCII);
        }

        /// <summary>
        /// Converts string to byte array in UTF-8 encoding.
        /// </summary>
        /// <param name="value">this</param>
        /// <returns>UTF-8 byte array</returns>
        public static byte[] ToUtf8Bytes(this string value)
        {
            return ToBytes(value, Encoding.UTF8);
        }

        /// <summary>
        /// Compares string with case ignore.
        /// </summary>
        /// <param name="value">this</param>
        /// <param name="comparedTo">compared to</param>
        /// <returns>true if is equal</returns>
        public static bool EqualsIgnoreCases(this string value, string comparedTo)
        {
            return value.Equals(comparedTo, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
