
namespace Sharpility.Util
{
    public static class Numbers
    {
        public static short? TryParseShort(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            short result;
            if (short.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static int? TryParseInt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static long? TryParseLong(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            long result;
            if (long.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static double? TryParseDouble(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            double result;
            if (double.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static float? TryParseFloat(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            float result;
            if (float.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }
    }
}
