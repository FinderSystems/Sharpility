﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NFluent;
using NUnit.Framework;
using Sharpility.Extensions;
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

            yield return new TestCaseData(new DateTime(2016, 2, 9, 10, 45, 0, DateTimeKind.Utc))
               .SetName("Should include UTC kind for DateTime")
               .Returns("2016-02-09 10:45:00Z");

            yield return new TestCaseData(new DateTime(2016, 2, 9, 10, 45, 0, DateTimeKind.Unspecified))
               .SetName("Should ignore unspecified kind for DateTime")
               .Returns("2016-02-09 10:45:00");
        }

        [Test]
        [Explicit] // Depends on system settings
        public void ToStringOfDateTimeWithLocalKindShouldIncludeZoneOffset()
        {
            // given
            var dateTime = new DateTime(2016, 2, 9, 10, 50, 0, DateTimeKind.Local);

            // when
            var dateTimeToString = Strings.ToString(dateTime);

            // then
            Check.That(dateTimeToString).Equals("2016-02-09 10:50:00+01:00");
        }

        [Test, TestCaseSource("StringFormatTestCases")]
        public string ShouldFormatStringParameters(string value, object[] parameters)
        {
            // when
            var formatted = Strings.Format(value, parameters);

            // then
            return formatted;
        }

        private static IEnumerable<ITestCaseData> StringFormatTestCases()
        {
            yield return new TestCaseData("test", new object[0])
                .SetName("Should format string without params")
                .Returns("test");

            yield return new TestCaseData("This is '{0}'", new object[] {null})
                .SetName("Should format null params")
                .Returns("This is 'null'");

            yield return new TestCaseData("{0}, {1}, {2}", new object[] { 1, "B", null })
                .SetName("Should format multiple params")
                .Returns("1, B, null");

            yield return new TestCaseData("This is {0}", new object[] {Lists.AsList(1, 2, 3)})
                .SetName("Should format list params")
                .Returns("This is [1, 2, 3]");

            yield return new TestCaseData("This is {0}", new object[] {
                Lists.AsList(Lists.AsList("A", "B"), Lists.AsList("C", "D"), Lists.EmptyList<string>())})
                .SetName("Should format nested list params")
                .Returns("This is [[A, B], [C, D], []]");

            yield return new TestCaseData("This is {0}", new object[] { Dictionaries.Create("A", 1, "B", 2) })
               .SetName("Should format dictionary params")
               .Returns("This is [(A, 1), (B, 2)]");
        }

        [Test, TestCaseSource("EqualsIgnoreCasesTestCases")]
        public bool ShouldExtendStringByEqualsIgnoreCasesMethod(string given, string comparedTo)
        {
            // when
            var result = given.EqualsIgnoreCases(comparedTo);

            // then
            return result;
        }

        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        [SuppressMessage("ReSharper", "RedundantAssignment")]
        private static IEnumerable<ITestCaseData> EqualsIgnoreCasesTestCases()
        {
            var given = "aa";
            var comparedTo = "aa";
            var expected = true;
            yield return new TestCaseData(given, comparedTo)
                .SetName(Strings.Format("'{0}' to '{1}' compared with ignored cases returns {2}", given, comparedTo, expected))
                .Returns(expected);

            given = "AAA";
            comparedTo = "AAA";
            expected = true;
            yield return new TestCaseData(given, comparedTo)
                .SetName(Strings.Format("'{0}' to '{1}' compared with ignored cases returns {2}", given, comparedTo, expected))
                .Returns(expected);

            given = "AAA";
            comparedTo = "aaa";
            expected = true;
            yield return new TestCaseData(given, comparedTo)
                .SetName(Strings.Format("'{0}' to '{1}' compared with ignored cases returns {2}", given, comparedTo, expected))
                .Returns(expected);

            given = "AaAaa";
            comparedTo = "aAAAa";
            expected = true;
            yield return new TestCaseData(given, comparedTo)
                .SetName(Strings.Format("'{0}' to '{1}' compared with ignored cases returns {2}", given, comparedTo, expected))
                .Returns(expected);

            given = "AAB";
            comparedTo = "AA";
            expected = false;
            yield return new TestCaseData(given, comparedTo)
                .SetName(Strings.Format("'{0}' to '{1}' compared with ignored cases returns {2}", given, comparedTo, expected))
                .Returns(expected);
        }

        [Test]
        public void ShouldReturnByteArrayAsString()
        {
            // given
            var bytes = "this is a test".ToBytes();

            // when
            var toString = Strings.ToString(bytes);

            // then
            Check.That(toString).IsEqualTo("this is a test");
        }
    }
}
