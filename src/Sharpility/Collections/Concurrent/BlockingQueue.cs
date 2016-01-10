using System;
using System.Threading;

namespace Sharpility.Collections.Concurrent
{
    /// <summary>
    /// A Queue that additionally supports operations that wait for the queue to become non-empty when retrieving an element, and wait for space to become available in the queue when storing an element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface BlockingQueue<T> : IQueue<T>
    {
        /// <summary>
        /// Inserts the specified element into this queue, waiting up to the specified wait time if necessary for space to become available.
        /// </summary>
        /// <param name="item">offered element</param>
        /// <param name="timeout">how long to wait before giving up</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        bool Offer(T item, TimeSpan timeout, CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        /// Retrieves and removes the head of this queue, waiting up to the specified wait time if necessary for an element to become available.
        /// </summary>
        /// <param name="timeout">how long to wait before giving up</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        T Poll(TimeSpan timeout, CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        /// Retrieves and removes the head of this queue, waiting if necessary until an element becomes available.
        /// </summary>
        /// <returns></returns>
        T Take();
    }
}
