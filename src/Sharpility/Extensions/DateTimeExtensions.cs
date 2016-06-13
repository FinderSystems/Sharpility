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

        /// <summary>
        /// Converts DateTime to Date with 00:00:00 time.
        /// </summary>
        /// <param name="source">this</param>
        /// <returns>Date</returns>
        public static DateTime ToDate(this DateTime source)
        {
            Preconditions.IsNotNull(source, () => new NullReferenceException("source"));
            return new DateTime(source.Year, source.Month, source.Day, 0, 0, 0, source.Kind);
        }

        /// <summary>
        /// Returns DateTime instance with days instance addition to it.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="days">Number of days added</param>
        /// <returns>DateTime with days added</returns>
        public static DateTime PlusDays(this DateTime source, uint days)
        {
            Preconditions.IsNotNull(source, () => new NullReferenceException("source"));
            return source.AddDays(days);
        }

        /// <summary>
        /// Returns DateTime instance with days instance substracted from it.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="days">Number of days substracted</param>
        /// <returns>DateTime with substracted days</returns>
        public static DateTime MinusDays(this DateTime source, uint days)
        {
            Preconditions.IsNotNull(source, () => new NullReferenceException("source"));
            return source.AddDays(-(int)days);
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
