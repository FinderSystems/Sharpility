
using System;
using System.Collections.Generic;
using System.Text;
using NFluent;
using NUnit.Framework;
using Sharpility.Extensions;
using Sharpility.Util;

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

        [Test, TestCaseSource("ToBytesTestCases")]
        public byte[] ShouldExtendStringByToBytesMethod(string value, Encoding encoding)
        {
            // when
            var result = value.ToBytes(encoding);

            // then
            return result;
        }

        private static IEnumerable<ITestCaseData> ToBytesTestCases()
        {
            const string value = "alice has cancer";
            yield return new TestCaseData(value, null)
                .SetName("Should convert string to bytes with default encoding when not specified")
                .Returns(Encoding.Default.GetBytes(value));

            yield return new TestCaseData(value, Encoding.Default)
               .SetName("Should convert string to bytes with default encoding")
               .Returns(Encoding.Default.GetBytes(value));

            yield return new TestCaseData(value, Encoding.UTF8)
                .SetName("Should convert string to bytes with UTF-8 encoding")
                .Returns(Encoding.UTF8.GetBytes(value));

            yield return new TestCaseData(value, Encoding.ASCII)
                .SetName("Should convert string to bytes with ASCII encoding")
                .Returns(Encoding.UTF8.GetBytes(value));
        }

        [Test]
        public void ShouldExtendStringByToUtf8BytesMethod()
        {
            // given
            const string value = "alice has cat";

            // when
            var bytes = value.ToUtf8Bytes();

            // then
            Check.That(bytes).ContainsExactly(Encoding.UTF8.GetBytes(value));
        }

        [Test]
        public void ShouldExtendStringByToAsciiBytesMethod()
        {
            // given
            const string value = "alice has cat";

            // when
            var bytes = value.ToAsciiBytes();

            // then
            Check.That(bytes).ContainsExactly(Encoding.ASCII.GetBytes(value));
        }
    }
}
