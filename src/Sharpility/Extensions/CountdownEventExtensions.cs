using System;
using System.Threading;
using Sharpility.Base;

namespace Sharpility.Extensions
{
    /// <summary>
    /// Extensions for CountdownEvent
    /// </summary>
    public static class CountdownEventExtensions
    {
        /// <summary>
        /// Signals if current count is greater than zero.
        /// </summary>
        /// <param name="source">source</param>
        public static bool TrySignal(this CountdownEvent source)
        {
            Preconditions.IsNotNull(source, () => new NullReferenceException("source"));
            if (source.CurrentCount > 0)
            {
                try
                {
                    source.Signal();
                }
                catch (ObjectDisposedException)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
