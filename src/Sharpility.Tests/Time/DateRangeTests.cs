using System;
using System.Collections.Generic;
using NFluent;
using NiceTry;
using NUnit.Framework;
using Sharpility.Time;

namespace Sharpility.Tests.Time
{
    [TestFixture]
    public class DateRangeTests
    {
        [Test, TestCaseSource("DateRangeContainsTestCases")]
        public bool ShouldReturnIsDateRangeContainsDateTime(DateRange dateRange, DateTime dateTime)
        {
            // when
            var contains = dateRange.Contains(dateTime);

            // then
            return contains;
        }

        private static IEnumerable<ITestCaseData> DateRangeContainsTestCases()
        {
            var dateRange = DateRange.Of("2015-06-01 00:00:00", "2015-06-02 15:00:00");

            var dateTime = new DateTime(2015, 6, 1, 0, 0, 0);
            yield return new TestCaseData(dateRange, dateTime)
                .SetName(String.Format("Should return that {0} contains {1}", dateRange, dateTime))
                .Returns(true);

            dateTime = new DateTime(2015, 6, 2, 0, 0, 0);
            yield return new TestCaseData(dateRange, dateTime)
                .SetName(String.Format("Should return that {0} contains {1}", dateRange, dateTime))
                .Returns(true);

            dateTime = new DateTime(2015, 6, 2, 15, 0, 0);
            yield return new TestCaseData(dateRange, dateTime)
                .SetName(String.Format("Should return that {0} contains {1}", dateRange, dateTime))
                .Returns(true);

            dateTime = new DateTime(2015, 5, 31, 23, 59, 59);
            yield return new TestCaseData(dateRange, dateTime)
                .SetName(String.Format("Should return that {0} not contains {1}", dateRange, dateTime))
                .Returns(false);

            dateTime = new DateTime(2015, 6, 2, 15, 0, 1);
            yield return new TestCaseData(dateRange, dateTime)
                .SetName(String.Format("Should return that {0} not contains {1}", dateRange, dateTime))
                .Returns(false);
        }

        [Test, TestCaseSource("DateRangeDurationTestCases")]
        public TimeSpan ShouldReturnDateRangeDuration(DateRange dateRange)
        {
            // when
            var duration = dateRange.Duration;

            // then
            return duration;
        }

        private static IEnumerable<ITestCaseData> DateRangeDurationTestCases()
        {
            var dateRange = DateRange.Of("2015-06-08 00:00:00", "2015-06-08 00:00:01");
            yield return new TestCaseData(dateRange)
                .SetName(String.Format("Should return duration of {0}", dateRange))
                .Returns(TimeSpan.FromSeconds(1));

            dateRange = DateRange.Of("2015-06-08 00:00:00", "2015-06-08 00:00:00");
            yield return new TestCaseData(dateRange)
                .SetName(String.Format("Should return duration of {0}", dateRange))
                .Returns(TimeSpan.FromMilliseconds(0));

            dateRange = DateRange.Of("2015-06-08 00:00:00", "2015-06-09 00:00:00");
            yield return new TestCaseData(dateRange)
                .SetName(String.Format("Should return duration of {0}", dateRange))
                .Returns(TimeSpan.FromDays(1));
        }

        [Test]
        public void ShouldThrowArgumentExceptionOnInvalidDateRangeCreation()
        {
            // given
            var from = new DateTime(2015, 6, 2, 0, 0, 0);
            var to = new DateTime(2015, 6, 1, 23, 59, 59);

            // when
            var result = Try.To(() => DateRange.Of(from, to));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
        }
    }
}
