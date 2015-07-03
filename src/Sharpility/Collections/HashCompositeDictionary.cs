using System.Collections.Generic;
namespace Sharpility.Collections
{
    public sealed class HashCompositeDictionary<TPrimaryKey, TSecondaryKey, TValue> : AbstractCompositeDictionary<TPrimaryKey, TSecondaryKey, TValue>
    {
        public HashCompositeDictionary(int primaryKeyCapacity, int secondaryKeyCapacity) 
            :base(new Dictionary<TPrimaryKey, IDictionary<TSecondaryKey, TValue>>(primaryKeyCapacity), secondaryKeyCapacity)
        {
        }

        public HashCompositeDictionary() 
            :base(new Dictionary<TPrimaryKey, IDictionary<TSecondaryKey, TValue>>())
        {
        }

        protected override IDictionary<T, TV> CreateDictionary<T, TV>(int capacity)
        {
            return new Dictionary<T, TV>(capacity);
        }

        protected override IDictionary<T, TV> CreateDictionary<T, TV>()
        {
            return new Dictionary<T, TV>();
        }

        protected override ICollection<T> CreateList<T>(int capacity)
        {
            return new List<T>(capacity);
        }

        protected override ICollection<T> CreateList<T>()
        {
            return new List<T>();
        }

        protected override ISet<T> CreateSet<T>()
        {
            return new HashSet<T>();
        }
    }
}
