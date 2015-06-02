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

        public DateTime CurrentTime
        {
            get { return DateTime.Now; }
        }
    }
}
