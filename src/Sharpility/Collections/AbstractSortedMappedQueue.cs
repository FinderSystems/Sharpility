using System;
using System.Collections;
using System.Collections.Generic;
using Sharpility.Extensions;

namespace Sharpility.Collections
{
    public abstract class AbstractSortedMappedQueue<TKey, TValue, TQ> : SortedMappedQueue<TValue>
        where TValue : class
        where TQ : ICollection<TValue>
    {
        private readonly IDictionary<TKey, TValue> values = new Dictionary<TKey, TValue>();
        private readonly Converter<TValue, TKey> keyExtractor;
        private readonly IComparer<TValue> comparer;
        private readonly TQ queue;

        protected AbstractSortedMappedQueue(Converter<TValue, TKey> keyExtractor, IComparer<TValue> comparer, TQ queue)
        {
            this.keyExtractor = keyExtractor;
            this.comparer = comparer;
            this.queue = queue;
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        public bool Put(TValue element)
        {
            var key = keyExtractor(element);
            if (values.ContainsKey(key))
            {
                queue.Remove(values.Get(key));
            }
            queue.Add(element);
            values.Put(key, element);
            SortQueue(queue, comparer);
            return true;
        }

        public bool Offer(TValue element)
        {
            var key = keyExtractor(element);
            if (values.ContainsKey(key))
            {
                return false;
            }
            Put(element);
            return true;
        }

        public TValue Peek()
        {
            return queue.IsEmpty() ? null : queue.First();
        }

        public TValue Poll()
        {
            return queue.RemoveFirst();
        }

        public void Clear()
        {
            queue.Clear();
            values.Clear();
        }

        public bool Contains(TValue element)
        {
            var key = keyExtractor(element);
            return values.ContainsKey(key);
        }

        public int Count
        {
            get { return queue.Count; }
        }

        protected abstract void SortQueue(TQ queue, IComparer<TValue> comparer);
    }
}
