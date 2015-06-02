using System.Collections;
using System.Collections.Generic;
using Sharpility.Util;

namespace Sharpility.Collections
{
    /// <summary>
    /// Default IQueue implementation using System.Collections.Generic.Queue 
    /// </summary>
    /// <typeparam name="T">Type of queue item</typeparam>
    public sealed class DefaultQueue<T>: IQueue<T>
    {
        private readonly Queue<T> queue;

        /// <summary>
        /// Creates new queue.
        /// </summary>
        public DefaultQueue()
        {
            queue = new Queue<T>();
        }

        /// <summary>
        /// Creates queue with given capacity.
        /// </summary>
        /// <param name="capacity">Queue capacity</param>
        public DefaultQueue(int capacity)
        {
            queue = new Queue<T>(capacity);
        }

        internal DefaultQueue(Queue<T> queue)
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

        public bool Offer(T item)
        {
            queue.Enqueue(item);
            return true;
        }

        public T Peek()
        {
            return queue.Peek();
        }

        public T Poll()
        {
            return queue.Dequeue();
        }

        public void Clear()
        {
            queue.Clear();
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
