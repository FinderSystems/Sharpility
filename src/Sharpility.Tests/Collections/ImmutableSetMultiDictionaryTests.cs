using System.Collections.Generic;
using System.Collections.Immutable;
using NFluent;
using NUnit.Framework;
using Sharpility.Collections;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Tests.Collections
{
    [TestFixture]
    public class ImmutableSetMultiDictionaryTests
    {
        [Test]
        public void ShouldBuildImmutableSetMultiDictionaryAndReturnImmutableHashSetValues()
        {
            // given
            var immutableSetMultiDictionary = ImmutableSetMultiDictionary<int, string>.Builder()
                .Put(1, "A")
                .Put(1, "A")
                .Put(1, "B")
                .PutAll(2, Lists.AsList("X", "Y", "Z"))
                .PutAll(ArrayListMultiDictionary<int, string>.Of(3, "Z", 3, "W"))
                .PutAll(Dictionaries.Create(4, "_"))
                .Build();

            // when
            var values1 = immutableSetMultiDictionary[1];
            var values2 = immutableSetMultiDictionary.Get(2);
            var values3 = immutableSetMultiDictionary.Get(3);
            var values4 = immutableSetMultiDictionary[4];

            // then
            Check.That(values1).HasSize(2).And.Contains("A", "B").And.IsInstanceOf<ImmutableHashSet<string>>();
            Check.That(values1.DistinctElementCount()).IsEqualTo(Dictionaries.Create("A", 1, "B", 1));

            Check.That(values2).HasSize(3).And.Contains("X", "Y", "Z").And.IsInstanceOf<ImmutableHashSet<string>>();
            Check.That(values2.DistinctElementCount()).IsEqualTo(Dictionaries.Create("X", 1, "Y", 1, "Z", 1));

            Check.That(values3).HasSize(2).And.Contains("Z", "W").And.IsInstanceOf<ImmutableHashSet<string>>();
            Check.That(values3.DistinctElementCount()).IsEqualTo(Dictionaries.Create("Z", 1, "W", 1));

            Check.That(values4).HasSize(1).And.Contains("_").And.IsInstanceOf<ImmutableHashSet<string>>();
            Check.That(values4.DistinctElementCount()).IsEqualTo(Dictionaries.Create("_", 1));
        }

        [Test]
        public void ShouldConvertImmutableSetMultiDictionaryToDictionaryWithHashSetAsValue()
        {
            // given
            var immutableSetMultiDictionary = ImmutableSetMultiDictionary<int, string>.Builder()
                .Put(1, "A")
                .Put(1, "A")
                .Put(1, "B")
                .PutAll(2, Lists.AsList("X", "Y", "Z"))
                .Build();

            // when
            var dictionary = immutableSetMultiDictionary.ToDictionary();

            // then
            Check.That(dictionary).IsEqualTo(Dictionaries.Create(
                1, Sets.AsSet("A", "B"),
                2, Sets.AsSet("X", "Y", "Z")));
            Check.That(dictionary[1]).IsInstanceOf<HashSet<string>>();
            Check.That(dictionary[2]).IsInstanceOf<HashSet<string>>();
        }
    }
}
