using System;
using System.Collections;
using Sharpility.Extensions;

namespace Sharpility.Util
{
    public static class Objects
    {
        public static int Hash(params object[] elements)
        {
            return HashCode(elements);
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
            else if (first is IDictionary && second is IDictionary)
            {
                return DictionariesEqual((IDictionary) first, (IDictionary) second);
            }
            else if (Sets.IsGenericSet(first) && Sets.IsGenericSet(second))
            {
                return ElementsEqual(first as IEnumerable, second as IEnumerable);
            }
            else if (first is IEnumerable && second is IEnumerable)
            {
                return SequenceEqual((IEnumerable) first, (IEnumerable) second);
            }
            return Equals(first, second);
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
            foreach (var element in enumerable)
            {
                hashCode = 31 * hashCode + HashCode(element);
            }
            return hashCode;
        }

        private static bool ElementsEqual(IEnumerable first, IEnumerable second)
        {
            return first.Count() == second.Count() && 
                first.ItemType() == second.ItemType() &&
                first.ContainsAll(second);
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

        private static bool DictionariesEqual(IDictionary first, IDictionary second)
        {
            if (first.Count != second.Count)
            {
                return false;
            }
            var iterator = first.GetEnumerator();
            while (iterator.MoveNext())
            {
                var entry = iterator.Entry;
                if (!second.Contains(entry.Key) || !Equal(entry.Value, second[entry.Key]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
