using System;
using Sharpility.Time;

namespace Sharpility.Extensions
{
    /// <summary>
    /// Extensions for TimeSpan.
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Truncate TimeSpan to given unit.
        /// </summary>
        /// <param name="timespan">this</param>
        /// <param name="unit">Time unit</param>
        /// <returns>Truncated TimeSPan</returns>
        public static TimeSpan TruncateTo(this TimeSpan timespan, TimeUnit unit)
        {
            TimeSpan truncated;
            switch (unit)
            {
                case TimeUnit.Second:
                    truncated = TimeSpan.FromMilliseconds(timespan.Milliseconds);
                    return timespan - truncated;
                case TimeUnit.Minute:
                    truncated = TimeSpan.FromSeconds(timespan.Seconds) +
                        TimeSpan.FromMilliseconds(timespan.Milliseconds);
                    return timespan - truncated;
                case TimeUnit.Hour:
                    truncated =
                        TimeSpan.FromMinutes(timespan.Minutes) +
                        TimeSpan.FromSeconds(timespan.Seconds) +
                        TimeSpan.FromMilliseconds(timespan.Milliseconds);
                    return timespan - truncated;
                case TimeUnit.Day:
                    truncated =
                        TimeSpan.FromHours(timespan.Hours) +
                         TimeSpan.FromMinutes(timespan.Minutes) +
                         TimeSpan.FromSeconds(timespan.Seconds) +
                         TimeSpan.FromMilliseconds(timespan.Milliseconds);
                    return timespan - truncated;
                default:
                    return timespan;
            }
        }

        /// <summary>
        /// Converts TimeSpan to milliseconds.
        /// </summary>
        /// <param name="timespan">this</param>
        /// <returns>Total number of full milliseconds</returns>
        public static long ToMillis(this TimeSpan timespan)
        {
            return (long)timespan.TotalMilliseconds;
        }

        /// <summary>
        /// Converts TimeSpan to seconds.
        /// </summary>
        /// <param name="timespan">this</param>
        /// <returns>Total number of full seconds</returns>
        public static int ToSeconds(this TimeSpan timespan)
        {
            return (int)timespan.TotalSeconds;
        }

        /// <summary>
        /// Converts TimeSpan to minutes.
        /// </summary>
        /// <param name="timespan">this</param>
        /// <returns>Total number of full minutes</returns>
        public static int ToMinutes(this TimeSpan timespan)
        {
            return (int)timespan.TotalMinutes;
        }

        /// <summary>
        /// Converts TimeSpan to hours.
        /// </summary>
        /// <param name="timespan">this</param>
        /// <returns>Total number of full hours</returns>
        public static int ToHours(this TimeSpan timespan)
        {
            return (int)timespan.TotalHours;
        }
    }
}
