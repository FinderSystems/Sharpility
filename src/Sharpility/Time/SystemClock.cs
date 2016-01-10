using System;
namespace Sharpility.Time
{
    /// <summary>
    /// Clock implementations using DataTime.Now for currentTime.
    /// </summary>
    public sealed class SystemClock: Clock
    {
        private SystemClock()
        {
        }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <returns></returns>
        public static Clock Create()
        {
            return new SystemClock();
        }

        /// <summary>
        /// UTC system clock.
        /// </summary>
        public static Clock UTC
        {
            get { return new UtcSystemClock();}
        }

        public DateTime CurrentTime
        {
            get { return DateTime.Now; }
        }

        private class UtcSystemClock : Clock
        {
            public DateTime CurrentTime
            {
                get { return DateTime.UtcNow; }
            }
        }
    }
}
