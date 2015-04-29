
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using NFluent;
using NUnit.Framework;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Tests.Extensions
{
    [TestFixture]
    public class DictionaryExtensionsTests
    {
        [Test]
        public void ShouldExtendDictionaryByPutMethod()
        {
            // given
            var dictionary = new Dictionary<int, string>();

            // when
            dictionary.Put(1, "1")
                .Put(2, "2")
                .Put(new KeyValuePair<int, string>(3, "3"));

            // then
            Check.That(dictionary).HasSize(3);
            Check.That(dictionary.Keys).Contains(1, 2, 3);
            Check.That(dictionary[1]).IsEqualTo("1");
            Check.That(dictionary[2]).IsEqualTo("2");
            Check.That(dictionary[3]).IsEqualTo("3");
        }

        [Test]
        public void ShouldExtendDictionaryByPutAllMethod()
        {
            // given
            var dictionary = new Dictionary<int, string>();

            // when
            dictionary.PutAll(Lists.AsList(Dictionaries.Entry(1, "1"), Dictionaries.Entry(2, "2")))
                .PutAll(Dictionaries.Entry(3, "3"), Dictionaries.Entry(4, "4"));

            // then
            Check.That(dictionary).HasSize(4);
            Check.That(dictionary.Keys).Contains(1, 2, 3, 4);
            Check.That(dictionary[1]).IsEqualTo("1");
            Check.That(dictionary[2]).IsEqualTo("2");
            Check.That(dictionary[3]).IsEqualTo("3");
            Check.That(dictionary[4]).IsEqualTo("4");
        }

        [Test]
        public void ShouldReturnNullObjectIfNotPresentInDictionary()
        {
            // given
            var dictionary = Dictionaries.Empty<string, string>();

            // when
            var value = dictionary.GetIfPresent("not exising");

            // then
            Check.That(value).IsNull();
        }

        [Test]
        public void ShouldReturnNullObjectIfNotPresentInReadOnlyDictionary()
        {
            // given
            IReadOnlyDictionary<string, object> dictionary = ImmutableDictionary.Create<string, object>();

            // when
            var value = dictionary.GetIfPresent("not exising");

            // then
            Check.That(value).IsNull();
        }

        [Test]
        public void ShouldReturnObjectIfIsPresentInDictionary()
        {
            // given
            var expectedValue = DateTime.Now;
            var dictionary = Dictionaries.Create<int, DateTime?>(1, expectedValue);

            // when
            var value = dictionary.GetIfPresent(1);

            // then
            Check.That(value).IsEqualTo(expectedValue);
        }

        [Test]
        public void ShouldReturnObjectIfIsPresentInReadOnlyDictionary()
        {
            // given
            var expectedValue = DateTime.Now;
            IReadOnlyDictionary<int, DateTime?> dictionary = Dictionaries.CreateImmutable<int, DateTime?>(1, expectedValue);

            // when
            var value = dictionary.GetIfPresent(1);

            // then
            Check.That(value).IsEqualTo(expectedValue);
        }

        [Test]
        public void ShouldExtendDictionaryByEntriesMethod()
        {
            // given
            var dictionary = Dictionaries.Create("A", "A1", "B", "B1", "C", "C1");

            // when
            var entries = dictionary.Entries();

            // then
            Check.That(entries).HasSize(3);
            Check.That(entries).Contains(
                Dictionaries.Entry("A", "A1"), 
                Dictionaries.Entry("B", "B1"), 
                Dictionaries.Entry("C", "C1"));
        }

        [Test]
        public void ShouldExtendReadOnlyDictionaryByEntriesMethod()
        {
            // given
            var dictionary = Dictionaries.CreateImmutable("A", "A1", "B", "B1", "C", "C1");

            // when
            var entries = dictionary.Entries();

            // then
            Check.That(entries).HasSize(3);
            Check.That(entries).Contains(
                Dictionaries.Entry("A", "A1"),
                Dictionaries.Entry("B", "B1"),
                Dictionaries.Entry("C", "C1"));
        }
    }
}
