
namespace Sharpility.Collections
{
    /// <summary>
    /// Queue with item uniqueness control.
    /// </summary>
    /// <typeparam name="T">Type of queue element</typeparam>
    public interface MappedQueue<T> : IQueue<T>
    {
        /// <summary>
        /// Adds or replace element to queue.
        /// </summary>
        /// <param name="element">Offered element</param>
        /// <returns>True if element was replaced</returns>
        bool Put(T element);
    }
}
