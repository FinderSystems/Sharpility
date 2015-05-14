
using System.Collections.Generic;
using NUnit.Framework;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class ObjectsTests
    {
        [Test, TestCaseSource("ObjectComparationTestCases")]
        public bool ShouldCompareObjecs(object first, object second)
        {
            // when
            var equals = Objects.Equal(first, second);

            // then
            return equals;
        }

        private static IEnumerable<ITestCaseData> ObjectComparationTestCases()
        {
            yield return new TestCaseData("string", "string")
                .SetName("Should return that strings are equals")
                .Returns(true);

            yield return new TestCaseData("a", "b")
                .SetName("Should return that strings are not equals")
                .Returns(false);

            var obj = new object();
            yield return new TestCaseData(obj, obj)
                .SetName("Should return that objects are euals be reference")
                .Returns(true);

            yield return new TestCaseData(new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3 })
                .SetName("Should return that collections are equals")
                .Returns(true);

            yield return new TestCaseData(new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3, 4 })
                .SetName("Should return that collections are not equals")
                .Returns(false);

            yield return new TestCaseData(new object[] { "A", 13 }, new object[] { "A", 13 })
                .SetName("Should return that arrays are not equals")
                .Returns(true);

            yield return new TestCaseData(Dictionaries.Create("A", 2, "B", 3), Dictionaries.Create("A", 2, "B", 3))
              .SetName("Should return that dictionaries are equals")
              .Returns(true);

            yield return new TestCaseData(Dictionaries.Create("A", 2, "B", 3), Dictionaries.Create("B", 3, "A", 2))
                .SetName("Should return that dictionaries created in reverse order are equals")
                .Returns(true);

            yield return new TestCaseData(Dictionaries.Create("A", 2, "B", 3), Dictionaries.Create("A", 2, "B", 4))
                .SetName("Should return that dictionaries are not equals")
                .Returns(false);

            yield return new TestCaseData(null, null)
                .SetName("Should return that nulls are equals")
                .Returns(true);

            yield return new TestCaseData(null, "123")
                .SetName("Should return that null are not equals to object")
                .Returns(false);

            yield return new TestCaseData(1324, null)
                .SetName("Should return that object is not equals to null")
                .Returns(false);

            yield return new TestCaseData(Sets.AsSet(1, 2, 3), Sets.AsSet(3, 2, 1))
                .SetName("Should return that sets are equals")
                .Returns(true);

            yield return new TestCaseData(Sets.EmptySet<int>(), Sets.EmptySet<int>())
               .SetName("Should return that empty sets are equals")
               .Returns(true);

            yield return new TestCaseData(Sets.EmptySet<int>(), Sets.EmptySet<string>())
               .SetName("Should return that empty sets with different types are not equals")
               .Returns(false);

            yield return new TestCaseData(Sets.AsSet(1, 2, 3), Sets.AsSet(3, 2))
               .SetName("Should return that sets are not equals")
               .Returns(false);
        } 

        // TODO hashCode tests
    }
}
