using System.Collections.Generic;

namespace Sharpility.Collections
{
    public interface IQueue<T> : IEnumerable<T>
    {
        bool Offer(T element);

        T Peek();

        T Poll();

        void Clear();

        bool Contains(T element);

        int Count { get; }
    }
}
