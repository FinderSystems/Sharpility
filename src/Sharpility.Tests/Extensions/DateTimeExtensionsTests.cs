using System;
using NFluent;
using NUnit.Framework;
using Sharpility.Extensions;

namespace Sharpility.Tests.Extensions
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void ShouldExtendDateTimeByToMillisecondsMethod()
        {
            // given
            var dateTime = new DateTime(2015, 6, 8, 11, 15, 0, DateTimeKind.Utc);

            // when
            var milliseconds = dateTime.ToMilliseconds();

            // then
            Check.That(milliseconds).IsEqualTo(1433762100000L);
        }
    }
}
