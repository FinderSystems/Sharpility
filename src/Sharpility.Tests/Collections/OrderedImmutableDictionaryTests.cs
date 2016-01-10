using NFluent;
using NUnit.Framework;
using Sharpility.Collections;
using Sharpility.Util;

namespace Sharpility.Tests.Collections
{
    [TestFixture]
    public class OrderedImmutableDictionaryTests
    {
        [Test]
        public void ShouldReturnKeysInPutOrder()
        {
            // given
            var dictionary = OrderedHashImmutableDictionary<string, int>.Builder()
                .Put("Z", 1)
                .Put("Y", 2)
                .Put("X", 3)
                .Build();

            // when
            var orderedKeys = dictionary.OrderedKeys;

            // then
            Check.That(orderedKeys).ContainsExactly("Z", "Y", "X");
        }

        [Test]
        public void ShouldReturnEntriesInPutOrder()
        {
            // given
            var dictionary = OrderedHashImmutableDictionary<string, int>.Builder()
                .Put("Z", 1)
                .Put("Y", 2)
                .Put("X", 3)
                .Build();

            // when
            var orderedEntries = dictionary.OrderedEntries;

            // then
            Check.That(orderedEntries).ContainsExactly(
                Dictionaries.Entry("Z", 1), 
                Dictionaries.Entry("Y", 2), 
                Dictionaries.Entry("X", 3));
        }
    }
}
