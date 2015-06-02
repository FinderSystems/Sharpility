using System.Collections.Generic;

namespace Sharpility.Collections
{
    public interface IQueue<T> : IEnumerable<T>
    {
        /// <summary>
        /// Offers item to queue.
        /// </summary>
        /// <param name="item">offered item</param>
        /// <returns>true if item was accepted</returns>
        bool Offer(T item);

        /// <summary>
        /// Returns first item in queue
        /// </summary>
        /// <returns>first item in queue</returns>
        T Peek();

        /// <summary>
        /// Removes and return first item in queue.
        /// </summary>
        /// <returns>Removed first queue item</returns>
        T Poll();

        /// <summary>
        /// Clears queue.
        /// </summary>
        void Clear();

        /// <summary>
        /// Checks is queue contains item.
        /// </summary>
        /// <param name="item">checkd item</param>
        /// <returns>is queue contains item</returns>
        bool Contains(T item);

        /// <summary>
        /// Returns number of queue items.
        /// </summary>
        int Count { get; }
    }
}
