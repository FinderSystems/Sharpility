
using System.Globalization;

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

        public static double? TryParseDouble(string value, NumberFormatInfo format = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            const NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands;
            return TryParseDouble(value, style, format);
        }

        public static double? TryParseDouble(string value, NumberStyles style, NumberFormatInfo format = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            double result;
            if (double.TryParse(value, style, format ?? NumberFormatInfo.InvariantInfo, out result))
            {
                return result;
            }
            return null;
        }

        public static float? TryParseFloat(string value, NumberFormatInfo format = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            const NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands;
            return TryParseFloat(value, style, format);
        }

        public static float? TryParseFloat(string value, NumberStyles style, NumberFormatInfo format = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            float result;
            if (float.TryParse(value, style, format ?? NumberFormatInfo.InvariantInfo, out result))
            {
                return result;
            }
            return null;
        }
    }
}
