using NFluent;
using NUnit.Framework;
using Sharpility.Collections;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Tests.Extensions
{
    [TestFixture]
    public class MultiDictionaryExtensionsTests
    {
        [Test]
        public void ShouldConvertArrayListMultiDictionaryToImmutable()
        {
            // given
            var multiDictionary = new ArrayListMultiDictionary<int, string>();
            multiDictionary.Put(1, "A");
            multiDictionary.PutAll(2, Lists.AsList("A", "B", "B"));
            multiDictionary.PutAll(3, Lists.AsList("X", "Y"));

            // when
            var immutableMultiDictionary = multiDictionary.ToImmutableMultiDictionary();

            // then
            Check.That(immutableMultiDictionary).IsInstanceOf<ImmutableListMultiDictionary<int, string>>();
            Check.That(immutableMultiDictionary).IsEqualTo(ImmutableListMultiDictionary<int, string>.Of(
                1, "A",
                2, "A", 2, "B", 2, "B", 
                3, "X", 3, "Y"));
        }

        [Test]
        public void ShouldConvertHashSetMultiDictionaryToImmutable()
        {
            // given
            var multiDictionary = new HashSetMultiDictionary<int, string>();
            multiDictionary.Put(1, "A");
            multiDictionary.PutAll(2, Lists.AsList("A", "B"));
            multiDictionary.PutAll(3, Lists.AsList("X", "Y"));

            // when
            var immutableMultiDictionary = multiDictionary.ToImmutableMultiDictionary();

            // then
            Check.That(immutableMultiDictionary).IsInstanceOf<ImmutableSetMultiDictionary<int, string>>();
            Check.That(immutableMultiDictionary).IsEqualTo(HashSetMultiDictionary<int, string>.Of(
                1, "A",
                2, "A", 2, "B",
                3, "X", 3, "Y"));
        }

        [Test]
        public void ShouldConvertArrayListMultiDictionaryToImmutableSetMultiDictionary()
        {
            // given
            // given
            var multiDictionary = new ArrayListMultiDictionary<int, string>();
            multiDictionary.Put(1, "A");
            multiDictionary.PutAll(2, Lists.AsList("A", "B", "B"));
            multiDictionary.PutAll(3, Lists.AsList("X", "Y"));

            // when
            var immutableMultiDictionary = multiDictionary.ToImmutableSetMultiDictionary();

            // then
            // then
            Check.That(immutableMultiDictionary).IsInstanceOf<ImmutableSetMultiDictionary<int, string>>();
            Check.That(immutableMultiDictionary).IsEqualTo(HashSetMultiDictionary<int, string>.Of(
                1, "A",
                2, "A", 2, "B",
                3, "X", 3, "Y"));
        }

        [Test]
        public void ShouldConvertHashSetMultiDictionaryToImmutableListMultiDictionary()
        {
            // given
            var multiDictionary = new HashSetMultiDictionary<int, string>();
            multiDictionary.Put(1, "A");
            multiDictionary.PutAll(2, Lists.AsList("A", "B"));
            multiDictionary.PutAll(3, Lists.AsList("X", "Y"));

            // when
            var immutableMultiDictionary = multiDictionary.ToImmutableListMultiDictionary();

            // then
            Check.That(immutableMultiDictionary).IsEqualTo(ImmutableListMultiDictionary<int, string>.Of(
                1, "A",
                2, "A", 2, "B",
                3, "X", 3, "Y"));
        }

        [Test]
        public void ShouldReturnSameReferenceWhenConvertingImmutableSetMutliDictionaryToImmutalbe()
        {
            // given
            var multiDictionary = ImmutableSetMultiDictionary<int, string>.Of(1, "A", 2, "B");

            // when
            var converted = multiDictionary.ToImmutableMultiDictionary();

            // then
            Check.That(converted).IsSameReferenceThan(multiDictionary);
        }

        [Test]
        public void ShouldReturnSameReferenceWhenConvertingImmutableListMutliDictionaryToImmutalbe()
        {
            // given
            var multiDictionary = ImmutableListMultiDictionary<int, string>.Of(1, "A", 2, "B");

            // when
            var converted = multiDictionary.ToImmutableMultiDictionary();

            // then
            Check.That(converted).IsSameReferenceThan(multiDictionary);
        }
    }
}
