using System.Collections.Generic;
using NFluent;
using NUnit.Framework;
using Sharpility.Collections;
using Sharpility.Util;

namespace Sharpility.Tests.Collections
{
    [TestFixture]
    public class LinkedListMultiDictionaryTests
    {
        [Test]
        public void ShouldAllowDuplicatedValuesForKey()
        {
            // given
            var multiDictionary = new LinkedListMultiDictionary<int, string>();

            // when
            multiDictionary.Put(1, "A");
            multiDictionary.Put(1, "A");
            multiDictionary.PutAll(2, Lists.AsList("A", "B", "B"));

            // then
            Check.That(multiDictionary.Count).IsEqualTo(5);
            Check.That(multiDictionary.Values).ContainsExactly("A", "A", "A", "B", "B");
            Check.That(multiDictionary[1]).ContainsExactly("A", "A");
            Check.That(multiDictionary[1]).IsInstanceOf<LinkedList<string>>();
            Check.That(multiDictionary[2]).ContainsExactly("A", "B", "B");
            Check.That(multiDictionary[2]).IsInstanceOf<LinkedList<string>>();
        }

        [Test]
        public void ShouldConvertMultiDictionaryToDictionaryWithLinkedLists()
        {
            // given
            var multiDictionary = new LinkedListMultiDictionary<int, string>();
            multiDictionary.Put(1, "A");
            multiDictionary.Put(2, "B");
            multiDictionary.PutAll(3, Lists.AsList("A", "B", "C", "C"));

            // when
            var dictionary = multiDictionary.ToDictionary();

            // then
            Check.That(dictionary[1]).IsInstanceOf<LinkedList<string>>()
                .And.ContainsExactly("A");
            Check.That(dictionary[2]).IsInstanceOf<LinkedList<string>>()
                .And.ContainsExactly("B");
            Check.That(dictionary[3]).IsInstanceOf<LinkedList<string>>()
                .And.ContainsExactly("A", "B", "C", "C");
        }
    }
}
