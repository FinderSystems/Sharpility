using System;
using Sharpility.Base;

namespace Sharpility.Extensions
{
    /// <summary>
    /// Extensions of DateTime stuct.
    /// </summary>
    public static class DateTimeExtensions
    {
        private static readonly DateTime StartOfEra = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Returns number of milliseconds from 1970-01-01
        /// </summary>
        /// <param name="source">this</param>
        /// <returns>number of milliseconds</returns>
        public static long ToMilliseconds(this DateTime source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return (source - StartOfEra).Ticks / TimeSpan.TicksPerMillisecond;
        }

        /// <summary>
        /// Checks is source DateTime is after given DateTime.
        /// Supports Local and UTC kind comparation.
        /// </summary>
        /// <param name="source">Source DateTime</param>
        /// <param name="dateTime">DateTime compared to</param>
        /// <returns></returns>
        public static bool IsAfter(this DateTime source, DateTime dateTime)
        {
            if (IsUtcConvertable(source) && IsUtcConvertable(dateTime))
            {
                return source.ToUniversalTime() > dateTime.ToUniversalTime();
            }
            return source > dateTime;
        }

        /// <summary>
        /// Checks is source DateTime is after or equal given DateTime.
        /// Supports Local and UTC kind comparation.
        /// </summary>
        /// <param name="source">Source DateTime</param>
        /// <param name="dateTime">DateTime compared to</param>
        /// <returns></returns>
        public static bool IsAfterOrEquals(this DateTime source, DateTime dateTime)
        {
            if (IsUtcConvertable(source) && IsUtcConvertable(dateTime))
            {
                return source.ToUniversalTime() >= dateTime.ToUniversalTime();
            }
            return source >= dateTime;
        }

        /// <summary>
        /// Checks is source DateTime is before given DateTime.
        /// Supports Local and UTC kind comparation.
        /// </summary>
        /// <param name="source">Source DateTime</param>
        /// <param name="dateTime">DateTime compared to</param>
        /// <returns></returns>
        public static bool IsBefore(this DateTime source, DateTime dateTime)
        {
            if (IsUtcConvertable(source) && IsUtcConvertable(dateTime))
            {
                return source.ToUniversalTime() < dateTime.ToUniversalTime();
            }
            return source < dateTime;
        }

        /// <summary>
        /// Checks is source DateTime is before or equal given DateTime.
        /// Supports Local and UTC kind comparation.
        /// </summary>
        /// <param name="source">Source DateTime</param>
        /// <param name="dateTime">DateTime compared to</param>
        /// <returns></returns>
        public static bool IsBeforeOrEquals(this DateTime source, DateTime dateTime)
        {
            if (IsUtcConvertable(source) && IsUtcConvertable(dateTime))
            {
                return source.ToUniversalTime() <= dateTime.ToUniversalTime();
            }
            return source <= dateTime;
        }

        /// <summary>
        /// Checks is source DateTime is equal given DateTime.
        /// Supports Local and UTC kind comparation.
        /// </summary>
        /// <param name="source">Source DateTime</param>
        /// <param name="dateTime">DateTime compared to</param>
        /// <returns></returns>
        public static bool IsEqualsTo(this DateTime source, DateTime dateTime)
        {
            if (IsUtcConvertable(source) && IsUtcConvertable(dateTime))
            {
                return source.ToUniversalTime() == dateTime.ToUniversalTime();
            }
            return source == dateTime;
        }

        private static bool IsUtcConvertable(DateTime dateTime)
        {
            switch (dateTime.Kind)
            {
                case DateTimeKind.Utc:
                case DateTimeKind.Local:
                    return true;
                default:
                    return false;
            }
        }
    }
}
