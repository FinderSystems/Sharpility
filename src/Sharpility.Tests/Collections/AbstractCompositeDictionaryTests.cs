using System.Collections.Generic;
using NFluent;
using NUnit.Framework;
using Sharpility.Collections;
using Sharpility.Util;

namespace Sharpility.Tests.Collections
{
    [TestFixture]
    public class AbstractCompositeDictionaryTests
    {
        [Test, TestCaseSource("CompositeDictionaryCases")]
        public void ShouldPutValuesIntoCompositeDictionary(CompositeDictionaryCreator creator)
        {
            // given
            var compositeDictionary = creator.Create<string, int, string>();

            // when
            compositeDictionary.Put("A", 1, "A1");
            compositeDictionary.Put("A", 2, "A2");
            compositeDictionary.Put("A", 3, "A3");
            compositeDictionary.Put("B", 1, "B1");
            compositeDictionary.PutAll(Dictionaries.Create(
                "C", Dictionaries.Create(1, "C1", 2, "C2"), 
                "D", Dictionaries.Create(1, "D1", 2, "D2")));

            // then
            Check.That(compositeDictionary.Count).IsEqualTo(8);
            Check.That(compositeDictionary.PrimaryKeysCount).IsEqualTo(4);
            Check.That(compositeDictionary.IsEmpty).IsFalse();
            Check.That(compositeDictionary.Get("A", 1)).IsEqualTo("A1");
            Check.That(compositeDictionary[CompositeKeys.Of("A", 2)]).IsEqualTo("A2");
            Check.That(compositeDictionary.Get(CompositeKeys.Of("A", 3))).IsEqualTo("A3");
            Check.That(compositeDictionary.Get("B", 1)).IsEqualTo("B1");
            Check.That(compositeDictionary.Get("C", 1)).IsEqualTo("C1");
            Check.That(compositeDictionary.Get("C", 2)).IsEqualTo("C2");
            Check.That(compositeDictionary.Get("D", 1)).IsEqualTo("D1");
            Check.That(compositeDictionary.Get("D", 2)).IsEqualTo("D2");

            Check.That(compositeDictionary.Values).HasSize(8).And
                .Contains("A1", "A2", "A3", "B1", "C1", "C2", "D1", "D2");

            Check.That(compositeDictionary.Entries).HasSize(8).And.Contains(
                Dictionaries.Entry("A", Dictionaries.Entry(1, "A1")),
                Dictionaries.Entry("A", Dictionaries.Entry(2, "A2")),
                Dictionaries.Entry("A", Dictionaries.Entry(3, "A3")),
                Dictionaries.Entry("B", Dictionaries.Entry(1, "B1")),
                Dictionaries.Entry("C", Dictionaries.Entry(1, "C1")),
                Dictionaries.Entry("C", Dictionaries.Entry(2, "C2")),
                Dictionaries.Entry("D", Dictionaries.Entry(1, "D1")),
                Dictionaries.Entry("D", Dictionaries.Entry(2, "D2")));

            Check.That(compositeDictionary.PrimaryKeys).HasSize(4).And
                .Contains("A", "B", "C", "D");
            Check.That(compositeDictionary.SecondaryKeys).HasSize(3).And
                .Contains(1, 2, 3);

            Check.That(compositeDictionary.PrimaryKeyEntries("A")).HasSize(3).And
                .Contains(Dictionaries.Entry(1, "A1"), Dictionaries.Entry(2, "A2"), Dictionaries.Entry(3, "A3"));
            Check.That(compositeDictionary.PrimaryKeyEntries("B")).HasSize(1).And
                .Contains(Dictionaries.Entry(1, "B1"));
            Check.That(compositeDictionary.PrimaryKeyEntries("C")).HasSize(2).And
                .Contains(Dictionaries.Entry(1, "C1"), Dictionaries.Entry(2, "C2"));
            Check.That(compositeDictionary.PrimaryKeyEntries("D")).HasSize(2).And
                .Contains(Dictionaries.Entry(1, "D1"), Dictionaries.Entry(2, "D2"));
            Check.That(compositeDictionary.PrimaryKeyValues("A")).HasSize(3).And.Contains("A1", "A2", "A3");
            Check.That(compositeDictionary.PrimaryKeyValues("B")).HasSize(1).And.Contains("B1");
            Check.That(compositeDictionary.PrimaryKeyValues("C")).HasSize(2).And.Contains("C1", "C2");
            Check.That(compositeDictionary.PrimaryKeyValues("D")).HasSize(2).And.Contains("D1", "D2");

            Check.That(compositeDictionary.SecondaryKeyEntries(1)).HasSize(4).And
                .Contains(Dictionaries.Entry("A", "A1"), Dictionaries.Entry("B", "B1"), 
                    Dictionaries.Entry("C", "C1"), Dictionaries.Entry("D", "D1"));
            Check.That(compositeDictionary.SecondaryKeyEntries(2)).HasSize(3).And
                .Contains(Dictionaries.Entry("A", "A2"), Dictionaries.Entry("C", "C2"), Dictionaries.Entry("D", "D2"));
            Check.That(compositeDictionary.SecondaryKeyEntries(3)).HasSize(1).And
                .Contains(Dictionaries.Entry("A", "A3"));
            Check.That(compositeDictionary.SecondaryKeyValues(1)).HasSize(4).And.Contains("A1", "B1", "C1", "D1");
            Check.That(compositeDictionary.SecondaryKeyValues(2)).HasSize(3).And.Contains("A2", "C2", "D2");
            Check.That(compositeDictionary.SecondaryKeyValues(3)).HasSize(1).And.Contains("A3");
        }

        [Test, TestCaseSource("CompositeDictionaryCases")]
        public void ShouldReturnNullForNotExitingKey(CompositeDictionaryCreator creator)
        {
            // given
            var compositeDictionary = creator.Create<int, int, string>();

            // when
            var value1 = compositeDictionary.Get(1, 1);
            var value2 = compositeDictionary[CompositeKeys.Of(1, 1)];

            // then
            Check.That(value1).IsNull();
            Check.That(value2).IsNull();
            Check.That(compositeDictionary.ContainsKey(1, 1)).IsFalse();
            Check.That(compositeDictionary.ContainsKey(CompositeKeys.Of(1, 1))).IsFalse();
        }

        private static IEnumerable<CompositeDictionaryCreator> CompositeDictionaryCases()
        {
            yield return new HashCompositeDictionaryCreator();
        } 

        public interface CompositeDictionaryCreator
        {
            CompositeDictionary<TPrimaryKey, TSecondaryKey, TValue> Create<TPrimaryKey, TSecondaryKey, TValue>();
        }

        private class HashCompositeDictionaryCreator : CompositeDictionaryCreator
        {
            public CompositeDictionary<TPrimaryKey, TSecondaryKey, TValue> Create<TPrimaryKey, TSecondaryKey, TValue>()
            {
                return new HashCompositeDictionary<TPrimaryKey, TSecondaryKey, TValue>();
            }

            public override string ToString()
            {
                return "HashCompositeDictionary";
            }
        }
    }
}
