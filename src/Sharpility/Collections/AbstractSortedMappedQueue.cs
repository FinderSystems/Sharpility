﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sharpility.Extensions;

namespace Sharpility.Collections
{
    /// <summary>
    /// Abstraction of sorted mapped queue.
    /// </summary>
    /// <typeparam name="TKey">Type of key</typeparam>
    /// <typeparam name="TValue">Type of value</typeparam>
    /// <typeparam name="TQ">Type of queue</typeparam>
    public abstract class AbstractSortedMappedQueue<TKey, TValue, TQ> : SortedMappedQueue<TValue>
        where TValue : class
        where TQ : ICollection<TValue>
    {
        private readonly IDictionary<TKey, TValue> values = new Dictionary<TKey, TValue>();
        private readonly Converter<TValue, TKey> keyExtractor;
        private readonly IComparer<TValue> comparer;
        private readonly TQ queue;

        /// <summary>
        /// Created AbstractSortedMappedQueue.
        /// </summary>
        /// <param name="keyExtractor">Extractor key from value</param>
        /// <param name="comparer">Comparer of values</param>
        /// <param name="queue">queue</param>
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

        public bool Offer(TValue item)
        {
            var key = keyExtractor(item);
            if (values.ContainsKey(key))
            {
                return false;
            }
            Put(item);
            return true;
        }

        public TValue Peek()
        {
            return queue.IsEmpty() ? null : queue.FirstOrDefault();
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

        public bool Contains(TValue item)
        {
            var key = keyExtractor(item);
            return values.ContainsKey(key);
        }

        public int Count
        {
            get { return queue.Count; }
        }

        /// <summary>
        /// Sorts queue with given comparer.
        /// </summary>
        /// <param name="queue">Sorted queue</param>
        /// <param name="comparer">Comparer of queue items</param>
        protected abstract void SortQueue(TQ queue, IComparer<TValue> comparer);
    }
}
