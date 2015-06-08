using System.Collections.Generic;
using System.Collections.Immutable;
using NFluent;
using NUnit.Framework;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class ListsTests
    {
        [Test]
        public void ShouldCreateEmptyList()
        {
            // when
            var list = Lists.EmptyList<int>();

            // when
            Check.That(list).IsEmpty();
            Check.That(list).IsInstanceOf<ImmutableList<int>>();
        }

        [Test]
        public void ShouldCreateEmptyMutableList()
        {
            // when
            var list = Lists.EmptyMutableList<int>();

            // when
            Check.That(list).IsEmpty();
            Check.That(list).IsInstanceOf<List<int>>();
        }

        [Test]
        public void ShouldCreateSingletonList()
        {
            // given
            const string element = "test";

            // when
            var list = Lists.Singleton(element);

            // then
            Check.That(list).HasSize(1);
            Check.That(list).ContainsExactly(element);
            Check.That(list).IsInstanceOf<ImmutableList<string>>();
        }

        [Test, TestCaseSource("NullSafeListTestCases")]
        public IList<string> ShouldCreateNullSafeList(IList<string> given)
        {
            // when
            var nullSafe = Lists.NullSafeList(given);

            // then
            return nullSafe;
        }

        private static IEnumerable<TestCaseData> NullSafeListTestCases()
        {
            yield return new TestCaseData((IList<string>) null)
                .SetName("Should create empty list for null")
                .Returns(Lists.EmptyMutableList<string>());

            var notNullList = Lists.AsList("A", "B");
            yield return new TestCaseData(notNullList)
                .SetName("Should return given list if not null")
                .Returns(notNullList);
        }
    }
}
