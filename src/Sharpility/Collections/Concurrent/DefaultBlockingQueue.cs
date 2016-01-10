using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Sharpility.Util;

namespace Sharpility.Collections.Concurrent
{
    /// <summary>
    /// Implementation of BlockingQueue.
    /// </summary>
    /// <typeparam name="T">Type of queue item</typeparam>
    public sealed class DefaultBlockingQueue<T> : BlockingQueue<T>
    {
        private readonly BlockingCollection<T> queue;

        /// <summary>
        /// Uses BlockingCollection.
        /// </summary>
        public DefaultBlockingQueue()
        {
            queue = new BlockingCollection<T>();
        }

        /// <summary>
        /// Creates blocking queue with bounded queue capacity.
        /// </summary>
        /// <param name="boundedCapacity">Queue capacity</param>
        public DefaultBlockingQueue(int boundedCapacity)
        {
            queue = new BlockingCollection<T>(boundedCapacity);
        }

        /// <summary>
        /// Creates blocking from given producer and bounded queue capacity.
        /// </summary>
        /// <param name="producer">Producer of blocking producer</param>
        /// <param name="boundedCapacity">Queue capacity</param>
        public DefaultBlockingQueue(IProducerConsumerCollection<T> producer, int boundedCapacity)
        {
            queue = new BlockingCollection<T>(producer, boundedCapacity);
        }

        /// <summary>
        /// Creates blocking queue from given producer.
        /// </summary>
        /// <param name="producer">Producer of blocking producer</param>
        public DefaultBlockingQueue(IProducerConsumerCollection<T> producer)
        {
            queue = new BlockingCollection<T>(producer);
        }

        public IEnumerator<T> GetEnumerator()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var field = typeof(BlockingCollection<T>).GetField("m_collection", flags);
            if (field == null)
            {
                throw new NotSupportedException("Enumerator is not supported");
            }
            var underlyingCollection = (IProducerConsumerCollection<T>) field.GetValue(queue);
            return underlyingCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Offer(T item)
        {
            return queue.TryAdd(item);
        }

        public bool Offer(T item, TimeSpan timeout, CancellationToken cancellationToken = new CancellationToken())
        {
            return queue.TryAdd(item, (int)timeout.TotalMilliseconds, cancellationToken);
        }

        public T Peek()
        {
            using (var enumerator = GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    return enumerator.Current;
                }
                return default(T);
            }
        }

        public T Poll()
        {
            T result;
            if (queue.TryTake(out result))
            {
                return result;
            }
            return default(T);
        }

        public T Take()
        {
            return queue.Take();
        }

        public T Poll(TimeSpan timeout, CancellationToken cancellationToken = new CancellationToken())
        {
            T result;
            return queue.TryTake(out result, (int)timeout.TotalMilliseconds, cancellationToken) ? result : default(T);
        }

        public void Clear()
        {
            T item;
            while (queue.TryTake(out item)) ;
        }

        public bool Contains(T item)
        {
            return queue.Contains(item);
        }

        public int Count
        {
            get { return queue.Count; }
        }

        public override bool Equals(object obj)
        {
            return Objects.Equal(this, obj);
        }

        public override int GetHashCode()
        {
            return Objects.HashCode(this);
        }

        public override string ToString()
        {
            return Strings.ToString(this);
        }
    }
}
