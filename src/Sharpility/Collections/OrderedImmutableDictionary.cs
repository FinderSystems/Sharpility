using System.Collections.Generic;
using System.Collections.Immutable;

namespace Sharpility.Collections
{
    /// <summary>
    /// Immutable dictionary holding putted keys order.
    /// </summary>
    /// <typeparam name="T">Type of key</typeparam>
    /// <typeparam name="TV">Type of value</typeparam>
    public interface OrderedImmutableDictionary<T, TV>: IImmutableDictionary<T, TV>
    {
        /// <summary>
        /// Dictionary keys with put order.
        /// </summary>
        IImmutableList<T> OrderedKeys { get; }

        /// <summary>
        /// Entry set sorted by keys put order.
        /// </summary>
        IImmutableList<KeyValuePair<T, TV>> OrderedEntries { get; }
    }
}
