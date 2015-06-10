
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        [Test, TestCaseSource("DoubleToDecimalTestCases")]
        public decimal? ShouldConvertDoubleToDecimal(double? given)
        {
            // when
            var result = Numbers.DoubleToDecimal(given);

            // then
            return result;
        }

        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        private static IEnumerable<ITestCaseData> DoubleToDecimalTestCases()
        {
            double? given = 10D;
            decimal? expected = 10M;
            yield return new TestCaseData(given)
                .SetName(Strings.Format("Should convert double {0} to decimal {1}", given, expected))
                .Returns(expected);

            given = 150.50D;
            expected = 150.50M;
            yield return new TestCaseData(given)
                .SetName(Strings.Format("Should convert double {0} to decimal {1}", given, expected))
                .Returns(expected);

            given = null;
            expected = null;
            yield return new TestCaseData(given)
                .SetName(Strings.Format("Should convert double {0} to decimal {1}", given, expected))
                .Returns(expected);
        }

        [Test, TestCaseSource("DecimalToDoubleTestCases")]
        public double? ShouldConvertDecimalToDouble(decimal? given)
        {
            // when
            var result = Numbers.DecimalToDouble(given);

            // then
            return result;
        }

        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        private static IEnumerable<ITestCaseData> DecimalToDoubleTestCases()
        {
            decimal? given = 10M;
            double? expected = 10D;
            yield return new TestCaseData(given)
                .SetName(Strings.Format("Should convert decimal {0} to double {1}", given, expected))
                .Returns(expected);

            given = 150.50M;
            expected = 150.50D;
            yield return new TestCaseData(given)
                .SetName(Strings.Format("Should convert decimal {0} to double {1}", given, expected))
                .Returns(expected);

            given = null;
            expected = null;
            yield return new TestCaseData(given)
                .SetName(Strings.Format("Should convert decimal {0} to double {1}", given, expected))
                .Returns(expected);
        } 
    }
}
