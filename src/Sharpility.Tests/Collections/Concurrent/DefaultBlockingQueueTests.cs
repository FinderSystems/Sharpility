using System;
using System.Threading;
using System.Threading.Tasks;
using NFluent;
using NUnit.Framework;
using Sharpility.Collections.Concurrent;

namespace Sharpility.Tests.Collections.Concurrent
{
    [TestFixture]
    public class DefaultBlockingQueueTests
    {
        [Test]
        public void PeekShouldReturnNull()
        {
            // given
            var queue = new DefaultBlockingQueue<string>();

            // when
            var item = queue.Peek();

            // then
            Check.That(item).IsNull();
        }

        [Test]
        public void PeekShouldReturnValueWithoutDequeue()
        {
            // given
            var queue = new DefaultBlockingQueue<string>();
            queue.Offer("test");

            // when
            var item = queue.Peek();

            // then
            Check.That(item).IsEqualTo("test");
            Check.That(queue).HasSize(1);
        }

        [Test]
        public void ShouldPollValue()
        {
            // given
            var queue = new DefaultBlockingQueue<string>();
            queue.Offer("test");

            // when
            var item = queue.Poll();

            // then
            Check.That(item).IsEqualTo("test");
            Check.That(queue).IsEmpty();
        }

        [Test]
        public void PollShouldReturnNull()
        {
            // given
            var queue = new DefaultBlockingQueue<string>();

            // when
            var item = queue.Poll();

            // then
            Check.That(item).IsNull();
        }

        [Test, Timeout(2000)]
        public void TakeShouldWaitForElementAvailability()
        {
            // given
            var queue = new DefaultBlockingQueue<string>();
            const string element = "test";
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                queue.Offer(element);
            });

            // when
            var item = queue.Take();

            // then
            Check.That(item).IsEqualTo(element);
        }


        [Test]
        public void ShouldWaitForValueTake()
        {
            // given
            var queue = new DefaultBlockingQueue<string>();
            var timeout = TimeSpan.FromSeconds(10);
            const string itemToOffer = "item";

            // when
            Task.Run(() =>
            {
                Thread.Sleep(100);
                queue.Offer(itemToOffer);
            });
            var item = queue.Poll(timeout);

            // then
            Check.That(item).IsEqualTo(itemToOffer);
        }
    }
}
