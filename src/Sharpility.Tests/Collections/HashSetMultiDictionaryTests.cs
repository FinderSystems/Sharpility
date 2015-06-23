using System.Collections.Generic;
using NFluent;
using NUnit.Framework;
using Sharpility.Collections;
using Sharpility.Util;

namespace Sharpility.Tests.Collections
{
    [TestFixture]
    public class HashSetMultiDictionaryTests
    {

        [Test]
        public void ShouldNotAllowDuplicatedValuesForKey()
        {
            // given
            var multiDictionary = new HashSetMultiDictionary<int, string>();

            // when
            multiDictionary.Put(1, "A");
            multiDictionary.Put(1, "A");
            multiDictionary.PutAll(2, Lists.AsList("A", "B", "B"));

            // then
            Check.That(multiDictionary.Count).IsEqualTo(3);
            Check.That(multiDictionary.Values).ContainsExactly("A", "A", "B");
            Check.That(multiDictionary[1]).ContainsExactly("A");
            Check.That(multiDictionary[1]).IsInstanceOf<HashSet<string>>();
            Check.That(multiDictionary[2]).ContainsExactly("A", "B");
            Check.That(multiDictionary[2]).IsInstanceOf<HashSet<string>>();
        }

        [Test]
        public void ShouldConvertMultiDictionaryToDictionaryWithHashSets()
        {
            // given
            var multiDictionary = new HashSetMultiDictionary<int, string>();
            multiDictionary.Put(1, "A");
            multiDictionary.Put(2, "B");
            multiDictionary.PutAll(3, Lists.AsList("A", "B", "C", "C"));

            // when
            var dictionary = multiDictionary.ToDictionary();

            // then
            Check.That(dictionary[1]).IsInstanceOf<HashSet<string>>()
                .And.ContainsExactly("A");
            Check.That(dictionary[2]).IsInstanceOf<HashSet<string>>()
                .And.ContainsExactly("B");
            Check.That(dictionary[3]).IsInstanceOf<HashSet<string>>()
                .And.ContainsExactly("A", "B", "C");
        }
    }
}
