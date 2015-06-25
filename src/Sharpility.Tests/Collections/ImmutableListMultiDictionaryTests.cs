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
    public class ImmutableListMultiDictionaryTests
    {
        [Test]
        public void ShouldBuildImmutableListMultiDictionaryAndReturnImmutableListValues()
        {
            // given
            var immutableListMultiDictionary = ImmutableListMultiDictionary<int, string>.Builder()
                .Put(1, "A")
                .Put(1, "A")
                .Put(1, "B")
                .PutAll(2, Lists.AsList("X", "Y", "Z"))
                .PutAll(ArrayListMultiDictionary<int, string>.Of(3, "Z", 3, "W"))
                .PutAll(Dictionaries.Create(4, "_"))
                .Build();

            // when
            var values1 = immutableListMultiDictionary[1];
            var values2 = immutableListMultiDictionary.Get(2);
            var values3 = immutableListMultiDictionary.Get(3);
            var values4 = immutableListMultiDictionary[4];

            // then
            Check.That(values1).HasSize(3).And.Contains("A", "B").And.IsInstanceOf<ImmutableList<string>>();
            Check.That(values1.DistinctElementCount()).IsEqualTo(Dictionaries.Create("A", 2, "B", 1));

            Check.That(values2).HasSize(3).And.Contains("X", "Y", "Z").And.IsInstanceOf<ImmutableList<string>>();
            Check.That(values2.DistinctElementCount()).IsEqualTo(Dictionaries.Create("X", 1, "Y", 1, "Z", 1));

            Check.That(values3).HasSize(2).And.Contains("Z", "W").And.IsInstanceOf<ImmutableList<string>>();
            Check.That(values3.DistinctElementCount()).IsEqualTo(Dictionaries.Create("Z", 1, "W", 1));

            Check.That(values4).HasSize(1).And.Contains("_").And.IsInstanceOf<ImmutableList<string>>();
            Check.That(values4.DistinctElementCount()).IsEqualTo(Dictionaries.Create("_", 1));
        }

        [Test]
        public void ShouldConvertImmutableListMultiDictionaryToDictionaryWithListAsValue()
        {
            // given
            var immutableListMultiDictionary = ImmutableListMultiDictionary<int, string>.Builder()
                .Put(1, "A")
                .Put(1, "A")
                .Put(1, "B")
                .PutAll(2, Lists.AsList("X", "Y", "Z"))
                .Build();

            // when
            var dictionary = immutableListMultiDictionary.ToDictionary();

            // then
            Check.That(dictionary).IsEqualTo(Dictionaries.Create(
                1, Lists.AsList("A", "A", "B"), 
                2, Lists.AsList("X", "Y", "Z")));
            Check.That(dictionary[1]).IsInstanceOf<List<string>>();
            Check.That(dictionary[2]).IsInstanceOf<List<string>>();
        }
    }
}
