using System;
using System.Collections;

namespace Sharpility.Util
{
    public static class Objects
    {
        public static int Hash(params object[] elements)
        {
            var hashCode = 1;
            foreach (object element in elements)
            {
                hashCode = 31 * hashCode + HashCode(element);
            }
            return hashCode;
        }

        public static bool Equal(object first, object second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }
            else if (first is IComparable && second is IComparable)
            {
                return ((IComparable)first).CompareTo((IComparable)second) == 0;
            }
            else if (first is IEnumerable && second is IEnumerable)
            {
                return SequenceEqual((IEnumerable) first, (IEnumerable) second);
            }
            return object.Equals(first, second);
        }

        public static int HashCode(object obj)
        {
            if (obj is ICollection)
            {
                return HashCode((IEnumerable)obj);
            }
            return obj == null ? 0 : obj.GetHashCode();
        }

        private static int HashCode(IEnumerable enumerable)
        {
            var hashCode = 1;
            foreach (object element in enumerable)
            {
                hashCode = 31 * hashCode + HashCode(element);
            }
            return hashCode;
        }

        private static bool SequenceEqual(IEnumerable first, IEnumerable second)
        {
            var secondIterator = second.GetEnumerator();
            foreach (var firstElement in first)
            {
                if (!secondIterator.MoveNext() || !Equal(firstElement, secondIterator.Current))
                {
                    return false;
                }
            }
            return !secondIterator.MoveNext();
        }
    }
}
