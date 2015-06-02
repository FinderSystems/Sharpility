using System;

namespace Sharpility.Time
{
    public interface Clock
    {
        /// <summary>
        /// Returns current time.
        /// </summary>
        DateTime CurrentTime { get; }
    }
}
