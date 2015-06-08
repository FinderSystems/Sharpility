using System;

namespace Sharpility.Time
{
    /// <summary>
    /// Provides current time.
    /// </summary>
    public interface Clock
    {
        /// <summary>
        /// Returns current time.
        /// </summary>
        DateTime CurrentTime { get; }
    }
}
