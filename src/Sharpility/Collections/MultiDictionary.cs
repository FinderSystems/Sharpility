using System.Collections.Generic;

namespace Sharpility.Collections
{
    /// <summary>
    /// Dictionary that contains multiple values at given key.
    /// </summary>
    /// <typeparam name="TKey">Type of key</typeparam>
    /// <typeparam name="TValue">Type of value</typeparam>
    public interface MultiDictionary<TKey, TValue>
    {
        /// <summary>
        /// Puts value at given key.
        /// </summary>
        /// <param name="key">key value</param>
        /// <param name="value">value</param>
        void Put(TKey key, TValue value);

        /// <summary>
        /// Puts multiple values to given key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        void PutAll(TKey key, IEnumerable<TValue> values);

        /// <summary>
        /// Pults all entries from given multiDictionary.
        /// </summary>
        /// <param name="multiDictionary">MultiDictionary</param>
        void PutAll(MultiDictionary<TKey, TValue> multiDictionary);

        /// <summary>
        /// Puts all entries from given dictionary.
        /// </summary>
        /// <param name="dictionary">dicionary</param>
        void PutAll(IDictionary<TKey, TValue> dictionary);

        /// <summary>
        /// Removes key entries.
        /// </summary>
        /// <param name="key">Removed key</param>
        /// <returns>true - if any value was removed</returns>
        bool Remove(TKey key);

        /// <summary>
        /// Removes key-value entry.
        /// </summary>
        /// <param name="key">Removed key</param>
        /// <param name="value">Removed value</param>
        /// <returns>true if entry was removed</returns>
        bool Remove(TKey key, TValue value);

        /// <summary>
        /// Removes all values at given key.
        /// </summary>
        /// <param name="key">Removed keyh</param>
        /// <returns>Removed values</returns>
        ICollection<TValue> RemoveAll(TKey key);

        /// <summary>
        /// Replaces all values by given key.
        /// </summary>
        /// <param name="key">Key value</param>
        /// <param name="values">Values to replace</param>
        /// <returns>Replaced values</returns>
        ICollection<TValue> ReplaceValues(TKey key, IEnumerable<TValue> values);

        /// <summary>
        /// Removes all entries.
        /// </summary>
        void Clear();

        /// <summary>
        /// Returns all values mapped by given key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Values for given key</returns>
        ICollection<TValue> Get(TKey key);

        /// <summary>
        /// Returns all values mapped by given key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Values for given key</returns>
        ICollection<TValue> this[TKey key] { get; }

        /// <summary>
        /// Checks is dictionary contains given key.
        /// </summary>
        /// <param name="key">Checked key</param>
        /// <returns>true if any value is stored by given key</returns>
        bool ContainsKey(TKey key);

        /// <summary>
        /// Returns number of all values.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Returns number of all values stored by given key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Number of all values stored by given key</returns>
        int ValuesCount(TKey key);

        /// <summary>
        /// Returns set of all keys.
        /// </summary>
        ISet<TKey> Keys { get; }

        /// <summary>
        /// Returns all values.
        /// </summary>
        ICollection<TValue> Values { get; }

        /// <summary>
        /// Returns all entries.
        /// </summary>
        ICollection<KeyValuePair<TKey, TValue>> Entries { get; }

        /// <summary>
        /// Returns entries with multiple values for given key.
        /// </summary>
        ICollection<KeyValuePair<TKey, ICollection<TValue>>> MultiEntries { get; }

        /// <summary>
        /// Checks is dictionary is emtpy.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Checks is dictionary contains given value.
        /// </summary>
        /// <param name="value">Checked value</param>
        /// <returns>true if value is stored in dictionary</returns>
        bool ContainsValue(TValue value);

        /// <summary>
        /// Checks is dictionary contains given entry.
        /// </summary>
        /// <param name="key">Checked key</param>
        /// <param name="value">Checked value</param>
        /// <returns>true if entry is stored in dictionary</returns>
        bool ContainsEntry(TKey key, TValue value);

        /// <summary>
        /// Converts multi dictionary to regular dictionary.
        /// </summary>
        /// <returns></returns>
        IDictionary<TKey, ICollection<TValue>> ToDictionary();
    }
}
