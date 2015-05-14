using System;

namespace Sharpility.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime StartOfEra = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ToMilliseconds(this DateTime dateTime)
        {
            return (dateTime - StartOfEra).Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}
