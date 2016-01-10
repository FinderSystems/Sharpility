using System;
using System.Collections.Generic;
using NFluent;
using NUnit.Framework;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class MultiComparerTests
    {
        [Test]
        public void ShouldCompareByValue()
        {
            // given
            var list = new List<TestObj>
            {
                new TestObj {Value = 10, Time = new DateTime(2015, 9, 15, 10, 0, 0)},
                new TestObj {Value = 7, Time = new DateTime(2015, 9, 15, 11, 0, 0)},
                new TestObj {Value = 6, Time = new DateTime(2015, 9, 15, 11, 0, 0)},
                new TestObj {Value = 8, Time = new DateTime(2015, 9, 15, 8, 0, 0)},
                new TestObj {Value = 3, Time = new DateTime(2015, 9, 15, 15, 0, 0)}
            };
            var comparer = MultiComparer.Of(new ValueComparer());

            // when
            var sorted = list.SortBy(comparer);

            // then
            Check.That(sorted).ContainsExactly(
                new TestObj { Value = 3, Time = new DateTime(2015, 9, 15, 15, 0, 0) },
                new TestObj { Value = 6, Time = new DateTime(2015, 9, 15, 11, 0, 0) },
                new TestObj { Value = 7, Time = new DateTime(2015, 9, 15, 11, 0, 0) },
                new TestObj { Value = 8, Time = new DateTime(2015, 9, 15, 8, 0, 0) },
                new TestObj { Value = 10, Time = new DateTime(2015, 9, 15, 10, 0, 0) });
        }

        [Test]
        public void ShouldCompareByTime()
        {
            // given
            var list = new List<TestObj>
            {
                new TestObj {Value = 10, Time = new DateTime(2015, 9, 15, 10, 0, 0)},
                new TestObj {Value = 7, Time = new DateTime(2015, 9, 15, 11, 0, 0)},
                new TestObj {Value = 6, Time = new DateTime(2015, 9, 15, 11, 0, 0)},
                new TestObj {Value = 8, Time = new DateTime(2015, 9, 15, 8, 0, 0)},
                new TestObj {Value = 3, Time = new DateTime(2015, 9, 15, 15, 0, 0)}
            };
            var comparer = MultiComparer.Of(new TimeComparer());

            // when
            var sorted = list.SortBy(comparer);

            // then
            Check.That(sorted).ContainsExactly(
                new TestObj { Value = 8, Time = new DateTime(2015, 9, 15, 8, 0, 0) },
                new TestObj { Value = 10, Time = new DateTime(2015, 9, 15, 10, 0, 0) },
                new TestObj { Value = 7, Time = new DateTime(2015, 9, 15, 11, 0, 0) },
                new TestObj { Value = 6, Time = new DateTime(2015, 9, 15, 11, 0, 0) },
                new TestObj { Value = 3, Time = new DateTime(2015, 9, 15, 15, 0, 0) });
        }

        [Test]
        public void ShouldCompareByTimeAndValue()
        {
            // given
            var list = new List<TestObj>
            {
                new TestObj {Value = 10, Time = new DateTime(2015, 9, 15, 10, 0, 0)},
                new TestObj {Value = 7, Time = new DateTime(2015, 9, 15, 11, 0, 0)},
                new TestObj {Value = 6, Time = new DateTime(2015, 9, 15, 11, 0, 0)},
                new TestObj {Value = 8, Time = new DateTime(2015, 9, 15, 8, 0, 0)},
                new TestObj {Value = 3, Time = new DateTime(2015, 9, 15, 15, 0, 0)}
            };
            var comparer = MultiComparer.Of(
                new TimeComparer(),
                new ValueComparer());

            // when
            var sorted = list.SortBy(comparer);

            // then
            Check.That(sorted).ContainsExactly(
                new TestObj { Value = 8, Time = new DateTime(2015, 9, 15, 8, 0, 0) },
                new TestObj { Value = 10, Time = new DateTime(2015, 9, 15, 10, 0, 0) },
                new TestObj { Value = 6, Time = new DateTime(2015, 9, 15, 11, 0, 0) },
                new TestObj { Value = 7, Time = new DateTime(2015, 9, 15, 11, 0, 0) },
                new TestObj { Value = 3, Time = new DateTime(2015, 9, 15, 15, 0, 0) });
        }

        [Test]
        public void ShouldIgnoreComparationWhenNoComparersSpecified()
        {
            // given
            var list = new List<TestObj>
            {
                new TestObj {Value = 10, Time = new DateTime(2015, 9, 15, 10, 0, 0)},
                new TestObj {Value = 7, Time = new DateTime(2015, 9, 15, 11, 0, 0)},
                new TestObj {Value = 6, Time = new DateTime(2015, 9, 15, 11, 0, 0)},
                new TestObj {Value = 8, Time = new DateTime(2015, 9, 15, 8, 0, 0)},
                new TestObj {Value = 3, Time = new DateTime(2015, 9, 15, 15, 0, 0)}
            };
            var comparer = MultiComparer.Of<TestObj>();

            // when
            var sorted = list.SortBy(comparer);

            // then
            Check.That(sorted).ContainsExactly(
                new TestObj { Value = 10, Time = new DateTime(2015, 9, 15, 10, 0, 0) },
                new TestObj { Value = 7, Time = new DateTime(2015, 9, 15, 11, 0, 0) },
                new TestObj { Value = 6, Time = new DateTime(2015, 9, 15, 11, 0, 0) },
                new TestObj { Value = 8, Time = new DateTime(2015, 9, 15, 8, 0, 0) },
                new TestObj { Value = 3, Time = new DateTime(2015, 9, 15, 15, 0, 0) });
        }

        private class TestObj
        {
            public int Value { get; set; }
            public DateTime Time { get; set; }

            public override bool Equals(object obj)
            {
                return this.EqualsByProperties(obj);
            }

            public override int GetHashCode()
            {
                return this.PropertiesHash();
            }

            public override string ToString()
            {
                return this.PropertiesToString();
            }
        }

        private class ValueComparer : IComparer<TestObj>
        {
            public int Compare(TestObj first, TestObj second)
            {
                return first.Value.CompareTo(second.Value);
            }
        }

        private class TimeComparer : IComparer<TestObj>
        {
            public int Compare(TestObj first, TestObj second)
            {
                return first.Time.CompareTo(second.Time);
            }
        }
    }
}
