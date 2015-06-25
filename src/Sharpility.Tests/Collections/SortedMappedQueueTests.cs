using System;
using System.Collections.Generic;
using NFluent;
using NUnit.Framework;
using Sharpility.Collections;
using Sharpility.Extensions;
using Sharpility.Function;
using Sharpility.Util;

namespace Sharpility.Tests.Collections
{
    [TestFixture]
    public class SortedMappedQueueTests
    {

        [Test, TestCaseSource("MappedQueues")]
        public void ShouldSortQueueItems(SortedMappedQueue<string> queue)
        {
            // when
            queue.Offer("X");
            queue.Offer("A");
            queue.Offer("D");
            queue.Offer("Z");
            queue.Offer("B");

            // then
            Check.That(queue).ContainsExactly("A", "B", "D", "X", "Z");
        }

        [Test, TestCaseSource("MappedQueues")]
        public void ShouldPollQueueItemsInSortedOrder(SortedMappedQueue<string> queue)
        {
            // given
            queue.Offer("X");
            queue.Offer("A");
            queue.Offer("D");
            queue.Offer("Z");
            queue.Offer("B");
            var result = new List<string>();

            // when
            while (queue.Peek() != null)
            {
                result.Add(queue.Poll());                
            }

            // then
            Check.That(result).ContainsExactly("A", "B", "D", "X", "Z");
        }

        [Test, TestCaseSource("MappedQueues")]
        public void ShouldIgnoreItemDuplicates(SortedMappedQueue<string> queue)
        {
             // when
             const string item = "A";
             var offer1Accepted = queue.Offer(item);
             var offer2Accepted = queue.Offer(item);

             // then
             Check.That(offer1Accepted).IsTrue();
             Check.That(offer2Accepted).IsFalse();
             Check.That(queue).ContainsExactly(item);
        }

        [Test, TestCaseSource("ComplexMappedQueues")]
        public void ShouldReplaceMappedQueueItem(BiConverter<Converter<TestObj, int>, IComparer<TestObj>, SortedMappedQueue<TestObj>> queueCreator)
        {
            // given
            Converter<TestObj, int> keyExtractor = obj => obj.Id;
            var comparer = Comparers.CompareBy<TestObj, DateTime>(obj => obj.Date);
            var queue = queueCreator(keyExtractor, comparer);
            var obj1 = new TestObj {Id = 1, Date = new DateTime(2015, 6, 17, 13, 30, 0)};
            var obj2 = new TestObj { Id = 1, Date = new DateTime(2015, 6, 17, 15, 30, 0) };
            var obj3 = new TestObj { Id = 2, Date = new DateTime(2015, 6, 17, 13, 35, 0)};

            // when
            var offer1Accepted = queue.Offer(obj1);
            var offer2Accepted = queue.Offer(obj2);
            var offer3Accepted = queue.Offer(obj3);
            queue.Put(obj2);

            // then
            Check.That(offer1Accepted).IsTrue();
            Check.That(offer2Accepted).IsFalse();
            Check.That(offer3Accepted).IsTrue();
            Check.That(queue).ContainsExactly(obj3, obj2);
        }

        private static IEnumerable<SortedMappedQueue<string>> MappedQueues()
        {
            yield return SortedSetMappedQueue<string, string>.Create<string>();

            yield return ArrayListSortedMappedQueue<string, string>.Create<string>();
        }

        private static IEnumerable<BiConverter<Converter<TestObj, int>, IComparer<TestObj>, SortedMappedQueue<TestObj>>> ComplexMappedQueues()
        {
            BiConverter<Converter<TestObj, int>, IComparer<TestObj>, SortedMappedQueue<TestObj>> result =
                SortedSetMappedQueue<int, TestObj>.Create;
            yield return result;

            result = ArrayListSortedMappedQueue<int, TestObj>.Create;
            yield return result;
        } 

        public class TestObj
        {
            internal int Id { get; set; }
            internal DateTime Date { get; set; }

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
