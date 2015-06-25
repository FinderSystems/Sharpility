using System;
using System.Collections.Generic;
using NFluent;
using NiceTry;
using NUnit.Framework;
using Sharpility.Collections;
using Sharpility.Util;

namespace Sharpility.Tests.Collections
{
    [TestFixture]
    public class ImmutableMultiDictionaryTests
    {
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
