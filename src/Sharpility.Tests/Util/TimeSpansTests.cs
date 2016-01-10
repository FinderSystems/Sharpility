using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class TimeSpansTests
    {
        [Test]
        [TestCaseSource("MaximumTimeSpanCases")]
        public TimeSpan ShouldReturnMaximumTimeSpan(TimeSpan first, TimeSpan second)
        {
            // when
            var max = TimeSpans.Max(first, second);

            // then
            return max;
        }

        private static IEnumerable<ITestCaseData> MaximumTimeSpanCases()
        {
            var first = TimeSpan.FromSeconds(5);
            var second = TimeSpan.FromSeconds(6);
            var exepected = second;
            yield return new TestCaseData(first, second)
            .SetName(string.Format("Should return {0} as max of {1}, {2}", exepected, first, second))
                .Returns(exepected);

            first = TimeSpan.FromSeconds(0);
            second = TimeSpan.FromSeconds(-1);
            exepected = first;
            yield return new TestCaseData(first, second)
            .SetName(string.Format("Should return {0} as max of {1}, {2}", exepected, first, second))
                .Returns(exepected);
        }

        [Test]
        [TestCaseSource("MinimumTimeSpanCases")]
        public TimeSpan ShouldReturnMinimumTimeSpan(TimeSpan first, TimeSpan second)
        {
            // when
            var min = TimeSpans.Min(first, second);

            // then
            return min;
        }

        private static IEnumerable<ITestCaseData> MinimumTimeSpanCases()
        {
            var first = TimeSpan.FromSeconds(5);
            var second = TimeSpan.FromSeconds(6);
            var exepected = first;
            yield return new TestCaseData(first, second)
            .SetName(string.Format("Should return {0} as min of {1}, {2}", exepected, first, second))
                .Returns(exepected);

            first = TimeSpan.FromSeconds(0);
            second = TimeSpan.FromSeconds(-1);
            exepected = second;
            yield return new TestCaseData(first, second)
            .SetName(string.Format("Should return {0} as min of {1}, {2}", exepected, first, second))
                .Returns(exepected);
        }

        [Test]
        [TestCaseSource("AbsoluateTimeSpanCases")]
        public TimeSpan ShouldReturnAbsoluteTimeSpan(TimeSpan timespan)
        {
            // when
            var abs = TimeSpans.Abs(timespan);

            // then
            return abs;
        }

        private static IEnumerable<ITestCaseData> AbsoluateTimeSpanCases()
        {
            var given = TimeSpan.Zero;
            var expected = given;
            yield return new TestCaseData(given)
                .SetName(string.Format("Abs({0}) should return {1}", given, expected))
                .Returns(expected);

            given = TimeSpan.FromMilliseconds(1);
            expected = given;
            yield return new TestCaseData(given)
                .SetName(string.Format("Abs({0}) should return {1}", given, expected))
                .Returns(expected);

            given = TimeSpan.FromMinutes(15).Negate();
            expected = TimeSpan.FromMinutes(15);
            yield return new TestCaseData(given)
                .SetName(string.Format("Abs({0}) should return {1}", given, expected))
                .Returns(expected);
        }
    }
}
