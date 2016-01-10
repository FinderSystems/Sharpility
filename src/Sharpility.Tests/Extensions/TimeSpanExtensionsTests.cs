using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sharpility.Extensions;
using Sharpility.Time;

namespace Sharpility.Tests.Extensions
{
    [TestFixture]
    public class TimeSpanExtensionsTests
    {
        [Test]
        [TestCaseSource("TimeSpanTruncateToCases")]
        public TimeSpan ShouldTruncateTimeSpanTo(TimeSpan timespan, TimeUnit unit)
        {
            // when
            var result = timespan.TruncateTo(unit);

            // then
            return result;
        }

        private static IEnumerable<ITestCaseData> TimeSpanTruncateToCases()
        {
            var timespan = TimeSpan.FromDays(1) +
                           TimeSpan.FromHours(12) +
                           TimeSpan.FromMinutes(30) +
                           TimeSpan.FromSeconds(33) +
                           TimeSpan.FromMilliseconds(111);
            var unit = TimeUnit.Millisecond;
            var expected = timespan;
            yield return new TestCaseData(timespan, unit)
                .SetName(String.Format("{0} truncated to {1} should return {2}", timespan, unit, expected))
                .Returns(expected);

            unit = TimeUnit.Second;
            expected = TimeSpan.FromDays(1) +
                       TimeSpan.FromHours(12) +
                       TimeSpan.FromMinutes(30) +
                       TimeSpan.FromSeconds(33);
            yield return new TestCaseData(timespan, unit)
                .SetName(String.Format("{0} truncated to {1} should return {2}", timespan, unit, expected))
                .Returns(expected);

            unit = TimeUnit.Minute;
            expected = TimeSpan.FromDays(1) +
                       TimeSpan.FromHours(12) +
                       TimeSpan.FromMinutes(30);
            yield return new TestCaseData(timespan, unit)
                .SetName(String.Format("{0} truncated to {1} should return {2}", timespan, unit, expected))
                .Returns(expected);

            unit = TimeUnit.Hour;
            expected = TimeSpan.FromDays(1) +
                       TimeSpan.FromHours(12);
            yield return new TestCaseData(timespan, unit)
                .SetName(String.Format("{0} truncated to {1} should return {2}", timespan, unit, expected))
                .Returns(expected);

            unit = TimeUnit.Day;
            expected = TimeSpan.FromDays(1);
            yield return new TestCaseData(timespan, unit)
                .SetName(String.Format("{0} truncated to {1} should return {2}", timespan, unit, expected))
                .Returns(expected);
        }

        [Test]
        [TestCaseSource("TimeSpanToMillisCases")]
        public long ShouldConvertTimeSpanToMilliseconds(TimeSpan timespan)
        {
            // when
            var millis = timespan.ToMillis();

            // then
            return millis;
        }

        private static IEnumerable<ITestCaseData> TimeSpanToMillisCases()
        {
            var given = TimeSpan.FromMilliseconds(1500);
            var expected = 1500L;
            yield return new TestCaseData(given)
                .SetName(string.Format("{0} to millis should return {1}", given, expected))
                .Returns(expected);

            given = TimeSpan.FromMilliseconds(50.400D);
            expected = 50L;
            yield return new TestCaseData(given)
                .SetName(string.Format("{0} to millis should return {1}", given, expected))
                .Returns(expected);

            given = TimeSpan.Zero;
            expected = 0L;
            yield return new TestCaseData(given)
                .SetName(string.Format("{0} to millis should return {1}", given, expected))
                .Returns(expected);

            given = TimeSpan.FromMinutes(1).Negate();
            expected = -60000L;
            yield return new TestCaseData(given)
                .SetName(string.Format("{0} to millis should return {1}", given, expected))
                .Returns(expected);
        }

        [Test]
        [TestCaseSource("TimeSpanToSecondsCases")]
        public long ShouldConvertTimeSpanToSeconds(TimeSpan timespan)
        {
            // when
            var seconds = timespan.ToSeconds();

            // then
            return seconds;
        }

        private static IEnumerable<ITestCaseData> TimeSpanToSecondsCases()
        {
            var given = TimeSpan.FromSeconds(50);
            var expected = 50;
            yield return new TestCaseData(given)
                .SetName(string.Format("{0} to millis should return {1}", given, expected))
                .Returns(expected);

            given = TimeSpan.FromSeconds(50) + TimeSpan.FromMilliseconds(550);
            expected = 50;
            yield return new TestCaseData(given)
                .SetName(string.Format("{0} to millis should return {1}", given, expected))
                .Returns(expected);

            given = TimeSpan.Zero;
            expected = 0;
            yield return new TestCaseData(given)
                .SetName(string.Format("{0} to millis should return {1}", given, expected))
                .Returns(expected);

            given = TimeSpan.FromMinutes(1).Negate();
            expected = -60;
            yield return new TestCaseData(given)
                .SetName(string.Format("{0} to millis should return {1}", given, expected))
                .Returns(expected);
        }

        [Test]
        [TestCaseSource("TimeSpanToMinutesCases")]
        public long ShouldConvertTimeSpanToMinutes(TimeSpan timespan)
        {
            // when
            var minutes = timespan.ToMinutes();

            // then
            return minutes;
        }

        private static IEnumerable<ITestCaseData> TimeSpanToMinutesCases()
        {
            var given = TimeSpan.FromMinutes(70);
            var expected = 70;
            yield return new TestCaseData(given)
                .SetName(string.Format("{0} to millis should return {1}", given, expected))
                .Returns(expected);

            given = TimeSpan.Parse("01:25:33") + TimeSpan.FromMilliseconds(131);
            expected = 85;
            yield return new TestCaseData(given)
                .SetName(string.Format("{0} to millis should return {1}", given, expected))
                .Returns(expected);

            given = TimeSpan.Zero;
            expected = 0;
            yield return new TestCaseData(given)
                .SetName(string.Format("{0} to millis should return {1}", given, expected))
                .Returns(expected);

            given = TimeSpan.FromMinutes(1).Negate();
            expected = -1;
            yield return new TestCaseData(given)
                .SetName(string.Format("{0} to millis should return {1}", given, expected))
                .Returns(expected);
        }

        [Test]
        [TestCaseSource("TimeSpanToHoursCases")]
        public long ShouldConvertTimeSpanToHours(TimeSpan timespan)
        {
            // when
            var hours = timespan.ToHours();

            // then
            return hours;
        }

        private static IEnumerable<ITestCaseData> TimeSpanToHoursCases()
        {
            var given = TimeSpan.FromMinutes(59);
            var expected = 0;
            yield return new TestCaseData(given)
                .SetName(string.Format("{0} to millis should return {1}", given, expected))
                .Returns(expected);

            given = TimeSpan.Parse("01:25:33") + TimeSpan.FromMilliseconds(131);
            expected = 1;
            yield return new TestCaseData(given)
                .SetName(string.Format("{0} to millis should return {1}", given, expected))
                .Returns(expected);

            given = TimeSpan.Zero;
            expected = 0;
            yield return new TestCaseData(given)
                .SetName(string.Format("{0} to millis should return {1}", given, expected))
                .Returns(expected);

            given = TimeSpan.FromHours(15).Negate();
            expected = -15;
            yield return new TestCaseData(given)
                .SetName(string.Format("{0} to millis should return {1}", given, expected))
                .Returns(expected);
        }
    }
}
