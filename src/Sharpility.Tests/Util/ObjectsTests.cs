
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sharpility.Extensions;
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

        [Test, TestCaseSource("HashCodeTestCases")]
        public bool ShouldGenerateHashCodes(object first, object second)
        {
            // when
            var firstHash = Objects.HashCode(first);
            var secondHash = Objects.HashCode(second);

            // then
            return firstHash == secondHash;
        }

        private static IEnumerable<ITestCaseData> HashCodeTestCases()
        {
            object first = Lists.AsList(1, 2, 3, 4);
            object second = Lists.AsList(1, 2, 3, 4);
            yield return new TestCaseData(first, second)
                .SetName("Should generate same hash codes for lists with exact elements")
                .Returns(true);

            first = new object[] {"A", 3, new DateTime(2015, 6, 9, 0, 0, 0)};
            second = new object[] { "A", 3, new DateTime(2015, 6, 9, 0, 0, 0)};
            yield return new TestCaseData(first, second)
                .SetName("Should generate same hash codes for arrays with exact elements")
                .Returns(true);

            first = Lists.AsList(1, 2, 3, 4);
            second = Lists.AsList(1, 2, 4, 3);
            yield return new TestCaseData(first, second)
                .SetName("Should generate different hash codes for lists with different elements order")
                .Returns(false);

            first = Lists.AsList(1, 2, 3, 4);
            second = Lists.AsList(1, 2, 3);
            yield return new TestCaseData(first, second)
                .SetName("Should generate different hash codes for lists with different elements count")
                .Returns(false);

            first = Lists.EmptyList<string>();
            second = Lists.AsList("A");
            yield return new TestCaseData(first, second)
                .SetName("Should generate different hash codes for empty list and not empty list")
                .Returns(false);

            first = "test";
            second = "test";
            yield return new TestCaseData(first, second)
                .SetName("Should generate same hash codes for same strings")
                .Returns(true);

            first = 5;
            second = 5;
            yield return new TestCaseData(first, second)
                .SetName("Should generate same hash codes for same ints")
                .Returns(true);

            first = Dictionaries.Create(1, "A", 2, "B");
            second = Dictionaries.Create(1, "A", 2, "B");
            yield return new TestCaseData(first, second)
                .SetName("Should generate same hash codes for same dictionaries")
                .Returns(true);

            first = Dictionaries.Create(1, "A", 2, "B");
            second = Dictionaries.Create(3, "C", 4, "D");
            yield return new TestCaseData(first, second)
                .SetName("Should generate same hash codes for different dictionaries")
                .Returns(false);

            first = Dictionaries.Create(1, "A", 2, "B");
            second = Dictionaries.Create(1, "A");
            yield return new TestCaseData(first, second)
                .SetName("Should generate same hash codes for different dictionaries sizes")
                .Returns(false);

            first = new HashObject(5);
            second = new HashObject(5);
            yield return new TestCaseData(first, second)
               .SetName("Should generate same hash codes using GetHashCode implementation")
               .Returns(true);

            first = new HashObject(5);
            second = new HashObject(1);
            yield return new TestCaseData(first, second)
               .SetName("Should generate different hash codes using GetHashCode implementation")
               .Returns(false);

            first = Lists.AsList(new HashObject(1), new HashObject(2), new HashObject(3));
            second = Lists.AsList(new HashObject(1), new HashObject(2), new HashObject(3));
            yield return new TestCaseData(first, second)
               .SetName("Should generate same hash codes using GetHashCode implementation for list items")
               .Returns(true);

            first = Lists.AsList(new HashObject(1), new HashObject(2), new HashObject(3));
            second = Lists.AsList(new HashObject(3), new HashObject(2), new HashObject(1));
            yield return new TestCaseData(first, second)
               .SetName("Should generate different hash codes using GetHashCode implementation for list items")
               .Returns(false);

            yield return new TestCaseData(null, null)
               .SetName("Should generate same hash codes for nulls")
               .Returns(true);

            first = new object();
            yield return new TestCaseData(first, null)
               .SetName("Should generate different hash codes for null and object")
               .Returns(false);
        }

        private class HashObject
        {
            private readonly int hashCode;

            internal HashObject(int hashCode)
            {
                this.hashCode = hashCode;
            }

            public override bool Equals(object obj)
            {
                return this.EqualsByFields(obj);
            }

            public override int GetHashCode()
            {
                return hashCode;
            }
        }
    }
}
