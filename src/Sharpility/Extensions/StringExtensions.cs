using System.Linq;
using System.Text;

namespace Sharpility.Extensions
{
    public static class StringExtensions
    {
        public static string Repeat(this string value, int count)
        {
            return count > 0 ? string.Concat(Enumerable.Repeat(value, count)) : "";
        }

        public static byte[] ToBytes(this string value, Encoding encoding = null)
        {
            return (encoding ?? Encoding.Default).GetBytes(value);
        }

        public static byte[] ToAsciiBytes(this string value)
        {
            return ToBytes(value, Encoding.ASCII);
        }

        public static byte[] ToUtf8Bytes(this string value)
        {
            return ToBytes(value, Encoding.UTF8);
        }
    }
}
