using System;
using System.Collections.Generic;
using NFluent;
using NUnit.Framework;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class ComparersTests
    {
        [Test]
        public void ShouldCreateComparerOfComparableObjects()
        {
            // given
            var comparer = Comparers.OfComparables<string>();
            var list = new List<string> {"A", "Z", "D", "B", null, "X"};

            // when
            list.Sort(comparer);

            // then
            Check.That(list).ContainsExactly("A", "B", "D", "X", "Z", null);
        }

        [Test]
        public void ShouldCreateComparerWithComparableConverter()
        {
            // given
            Converter<TestObj, int> comparableConverter = obj => obj.Value;
            var comparer = Comparers.CompareBy(comparableConverter);
            var list = new List<TestObj>
            {
                new TestObj { Value = 5 }, 
                new TestObj { Value = 3 }, 
                new TestObj { Value = 1 }, 
                new TestObj {Value = 4}
            };

            // when
            list.Sort(comparer);

            // then
            Check.That(list).ContainsExactly(
                new TestObj { Value = 1 }, 
                new TestObj { Value = 3 }, 
                new TestObj { Value = 4 }, 
                new TestObj { Value = 5 });
        }

        private class TestObj
        {
            internal int Value { get; set; }

            public override bool Equals(object obj)
            {
                return this.EqualsByProperties(obj);
            }

            public override int GetHashCode()
            {
                return this.PropertiesHash();
            }
        }
    }
}
