using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFluent;
using NUnit.Framework;
using Sharpility.Collections;
using Sharpility.Util;

namespace Sharpility.Tests.Collections
{
    [TestFixture]
    public class ArrayListMultiDictionaryTests
    {
        [Test]
        public void ShouldPutMultipleValuesForKey()
        {
            // given
            var multiDictionary = new ArrayListMultiDictionary<int, string>();

            // when
            multiDictionary.Put(1, "A");
            multiDictionary.Put(1, "B");
            multiDictionary.Put(1, "C");
            multiDictionary.Put(2, "A");
            multiDictionary.PutAll(3, Lists.AsList("X", "Y", "Z"));

            // then
            Check.That(multiDictionary.Count).IsEqualTo(7);
            Check.That(multiDictionary.Values).HasSize(7);
            Check.That(multiDictionary.Values).Contains("A", "B", "C", "A", "X", "Y", "Z");

            Check.That(multiDictionary.Get(1)).ContainsExactly("A", "B", "C");
            Check.That(multiDictionary.Get(1)).IsInstanceOf<List<string>>();
            Check.That(multiDictionary.ValuesCount(1)).IsEqualTo(3);

            Check.That(multiDictionary.Get(2)).ContainsExactly("A");
            Check.That(multiDictionary.Get(2)).IsInstanceOf<List<string>>();
            Check.That(multiDictionary.ValuesCount(2)).IsEqualTo(1);

            Check.That(multiDictionary[3]).ContainsExactly("X", "Y", "Z");
            Check.That(multiDictionary[3]).IsInstanceOf<List<string>>();
            Check.That(multiDictionary.ValuesCount(3)).IsEqualTo(3);
        }

        [Test]
        public void ShouldReturnEntries()
        {
            // given
            var multiDictionary = new ArrayListMultiDictionary<int, string>();
            multiDictionary.Put(1, "A");
            multiDictionary.Put(1, "B");
            multiDictionary.Put(1, "C");
            multiDictionary.Put(2, "A");
            multiDictionary.PutAll(3, Lists.AsList("X", "Y", "Z"));

            // when
            var entries = multiDictionary.Entries;

            // then
            Check.That(entries).HasSize(7);
            Check.That(entries).Contains(
                Dictionaries.Entry(1, "A"),
                Dictionaries.Entry(1, "B"),
                Dictionaries.Entry(1, "C"),
                Dictionaries.Entry(2, "A"),
                Dictionaries.Entry(3, "X"),
                Dictionaries.Entry(3, "Y"),
                Dictionaries.Entry(3, "Z"));
        }

        [Test]
        public void ShouldReturnMultiEntries()
        {
            // given
            var multiDictionary = new ArrayListMultiDictionary<int, string>();
            multiDictionary.Put(1, "A");
            multiDictionary.Put(1, "B");
            multiDictionary.Put(1, "C");
            multiDictionary.Put(2, "A");
            multiDictionary.PutAll(3, Lists.AsList("X", "Y", "Z"));

            // when
            var entries = multiDictionary.MultiEntries;

            // then
            Check.That(entries).HasSize(3);
            var entry1 = entries.First(entry => entry.Key == 1);
            var entry2 = entries.First(entry => entry.Key == 2);
            var entry3 = entries.First(entry => entry.Key == 3);

            Check.That(entry1.Value).IsEqualTo(Lists.AsList("A", "B", "C"));
            Check.That(entry2.Value).IsEqualTo(Lists.AsList("A"));
            Check.That(entry3.Value).IsEqualTo(Lists.AsList("X", "Y", "Z"));
        }

        [Test]
        public void RetunedCollectionsShouldNotAffectDictionary()
        {
            // given
            var multiDictionary = new ArrayListMultiDictionary<int, string>();
            multiDictionary.PutAll(1, Lists.AsList("X", "Y", "Z"));
            multiDictionary.PutAll(2, Lists.AsList("A", "B"));
            var keys = multiDictionary.Keys;
            var values = multiDictionary.Values;
            var key1Values = multiDictionary.Get(1);
            var key2Values = multiDictionary[2];

            // when
            keys.Clear();
            values.Clear();
            key1Values.Remove("Y");
            key2Values.Remove("B");

            // then
            Check.That(multiDictionary.Keys).HasSize(2);
            Check.That(multiDictionary.Keys).Contains(1, 2);

            Check.That(multiDictionary.Values).HasSize(5);
            Check.That(multiDictionary.Values).Contains("X", "Y", "Z", "A", "B");

            Check.That(multiDictionary.Get(1)).ContainsExactly("X", "Y", "Z");
            Check.That(multiDictionary.Get(2)).ContainsExactly("A", "B");
        }

        [Test]
        public void ShouldReturnEmptyListForNotExitingKey()
        {
            // given
            var multiDictionary = new ArrayListMultiDictionary<int, string>();

            // when
            var notExistingKeyValues = multiDictionary.Get(1);
            var notExistingKeyValues2 = multiDictionary[2];

            // then
            Check.That(notExistingKeyValues).IsEmpty();
            Check.That(notExistingKeyValues2).IsEmpty();
        }

        [Test]
        public void ShouldReplaceKeyValues()
        {
            // given
            var multiDictionary = new ArrayListMultiDictionary<string, int>();
            multiDictionary.PutAll("A", Lists.AsList(1, 2, 3, 4));

            // when
            var replaced = multiDictionary.ReplaceValues("A", Lists.AsList(5, 6));

            // when
            Check.That(multiDictionary.Count).IsEqualTo(2);
            Check.That(replaced).ContainsExactly(1, 2, 3, 4);
            Check.That(multiDictionary.Get("A")).ContainsExactly(5, 6);
        }

        [Test]
        public void ShouldRemoveDictionaryEntry()
        {
            // given
            var multiDictionary = new ArrayListMultiDictionary<string, int>();
            multiDictionary.PutAll("A", Lists.AsList(1, 2, 3, 4));

            // when
            var removed1 = multiDictionary.Remove("A", 3);
            var removed2 = multiDictionary.Remove("B", 1);
            var removed3 = multiDictionary.Remove("A", 5);

            // then
            Check.That(removed1).IsTrue();
            Check.That(removed2).IsFalse();
            Check.That(removed3).IsFalse();

            Check.That(multiDictionary.Count).IsEqualTo(3);
            Check.That(multiDictionary.Get("A")).ContainsExactly(1, 2, 4);
        }
    }
}
