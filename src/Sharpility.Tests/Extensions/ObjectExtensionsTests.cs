
using NUnit.Framework;
using NFluent;
using Sharpility.Extensions;
using System;
using Sharpility.Util;
using Moq;
using System.Collections.Generic;

namespace Sharpility.Tests.Extensions
{
    [TestFixture]
    class ObjectExtensionsTests
    {
        [Test]
        public void ShouldEqualsByReference()
        {
            Object first = new Object();
            var second = first;

            Check.That(Objects.Equal(first, second)).IsEqualTo(true);
        }

        [Test]
        public void ShouldNotEqualTwoBareObjects()
        {
            Object first = new Object();
            Object second = new Object();

            Check.That(Objects.Equal(first, second)).IsEqualTo(false);
        }

        [Test]
        public void ShouldEqualIComparableZero()
        {
            var first = new Mock<IComparable>();
            var second = new Mock<IComparable>();
            first.Setup(i => i.CompareTo(second.Object)).Returns(0);

            Check.That(Objects.Equal(first.Object, second.Object)).IsEqualTo(true);
        }

        [Test]
        public void ShouldNotEqualIComparableOne()
        {
            var first = new Mock<IComparable>();
            var second = new Mock<IComparable>();
            first.Setup(i => i.CompareTo(second.Object)).Returns(1);

            Check.That(Objects.Equal(first.Object, second.Object)).IsEqualTo(false);
        }

        [Test]
        public void ShouldNotEqualIComparableMinusOne()
        {
            var first = new Mock<IComparable>();
            var second = new Mock<IComparable>();
            first.Setup(i => i.CompareTo(second.Object)).Returns(-1);

            Check.That(Objects.Equal(first.Object, second.Object)).IsEqualTo(false);
        }

        [Test]
        public void ShouldEqualIDictionarySame()
        {
            IDictionary<string, string> first =
                new Dictionary<string, string>();
            first.Add("A", "Alpha");
            first.Add("B", "Beta");
            first.Add("C", "Whatever");

            IDictionary<string, string> second =
                new Dictionary<string, string>();
            second.Add("A", "Alpha");
            second.Add("B", "Beta");
            second.Add("C", "Whatever");

            Check.That(Objects.Equal(first, second)).IsEqualTo(true);
        }

        [Test]
        public void ShouldNotEqualIDictionaryDifferent()
        {
            IDictionary<string, string> first =
                new Dictionary<string, string>();
            first.Add("A", "Alpha");
            first.Add("B", "Beta");
            first.Add("C", "Whatever");

            IDictionary<string, string> second =
                new Dictionary<string, string>();
            second.Add("A", "Alpha");
            second.Add("B", "Beta");
            second.Add("C", "Whenever");

            Check.That(Objects.Equal(first, second)).IsEqualTo(false);
        }
    }
}
