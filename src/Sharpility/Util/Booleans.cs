
namespace Sharpility.Util
{
    /// <summary>
    /// Utils for bools.
    /// </summary>
    public static class Booleans
    {
        /// <summary>
        /// Tries to parse bool from string. When parsing fails returns null.
        /// </summary>
        /// <param name="value">String value to parse</param>
        /// <returns>bool or null</returns>
        public static bool? TryParse(string value)
        {
            bool result;
            if (bool.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }
    }
}
