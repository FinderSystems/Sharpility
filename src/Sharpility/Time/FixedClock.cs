using System;

namespace Sharpility.Time
{
    public sealed class FixedClock: Clock
    {
        private DateTime Time { get; set; }

        private FixedClock(DateTime time)
        {
            Time = time;
        }

        public static FixedClock At(DateTime time)
        {
            return new FixedClock(time);
        }

        public static FixedClock AtThisInstance() 
        {
            return At(DateTime.Now);
        }

        public void Set(DateTime time)
        {
            Time = time;
        }

        public DateTime CurrentTime
        {
            get { return Time; }
        }
    }
}
