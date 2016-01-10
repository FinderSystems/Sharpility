using System.Collections;
using System.Collections.Generic;
using Sharpility.Util;

namespace Sharpility.Collections
{
    internal sealed class ComparableCollection<T>: ICollection<T>
    {
        private readonly ICollection<T> decorated;

        private ComparableCollection(ICollection<T> decorated)
        {
            this.decorated = decorated;
        }

        internal static ICollection<T> Of(ICollection<T> collection)
        {
            return new ComparableCollection<T>(collection);
        } 

        public IEnumerator<T> GetEnumerator()
        {
            return decorated.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            decorated.Add(item);
        }

        public void Clear()
        {
            decorated.Clear();
        }

        public bool Contains(T item)
        {
            return decorated.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            decorated.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
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
            return Objects.Equal(decorated, obj);
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
