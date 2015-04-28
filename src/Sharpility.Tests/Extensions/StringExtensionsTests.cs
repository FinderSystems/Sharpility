
using System.Collections.Generic;
using NUnit.Framework;
using Sharpility.Extensions;

namespace Sharpility.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test, TestCaseSource("RepeatStringTestCases")]
        public string ShouldRepeatString(string value, int times)
        {
            // when
            var result = value.Repeat(times);
            
            // then
            return result;
        }

        private static IEnumerable<ITestCaseData> RepeatStringTestCases()
        {
            yield return new TestCaseData("X", 5)
                .SetName("Should repeat 'X' five times")
                .Returns("XXXXX");

            yield return new TestCaseData("test", 2)
                .SetName("Should repeat 'test' two times")
                .Returns("testtest");

            yield return new TestCaseData("y", 0)
                .SetName("Should return empty string zero reps")
                .Returns("");

            yield return new TestCaseData("abc", -1)
               .SetName("Should return empty string on nagative number of reps")
               .Returns("");
        }
    }
}
