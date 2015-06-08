using System.Collections.Generic;
using NUnit.Framework;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class StringsTests
    {
        [Test, TestCaseSource("LimitedStringTestCases")]
        public string ShouldReturnLimitedString(string value, int length)
        {
            // when
            var result = Strings.LimitedString(value, length);

            // then
            return result;
        }

        private static IEnumerable<ITestCaseData> LimitedStringTestCases()
        {
            yield return new TestCaseData(null, 100)
                .SetName("Should return null on null string limited to 100 characters")
                .Returns(null);

            yield return new TestCaseData("alice has a cat", 5)
                .SetName("Should return string limited to 5 characters")
                .Returns("alice");

            yield return new TestCaseData("alice has a cat", 1000)
                .SetName("Should return same string when characets limit is greater than string length")
                .Returns("alice has a cat");
        }

        [Test, TestCaseSource("StringLengthTestCases")]
        public int ShouldReturnStringLength(string value)
        {
            // when
            var length = Strings.Length(value);

            // then
            return length;
        }

        private static IEnumerable<ITestCaseData> StringLengthTestCases()
        {
            yield return new TestCaseData("test")
                .SetName("Should return string length")
                .Returns(4);

            yield return new TestCaseData(null)
                .SetName("Should return 0 length for null string")
                .Returns(0);
        }

        [Test, TestCaseSource("ToStringTestCases")]
        public string ShouldReturnReturnObjectToString(object obj)
        {
            // when
            var toString = Strings.ToString(obj);

            // then
            return toString;
        }

        private static IEnumerable<ITestCaseData> ToStringTestCases()
        {
            yield return new TestCaseData("test")
                .SetName("Should return string")
                .Returns("test");

            yield return new TestCaseData(null)
                .SetName("Should generate toString of null object")
                .Returns("null");

            yield return new TestCaseData(new List<int?> { 1, 2, null, 3 })
                .SetName("Should generate toString of collection")
                .Returns("[1, 2, null, 3]");

            yield return new TestCaseData(Lists.EmptyList<int>())
                .SetName("Should generate toString of empty collection")
                .Returns("[]");

            yield return new TestCaseData((object) new object[] {"A", "B", 3, 4})
               .SetName("Should generate toString of array")
               .Returns("[A, B, 3, 4]");

            yield return new TestCaseData(Dictionaries.Create("A", 1, "B", 2, "C", 3))
              .SetName("Should generate toString of dictionary")
              .Returns("[(A, 1), (B, 2), (C, 3)]");

            yield return new TestCaseData(Dictionaries.Create(
                "A", Lists.AsList(Lists.AsList(1, 2, 3), Lists.AsList(4, 5, 6)),
                "B", Lists.AsList(Lists.AsList(1), Lists.AsList(2)),
                "C", Lists.AsList(Lists.EmptyList<int>(), null)))
              .SetName("Should generate toString of complex dictionary")
              .Returns("[(A, [[1, 2, 3], [4, 5, 6]]), (B, [[1], [2]]), (C, [[], null])]");
        }
    }
}
