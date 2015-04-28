
using System.Collections.Generic;
using NUnit.Framework;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class NumbersTests
    {
        [Test, TestCaseSource("ParseShortTestCases")]
        public short? ShouldParseShort(string value)
        {
            // when
            var result = Numbers.TryParseShort(value);

            // then
            return result;
        }

        private static IEnumerable<ITestCaseData> ParseShortTestCases()
        {
            yield return new TestCaseData("32767")
                .SetName("Should parse short from string")
                .Returns((short) 32767);

            yield return new TestCaseData("invalid")
                .SetName("Should return null when parsing invalid short")
                .Returns(null);

            yield return new TestCaseData("")
                .SetName("Should return null when parsing short from empty string")
                .Returns(null);

            yield return new TestCaseData(null)
                .SetName("Should return null when parsing short from null string")
                .Returns(null);
        }

        [Test, TestCaseSource("ParseIntTestCases")]
        public int? ShouldParseInt(string value)
        {
            // when
            var result = Numbers.TryParseInt(value);

            // then
            return result;
        }

        private static IEnumerable<ITestCaseData> ParseIntTestCases()
        {
            yield return new TestCaseData("5")
                .SetName("Should parse int from string")
                .Returns(5);

            yield return new TestCaseData("invalid")
                .SetName("Should return null when parsing invalid int")
                .Returns(null);

            yield return new TestCaseData("")
                .SetName("Should return null when parsing long from empty string")
                .Returns(null);

            yield return new TestCaseData(null)
                .SetName("Should return null when parsing long from null string")
                .Returns(null);
        }

        [Test, TestCaseSource("ParseLongTestCases")]
        public long? ShouldParseLong(string value)
        {
            // when
            var result = Numbers.TryParseLong(value);

            // then
            return result;
        }

        private static IEnumerable<ITestCaseData> ParseLongTestCases()
        {
            yield return new TestCaseData("9223372036854775807")
                .SetName("Should parse long from string")
                .Returns(9223372036854775807L);

            yield return new TestCaseData("invalid")
                .SetName("Should return null when parsing invalid long")
                .Returns(null);

            yield return new TestCaseData("")
                .SetName("Should return null when parsing long from empty string")
                .Returns(null);

            yield return new TestCaseData(null)
                .SetName("Should return null when parsing long from null string")
                .Returns(null);
        }

        [Test, TestCaseSource("ParseInvariantFloatTestCases")]
        public float? ShouldParseFloatUsingInvariantFormat(string value)
        {
            // when
            var result = Numbers.TryParseFloat(value);

            // then
            return result;
        }

        private static IEnumerable<ITestCaseData> ParseInvariantFloatTestCases()
        {
            yield return new TestCaseData("1243.53")
                .SetName("Should parse float from string")
                .Returns(1243.53F);

            yield return new TestCaseData("1,243,123.12345")
               .SetName("Should parse float from string with tausend group")
               .Returns(1243123.12345F);

            yield return new TestCaseData("invalid")
                .SetName("Should return null when parsing invalid float")
                .Returns(null);

            yield return new TestCaseData("")
                .SetName("Should return null when parsing float from empty string")
                .Returns(null);

            yield return new TestCaseData(null)
                .SetName("Should return null when parsing float from null string")
                .Returns(null);
        }

        [Test, TestCaseSource("ParseInvariantDoubleTestCases")]
        public double? ShouldParseDoubleUsingInvariantFormat(string value)
        {
            // when
            var result = Numbers.TryParseDouble(value);

            // then
            return result;
        }

        private static IEnumerable<ITestCaseData> ParseInvariantDoubleTestCases()
        {
            yield return new TestCaseData("5121.54")
                .SetName("Should parse double from string")
                .Returns(5121.54D);

            yield return new TestCaseData("1,111,222.5")
               .SetName("Should parse double from string with tausend group")
               .Returns(1111222.5D);

            yield return new TestCaseData("invalid")
                .SetName("Should return null when parsing invalid double")
                .Returns(null);

            yield return new TestCaseData("")
                .SetName("Should return null when parsing double from empty string")
                .Returns(null);

            yield return new TestCaseData(null)
                .SetName("Should return null when parsing double from null string")
                .Returns(null);
        } 
    }
}
