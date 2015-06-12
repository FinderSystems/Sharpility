using System.Collections.Generic;
using NUnit.Framework;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class BooleansTest
    {
        [Test, TestCaseSource("ParseBooleansTestCases")]
        public bool? ShouldParseBooleans(string value)
        {
            // when
            var parsed = Booleans.TryParse(value);

            // then
            return parsed;
        }

        private static IEnumerable<ITestCaseData> ParseBooleansTestCases()
        {
            yield return new TestCaseData("true")
                .SetName("Should parse 'true' string as true")
                .Returns(true);

            yield return new TestCaseData("TRUE")
                .SetName("Should parse 'TRUE' string as true")
                .Returns(true);

            yield return new TestCaseData("false")
                .SetName("Should parse 'false' string as false")
                .Returns(false);

            yield return new TestCaseData("invalid")
                .SetName("Should parse 'invalid' string as null bool")
                .Returns(null);

            yield return new TestCaseData(null)
                .SetName("Should parse 'null string as null bool")
                .Returns(null);
        } 
    }
}
