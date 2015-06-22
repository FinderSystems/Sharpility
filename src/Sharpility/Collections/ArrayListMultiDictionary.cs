using System.Collections.Generic;

namespace Sharpility.Collections
{
    public sealed class ArrayListMultiDictionary<TKey, TValue>: AbstractMultiDictionary<TKey, TValue>
    {
        public ArrayListMultiDictionary()
        {
        }

        public ArrayListMultiDictionary(int keysCapacity, int valuesCapacity)
            : base(keysCapacity, valuesCapacity)
        {
        }

        protected override ICollection<TValue> CreateCollection(int capacity)
        {
            return capacity == 0 ? new List<TValue>() : new List<TValue>(capacity);
        }

        protected override ICollection<TValue> ResultCollection(ICollection<TValue> collection)
        {
            return new List<TValue>(collection);
        }

        protected override ICollection<TValue> ComparableCollection(ICollection<TValue> collection)
        {
            var list = new List<TValue>(collection);
            return ComparableList<TValue>.Of(list);
        }
    }
}
