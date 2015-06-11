
using System.Collections.Generic;
using System.Collections.Immutable;
using NFluent;
using NUnit.Framework;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class SetsTests
    {
        [Test]
        public void ShouldCreateEmptySet()
        {
            // when
            var set = Sets.EmptySet<int>();

            // when
            Check.That(set).IsEmpty();
            Check.That(set).IsInstanceOf<ImmutableHashSet<int>>();
        }

        [Test]
        public void ShouldCreateEmptyMutableSet()
        {
            // when
            var set = Sets.EmptyMutableSet<int>();

            // when
            Check.That(set).IsEmpty();
            Check.That(set).IsInstanceOf<HashSet<int>>();
        }

        [Test]
        public void ShouldCreateSingletonSet()
        {
            // given
            const string element = "test";

            // when
            var set = Sets.Singleton(element);

            // then
            Check.That(set).HasSize(1);
            Check.That(set).ContainsExactly(element);
            Check.That(set).IsInstanceOf<ImmutableHashSet<string>>();
        }

        [Test, TestCaseSource("NullSafeSetTestCases")]
        public ISet<string> ShouldCreateNullSafeList(ISet<string> given)
        {
            // when
            var nullSafe = Sets.NullSafeSet(given);

            // then
            return nullSafe;
        }

        private static IEnumerable<TestCaseData> NullSafeSetTestCases()
        {
            yield return new TestCaseData((ISet<string>)null)
                .SetName("Should create empty set for null")
                .Returns(Sets.EmptyMutableSet<string>());

            var notNullSet = Sets.AsSet("A", "B");
            yield return new TestCaseData(notNullSet)
                .SetName("Should return given set if not null")
                .Returns(notNullSet);
        }

        [Test, TestCaseSource("SetFromArrayTestCases")]
        public ISet<string> ShouldCreateSetFromEnumerable(string[] values)
        {
            // when
            var set = Sets.AsSet(values);

            // then
            return set;
        }

        private static IEnumerable<ITestCaseData> SetFromArrayTestCases()
        {
            yield return new TestCaseData((object) new [] {"A", "B", "C"})
                .SetName("Should create set from list")
                .Returns(new HashSet<string> {"A", "B", "C"});

            yield return new TestCaseData((object)new[] { "A", "A", "B", "C", "C"})
                .SetName("Should create set from not unique list")
                .Returns(new HashSet<string> { "A", "B", "C" });

            yield return new TestCaseData((object) new string[0])
                .SetName("Should create set from not empty list")
                .Returns(new HashSet<string>());
        }
    }
}
