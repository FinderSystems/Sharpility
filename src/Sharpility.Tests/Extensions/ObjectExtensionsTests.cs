
using NUnit.Framework;
using NFluent;
using Sharpility.Extensions;
using System;
using Sharpility.Util;
using Moq;

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
        public void ShouldEqualIComparable()
        {

        }
    }
}
