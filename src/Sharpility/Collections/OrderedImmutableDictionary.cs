using System.Collections.Generic;
using System.Collections.Immutable;

namespace Sharpility.Collections
{
    public interface OrderedImmutableDictionary<T, TV>: IImmutableDictionary<T, TV>
    {
        IImmutableList<T> OrderedKeys { get; }

        IImmutableList<KeyValuePair<T, TV>> OrderedEntries { get; }
    }
}
