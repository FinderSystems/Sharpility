using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Sharpility.Util;

namespace Sharpility.Collections.Concurrent
{
    public sealed class DefaultConcurrentQueue<T>: IQueue<T>
    {
        private readonly ConcurrentQueue<T> queue;

        public DefaultConcurrentQueue()
        {
            queue = new ConcurrentQueue<T>();
        }

        public DefaultConcurrentQueue(IEnumerable<T> values)
        {
            queue = new ConcurrentQueue<T>(values);
        }

        internal DefaultConcurrentQueue(ConcurrentQueue<T> queue)
        {
            this.queue = queue;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        public bool Offer(T element)
        {
            queue.Enqueue(element);
            return true;
        }

        public T Peek()
        {
            T result;
            return queue.TryPeek(out result) ? result : default(T);
        }

        public T Poll()
        {
            T result;
            if (queue.TryDequeue(out result))
            {
                return result;
            } 
            throw new InvalidOperationException("Could not poll result");
        }

        public void Clear()
        {
            while (Count > 0)
            {
                Poll();
            }
        }

        public bool Contains(T element)
        {
            return queue.Contains(element);
        }

        public int Count
        {
            get { return queue.Count; }
        }

        public override bool Equals(object obj)
        {
            var collection = obj as IEnumerable;
            return collection != null && Objects.Equal(collection, queue);
        }

        public override int GetHashCode()
        {
            return Objects.Hash(queue);
        }

        public override string ToString()
        {
            return Strings.ToString(queue);
        }
    }
}
