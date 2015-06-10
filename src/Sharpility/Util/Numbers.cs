
using System;
using System.Globalization;

namespace Sharpility.Util
{
    /// <summary>
    /// Utilities for numbers.
    /// </summary>
    public static class Numbers
    {
        /// <summary>
        /// Tries to parse short from string value.
        /// If value could not be parsed returns null.
        /// </summary>
        /// <param name="value">Parsed value</param>
        /// <returns>short or null</returns>
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

        /// <summary>
        /// Tries to parse int from string value.
        /// If value could not be parsed returns null.
        /// </summary>
        /// <param name="value">Parsed value</param>
        /// <returns>int or null</returns>
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

        /// <summary>
        /// Tries to parse long from string value.
        /// If value could not be parsed returns null.
        /// </summary>
        /// <param name="value">Parsed value</param>
        /// <returns>long or null</returns>
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

        /// <summary>
        /// Tries to parse double from string value.
        /// If value could not be parsed returns null.
        /// </summary>
        /// <param name="value">Parsed value</param>
        /// <param name="format">Double format, default: NumberFormatInfo.InvariantInfo</param>
        /// <returns>double or null</returns>
        public static double? TryParseDouble(string value, NumberFormatInfo format = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            const NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands;
            return TryParseDouble(value, style, format);
        }

        /// <summary>
        /// Tries to parse double from string value.
        /// If value could not be parsed returns null.
        /// </summary>
        /// <param name="value">Parsed value</param>
        /// <param name="style">Style of double value</param>
        /// <param name="format">Double format, default: NumberFormatInfo.InvariantInfo</param>
        /// <returns>double or null</returns>
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

        /// <summary>
        /// Tries to parse float from string value.
        /// If value could not be parsed returns null.
        /// </summary>
        /// <param name="value">Parsed value</param>
        /// <param name="format">Float format, default: NumberFormatInfo.InvariantInfo</param>
        /// <returns>float or null</returns>
        public static float? TryParseFloat(string value, NumberFormatInfo format = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            const NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands;
            return TryParseFloat(value, style, format);
        }

        /// <summary>
        /// Tries to parse float from string value.
        /// If value could not be parsed returns null.
        /// </summary>
        /// <param name="value">Parsed value</param>
        /// <param name="style">Style of float value</param>
        /// <param name="format">Double format, default: NumberFormatInfo.InvariantInfo</param>
        /// <returns>float or null</returns>
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

        /// <summary>
        /// Converts nullable decimal value to nullable double.
        /// </summary>
        /// <param name="value">decimal value</param>
        /// <returns>double value</returns>
        public static double? DecimalToDouble(decimal? value)
        {
            return value == null ? (double?) null : Convert.ToDouble(value);
        }

        /// <summary>
        /// Converts nullable double value to nullable decimal.
        /// </summary>
        /// <param name="value">double value</param>
        /// <returns>decimal value</returns>
        public static decimal? DoubleToDecimal(double? value)
        {
            return value == null ? (decimal?)null : Convert.ToDecimal(value);
        }
    }
}
