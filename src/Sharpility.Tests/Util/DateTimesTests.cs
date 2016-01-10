using System;
using System.Collections.Generic;
using NFluent;
using NUnit.Framework;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class DateTimesTests
    {
        [Test]
        public void ShouldCreateDateTimeFromMilliseconds()
        {
            // given
            const long milliseconds = 1440426600000L;

            // when
            var dateTime = DateTimes.FromMilliseconds(milliseconds);

            // then
            Check.That(dateTime).IsEqualTo(new DateTime(2015, 8, 24, 14, 30, 0, DateTimeKind.Utc));
        }

        [Test]
        [TestCaseSource("LatestDateTimeCases")]
        public DateTime ShouldReturnLatestDateTime(DateTime first, DateTime second)
        {
            // when
            var latest = DateTimes.Latest(first, second);

            // then
            return latest;
        }

        private static IEnumerable<ITestCaseData> LatestDateTimeCases()
        {
            var first = new DateTime(2015, 9, 21, 10, 0, 0, DateTimeKind.Utc);
            var second = new DateTime(2015, 9, 21, 10, 0, 1, DateTimeKind.Utc);
            var exepected = second;
            yield return new TestCaseData(first, second)
            .SetName(string.Format("Should return {0} as max of {1}, {2}", exepected, first, second))
                .Returns(exepected);

            first = new DateTime(2015, 9, 21, 11, 0, 0, DateTimeKind.Utc);
            second = new DateTime(2015, 9, 21, 10, 0, 0, DateTimeKind.Utc);
            exepected = first;
            yield return new TestCaseData(first, second)
            .SetName(string.Format("Should return {0} as max of {1}, {2}", exepected, first, second))
                .Returns(exepected);
        }

        [Test]
        [TestCaseSource("EarliestDateTimeCases")]
        public DateTime ShouldReturnEarliestDateTime(DateTime first, DateTime second)
        {
            // when
            var earliest = DateTimes.Earliest(first, second);

            // then
            return earliest;
        }

        private static IEnumerable<ITestCaseData> EarliestDateTimeCases()
        {
            var first = new DateTime(2015, 9, 21, 10, 0, 0, DateTimeKind.Utc);
            var second = new DateTime(2015, 9, 21, 10, 0, 1, DateTimeKind.Utc);
            var exepected = first;
            yield return new TestCaseData(first, second)
            .SetName(String.Format("Should return {0} as min of {1}, {2}", exepected, first, second))
                .Returns(exepected);

            first = new DateTime(2015, 9, 21, 11, 0, 0, DateTimeKind.Utc);
            second = new DateTime(2015, 9, 21, 10, 0, 0, DateTimeKind.Utc);
            exepected = second;
            yield return new TestCaseData(first, second)
            .SetName(String.Format("Should return {0} as min of {1}, {2}", exepected, first, second))
                .Returns(exepected);
        }
    }
}
