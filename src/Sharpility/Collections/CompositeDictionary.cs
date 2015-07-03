using System.Collections.Generic;

namespace Sharpility.Collections
{
    /// <summary>
    /// Dictionary containing values mapped by two keys.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key</typeparam>
    /// <typeparam name="TSecondaryKey">Type of secondary key</typeparam>
    /// <typeparam name="TValue">Type of dictionary value</typeparam>
    public interface CompositeDictionary<TPrimaryKey, TSecondaryKey, TValue>
    {
        /// <summary>
        /// Puts value by primary and secondary key.
        /// </summary>
        /// <param name="primaryKey">Primary key value</param>
        /// <param name="secondaryKey">Secondary key value</param>
        /// <param name="value">Value</param>
        void Put(TPrimaryKey primaryKey, TSecondaryKey secondaryKey, TValue value);

        /// <summary>
        /// Puts value by composite key.
        /// </summary>
        /// <param name="key">Composite key of primary and secondary key</param>
        /// <param name="value">Value</param>
        void Put(CompositeKey<TPrimaryKey, TSecondaryKey> key, TValue value);

        /// <summary>
        /// Puts all values from composite dictionary.
        /// </summary>
        /// <param name="compositeDictionary">CompositeDictionary</param>
        void PutAll(CompositeDictionary<TPrimaryKey, TSecondaryKey, TValue> compositeDictionary);

        /// <summary>
        /// Puts all values from dictionary.
        /// </summary>
        /// <param name="dictionary">Dictionary</param>
        void PutAll(IDictionary<TPrimaryKey, IDictionary<TSecondaryKey, TValue>> dictionary);

        /// <summary>
        /// Returns value for composite key.
        /// </summary>
        /// <param name="key">Composite key of primary and secondary key</param>
        /// <returns>Value</returns>
        TValue Get(CompositeKey<TPrimaryKey, TSecondaryKey> key);

        /// <summary>
        /// Returns value for primary and secondary key.
        /// </summary>
        /// <param name="primaryKey">Primary key value</param>
        /// <param name="secondaryKey">Secondary key value</param>
        /// <returns>Value</returns>
        TValue Get(TPrimaryKey primaryKey, TSecondaryKey secondaryKey);

        /// <summary>
        /// Returns value for composite key.
        /// </summary>
        /// <param name="key">Composite key of primary and secondary key</param>
        /// <returns>Value</returns>
        TValue this[CompositeKey<TPrimaryKey, TSecondaryKey> key] { get; }

        /// <summary>
        /// Returns all values.
        /// </summary>
        ICollection<TValue> Values { get; }

        /// <summary>
        /// Returns all values for primary key.
        /// </summary>
        /// <param name="primaryKey">Primary key</param>
        /// <returns>Values</returns>
        ICollection<TValue> PrimaryKeyValues(TPrimaryKey primaryKey);

        /// <summary>
        /// Returns all values for secondary key.
        /// </summary>
        /// <param name="secondaryKey">Secondary key</param>
        /// <returns>Values</returns>
        ICollection<TValue> SecondaryKeyValues(TSecondaryKey secondaryKey);

        /// <summary>
        /// Returns set of composite keys.
        /// </summary>
        ISet<CompositeKey<TPrimaryKey, TSecondaryKey>> Keys { get; }

        /// <summary>
        /// Retunrs set of primary keys.
        /// </summary>
        ISet<TPrimaryKey> PrimaryKeys { get; }

        /// <summary>
        /// Retunrs set of secondary keys.
        /// </summary>
        ISet<TSecondaryKey> SecondaryKeys { get; }

        /// <summary>
        /// Returns all values count.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Returns number of primary keys.
        /// </summary>
        int PrimaryKeysCount { get; }

        /// <summary>
        /// Checks if dictionary is empty.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Removes all entries.
        /// </summary>
        void Clear();

        /// <summary>
        /// Returns all entries.
        /// </summary>
        ICollection<KeyValuePair<TPrimaryKey, KeyValuePair<TSecondaryKey, TValue>>> Entries { get; }

        /// <summary>
        /// Returns all entries for primary key.
        /// </summary>
        /// <param name="primaryKey">Primary key value</param>
        /// <returns>Primary key entries</returns>
        ICollection<KeyValuePair<TSecondaryKey, TValue>> PrimaryKeyEntries(TPrimaryKey primaryKey);

        /// <summary>
        /// Returns all entries for secondary key.
        /// </summary>
        /// <param name="secondaryKey">Secondary key value</param>
        /// <returns>Secondary key entries</returns>
        ICollection<KeyValuePair<TPrimaryKey, TValue>> SecondaryKeyEntries(TSecondaryKey secondaryKey);

        /// <summary>
        /// Removes value by primary and secondary key.
        /// </summary>
        /// <param name="primaryKey">Primary key value</param>
        /// <param name="secondaryKey">Secondary key value</param>
        /// <returns>true if value was removed</returns>
        bool Remove(TPrimaryKey primaryKey, TSecondaryKey secondaryKey);

        /// <summary>
        /// Removes value by composite key.
        /// </summary>
        /// <param name="key">Composite key of primary and secondary key</param>
        /// <returns>true if value was removed</returns>
        bool Remove(CompositeKey<TPrimaryKey, TSecondaryKey> key);

        /// <summary>
        /// Removes all values by primary key.
        /// </summary>
        /// <param name="primaryKey">Primary key value</param>
        /// <returns>Removed values</returns>
        ICollection<TValue> RemoveByPrimaryKey(TPrimaryKey primaryKey);

        /// <summary>
        /// Removes all values by secondary key.
        /// </summary>
        /// <param name="secondaryKey">Secondary key value</param>
        /// <returns>Removed values</returns>
        ICollection<TValue> RemoveBySecondaryKey(TSecondaryKey secondaryKey);

        /// <summary>
        /// Checks is dictionary contains primary and secondary key.
        /// </summary>
        /// <param name="primaryKey">Primary key value</param>
        /// <param name="secondaryKey">Secondary key value</param>
        /// <returns>true if dictionary contains primary and secondary key</returns>
        bool ContainsKey(TPrimaryKey primaryKey, TSecondaryKey secondaryKey);

        /// <summary>
        /// Checks is dictionary contains composite key.
        /// </summary>
        /// <param name="key">Composite key value</param>
        /// <returns>true if dictionary contains primary and secondary key</returns>
        bool ContainsKey(CompositeKey<TPrimaryKey, TSecondaryKey> key);

        /// <summary>
        /// Checks is dictionary contains value.
        /// </summary>
        /// <param name="value">Checked value</param>
        /// <returns>true if dictionary contains given value</returns>
        bool ContainsValue(TValue value);

        /// <summary>
        /// Checks is dictionary contains value mapped by primary key.
        /// </summary>
        /// <param name="primaryKey">Primary key value</param>
        /// <param name="value">Checked value</param>
        /// <returns>true if dictionary contains value for given primary key</returns>
        bool ContainsPrimaryKeyValue(TPrimaryKey primaryKey, TValue value);

        /// <summary>
        /// Checks is dictionary contains value mapped by secondary key.
        /// </summary>
        /// <param name="secondaryKey">Secondary key value</param>
        /// <param name="value">Checked value</param>
        /// <returns>true if dictionary contains value for given secondary key</returns>
        bool ContainsSecondaryKeyValue(TSecondaryKey secondaryKey, TValue value);

        /// <summary>
        /// Checks is dictionary contains entry of primary-secondary-key, value
        /// </summary>
        /// <param name="primaryKey">Primary key value</param>
        /// <param name="secondaryKey">Secondary key value</param>
        /// <param name="value">Checked value</param>
        /// <returns>true if given value is mapped by given primary and secondary key</returns>
        bool ContainsEntry(TPrimaryKey primaryKey, TSecondaryKey secondaryKey, TValue value);

        /// <summary>
        /// Checks is dictionary contains entry.
        /// </summary>
        /// <param name="key">Entry key</param>
        /// <param name="value">Checked value</param>
        /// <returns>true if given value is mapped by given key</returns>
        bool ContainsEntry(CompositeKey<TPrimaryKey, TSecondaryKey> key, TValue value);

        /// <summary>
        /// Converts CompositeDictionary to regular dictionary.
        /// </summary>
        /// <returns>dictionary</returns>
        IDictionary<TPrimaryKey, IDictionary<TSecondaryKey, TValue>> ToDictionary();
    }
}
