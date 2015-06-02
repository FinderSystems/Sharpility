using System;

namespace Sharpility.Time
{
    /// <summary>
    /// Clock with fixed time.
    /// </summary>
    public sealed class FixedClock: Clock
    {
        private DateTime Time { get; set; }

        private FixedClock(DateTime time)
        {
            Time = time;
        }

        /// <summary>
        /// Create new clock instance at given time.
        /// </summary>
        /// <param name="time">time</param>
        /// <returns>clock</returns>
        public static FixedClock At(DateTime time)
        {
            return new FixedClock(time);
        }

        /// <summary>
        /// Create new clock instance at current time.
        /// </summary>
        /// <returns></returns>
        public static FixedClock AtThisInstance() 
        {
            return At(DateTime.Now);
        }

        /// <summary>
        /// Sets clock time.
        /// </summary>
        /// <param name="time"></param>
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
