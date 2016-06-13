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

        [Test]
        public void ShouldConvertDateTimeToDate()
        {
            // given
            var dateTime = new DateTime(2015, 12, 30, 11, 30, 15, DateTimeKind.Utc);

            // when
            var date = dateTime.ToDate();

            // then
            Check.That(date).IsEqualTo(new DateTime(2015, 12, 30, 0, 0, 0, DateTimeKind.Utc));
        }

        [Test]
        public void ShouldSubstractDaysFromDateTime()
        {
            // given
            var dateTime = new DateTime(2015, 12, 30, 11, 30, 15, DateTimeKind.Utc);

            // when
            var results = dateTime.MinusDays(7);

            // then
            Check.That(results).IsEqualTo(new DateTime(2015, 12, 23, 11, 30, 15, DateTimeKind.Utc));
        }

        [Test]
        public void ShouldAddDaysToDateTime()
        {
            // given
            var dateTime = new DateTime(2015, 12, 30, 11, 30, 15, DateTimeKind.Utc);

            // when
            var results = dateTime.PlusDays(7);

            // then
            Check.That(results).IsEqualTo(new DateTime(2016, 1, 6, 11, 30, 15, DateTimeKind.Utc));
        }
    }
}
