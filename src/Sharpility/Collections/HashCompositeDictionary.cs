using System.Collections.Generic;
namespace Sharpility.Collections
{
    /// <summary>
    /// HashMap implementation of CompositeDictionary.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key</typeparam>
    /// <typeparam name="TSecondaryKey">Type of secondary key</typeparam>
    /// <typeparam name="TValue">Type of value</typeparam>
    public sealed class HashCompositeDictionary<TPrimaryKey, TSecondaryKey, TValue> : AbstractCompositeDictionary<TPrimaryKey, TSecondaryKey, TValue>
    {
        /// <summary>
        /// Created composite dictionary with primary and secondary keys capacities.
        /// </summary>
        /// <param name="primaryKeyCapacity">Capacity of primary keys</param>
        /// <param name="secondaryKeyCapacity">Capacity of secondary keys</param>
        public HashCompositeDictionary(int primaryKeyCapacity, int secondaryKeyCapacity) 
            :base(new Dictionary<TPrimaryKey, IDictionary<TSecondaryKey, TValue>>(primaryKeyCapacity), secondaryKeyCapacity)
        {
        }

        /// <summary>
        /// Created composite dictionary.
        /// </summary>
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
