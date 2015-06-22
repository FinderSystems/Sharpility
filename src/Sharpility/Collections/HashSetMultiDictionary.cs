using System.Collections.Generic;

namespace Sharpility.Collections
{
    public sealed class HashSetMultiDictionary<TKey, TValue> : AbstractMultiDictionary<TKey, TValue>
    {
        public HashSetMultiDictionary()
        {
        }

        public HashSetMultiDictionary(int keysCapacity)
            : base(keysCapacity, 0)
        {
        }

        protected override ICollection<TValue> CreateCollection(int capacity)
        {
            return new HashSet<TValue>();
        }

        protected override ICollection<TValue> ResultCollection(ICollection<TValue> collection)
        {
            return new HashSet<TValue>(collection);
        }

        protected override ICollection<TValue> ComparableCollection(ICollection<TValue> collection)
        {
            var list = new HashSet<TValue>(collection);
            return ComparableSet<TValue>.Of(list);
        }
    }
}
