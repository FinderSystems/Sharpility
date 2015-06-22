using System.Collections.Generic;
using Sharpility.Util;

namespace Sharpility.Collections
{
    public sealed class LinkedListMultiDictionary<TKey, TValue> : AbstractMultiDictionary<TKey, TValue>
    {
        public LinkedListMultiDictionary()
        {
        }

        public LinkedListMultiDictionary(int keysCapacity)
            : base(keysCapacity, 0)
        {
        }

        protected override ICollection<TValue> CreateCollection(int capacity)
        {
            return new LinkedList<TValue>();
        }

        protected override ICollection<TValue> ResultCollection(ICollection<TValue> collection)
        {
            return Lists.AsLinkedList(collection);
        }

        protected override ICollection<TValue> ComparableCollection(ICollection<TValue> collection)
        {
            var list = new List<TValue>(collection);
            return ComparableList<TValue>.Of(list);
        }
    }
}
