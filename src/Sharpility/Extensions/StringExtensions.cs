using System;
using System.Linq;
using System.Text;
using Sharpility.Base;

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
        /// <param name="source">this</param>
        /// <param name="count">number of repetitions</param>
        /// <returns>repeated string</returns>
        public static string Repeat(this string source, int count)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return count > 0 ? string.Concat(Enumerable.Repeat(source, count)) : "";
        }

        /// <summary>
        /// Convert string to byte array in given encoding.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="encoding">encoding, if not provided Encoding.Default</param>
        /// <returns>byte array</returns>
        public static byte[] ToBytes(this string source, Encoding encoding = null)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return (encoding ?? Encoding.Default).GetBytes(source);
        }

        /// <summary>
        /// Converts string to byte array in ASCII encoding.
        /// </summary>
        /// <param name="source">this</param>
        /// <returns>ASCII byte array</returns>
        public static byte[] ToAsciiBytes(this string source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return ToBytes(source, Encoding.ASCII);
        }

        /// <summary>
        /// Converts string to byte array in UTF-8 encoding.
        /// </summary>
        /// <param name="source">this</param>
        /// <returns>UTF-8 byte array</returns>
        public static byte[] ToUtf8Bytes(this string source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return ToBytes(source, Encoding.UTF8);
        }

        /// <summary>
        /// Compares string with case ignore.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="comparedTo">compared to</param>
        /// <returns>true if is equal</returns>
        public static bool EqualsIgnoreCases(this string source, string comparedTo)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return source.Equals(comparedTo, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
