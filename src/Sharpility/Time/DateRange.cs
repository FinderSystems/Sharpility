using System;
using Sharpility.Base;
using Sharpility.Extensions;

namespace Sharpility.Time
{
    /// <summary>
    /// Date interval between two dateTimes.
    /// </summary>
    public sealed class DateRange
    {
        /// <summary>
        /// DateTime From
        /// </summary>
        public DateTime From { get; private set; }

        /// <summary>
        /// DateTime To
        /// </summary>
        public DateTime To { get; private set; }

        private DateRange(DateTime from, DateTime to)
        {
            Preconditions.Evaluate(from <= to, 
                String.Format("Invalid date range {0} - {1}", from, to));
            From = from;
            To = to;
        }

        /// <summary>
        /// Create new DateRange instance.
        /// </summary>
        /// <param name="from">DateTime From</param>
        /// <param name="to">DateTiem To</param>
        /// <returns>DateRange</returns>
        public static DateRange Of(DateTime from, DateTime to)
        {
            return new DateRange(from, to);
        }

        /// <summary>
        /// Create new DateRange instance from.
        /// </summary>
        /// <param name="from">DateTime From parsed using DateTime Parse</param>
        /// <param name="to">DateTime From parsed using DateTime Parse</param>
        /// <param name="format">Date time format</param>
        /// <returns>DateRange</returns>
        public static DateRange Of(string from, string to, IFormatProvider format = null)
        {
            return format != null ? Of(DateTime.Parse(from, format), DateTime.Parse(to, format)) : 
                Of(DateTime.Parse(from), DateTime.Parse(to));
        }

        /// <summary>
        /// Checks is date is between date range interval.
        /// </summary>
        /// <param name="date">checked date</param>
        /// <returns>true if date is between date range interval</returns>
        public bool Contains(DateTime date)
        {
            return date >= From && date <= To;
        }

        /// <summary>
        /// Returns date range duration.
        /// </summary>
        public TimeSpan Duration
        {
            get { return TimeSpan.FromMilliseconds(To.ToMilliseconds() - From.ToMilliseconds()); }
        }

        /// <summary>
        /// Converts date range to two elements array { From, To}.
        /// </summary>
        /// <returns></returns>
        public DateTime[] ToArray()
        {
            return new[] { From, To };
        }

        public override bool Equals(object obj)
        {
            return this.EqualsByProperties(obj);
        }

        public override int GetHashCode()
        {
            return this.PropertiesHash();
        }

        public override string ToString()
        {
            return this.PropertiesToString();
        }
    }
}
