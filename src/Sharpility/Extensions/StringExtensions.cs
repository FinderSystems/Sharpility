using System.Linq;

namespace Sharpility.Extensions
{
    public static class StringExtensions
    {
        public static string Repeat(this string value, int count)
        {
            return string.Concat(Enumerable.Repeat(value, count));
        }
    }
}
