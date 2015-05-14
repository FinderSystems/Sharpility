using System;
using Sharpility.Base;
using Sharpility.Extensions;

namespace Sharpility.Time
{
    public sealed class DateRange
    {
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }

        private DateRange(DateTime from, DateTime to)
        {
            Precognitions.Evaluate(from <= to, 
                String.Format("Invalid date range {0} - {1}", from, to));
            From = from;
            To = to;
        }

        public static DateRange Of(DateTime from, DateTime to)
        {
            return new DateRange(from, to);
        }

        public static DateRange Of(string from, string to)
        {
            return Of(DateTime.Parse(from), DateTime.Parse(to));
        }

        public bool Contains(DateTime date)
        {
            return date >= From && date <= To;
        }

        public TimeSpan Duration
        {
            get { return TimeSpan.FromMilliseconds(To.ToMilliseconds() - From.ToMilliseconds()); }
        }

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
