using System;
using Sharpility.Base;
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
        /// <param name="source">this</param>
        /// <param name="unit">Time unit</param>
        /// <returns>Truncated TimeSPan</returns>
        public static TimeSpan TruncateTo(this TimeSpan source, TimeUnit unit)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            TimeSpan truncated;
            switch (unit)
            {
                case TimeUnit.Second:
                    truncated = TimeSpan.FromMilliseconds(source.Milliseconds);
                    return source - truncated;
                case TimeUnit.Minute:
                    truncated = TimeSpan.FromSeconds(source.Seconds) +
                        TimeSpan.FromMilliseconds(source.Milliseconds);
                    return source - truncated;
                case TimeUnit.Hour:
                    truncated =
                        TimeSpan.FromMinutes(source.Minutes) +
                        TimeSpan.FromSeconds(source.Seconds) +
                        TimeSpan.FromMilliseconds(source.Milliseconds);
                    return source - truncated;
                case TimeUnit.Day:
                    truncated =
                        TimeSpan.FromHours(source.Hours) +
                         TimeSpan.FromMinutes(source.Minutes) +
                         TimeSpan.FromSeconds(source.Seconds) +
                         TimeSpan.FromMilliseconds(source.Milliseconds);
                    return source - truncated;
                default:
                    return source;
            }
        }

        /// <summary>
        /// Converts TimeSpan to milliseconds.
        /// </summary>
        /// <param name="source">this</param>
        /// <returns>Total number of full milliseconds</returns>
        public static long ToMillis(this TimeSpan source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return (long)source.TotalMilliseconds;
        }

        /// <summary>
        /// Converts TimeSpan to seconds.
        /// </summary>
        /// <param name="source">this</param>
        /// <returns>Total number of full seconds</returns>
        public static int ToSeconds(this TimeSpan source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return (int)source.TotalSeconds;
        }

        /// <summary>
        /// Converts TimeSpan to minutes.
        /// </summary>
        /// <param name="source">this</param>
        /// <returns>Total number of full minutes</returns>
        public static int ToMinutes(this TimeSpan source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return (int)source.TotalMinutes;
        }

        /// <summary>
        /// Converts TimeSpan to hours.
        /// </summary>
        /// <param name="source">this</param>
        /// <returns>Total number of full hours</returns>
        public static int ToHours(this TimeSpan source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return (int)source.TotalHours;
        }
    }
}
