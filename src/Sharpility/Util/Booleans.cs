
namespace Sharpility.Util
{
    /// <summary>
    /// Utils for bools.
    /// </summary>
    public static class Booleans
    {
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
