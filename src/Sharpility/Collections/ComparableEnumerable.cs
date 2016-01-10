using System.Collections;
using System.Collections.Generic;
using Sharpility.Util;

namespace Sharpility.Collections
{
    internal sealed class ComparableEnumerable<T>: IEnumerable<T>
    {
        private readonly IEnumerable<T> decorated;

        private ComparableEnumerable(IEnumerable<T> decorated)
        {
            this.decorated = decorated;
        }

        internal static IEnumerable<T> Of(IEnumerable<T> enumerable)
        {
            return new ComparableEnumerable<T>(enumerable);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return decorated.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
