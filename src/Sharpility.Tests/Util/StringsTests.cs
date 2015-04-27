using System.Collections.Generic;
using NFluent;
using NUnit.Framework;
using Sharpility.Collections;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class StringsTests
    {
        [Test]
        public void ShouldReturnNullOnLimitedStringFromNull()
        {
            // given
            const string value = null;
            const int length = 100;

            // when
            var result = Strings.LimitedString(value, length);

            // then
            Check.That(result).IsNull();
        }

        [Test]
        public void ShouldReturnGivenStringWhenLengthIsLowerThanLimit()
        {
            // given
            const string value = "ala ma kota";
            const int length = 500;

            // when
            var result = Strings.LimitedString(value, length);

            // then
            Check.That(result).IsEqualTo(value);
        }

        [Test]
        public void ShouldReturnStringLimitedToLength()
        {
            // given
            const string value = "ala ma kota";
            const int length = 3;

            // when
            var result = Strings.LimitedString(value, length);

            // then
            Check.That(result).IsEqualTo("ala");
        }

        [Test]
        public void ShouldReturnStringLength()
        {
            // given
            const string value = "test";

            // when
            var length = Strings.Length(value);

            // then
            Check.That(length).IsEqualTo(value.Length);
        }

        [Test]
        public void ShouldReturnZeroLengthForNullString()
        {
            // given
            const string value = null;

            // when
            var length = Strings.Length(value);

            // then
            Check.That(length).IsEqualTo(0);
        }
        
        [Test]
        public void ShouldGenerateToStringForString()
        {
            // given
            const string value = "test";

            // when
            var toString = Strings.ToString(value);

            // then
            Check.That(toString).IsEqualTo(value);
        }

        [Test]
        public void ShouldGenerateToStringForCollection()
        {
            // given
            IEnumerable<int?> collection = new List<int?> { 1, 2, null, 3 };

            // when
            var toString = Strings.ToString(collection);

            // then
            Check.That(toString).IsEqualTo("[1, 2, null, 3]");
        }

        [Test]
        public void ShouldGenerateToStringForNullCollection()
        {
            // given
            IEnumerable<int?> collection = null;

            // when
            // ReSharper disable once ExpressionIsAlwaysNull
            var toString = Strings.ToString(collection);

            // then
            Check.That(toString).IsEqualTo("null");
        }

        [Test]
        public void ShouldGenerateToStringForEmptyCollection()
        {
            // given
            IEnumerable<int> collection = Lists.EmptyList<int>();

            // when
            var toString = Strings.ToString(collection);

            // then
            Check.That(toString).IsEqualTo("[]");
        }

        [Test]
        public void ShouldGenerateToStringForDictionary()
        {
            // given
            IDictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary["A"] = 1;
            dictionary["B"] = 2;
            dictionary["C"] = 3;

            // when
            var toString = Strings.ToString(dictionary);

            // then
            Check.That(toString).IsEqualTo("{[A, 1], [B, 2], [C, 3]}");
        }

        [Test]
        public void ShouldGenerateToStringForComplexDictionary()
        {
            // given
            IDictionary<string, IList<IList<int>>> dictionary = new Dictionary<string, IList<IList<int>>>();
            dictionary["A"] = new List<IList<int>>(Lists.AsList(Lists.AsList(1, 2, 3), Lists.AsList(4, 5, 6)));
            dictionary["B"] = new List<IList<int>>(Lists.AsList(Lists.AsList(1), Lists.AsList(2)));
            dictionary["C"] = new List<IList<int>>(Lists.AsList(Lists.EmptyList<int>(), null));

            // when
            var toString = Strings.ToString(dictionary);

            // then
            Check.That(toString).IsEqualTo("{[A, [[1, 2, 3], [4, 5, 6]]], [B, [[1], [2]]], [C, [[], null]]}");
        }

        [Test]
        public void ShouldGenerateToStringForArray()
        {
            // given
            var array = new object[] {1, "2", 3D};

            // when
            var toString = Strings.ToString(array);

            // then
            Check.That(toString).IsEqualTo("[1, 2, 3]");
        }
    }
}
