using System;

namespace Sharpility.Util
{
    /// <summary>
    /// Utilities for time span.
    /// </summary>
    public static class TimeSpans
    {
        /// <summary>
        /// Returns maximum TimeSpan.
        /// </summary>
        /// <param name="first">First of compared time spans</param>
        /// <param name="second">Second of compared time spans</param>
        /// <returns>Maxmimum TimeSpan</returns>
        public static TimeSpan Max(TimeSpan first, TimeSpan second)
        {
            return first > second ? first : second;
        }

        /// <summary>
        /// Returns minimum TimeSpan.
        /// </summary>
        /// <param name="first">First of compared time spans</param>
        /// <param name="second">Second of compared time spans</param>
        /// <returns>Minimum TimeSpan</returns>
        public static TimeSpan Min(TimeSpan first, TimeSpan second)
        {
            return first < second ? first : second;
        }

        /// <summary>
        /// Returns absolute TimeSpan.
        /// </summary>
        /// <param name="timespan">TimeSpan</param>
        /// <returns>Absolute TimeSpan value</returns>
        public static TimeSpan Abs(TimeSpan timespan)
        {
            return timespan < TimeSpan.Zero ? timespan.Negate() : timespan;
        }
    }
}
