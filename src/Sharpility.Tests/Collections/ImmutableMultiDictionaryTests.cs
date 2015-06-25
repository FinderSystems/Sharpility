using System;
using System.Collections.Generic;
using System.Linq;
using NFluent;
using NiceTry;
using NUnit.Framework;
using Sharpility.Collections;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Tests.Collections
{
    [TestFixture]
    public class ImmutableMultiDictionaryTests
    {
        [Test, TestCaseSource("ImmutableMultiDictionaries")]
        public void ShouldReturnImmutableMultiDictionaryEntries(ImmutableMultiDictionaryCreator creator)
        {
            // given
            var entires = Lists.AsList(
                Dictionaries.Entry(1, "A"),
                Dictionaries.Entry(1, "B"),
                Dictionaries.Entry(1, "C"),
                Dictionaries.Entry(2, "A"),
                Dictionaries.Entry(3, "C"));
            var immutableMultiDictionary = creator.Create(entires.ToArray());

            // when
            var result = immutableMultiDictionary.Entries;

            // then
            Check.That(immutableMultiDictionary.Count).IsEqualTo(entires.Count);
            Check.That(result).HasSize(entires.Count).And.Contains(entires.ToArray());
        }

        [Test, TestCaseSource("ImmutableMultiDictionaries")]
        public void ShouldReturnImmutableMultiDictionaryMultiEntries(ImmutableMultiDictionaryCreator creator)
        {
            // given
            var entires = Lists.AsList(
                Dictionaries.Entry(1, "A"),
                Dictionaries.Entry(1, "B"),
                Dictionaries.Entry(1, "C"),
                Dictionaries.Entry(2, "A"),
                Dictionaries.Entry(3, "C"));
            var immutableMultiDictionary = creator.Create(entires.ToArray());

            // when
            var result = immutableMultiDictionary.MultiEntries;

            // then
            var mutliEntriesDictionary = result.ToDictionary(element => element.Key, element => element.Value);
            Check.That(immutableMultiDictionary.Count).IsEqualTo(entires.Count);
            Check.That(result).HasSize(3);
            Check.That(mutliEntriesDictionary.Keys).HasSize(3).And.Contains(1, 2, 3);
            Check.That(mutliEntriesDictionary[1]).HasSize(3).And.Contains("A", "B", "C");
            Check.That(mutliEntriesDictionary[2]).HasSize(1).And.Contains("A");
            Check.That(mutliEntriesDictionary[3]).HasSize(1).And.Contains("C");
        }

        [Test, TestCaseSource("ImmutableMultiDictionaries")]
        public void ShouldReturnEmptyCollectionForNotExistingKey(ImmutableMultiDictionaryCreator creator)
        {
            // given
            var immutableMultiDictionary = creator.Create<string, string>();

            // when
            var keyValues = immutableMultiDictionary["test"];
            var keyValues2 = immutableMultiDictionary.Get("test");

            // then
            Check.That(keyValues).IsEmpty();
            Check.That(keyValues2).IsEmpty();
            Check.That(keyValues is IReadOnlyCollection<string>).IsTrue();
            Check.That(keyValues2 is IReadOnlyCollection<string>).IsTrue();
        }

        [Test, TestCaseSource("ImmutableMultiDictionaries")]
        public void ShouldReturnImmutableMultiDictionaryValuesForKey(ImmutableMultiDictionaryCreator creator)
        {
            // given
            var immutableMultiDictionary = creator.Create
                (Dictionaries.Entry("A", "B"), 
                Dictionaries.Entry("A", "C"),
                Dictionaries.Entry("A", "D"));

            // when
            var keyValues = immutableMultiDictionary["A"];
            var keyValues2 = immutableMultiDictionary.Get("A");

            // then
            Check.That(keyValues).HasSize(3).And.Contains("B", "C", "D");
            Check.That(keyValues2).HasSize(3).And.Contains("B", "C", "D");

            Check.That(keyValues is IReadOnlyCollection<string>).IsTrue();
            Check.That(keyValues2 is IReadOnlyCollection<string>).IsTrue();
        }

        [Test, TestCaseSource("ImmutableMultiDictionaries")]
        public void ShouldReturImmutableMultiDictionaryValues(ImmutableMultiDictionaryCreator creator)
         {
            // given
            var immutableMultiDictionary = creator.Create(ArrayListMultiDictionary<int, string>.Of(
                 1, "A", 1, "B", 1, "C", 
                 2, "A", 3, "C", 
                 3, "X").Entries.ToArray());

            // when
            var values = immutableMultiDictionary.Values;

            // then
            Check.That(immutableMultiDictionary.Count).IsEqualTo(6);
            Check.That(values).HasSize(6).And.Contains("A", "B", "C", "X");
            Check.That(Objects.Equal(values.DistinctElementCount(), 
                Dictionaries.Create("A", 2, "B", 1, "C", 2, "X", 1)))
                .IsTrue();
            Check.That(values is IReadOnlyCollection<string>).IsTrue();
        }

        [Test, TestCaseSource("ImmutableMultiDictionaries")]
        public void ShouldPreventImmutableMultiDictionaryPut(ImmutableMultiDictionaryCreator creator)
        {
            // given
            var immutableMultiDictionary = creator.Create<int, string>();

            // when
            var result = Try.To(() => immutableMultiDictionary.Put(1, "A"));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidOperationException>();
            Check.That(immutableMultiDictionary).IsEqualTo(creator.Create<int, string>());
        }

        [Test, TestCaseSource("ImmutableMultiDictionaries")]
        public void ShouldPreventImmutableMultiDictionaryPutAllEntries(ImmutableMultiDictionaryCreator creator)
        {
            // given
            var immutableMultiDictionary = creator.Create<int, string>();
            var entries = Dictionaries.Create(1, "A", 2, "B");

            // when
            var result = Try.To(() => immutableMultiDictionary.PutAll(entries));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidOperationException>();
            Check.That(immutableMultiDictionary).IsEqualTo(creator.Create<int, string>());
        }

        [Test, TestCaseSource("ImmutableMultiDictionaries")]
        public void ShouldPreventImmutableMultiDictionaryPutAllMultiEntries(ImmutableMultiDictionaryCreator creator)
        {
            // given
            var immutableMultiDictionary = creator.Create<int, string>();
            var entries = ArrayListMultiDictionary<int, string>.Of(1, "A", 1, "B", 2, "A");

            // when
            var result = Try.To(() => immutableMultiDictionary.PutAll(entries));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidOperationException>();
            Check.That(immutableMultiDictionary).IsEqualTo(creator.Create<int, string>());
        }

        [Test, TestCaseSource("ImmutableMultiDictionaries")]
        public void ShouldPreventImmutableMultiDictionaryPutAllKeyEntries(ImmutableMultiDictionaryCreator creator)
        {
            // given
            var immutableMultiDictionary = creator.Create<int, string>();

            // when
            var result = Try.To(() => immutableMultiDictionary.PutAll(1, Lists.AsList("A", "B", "C")));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidOperationException>();
            Check.That(immutableMultiDictionary).IsEqualTo(creator.Create<int, string>());
        }

        [Test, TestCaseSource("ImmutableMultiDictionaries")]
        public void ShouldPreventImmutableMultiDictionaryKeyRemove(ImmutableMultiDictionaryCreator creator)
        {
            // given
            var immutableMultiDictionary = creator.Create(Dictionaries.Entry(1, "A"));

            // when
            var result = Try.To(() => immutableMultiDictionary.Remove(1));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidOperationException>();
            Check.That(immutableMultiDictionary).IsEqualTo(creator.Create(Dictionaries.Entry(1, "A")));
        }

        [Test, TestCaseSource("ImmutableMultiDictionaries")]
        public void ShouldPreventImmutableMultiDictionaryKeyEntriesRemove(ImmutableMultiDictionaryCreator creator)
        {
            // given
            var immutableMultiDictionary = creator.Create(Dictionaries.Entry(1, "A"));

            // when
            var result = Try.To(() => immutableMultiDictionary.RemoveAll(1));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidOperationException>();
            Check.That(immutableMultiDictionary).IsEqualTo(creator.Create(Dictionaries.Entry(1, "A")));
        }

        [Test, TestCaseSource("ImmutableMultiDictionaries")]
        public void ShouldPreventImmutableMultiDictionaryEntryRemove(ImmutableMultiDictionaryCreator creator)
        {
            // given
            var immutableMultiDictionary = creator.Create(Dictionaries.Entry(1, "A"), Dictionaries.Entry(1, "B"));

            // when
            var result = Try.To(() => immutableMultiDictionary.Remove(1, "B"));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidOperationException>();
            Check.That(immutableMultiDictionary).IsEqualTo(creator.Create(Dictionaries.Entry(1, "A"), Dictionaries.Entry(1, "B")));
        }

        [Test, TestCaseSource("ImmutableMultiDictionaries")]
        public void ShouldPreventImmutableMultiDictionaryValuesReplacement(ImmutableMultiDictionaryCreator creator)
        {
            // given
            var immutableMultiDictionary = creator.Create(
                Dictionaries.Entry(1, "A"), 
                Dictionaries.Entry(1, "B"), 
                Dictionaries.Entry(1, "C"));

            // when
            var result = Try.To(() => immutableMultiDictionary.ReplaceValues(1, Lists.AsList("X", "Y")));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidOperationException>();
            Check.That(immutableMultiDictionary).IsEqualTo(creator.Create(
                Dictionaries.Entry(1, "A"),
                Dictionaries.Entry(1, "B"),
                Dictionaries.Entry(1, "C")));
        }

        [Test, TestCaseSource("ImmutableMultiDictionaries")]
        public void ShouldPreventImmutableMultiDictionaryClear(ImmutableMultiDictionaryCreator creator)
        {
            // given
            var immutableMultiDictionary = creator.Create(Dictionaries.Entry(1, "A"));

            // when
            var result = Try.To(() => immutableMultiDictionary.Clear());

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidOperationException>();
            Check.That(immutableMultiDictionary).IsEqualTo(creator.Create(Dictionaries.Entry(1, "A")));
        }

        private static IEnumerable<ImmutableMultiDictionaryCreator> ImmutableMultiDictionaries()
        {
            yield return new ListImmutableMultiDictionaryCreator();

            yield return new SetImmutableMultiDictionaryCreator();
        } 

        public interface ImmutableMultiDictionaryCreator
        {
            ImmutableMultiDictionary<TKey, TValue> Create<TKey, TValue>(params KeyValuePair<TKey, TValue>[] entries);
        }

        private sealed class ListImmutableMultiDictionaryCreator : ImmutableMultiDictionaryCreator
        {
            public ImmutableMultiDictionary<TKey, TValue> Create<TKey, TValue>(params KeyValuePair<TKey, TValue>[] entries)
            {
                var builder = ImmutableListMultiDictionary<TKey, TValue>.Builder();
                builder.PutAll(entries);
                return builder.Build();
            }

            public override string ToString()
            {
                return "ImmutableListMultiDictionary";
            }
        }

        private sealed class SetImmutableMultiDictionaryCreator : ImmutableMultiDictionaryCreator
        {
            public ImmutableMultiDictionary<TKey, TValue> Create<TKey, TValue>(params KeyValuePair<TKey, TValue>[] entries)
            {
                var builder = ImmutableSetMultiDictionary<TKey, TValue>.Builder();
                builder.PutAll(entries);
                return builder.Build();
            }

            public override string ToString()
            {
                return "SetImmutableMultiDictionaryCreator";
            }
        }
    }
}
