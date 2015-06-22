using System.Collections;
using System.Collections.Generic;
using Sharpility.Util;

namespace Sharpility.Collections
{
    internal sealed class ComparableSet<TValue>: ISet<TValue>
    {
        private readonly ISet<TValue> decorated;

        private ComparableSet(ISet<TValue> decorated)
        {
            this.decorated = decorated;
        }

        internal static ISet<TValue> Of(ISet<TValue> set)
        {
            return new ComparableSet<TValue>(set);
        }

        internal static ISet<TValue> Create(params TValue[] values)
        {
            return Of(new HashSet<TValue>(values));
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return decorated.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return decorated.GetEnumerator();
        }

        public void Add(TValue item)
        {
            decorated.Add(item);
        }

        bool ISet<TValue>.Add(TValue item)
        {
            return decorated.Add(item);
        }

        public void UnionWith(IEnumerable<TValue> other)
        {
            decorated.UnionWith(other);
        }

        public void IntersectWith(IEnumerable<TValue> other)
        {
            decorated.IntersectWith(other);
        }

        public void ExceptWith(IEnumerable<TValue> other)
        {
            decorated.ExceptWith(other);
        }

        public void SymmetricExceptWith(IEnumerable<TValue> other)
        {
            decorated.SymmetricExceptWith(other);
        }

        public bool IsSubsetOf(IEnumerable<TValue> other)
        {
            return decorated.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<TValue> other)
        {
            return decorated.IsSupersetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<TValue> other)
        {
            return decorated.IsProperSupersetOf(other);
        }

        public bool IsProperSubsetOf(IEnumerable<TValue> other)
        {
            return decorated.IsProperSubsetOf(other);
        }

        public bool Overlaps(IEnumerable<TValue> other)
        {
            return decorated.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<TValue> other)
        {
            return decorated.SetEquals(other);
        }

        public void Clear()
        {
            decorated.Clear();
        }

        public bool Contains(TValue item)
        {
            return decorated.Contains(item);
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            decorated.CopyTo(array, arrayIndex);
        }

        public bool Remove(TValue item)
        {
            return decorated.Remove(item);
        }

        public int Count
        {
            get { return decorated.Count; }
        }

        public bool IsReadOnly
        {
            get { return decorated.IsReadOnly; }
        }

        public override bool Equals(object obj)
        {
            var enumerable = obj as IEnumerable<TValue>;
            return enumerable != null && Objects.Equal(decorated, enumerable);
        }

        public override int GetHashCode()
        {
            return Objects.HashCode(decorated);
        }

        public override string ToString()
        {
            return Strings.ToString(decorated);
        }
    }
}
