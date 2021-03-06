﻿using System.Collections;
using System.Collections.Generic;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Collections
{
    /// <summary>
    /// IQueue implementation using LinkedList
    /// </summary>
    /// <typeparam name="T">Type of queue item</typeparam>
    public sealed class LinkedQueue<T>: IQueue<T>
    {
        private readonly LinkedList<T> list;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        public LinkedQueue()
        {
            list = new LinkedList<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public bool Offer(T item)
        {
            list.AddFirst(item);
            return true;
        }

        public T Peek()
        {
            return list.IsEmpty() ? default(T) : list.First.Value;
        }

        public T Poll()
        {
            var element = list.First.Value;
            list.RemoveFirst();
            return element;
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public override bool Equals(object obj)
        {
            var collection = obj as IEnumerable;
            return collection != null && Objects.Equal(collection, list);
        }

        public override int GetHashCode()
        {
            return Objects.Hash(list);
        }

        public override string ToString()
        {
            return Strings.ToString(list);
        }
    }
}
