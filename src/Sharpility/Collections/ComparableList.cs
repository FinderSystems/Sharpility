using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sharpility.Util;

namespace Sharpility.Collections
{
    internal sealed class ComparableList<TValue>: IList<TValue>
    {
        private readonly IList<TValue> decorated;

        private ComparableList(IList<TValue> decorated)
        {
            this.decorated = decorated;
        }

        internal static IList<TValue> Of(IList<TValue> list)
        {
            return new ComparableList<TValue>(list);
        }

        internal static IList<TValue> Create(params TValue[] values)
        {
            return Of(new List<TValue>(values));
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

        public int IndexOf(TValue item)
        {
            return decorated.IndexOf(item);
        }

        public void Insert(int index, TValue item)
        {
            decorated.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            decorated.RemoveAt(index);
        }

        public TValue this[int index]
        {
            get { return decorated[index]; }
            set { decorated[index] = value; }
        }

        public override bool Equals(object obj)
        {
            var enumerable = obj as IEnumerable<TValue>;
            return enumerable != null && decorated.SequenceEqual(enumerable);
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
