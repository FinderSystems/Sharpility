using System;

namespace Sharpility.Util
{
    /// <summary>
    /// Utilities for DateTime.
    /// </summary>
    public static class DateTimes
    {
        /// <summary>
        /// Created date time from millisconds since year 1970 in UTC zone.
        /// </summary>
        /// <param name="milliseconds">Number of millisconds since year 1970</param>
        /// <returns>DateTime</returns>
        public static DateTime FromMilliseconds(long milliseconds)
        {
            var result = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return result.AddMilliseconds(milliseconds);
        }

        /// <summary>
        /// Returns latest date.
        /// </summary>
        /// <param name="first">First of compared dates</param>
        /// <param name="second">Second of compared dates </param>
        /// <returns>Latest date</returns>
        public static DateTime Latest(DateTime first, DateTime second)
        {
            return first > second ? first : second;
        }

        /// <summary>
        /// Returns earliest date.
        /// </summary>
        /// <param name="first">First of compared dates</param>
        /// <param name="second">Second of compared dates </param>
        /// <returns>Earliest date</returns>
        public static DateTime Earliest(DateTime first, DateTime second)
        {
            return first < second ? first : second;
        }
    }
}
