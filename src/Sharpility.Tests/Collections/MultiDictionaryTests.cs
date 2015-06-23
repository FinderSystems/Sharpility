using System.Collections.Generic;
using System.Linq;
using NFluent;
using NUnit.Framework;
using Sharpility.Collections;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Tests.Collections
{
    [TestFixture]
    public class MultiDictionaryTests
    {
        [Test, TestCaseSource("MultiDictionaryCases")]
        public void ShouldPutMultipleValuesForKey(MultiDictionaryContext context)
        {
            // given
            var multiDictionary = context.Create<int, string>();

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
            Check.That(multiDictionary.ValuesCount(1)).IsEqualTo(3);

            Check.That(multiDictionary.Get(2)).ContainsExactly("A");
            Check.That(multiDictionary.ValuesCount(2)).IsEqualTo(1);

            Check.That(multiDictionary[3]).ContainsExactly("X", "Y", "Z");
            Check.That(multiDictionary.ValuesCount(3)).IsEqualTo(3);
        }

        [Test, TestCaseSource("MultiDictionaryCases")]
        public void ShouldPutEntriesFromDictionary(MultiDictionaryContext context)
        {
            // given
            var multiDictionary = context.Create<int, string>();
            multiDictionary.Put(1, "B");
            var dictionary = Dictionaries.Create(1, "A", 2, "B", 3, "C");

            // when
            multiDictionary.PutAll(dictionary);

            // then
            Check.That(multiDictionary.Count).IsEqualTo(4);
            Check.That(multiDictionary.Values).HasSize(4);
            Check.That(multiDictionary.Values).Contains("A", "B", "B", "C");
            Check.That(multiDictionary[1]).HasSize(2);
            Check.That(multiDictionary[1]).Contains("A", "B");
            Check.That(multiDictionary[2]).ContainsExactly("B");
            Check.That(multiDictionary[3]).ContainsExactly("C");
        }

        [Test, TestCaseSource("MultiDictionaryCases")]
        public void ShouldPutEntriesFromMultiDictionary(MultiDictionaryContext context)
        {
            // given
            var multiDictionary = context.Create<int, string>();
            multiDictionary.PutAll(1, context.CollectionOf("E", "F"));
            multiDictionary.Put(3, "R");
            var anotherMultiDictionary = new ArrayListMultiDictionary<int, string>();
            anotherMultiDictionary.PutAll(1, Lists.AsList("A", "B", "C"));
            anotherMultiDictionary.PutAll(2, Lists.AsList("X", "Y", "Z"));
            anotherMultiDictionary.PutAll(3, Lists.AsList("Q", "W", "E"));

            // when
            multiDictionary.PutAll(anotherMultiDictionary);

            // then
            Check.That(multiDictionary.Count).IsEqualTo(12);
            Check.That(multiDictionary.Values).HasSize(12);
            Check.That(multiDictionary.Values).Contains(
                "A", "B", "C", "E", "F", "Q", "W", "E", "R", "X", "Y", "Z");
            Check.That(multiDictionary[1]).HasSize(5).And.Contains("A", "B", "C", "E", "F");
            Check.That(multiDictionary[2]).HasSize(3).And.Contains("X", "Y", "Z");
            Check.That(multiDictionary[3]).HasSize(4).And.Contains("Q", "W", "E", "R");
        }

        [Test, TestCaseSource("MultiDictionaryCases")]
        public void ShouldReturnEntries(MultiDictionaryContext context)
        {
            // given
            var multiDictionary = context.Create<int, string>();
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

        [Test, TestCaseSource("MultiDictionaryCases")]
        public void ShouldReturnMultiEntries(MultiDictionaryContext context)
        {
            // given
            var multiDictionary = context.Create<int, string>();
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

            Check.That(entry1.Value).IsEqualTo(context.CollectionOf("A", "B", "C"));
            Check.That(entry2.Value).IsEqualTo(context.CollectionOf("A"));
            Check.That(entry3.Value).IsEqualTo(context.CollectionOf("X", "Y", "Z"));
        }

        [Test, TestCaseSource("MultiDictionaryCases")]
        public void RetunedCollectionsShouldNotAffectDictionary(MultiDictionaryContext context)
        {
            // given
            var multiDictionary = context.Create<int, string>();
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

        [Test, TestCaseSource("MultiDictionaryCases")]
        public void ShouldReturnEmptyListForNotExitingKey(MultiDictionaryContext context)
        {
            // given
            var multiDictionary = context.Create<int, string>();

            // when
            var notExistingKeyValues = multiDictionary.Get(1);
            var notExistingKeyValues2 = multiDictionary[2];

            // then
            Check.That(notExistingKeyValues).IsEmpty();
            Check.That(notExistingKeyValues2).IsEmpty();
        }

        [Test, TestCaseSource("MultiDictionaryCases")]
        public void ShouldReplaceKeyValues(MultiDictionaryContext context)
        {
            // given
            var multiDictionary = context.Create<string, int>();
            multiDictionary.PutAll("A", Lists.AsList(1, 2, 3, 4));

            // when
            var replaced = multiDictionary.ReplaceValues("A", Lists.AsList(5, 6));

            // when
            Check.That(multiDictionary.Count).IsEqualTo(2);
            Check.That(replaced).ContainsExactly(1, 2, 3, 4);
            Check.That(multiDictionary.Get("A")).ContainsExactly(5, 6);
        }

        [Test, TestCaseSource("MultiDictionaryCases")]
        public void ShouldRemoveDictionaryEntry(MultiDictionaryContext context)
        {
            // given
            var multiDictionary = context.Create<string, int>();
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

        [Test, TestCaseSource("MultiDictionaryCases")]
        public void ShouldRemoveDictionaryKeyWhenAllValuesRemoved(MultiDictionaryContext context)
        {
            // given
            var multiDictionary = context.Create<string, int>();
            multiDictionary.PutAll("A", Lists.AsList(1, 2));

            // when
            multiDictionary.Remove("A", 1);
            multiDictionary.Remove("A", 2);

            // then
            Check.That(multiDictionary.Count).IsEqualTo(0);
            Check.That(multiDictionary.Keys).IsEmpty();
            Check.That(multiDictionary.Values).IsEmpty();
            Check.That(multiDictionary["A"]).IsEmpty();
        }

        [Test, TestCaseSource("MultiDictionaryCases")]
        public void ShouldRemoveDictionaryKey(MultiDictionaryContext context)
        {
            // given
            var multiDictionary = context.Create<string, int>();
            multiDictionary.PutAll("A", context.CollectionOf(1, 2));
            multiDictionary.Put("B", 1);

            // when
            var removedA = multiDictionary.Remove("A");
            var removedC = multiDictionary.Remove("C");

            // then
            Check.That(removedA).IsTrue();
            Check.That(removedC).IsFalse();

            Check.That(multiDictionary.Count).IsEqualTo(1);
            Check.That(multiDictionary.Values).HasSize(1);
            Check.That(multiDictionary.Values).Contains(1);
            Check.That(multiDictionary.Keys).HasSize(1);
            Check.That(multiDictionary.Keys).Contains("B");
        }

        [Test, TestCaseSource("MultiDictionaryCases")]
        public void ShouldClearMultiDictionary(MultiDictionaryContext context)
        {
            // given
            var multiDictionary = context.Create<string, string>();
            multiDictionary.PutAll("A", Lists.AsList("A1", "A2", "A3"));
            multiDictionary.Put("B", "B1");
            multiDictionary.Put("C", "C1");

            // when
            multiDictionary.Clear();

            // then
            Check.That(multiDictionary.Count).IsEqualTo(0);
            Check.That(multiDictionary.Values).IsEmpty();
            Check.That(multiDictionary.Keys).IsEmpty();
            Check.That(multiDictionary.IsEmpty).IsTrue();
        }

        [Test, TestCaseSource("MultiDictionaryCases")]
        public void ShouldConvertMultiDictionaryToDictionary(MultiDictionaryContext context)
        {
            // given
            var multiDictionary = context.Create<string, string>();
            multiDictionary.PutAll("A", Lists.AsList("A1", "A2", "A3"));
            multiDictionary.Put("B", "B1");
            multiDictionary.Put("C", "C1");

            // when
            var dictionary = multiDictionary.ToDictionary();

            // then
            Check.That(Objects.Equal(dictionary, Dictionaries.Create(
                "A", context.CollectionOf("A1", "A2", "A3"),
                "B", context.CollectionOf("B1"),
                "C", context.CollectionOf("C1"))))
                .IsTrue();
        }

        private static IEnumerable<MultiDictionaryContext> MultiDictionaryCases()
        {
            yield return new ArrayListMultiDictionaryContext();

            yield return new HashSetMultiDictionaryContext();

            yield return new LinkedListMultiDictionaryContext();
        } 

        public interface MultiDictionaryContext
        {
            MultiDictionary<TKey, TValue> Create<TKey, TValue>();

            ICollection<TValue> CollectionOf<TValue>(params TValue[] values);
        }

        private class ArrayListMultiDictionaryContext : MultiDictionaryContext
        {
            public MultiDictionary<TKey, TValue> Create<TKey, TValue>()
            {
                return new ArrayListMultiDictionary<TKey, TValue>();
            }

            public ICollection<TValue> CollectionOf<TValue>(params TValue[] values)
            {
                return new List<TValue>(values);
            }

            public override string ToString()
            {
                return "ArrayListMultiDictionary";
            }
        }

        private class HashSetMultiDictionaryContext : MultiDictionaryContext
        {
            public MultiDictionary<TKey, TValue> Create<TKey, TValue>()
            {
                return new HashSetMultiDictionary<TKey, TValue>();
            }

            public ICollection<TValue> CollectionOf<TValue>(params TValue[] values)
            {
                return new HashSet<TValue>(values);
            }

            public override string ToString()
            {
                return "HashSetMultiDictionary";
            }
        }

        private class LinkedListMultiDictionaryContext : MultiDictionaryContext
        {
            public MultiDictionary<TKey, TValue> Create<TKey, TValue>()
            {
                return new LinkedListMultiDictionary<TKey, TValue>();
            }

            public ICollection<TValue> CollectionOf<TValue>(params TValue[] values)
            {
                var collection = new LinkedList<TValue>();
                collection.AddAll(values);
                return collection;
            }

            public override string ToString()
            {
                return "LinkedListMultiDictionary";
            }
        }
    }
}
