using System;

namespace Sharpility.Time
{
    public interface Clock
    {
        DateTime CurrentTime { get; }
    }
}
