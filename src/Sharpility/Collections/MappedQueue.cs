
namespace Sharpility.Collections
{
    public interface MappedQueue<T> : IQueue<T>
    {
        bool Put(T element);
    }
}
