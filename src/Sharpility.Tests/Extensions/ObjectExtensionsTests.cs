
using NUnit.Framework;
using NFluent;
using Sharpility.Extensions;
using System;
using Moq;
using System.Collections.Generic;

namespace Sharpility.Tests.Extensions
{
    [TestFixture]
    class ObjectExtensionsTests
    {
        [Test, TestCaseSource("ProperEqualsTestCases")]
        public Tuple<bool, bool, bool> ShouldRunProperEquals(object first, object second)
        {
            var properties = first.EqualsByProperties(second);
            var fields = first.EqualsByFields(second);
            var members = first.EqualsByMembers(second);
            return new Tuple<bool, bool, bool>(properties, fields, members);
        }

        private static IEnumerable<ITestCaseData> ProperEqualsTestCases()
        {
            var onlyProperies = new Tuple<bool, bool, bool>(true, false, false);
            var onlyFields = new Tuple<bool, bool, bool>(false, true, false);
            var allMembers = new Tuple<bool, bool, bool>(true, true, true);
            var none = new Tuple<bool, bool, bool>(false, false, false);

            var object1 = new Object();
            var object2 = new Object();
            yield return new TestCaseData(object1, object2)
                .SetName("Should equal by all members of empty objects")
                .Returns(allMembers);

            var first = new Testable(1, "2", false, null);
            var second = new Testable(1, "2", false, null);
            yield return new TestCaseData(first, second)
                .SetName("Should equal by all members with null list when properties are not set")
                .Returns(allMembers);

            first = new Testable(5, "2", true, new List<string> {"smell", "niuch", "woń"});
            second = new Testable(5, "2", true, new List<string> { "smell", "niuch", "woń" });
            yield return new TestCaseData(first, second)
                .SetName("Should equal by all members with the same list when properties are not set")
                .Returns(allMembers);

            first = new Testable(5, "2", true, new List<string> { "smell", "niuch", "woń" });
            second = new Testable(5, "2", true, new List<string> { "smell", "woń", "niuch" });
            yield return new TestCaseData(first, second)
                .SetName("Should equal by properties with different list in fields when properties are not set")
                .Returns(onlyProperies);

            first = new Testable(5, "2", null, new List<string> { "smell", "niuch", "woń" });
            second = new Testable(5, "2", null, new List<string> { "smell", "niuch", "woń" });
            yield return new TestCaseData(first, second)
                .SetName("Should equal by all members with the same list but nulled bool when properties are not set")
                .Returns(allMembers);

            first = new Testable(1, "2", false, null);
            second = new Testable(1, "2", false, null);
            second.Alpha = 3;
            yield return new TestCaseData(first, second)
                .SetName("Should equal by fields but not properties when property is different")
                .Returns(onlyFields);

            first = new Testable(1, "2", null);
            second = new Testable(1, "2", null);
            yield return new TestCaseData(first, second)
                .SetName("Should equal by all members with null property list when fields are not set")
                .Returns(allMembers);

            first = new Testable(1, "2", new List<int> { 1,3,2 });
            second = new Testable(1, "2", new List<int> { 1,3,2 });
            yield return new TestCaseData(first, second)
                .SetName("Should equal by all members with the same property list when fields are not set")
                .Returns(allMembers);

            first = new Testable(1, "2", new List<int> { 1, 3, 2 });
            second = new Testable(1, "2", new List<int> { 1, 2, 3 });
            yield return new TestCaseData(first, second)
                .SetName("Should equal by fields with different property list when fields are not set")
                .Returns(onlyFields);

            first = new Testable(1, "2", new List<int> { 1, 3, 2 });
            second = new Testable(1, "2", null);
            second.two = "2";
            yield return new TestCaseData(first, second)
                .SetName("Should not equal by all members")
                .Returns(none);
        }


        private class Testable
        {
            private int one;
            public string two;
            private bool? three;
            private List<string> four;

            public Testable(int one, string two, bool? three, List<string> four)
            {
                this.one = one;
                this.two = two;
                this.three = three;
                this.four = four;
            }

            public Testable(int alpha, string beta, List<int> theta)
            {
                this.Alpha = alpha;
                this.Beta = beta;
                this.Theta = theta;
            }

            public int Alpha { get; set; }
            public string Beta { get; set; }
            public List<int> Theta {get; set;}
        }
    }
}
