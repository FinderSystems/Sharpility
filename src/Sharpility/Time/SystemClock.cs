using System;
namespace Sharpility.Time
{
    public sealed class SystemClock: Clock
    {
        private SystemClock()
        {
        }

        public static Clock Create()
        {
            return new SystemClock();
        }

        public DateTime CurrentTime()
        {
            return DateTime.Now;
        }
    }
}
