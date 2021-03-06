﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NFluent;
using NiceTry;
using NUnit.Framework;
using Sharpility.Collections;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Tests.Extensions
{
    [TestFixture]
    public class CollectionExtensionsTests
    {
        [Test]
        public void ShouldExtendListByAddAllMethod()
        {
            // given
            IList<string> list = new List<string>();

            // when
            list.AddAll(Lists.AsList("A", "B", "C"));
            list.AddAll("D", "E", "F");

            // then
            Check.That(list).ContainsExactly("A", "B", "C", "D", "E", "F");
        }

        [Test]
        public void ShouldExtendSetByAddAllMethod()
        {
            // given
            ISet<string> set = new HashSet<string>();

            // when
            set.AddAll(Lists.AsList("A", "B", "C"));
            set.AddAll("D", "E", "F");

            // then
            Check.That(set).HasSize(6);
            Check.That(set).Contains("A", "B", "C", "D", "E", "F");
        }

        [Test]
        public void ShouldExtendListByRemoveAllMethod()
        {
            // given
            IList<string> list = new List<string> { "A", "A", "B", "C", "D", "E", "F" };

            // when
            list.RemoveAll(Lists.AsList("A", "B"));
            list.RemoveAll("E", "F");

            // then
            Check.That(list).ContainsExactly("C", "D");
        }

        [Test]
        public void ShouldExtendSetByRemoveAllMethod()
        {
            // given
            ISet<string> set = new HashSet<string> { "A", "B", "C", "D", "E", "F" };

            // when
            set.RemoveAll(Lists.AsList("A", "B"));
            set.RemoveAll("E", "F");

            // then
            Check.That(set).HasSize(2);
            Check.That(set).Contains("C", "D");
        }

        [Test]
        public void ShouldExtendListByConvertAllMethod()
        {
            // given
            IList<int> list = new List<int> {1, 2, 3, 4};
            Converter<int, string> converter = item => item.ToString();

            // when
            var result = list.ConvertAll(converter);

            // then
            Check.That(result).ContainsExactly("1", "2", "3", "4");
        }

        [Test]
        public void ShouldExtendSetByConvertAllMethod()
        {
            // given
            ISet<int> set = new HashSet<int> { 1, 2, 3, 4 };
            Converter<int, string> converter = item => item.ToString();

            // when
            var result = set.ConvertAll(converter);

            // then
            Check.That(result).HasSize(4);
            Check.That(result).Contains("1", "2", "3", "4");
        }

        [Test]
        public void ShouldExtenedArrayEnumerableByContainsMethod()
        {
            // given
            IEnumerable array = new object[] { "A", "B", 1 };

            // when
            var containsA = array.Contains("A");
            var containsOne = array.Contains(1);
            var containsC = array.Contains("C");

            Check.That(containsA).IsTrue();
            Check.That(containsOne).IsTrue();
            Check.That(containsC).IsFalse();
        }

        [Test]
        public void ShouldExtenedGenericEnumerableByContainsMethod()
        {
            // given
            IEnumerable set = new HashSet<string> {"A", "B"};

            // when
            var containsA = set.Contains("A");
            var containsOne = set.Contains(1);
            var containsC = set.Contains("C");

            Check.That(containsA).IsTrue();
            Check.That(containsOne).IsFalse();
            Check.That(containsC).IsFalse();
        }

        [Test]
        public void ShouldExtendArrayEnumerableByCountMethod()
        {
            // given
            IEnumerable array = new object[] { "A", "B", 1 };

            // when
            var count = CollectionExtensions.Count(array);

            // then
            Check.That(count).IsEqualTo(3);
        }

        [Test]
        public void ShouldExtendGenericEnumerableByCountMethod()
        {
            // given
            IEnumerable list = new List<int> {1, 2, 3};

            // when
            var count = CollectionExtensions.Count(list);

            // then
            Check.That(count).IsEqualTo(3);
        }

        [Test]
        public void ShouldExtenedEnumerableByItemTypeMethod()
        {
            // given
            IEnumerable array = new object[0];

            // when
            var itemType = array.ItemType();

            // then
            Check.That(itemType).IsEqualTo(typeof (object));
        }

        [Test]
        public void ShouldExtenedGenericArrayEnumerableByItemTypeMethod()
        {
            // given
            IEnumerable array = new string[0];

            // when
            var itemType = array.ItemType();

            // then
            Check.That(itemType).IsEqualTo(typeof(string));
        }

        [Test]
        public void ShouldExtenedGenericListEnumerableByItemTypeMethod()
        {
            // given
            IEnumerable list = new List<DateTime>();

            // when
            var itemType = list.ItemType();

            // then
            Check.That(itemType).IsEqualTo(typeof(DateTime));
        }

        [Test]
        public void ShouldExtendArrayEnumerableByContainsAllMethod()
        {
            // given
            IEnumerable array = new object[] {1, "A", "X"};

            // when
            var containsAll1 = array.ContainsAll(Lists.AsList("A", "X"));
            var containsAll2 = array.ContainsAll(Lists.AsList("A", "X", "B"));

            // then
            Check.That(containsAll1).IsTrue();
            Check.That(containsAll2).IsFalse();
        }

        [Test]
        public void ShouldExtendGenericEnumerableByContainsAllMethod()
        {
            // given
            IEnumerable array = new List<int> {1, 2, 3};

            // when
            var containsAll1 = array.ContainsAll(Lists.AsList(1, 2, 3));
            var containsAll2 = array.ContainsAll(Lists.AsList(2, 4));
            var containsAll3 = array.ContainsAll(new object[] {1, 3});
            var containsAll4 = array.ContainsAll(new object[] {"A", 1});

            // then
            Check.That(containsAll1).IsTrue();
            Check.That(containsAll2).IsFalse();
            Check.That(containsAll3).IsTrue();
            Check.That(containsAll4).IsFalse();
        }

        [Test]
        public void ShouldExtendExtendGenericEnumerableByIsEmptyMethod()
        {
            // given
            IEnumerable<int> emptyEnumerable = new List<int>();
            IEnumerable<int> notEmptyEnumerable = new List<int> {1};

            // when
            var isEmptyEnumerableEmpty = emptyEnumerable.IsEmpty();
            var isNotEmptyEnumerableEmpty = notEmptyEnumerable.IsEmpty();

            // then
            Check.That(isEmptyEnumerableEmpty).IsTrue();
            Check.That(isNotEmptyEnumerableEmpty).IsFalse();
        }

        [Test]
        public void ShouldExtendExtendNonGenericEnumerableByIsEmptyMethod()
        {
            // given
            IEnumerable emptyEnumerable = new object[0];
            IEnumerable notEmptyEnumerable = new object[] { 1 };

            // when
            var isEmptyEnumerableEmpty = emptyEnumerable.IsEmpty();
            var isNotEmptyEnumerableEmpty = notEmptyEnumerable.IsEmpty();

            // then
            Check.That(isEmptyEnumerableEmpty).IsTrue();
            Check.That(isNotEmptyEnumerableEmpty).IsFalse();
        }

        [Test]
        public void ShouldExtendExtendGenericEnumerableByIsNotEmptyMethod()
        {
            // given
            IEnumerable<int> emptyEnumerable = new List<int>();
            IEnumerable<int> notEmptyEnumerable = new List<int> { 1 };

            // when
            var isEmptyEnumerableNotEmpty = emptyEnumerable.IsNotEmpty();
            var isNotEmptyEnumerableNotEmpty = notEmptyEnumerable.IsNotEmpty();

            // then
            Check.That(isEmptyEnumerableNotEmpty).IsFalse();
            Check.That(isNotEmptyEnumerableNotEmpty).IsTrue();
        }

        [Test]
        public void ShouldExtendExtendNonGenericEnumerableByIsNotEmptyMethod()
        {
            // given
            IEnumerable emptyEnumerable = new object[0];
            IEnumerable notEmptyEnumerable = new object[] { 1 };

            // when
            var isEmptyEnumerableNotEmpty = emptyEnumerable.IsNotEmpty();
            var isNotEmptyEnumerableNotEmpty = notEmptyEnumerable.IsNotEmpty();

            // then
            Check.That(isEmptyEnumerableNotEmpty).IsFalse();
            Check.That(isNotEmptyEnumerableNotEmpty).IsTrue();
        }

        [Test]
        public void ShouldExtenedGenericEnumerableByIsSingletonMethod()
        {
            // given
            IEnumerable<int> emptyEnumerable = new List<int>();
            IEnumerable<string> singletonEnumerable = new List<string> { "X" };
            IEnumerable<string> enumerable = new List<string> { "A", "B" };

            // when
            var isEmptyEnumerableSingleton = emptyEnumerable.IsSingleton();
            var isSingletonEnumerableSingleton = singletonEnumerable.IsSingleton();
            var isEnumerableSingleton = enumerable.IsSingleton();

            // then
            Check.That(isEmptyEnumerableSingleton).IsFalse();
            Check.That(isSingletonEnumerableSingleton).IsTrue();
            Check.That(isEnumerableSingleton).IsFalse();
        }

        [Test]
        public void ShouldExtenedNonGenericEnumerableByIsSingletonMethod()
        {
            // given
            IEnumerable emptyEnumerable = new object[0];
            IEnumerable singletonEnumerable = new object[] {"A"};
            IEnumerable enumerable = new object[] {"A", "B"};

            // when
            var isEmptyEnumerableSingleton = emptyEnumerable.IsSingleton();
            var isSingletonEnumerableSingleton = singletonEnumerable.IsSingleton();
            var isEnumerableSingleton = enumerable.IsSingleton();

            // then
            Check.That(isEmptyEnumerableSingleton).IsFalse();
            Check.That(isSingletonEnumerableSingleton).IsTrue();
            Check.That(isEnumerableSingleton).IsFalse();
        }

        [Test]
        public void ShouldExtendSetByMinusMethod()
        {
            // given
            ISet<string> set = new HashSet<string> {"A", "B", "C", "D"};

            // when
            var result = set.Minus(Lists.AsList("B", "D", "X"));

            // then
            Check.That(result).HasSize(2);
            Check.That(result).Contains("A", "C");
        }

        [Test]
        public void ShouldExtendListByMinusMethod()
        {
            // given
            IList<string> list = new List<string> { "A", "B", "B", "C", "D" };

            // when
            var result = list.Minus(Lists.AsList("B", "D", "X"));

            // then
            Check.That(result).ContainsExactly("A", "C");
        }

        [Test]
        public void ShouldExtendImmutableSetByMinusMethod()
        {
            // given
            IImmutableSet<int> set = ImmutableHashSet.CreateRange(Lists.AsList(1, 2, 3));

            // when
            var result = set.Minus(Lists.AsList(1, 3, 4));

            // then
            Check.That(result).HasSize(1);
            Check.That(result).Contains(2);
        }

        [Test]
        public void ShouldExtendImmutableListByMinusMethod()
        {
            // given
            IImmutableList<int> list = ImmutableList.CreateRange(Lists.AsList(1, 2, 3, 4, 4));

            // when
            var result = list.Minus(Lists.AsList(1, 3, 4));

            // then
            Check.That(result).ContainsExactly(2);
        }

        [Test]
        public void ShouldExtendSetByPlusMethod()
        {
            // given
            ISet<string> set = new HashSet<string> {"A", "B"};

            // when
            var result = set.Plus(Lists.AsList("B", "C", "D"));

            // then
            Check.That(result).HasSize(4);
            Check.That(result).Contains("A", "B", "C", "D");
        }

        [Test]
        public void ShouldExtendListByPlusMethod()
        {
            // given
            IList<string> list = new List<string> { "A", "B" };

            // when
            var result = list.Plus(Lists.AsList("B", "C", "D"));

            // then
            Check.That(result).ContainsExactly("A", "B", "B", "C", "D");
        }

        [Test]
        public void ShouldExtendImmutableSetByPlusMethod()
        {
            // given
            IImmutableSet<string> set = ImmutableHashSet.CreateRange(Lists.AsList("A", "B"));

            // when
            var result = set.Plus(Lists.AsList("B", "C", "D"));

            // then
            Check.That(result).HasSize(4);
            Check.That(result).Contains("A", "B", "C", "D");
        }

        [Test]
        public void ShouldExtendImmutableListByPlusMethod()
        {
            // given
            IImmutableList<string> list = ImmutableList.CreateRange(Lists.AsList("A", "B"));

            // when
            var result = list.Plus(Lists.AsList("B", "C", "D"));

            // then
            Check.That(result).ContainsExactly("A", "B", "B", "C", "D");
        }

        [Test]
        public void ShouldExtenedLinkedListByFirstMethod()
        {
            // given
            ICollection<string> collection = new LinkedList<string>();
            collection.AddAll("A", "B", "C");

            // when
            var first = collection.First();

            // then
            Check.That(first).IsEqualTo("A");
        }

        [Test]
        public void ShouldExtenedListByFirstMethod()
        {
            // given
            ICollection<string> collection = new List<string>();
            collection.AddAll("A", "B", "C");

            // when
            var first = collection.First();

            // then
            Check.That(first).IsEqualTo("A");
        }

        [Test]
        public void ShouldExtenedLinkedListByLastMethod()
        {
            // given
            ICollection<string> collection = new LinkedList<string>();
            collection.AddAll("A", "B", "C");

            // when
            var first = collection.Last();

            // then
            Check.That(first).IsEqualTo("C");
        }

        [Test]
        public void ShouldExtenedListByLastMethod()
        {
            // given
            ICollection<string> collection = new List<string>();
            collection.AddAll("A", "B", "C");

            // when
            var first = collection.Last();

            // then
            Check.That(first).IsEqualTo("C");
        }

        [Test]
        public void ShouldExtendLinkedListByRemoveFirstMethod()
        {
            // given
            ICollection<string> collection = new LinkedList<string>();
            collection.AddAll("A", "B", "C");

            // then
            var removed = collection.RemoveFirst();

            // then
            Check.That(removed).IsEqualTo("A");
            Check.That(collection).ContainsExactly("B", "C");
        }

        [Test]
        public void ShouldExtendListByRemoveFirstMethod()
        {
            // given
            ICollection<string> collection = new List<string>();
            collection.AddAll("A", "B", "C");

            // then
            var removed = collection.RemoveFirst();

            // then
            Check.That(removed).IsEqualTo("A");
            Check.That(collection).ContainsExactly("B", "C");
        }

        [Test]
        public void ShouldThrowInvalidOperationWhenRemovingFirstFromLinkedList()
        {
            ICollection<string> collection = new LinkedList<string>();

            // then
            var result = Try.To(() => collection.RemoveFirst());

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidOperationException>();
        }

        [Test]
        public void ShouldThrowInvalidOperationWhenRemovingFirstFromList()
        {
            ICollection<string> collection = new List<string>();

            // then
            var result = Try.To(() => collection.RemoveFirst());

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidOperationException>();
        }

        [Test]
        public void ShouldExtendCollectionBySortMethod()
        {
            // given
            ICollection<string> collection = new List<string> {"D", "B", "A", "C"};

            // when
            collection.Sort();

            // then
            Check.That(collection).ContainsExactly("A", "B", "C", "D");
        }

        [Test]
        public void ShouldExtendCollectionBySortMethodWithComparer()
        {
            // given
            var comparer = Comparers.OfComparables<string>().Reverse();
            ICollection<string> collection = new List<string> { "D", "B", "A", "C" };

            // when
            collection.Sort(comparer);

            // then
            Check.That(collection).ContainsExactly("D", "C", "B", "A");
        }

        [Test]
        public void ShouldExtendLinkedListBySortMethod()
        {
            // given
            ICollection<string> collection = new LinkedList<string>();
            collection.AddAll("D", "B", "A", "C");

            // when
            collection.Sort();

            // then
            Check.That(collection).ContainsExactly("A", "B", "C", "D");
        }

        [Test]
        public void ShouldExtenedListByFindAllMethod()
        {
            // given
            IList<int> list = new List<int> {1, 2, 3, 4};
            Predicate<int> filter = item => item % 2 == 0;

            // when
            var filtered = list.FindAll(filter);

            // then
            Check.That(filtered).IsInstanceOf<List<int>>();
            Check.That(filtered).ContainsExactly(2, 4);
        }

        [Test]
        public void ShouldExtenedSetByFindAllMethod()
        {
            // given
            var set = Sets.AsSet(1, 2, 3, 4);
            Predicate<int> filter = item => item % 2 == 0;

            // when
            var filtered = set.FindAll(filter);

            // then
            Check.That(filtered).IsInstanceOf<HashSet<int>>();
            Check.That(filtered).ContainsExactly(2, 4);
        }

        [Test]
        public void ShouldExtenedLinkedListByFindAllMethod()
        {
            // given
            IEnumerable<int> list = Lists.AsLinkedList(Lists.AsList(1, 2, 3, 4));
            Predicate<int> filter = item => item % 2 == 0;

            // when
            var filtered = list.FindAll(filter);

            // then
            Check.That(filtered).IsInstanceOf<LinkedList<int>>();
            Check.That(filtered).ContainsExactly(2, 4);
        }
        
        [Test]
        public void ShouldExtenedEnumerableByToSetMethod()
        {
            // given
            IEnumerable<int> enumerable = Lists.AsList(1, 2, 3, 3, 3, 4, 5);

            // then
            var set = enumerable.ToSet();

            // then
            Check.That(set).HasSize(5);
            Check.That(set).Contains(1, 2, 3, 4, 5);
        }
        
        [Test]
        public void ShouldExtendEnumberableByToMultiDictionaryMethod()
        {
            // given
            IEnumerable<int> enumerable = Lists.AsList(1, 2, 3);
            Converter<int, string> keyConverter = element => element.ToString();
            Converter<int, int> valueConverter = element => element*2;

            // when
            var multiDictionary = enumerable.ToMultiDictionary(keyConverter, valueConverter);

            // then
            Check.That(multiDictionary).IsEqualTo(ArrayListMultiDictionary<string, int>.Of("1", 2, "2", 4, "3", 6));
        }

        [Test]
        public void ShouldExtendEnumberableByToMultiDictionaryMethodWithoutValueConverter()
        {
            // given
            IEnumerable<int> enumerable = Lists.AsList(1, 2, 3);
            Converter<int, string> keyConverter = element => element.ToString();

            // when
            var multiDictionary = enumerable.ToMultiDictionary(keyConverter);

            // then
            Check.That(multiDictionary).IsEqualTo(ArrayListMultiDictionary<string, int>.Of("1", 1, "2", 2, "3", 3));
        }

        [Test]
        public void ShouldExtenedEnumerableByDistinctElementCountMethod()
        {
            // given
            var set = Sets.AsSet("A", "B", "C");
            var list = Lists.AsList("A", "B", "B", "B", "C", "C", "D", "D", "D", "D");

            // when
            var setDistinctElementsCount = set.DistinctElementCount();
            var listDistinctElementsCount = list.DistinctElementCount();

            // then
            Check.That(setDistinctElementsCount).IsEqualTo(Dictionaries.Create("A", 1, "B", 1, "C", 1));
            Check.That(listDistinctElementsCount).IsEqualTo(Dictionaries.Create("A", 1, "B", 3, "C", 2, "D", 4));
        }

        [Test]
        [TestCaseSource("TopSliceCases")]
        public IList<string> ShouldReturnTopSliceOfList(IList<string> list, int size)
        {
            // when
            var result = list.TopSlice(size);

            // then
            return result;
        }

        private static IEnumerable<ITestCaseData> TopSliceCases()
        {
            var givenList = Lists.AsList("A", "B", "C");
            var givenSize = 3;
            var expectedSlice = Lists.AsList("A", "B", "C");
            yield return new TestCaseData(givenList, givenSize)
                .SetName(Strings.Format("Top {0} slice of {1} should return {2}", givenSize, givenList, expectedSlice))
                .Returns(expectedSlice);

            givenList = Lists.AsList("A", "B", "C");
            givenSize = 2;
            expectedSlice = Lists.AsList("B", "C");
            yield return new TestCaseData(givenList, givenSize)
                .SetName(Strings.Format("Top {0} slice of {1} should return {2}", givenSize, givenList, expectedSlice))
                .Returns(expectedSlice);

            givenList = Lists.AsList("A", "B", "C");
            givenSize = 1;
            expectedSlice = Lists.AsList("C");
            yield return new TestCaseData(givenList, givenSize)
                .SetName(Strings.Format("Top {0} slice of {1} should return {2}", givenSize, givenList, expectedSlice))
                .Returns(expectedSlice);

            givenList = Lists.AsList("A", "B", "C");
            givenSize = 5;
            expectedSlice = Lists.AsList("A", "B", "C");
            yield return new TestCaseData(givenList, givenSize)
                .SetName(Strings.Format("Top {0} slice of {1} should return {2}", givenSize, givenList, expectedSlice))
                .Returns(expectedSlice);

            givenList = Lists.AsList("A", "B", "C");
            givenSize = 0;
            expectedSlice = Lists.EmptyList<string>();
            yield return new TestCaseData(givenList, givenSize)
                .SetName(Strings.Format("Top {0} slice of {1} should return {2}", givenSize, givenList, expectedSlice))
                .Returns(expectedSlice);
        }

        [Test]
        [TestCaseSource("BottomSliceCases")]
        public IList<string> ShouldTrimCollectionRight(IList<string> list, int size)
        {
            // when
            var result = list.BottomSlice(size);

            // then
            return result;
        }

        private static IEnumerable<ITestCaseData> BottomSliceCases()
        {
            var givenList = Lists.AsList("A", "B", "C");
            var givenSize = 3;
            var expectedSlice = Lists.AsList("A", "B", "C");
            yield return new TestCaseData(givenList, givenSize)
                .SetName(Strings.Format("Bottom {0} slice of {1} should return {2}", givenSize, givenList, expectedSlice))
                .Returns(expectedSlice);

            givenList = Lists.AsList("A", "B", "C");
            givenSize = 2;
            expectedSlice = Lists.AsList("A", "B");
            yield return new TestCaseData(givenList, givenSize)
                .SetName(Strings.Format("Bottom {0} slice of {1} should return {2}", givenSize, givenList, expectedSlice))
                .Returns(expectedSlice);

            givenList = Lists.AsList("A", "B", "C");
            givenSize = 1;
            expectedSlice = Lists.AsList("A");
            yield return new TestCaseData(givenList, givenSize)
                .SetName(Strings.Format("Bottom {0} slice of {1} should return {2}", givenSize, givenList, expectedSlice))
                .Returns(expectedSlice);

            givenList = Lists.AsList("A", "B", "C");
            givenSize = 5;
            expectedSlice = Lists.AsList("A", "B", "C");
            yield return new TestCaseData(givenList, givenSize)
                .SetName(Strings.Format("Bottom {0} slice of {1} should return {2}", givenSize, givenList, expectedSlice))
                .Returns(expectedSlice);

            givenList = Lists.AsList("A", "B", "C");
            givenSize = 0;
            expectedSlice = Lists.EmptyList<string>();
            yield return new TestCaseData(givenList, givenSize)
                .SetName(Strings.Format("Bottom {0} slice of {1} should return {2}", givenSize, givenList, expectedSlice))
                .Returns(expectedSlice);
        }

        [Test]
        [TestCaseSource("SetLastListElementCases")]
        public IList<string> ShouldReplaceLastElementList(IList<string> elements, string element)
        {
            // given
            var list = new List<string>(elements);

            // when
            list.SetLast(element);

            // then
            return list;
        }

        private static IEnumerable<ITestCaseData> SetLastListElementCases()
        {
            const string element = "X";
            var givenList = Lists.AsList("A", "B", "C");
            var expected = Lists.AsList("A", "B", element);
            yield return new TestCaseData(givenList, element)
                .SetName(Strings.Format("Replace last item of {1} by {0} results {2}", element, givenList, expected))
                .Returns(expected);

            givenList = Lists.EmptyList<string>();
            expected = Lists.AsList(element);
            yield return new TestCaseData(givenList, element)
                .SetName(Strings.Format("Replace last item of {1} by {0} results {2}", element, givenList, expected))
                .Returns(expected);
        }

        [Test]
        [TestCaseSource("SetFirstListElementCases")]
        public IList<string> ShouldReplaceFirstElementList(IList<string> elements, string element)
        {
            // given
            var list = new List<string>(elements);

            // when
            list.SetFirst(element);

            // then
            return list;
        }

        private static IEnumerable<ITestCaseData> SetFirstListElementCases()
        {
            const string element = "X";
            var givenList = Lists.AsList("A", "B", "C");
            var expected = Lists.AsList(element, "B", "C");
            yield return new TestCaseData(givenList, element)
                .SetName(Strings.Format("Replace first item of {1} by {0} results {2}", element, givenList, expected))
                .Returns(expected);

            givenList = Lists.EmptyList<string>();
            expected = Lists.AsList(element);
            yield return new TestCaseData(givenList, element)
                .SetName(Strings.Format("Replace first item of {1} by {0} results {2}", element, givenList, expected))
                .Returns(expected);
        }

        [Test]
        public void ShouldConvertEnumerableToComparable()
        {
            // given
            IEnumerable<string> enumerable = new[] {"A", "B", "C"};

            // when
            var comparable = enumerable.ToComparable();

            // then
            Check.That(comparable).IsEqualTo(Lists.AsList("A", "B", "C"));
            Check.That(comparable.GetHashCode()).IsEqualTo(enumerable.ToComparable().GetHashCode());
            Check.That(comparable.ToString()).IsEqualTo("[A, B, C]");
        }

        [Test]
        public void ShouldConvertListToComparable()
        {
            // given
            IList<string> enumerable = new[] { "A", "B", "C" };

            // when
            var comparable = enumerable.ToComparable();

            // then
            Check.That(comparable).IsEqualTo(Lists.AsList("A", "B", "C"));
            Check.That(comparable.GetHashCode()).IsEqualTo(enumerable.ToComparable().GetHashCode());
            Check.That(comparable.ToString()).IsEqualTo("[A, B, C]");
        }

        [Test]
        public void ShouldConvertCollectionToComparable()
        {
            // given
            ICollection<string> enumerable = new[] { "A", "B", "C" };

            // when
            var comparable = enumerable.ToComparable();

            // then
            Check.That(comparable).IsEqualTo(Lists.AsList("A", "B", "C"));
            Check.That(comparable.GetHashCode()).IsEqualTo(enumerable.ToComparable().GetHashCode());
            Check.That(comparable.ToString()).IsEqualTo("[A, B, C]");
        }

        [Test]
        public void ShouldConvertSetToComparable()
        {
            // given
            ISet<string> enumerable = new HashSet<string>(new [] {"A", "B", "C"});

            // when
            var comparable = enumerable.ToComparable();

            // then
            Check.That(comparable).IsEqualTo(Lists.AsList("C", "B", "A"));
        }
    }
}
