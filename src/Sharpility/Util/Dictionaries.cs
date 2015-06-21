using System.Collections.Generic;
using System.Collections.Immutable;
using Sharpility.Extensions;

namespace Sharpility.Util
{
    /// <summary>
    /// Dictionary utils.
    /// </summary>
    public static class Dictionaries
    {
        /// <summary>
        /// Creates dictionary instance from KeyValuePair entries.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="entries">entries</param>
        /// <returns>dictionary</returns>
        public static IDictionary<T, TV> CreateFromEntries<T, TV>(params KeyValuePair<T, TV>[] entries)
        {
            var dictionary = new Dictionary<T, TV>(entries.Length);
            dictionary.PutAll(entries);
            return dictionary;
        }

        # region QuickDictionaryCreate

        /// <summary>
        /// Creates dictionary instance with given key and value.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">Value</param>
        /// <returns>dictionary</returns>
        public static IDictionary<T, TV> Create<T, TV>(T key, TV value)
        {
            var dictionary = new Dictionary<T, TV>();
            dictionary.Put(key, value);
            return dictionary;
        }

        /// <summary>
        /// Creates dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <returns>dictionary</returns>
        public static IDictionary<T, TV> Create<T, TV>(T key1, TV value1, T key2, TV value2)
        {
            var dictionary = new Dictionary<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            return dictionary;
        }

        /// <summary>
        /// Creates dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <param name="key3">key3</param>
        /// <param name="value3">Value3</param>
        /// <returns>dictionary</returns>
        public static IDictionary<T, TV> Create<T, TV>(T key1, TV value1, T key2, TV value2, T key3, TV value3)
        {
            var dictionary = new Dictionary<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            dictionary.Put(key3, value3);
            return dictionary;
        }

        /// <summary>
        /// Creates dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <param name="key3">key3</param>
        /// <param name="value3">Value3</param>
        /// <param name="key4">key4</param>
        /// <param name="value4">Value4</param>
        /// <returns>dictionary</returns>
        public static IDictionary<T, TV> Create<T, TV>(T key1, TV value1, T key2, TV value2, T key3, TV value3, T key4, TV value4)
        {
            var dictionary = new Dictionary<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            dictionary.Put(key3, value3);
            dictionary.Put(key4, value4);
            return dictionary;
        }

        /// <summary>
        /// Creates dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <param name="key3">key3</param>
        /// <param name="value3">Value3</param>
        /// <param name="key4">key4</param>
        /// <param name="value4">Value4</param>
        /// <param name="key5">key5</param>
        /// <param name="value5">Value5</param>
        /// <returns>dictionary</returns>
        public static IDictionary<T, TV> Create<T, TV>(T key1, TV value1, T key2, TV value2, T key3, TV value3, T key4, TV value4, T key5, TV value5)
        {
            var dictionary = new Dictionary<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            dictionary.Put(key3, value3);
            dictionary.Put(key4, value4);
            dictionary.Put(key5, value5);
            return dictionary;
        }

        /// <summary>
        /// Creates dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <param name="key3">key3</param>
        /// <param name="value3">Value3</param>
        /// <param name="key4">key4</param>
        /// <param name="value4">Value4</param>
        /// <param name="key5">key5</param>
        /// <param name="value5">Value5</param>
        /// <param name="key6">key6</param>
        /// <param name="value6">Value6</param>
        /// <returns>dictionary</returns>
        public static IDictionary<T, TV> Create<T, TV>(T key1, TV value1, T key2, TV value2, T key3, TV value3, T key4, TV value4, T key5, TV value5, 
            T key6, TV value6)
        {
            var dictionary = new Dictionary<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            dictionary.Put(key3, value3);
            dictionary.Put(key4, value4);
            dictionary.Put(key5, value5);
            dictionary.Put(key6, value6);
            return dictionary;
        }

        /// <summary>
        /// Creates dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <param name="key3">key3</param>
        /// <param name="value3">Value3</param>
        /// <param name="key4">key4</param>
        /// <param name="value4">Value4</param>
        /// <param name="key5">key5</param>
        /// <param name="value5">Value5</param>
        /// <param name="key6">key6</param>
        /// <param name="value6">Value6</param>
        /// <param name="key7">key7</param>
        /// <param name="value7">Value7</param>
        /// <returns>dictionary</returns>
        public static IDictionary<T, TV> Create<T, TV>(T key1, TV value1, T key2, TV value2, T key3, TV value3, T key4, TV value4, T key5, TV value5, 
            T key6, TV value6, T key7, TV value7)
        {
            var dictionary = new Dictionary<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            dictionary.Put(key3, value3);
            dictionary.Put(key4, value4);
            dictionary.Put(key5, value5);
            dictionary.Put(key6, value6);
            dictionary.Put(key7, value7);
            return dictionary;
        }

        /// <summary>
        /// Creates dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <param name="key3">key3</param>
        /// <param name="value3">Value3</param>
        /// <param name="key4">key4</param>
        /// <param name="value4">Value4</param>
        /// <param name="key5">key5</param>
        /// <param name="value5">Value5</param>
        /// <param name="key6">key6</param>
        /// <param name="value6">Value6</param>
        /// <param name="key7">key7</param>
        /// <param name="value7">Value7</param>
        /// <param name="key8">key8</param>
        /// <param name="value8">Value8</param>
        /// <returns>dictionary</returns>
        public static IDictionary<T, TV> Create<T, TV>(T key1, TV value1, T key2, TV value2, T key3, TV value3, T key4, TV value4, T key5, TV value5, 
            T key6, TV value6, T key7, TV value7, T key8, TV value8)
        {
            var dictionary = new Dictionary<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            dictionary.Put(key3, value3);
            dictionary.Put(key4, value4);
            dictionary.Put(key5, value5);
            dictionary.Put(key6, value6);
            dictionary.Put(key7, value7);
            dictionary.Put(key8, value8);
            return dictionary;
        }

        /// <summary>
        /// Creates dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <param name="key3">key3</param>
        /// <param name="value3">Value3</param>
        /// <param name="key4">key4</param>
        /// <param name="value4">Value4</param>
        /// <param name="key5">key5</param>
        /// <param name="value5">Value5</param>
        /// <param name="key6">key6</param>
        /// <param name="value6">Value6</param>
        /// <param name="key7">key7</param>
        /// <param name="value7">Value7</param>
        /// <param name="key8">key8</param>
        /// <param name="value8">Value8</param>
        /// <param name="key9">key9</param>
        /// <param name="value9">Value9</param>
        /// <returns>dictionary</returns>
        public static IDictionary<T, TV> Create<T, TV>(T key1, TV value1, T key2, TV value2, T key3, TV value3, T key4, TV value4, T key5, TV value5, 
            T key6, TV value6, T key7, TV value7, T key8, TV value8, T key9, TV value9)
        {
            var dictionary = new Dictionary<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            dictionary.Put(key3, value3);
            dictionary.Put(key4, value4);
            dictionary.Put(key5, value5);
            dictionary.Put(key6, value6);
            dictionary.Put(key7, value7);
            dictionary.Put(key8, value8);
            dictionary.Put(key9, value9);
            return dictionary;
        }

        /// <summary>
        /// Creates dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <param name="key3">key3</param>
        /// <param name="value3">Value3</param>
        /// <param name="key4">key4</param>
        /// <param name="value4">Value4</param>
        /// <param name="key5">key5</param>
        /// <param name="value5">Value5</param>
        /// <param name="key6">key6</param>
        /// <param name="value6">Value6</param>
        /// <param name="key7">key7</param>
        /// <param name="value7">Value7</param>
        /// <param name="key8">key8</param>
        /// <param name="value8">Value8</param>
        /// <param name="key9">key9</param>
        /// <param name="value9">Value9</param>
        /// <param name="key10">key10</param>
        /// <param name="value10">Value10</param>
        /// <returns>dictionary</returns>
        public static IDictionary<T, TV> Create<T, TV>(T key1, TV value1, T key2, TV value2, T key3, TV value3, T key4, TV value4, T key5, TV value5, 
            T key6, TV value6, T key7, TV value7, T key8, TV value8, T key9, TV value9, T key10, TV value10)
        {
            var dictionary = new Dictionary<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            dictionary.Put(key3, value3);
            dictionary.Put(key4, value4);
            dictionary.Put(key5, value5);
            dictionary.Put(key6, value6);
            dictionary.Put(key7, value7);
            dictionary.Put(key8, value8);
            dictionary.Put(key9, value9);
            dictionary.Put(key10, value10);
            return dictionary;
        }

        # endregion QuickDictionaryCreate

        # region QuickImmutableDictionaryCreate

        /// <summary>
        /// Creates immutable dictionary instance with given key and value.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">Value</param>
        /// <returns>dictionary</returns>
        public static IImmutableDictionary<T, TV> CreateImmutable<T, TV>(T key, TV value)
        {
            var dictionary = ImmutableDictionary.CreateBuilder<T, TV>();
            dictionary.Put(key, value);
            return dictionary.ToImmutable();
        }

        /// <summary>
        /// Creates immutable dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <returns>dictionary</returns>
        public static IImmutableDictionary<T, TV> CreateImmutable<T, TV>(T key1, TV value1, T key2, TV value2)
        {
            var dictionary = ImmutableDictionary.CreateBuilder<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            return dictionary.ToImmutable();
        }

        /// <summary>
        /// Creates immutable dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <param name="key3">key3</param>
        /// <param name="value3">Value3</param>
        /// <returns>dictionary</returns>
        public static IImmutableDictionary<T, TV> CreateImmutable<T, TV>(T key1, TV value1, T key2, TV value2, T key3, TV value3)
        {
            var dictionary = ImmutableDictionary.CreateBuilder<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            dictionary.Put(key3, value3);
            return dictionary.ToImmutable();
        }

        /// <summary>
        /// Creates immutable dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <param name="key3">key3</param>
        /// <param name="value3">Value3</param>
        /// <param name="key4">key4</param>
        /// <param name="value4">Value4</param>
        /// <returns>dictionary</returns>
        public static IImmutableDictionary<T, TV> CreateImmutable<T, TV>(T key1, TV value1, T key2, TV value2, T key3, TV value3, T key4, TV value4)
        {
            var dictionary = ImmutableDictionary.CreateBuilder<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            dictionary.Put(key3, value3);
            dictionary.Put(key4, value4);
            return dictionary.ToImmutable();
        }

        /// <summary>
        /// Creates immutable dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <param name="key3">key3</param>
        /// <param name="value3">Value3</param>
        /// <param name="key4">key4</param>
        /// <param name="value4">Value4</param>
        /// <param name="key5">key5</param>
        /// <param name="value5">Value5</param>
        /// <returns>dictionary</returns>
        public static IImmutableDictionary<T, TV> CreateImmutable<T, TV>(T key1, TV value1, T key2, TV value2, T key3, TV value3, T key4, TV value4, T key5, TV value5)
        {
            var dictionary = ImmutableDictionary.CreateBuilder<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            dictionary.Put(key3, value3);
            dictionary.Put(key4, value4);
            dictionary.Put(key5, value5);
            return dictionary.ToImmutable();
        }

        /// <summary>
        /// Creates immutable dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <param name="key3">key3</param>
        /// <param name="value3">Value3</param>
        /// <param name="key4">key4</param>
        /// <param name="value4">Value4</param>
        /// <param name="key5">key5</param>
        /// <param name="value5">Value5</param>
        /// <param name="key6">key6</param>
        /// <param name="value6">Value6</param>
        /// <returns>dictionary</returns>
        public static IImmutableDictionary<T, TV> CreateImmutable<T, TV>(T key1, TV value1, T key2, TV value2, T key3, TV value3, T key4, TV value4, T key5, TV value5,
            T key6, TV value6)
        {
            var dictionary = ImmutableDictionary.CreateBuilder<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            dictionary.Put(key3, value3);
            dictionary.Put(key4, value4);
            dictionary.Put(key5, value5);
            dictionary.Put(key6, value6);
            return dictionary.ToImmutable();
        }

        /// <summary>
        /// Creates immutable dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <param name="key3">key3</param>
        /// <param name="value3">Value3</param>
        /// <param name="key4">key4</param>
        /// <param name="value4">Value4</param>
        /// <param name="key5">key5</param>
        /// <param name="value5">Value5</param>
        /// <param name="key6">key6</param>
        /// <param name="value6">Value6</param>
        /// <param name="key7">key7</param>
        /// <param name="value7">Value7</param>
        /// <returns>dictionary</returns>
        public static IImmutableDictionary<T, TV> CreateImmutable<T, TV>(T key1, TV value1, T key2, TV value2, T key3, TV value3, T key4, TV value4, T key5, TV value5,
            T key6, TV value6, T key7, TV value7)
        {
            var dictionary = ImmutableDictionary.CreateBuilder<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            dictionary.Put(key3, value3);
            dictionary.Put(key4, value4);
            dictionary.Put(key5, value5);
            dictionary.Put(key6, value6);
            dictionary.Put(key7, value7);
            return dictionary.ToImmutable();
        }

        /// <summary>
        /// Creates immutable dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <param name="key3">key3</param>
        /// <param name="value3">Value3</param>
        /// <param name="key4">key4</param>
        /// <param name="value4">Value4</param>
        /// <param name="key5">key5</param>
        /// <param name="value5">Value5</param>
        /// <param name="key6">key6</param>
        /// <param name="value6">Value6</param>
        /// <param name="key7">key7</param>
        /// <param name="value7">Value7</param>
        /// <param name="key8">key8</param>
        /// <param name="value8">Value8</param>
        /// <returns>dictionary</returns>
        public static IImmutableDictionary<T, TV> CreateImmutable<T, TV>(T key1, TV value1, T key2, TV value2, T key3, TV value3, T key4, TV value4, T key5, TV value5,
            T key6, TV value6, T key7, TV value7, T key8, TV value8)
        {
            var dictionary = ImmutableDictionary.CreateBuilder<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            dictionary.Put(key3, value3);
            dictionary.Put(key4, value4);
            dictionary.Put(key5, value5);
            dictionary.Put(key6, value6);
            dictionary.Put(key7, value7);
            dictionary.Put(key8, value8);
            return dictionary.ToImmutable();
        }

        /// <summary>
        /// Creates immutable dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <param name="key3">key3</param>
        /// <param name="value3">Value3</param>
        /// <param name="key4">key4</param>
        /// <param name="value4">Value4</param>
        /// <param name="key5">key5</param>
        /// <param name="value5">Value5</param>
        /// <param name="key6">key6</param>
        /// <param name="value6">Value6</param>
        /// <param name="key7">key7</param>
        /// <param name="value7">Value7</param>
        /// <param name="key8">key8</param>
        /// <param name="value8">Value8</param>
        /// <param name="key9">key9</param>
        /// <param name="value9">Value9</param>
        /// <returns>dictionary</returns>
        public static IImmutableDictionary<T, TV> CreateImmutable<T, TV>(T key1, TV value1, T key2, TV value2, T key3, TV value3, T key4, TV value4, T key5, TV value5,
            T key6, TV value6, T key7, TV value7, T key8, TV value8, T key9, TV value9)
        {
            var dictionary = ImmutableDictionary.CreateBuilder<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            dictionary.Put(key3, value3);
            dictionary.Put(key4, value4);
            dictionary.Put(key5, value5);
            dictionary.Put(key6, value6);
            dictionary.Put(key7, value7);
            dictionary.Put(key8, value8);
            dictionary.Put(key9, value9);
            return dictionary.ToImmutable();
        }

        /// <summary>
        /// Creates immutable dictionary instance with given keys and values.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of value</typeparam>
        /// <param name="key1">key1</param>
        /// <param name="value1">Value1</param>
        /// <param name="key2">key2</param>
        /// <param name="value2">Value2</param>
        /// <param name="key3">key3</param>
        /// <param name="value3">Value3</param>
        /// <param name="key4">key4</param>
        /// <param name="value4">Value4</param>
        /// <param name="key5">key5</param>
        /// <param name="value5">Value5</param>
        /// <param name="key6">key6</param>
        /// <param name="value6">Value6</param>
        /// <param name="key7">key7</param>
        /// <param name="value7">Value7</param>
        /// <param name="key8">key8</param>
        /// <param name="value8">Value8</param>
        /// <param name="key9">key9</param>
        /// <param name="value9">Value9</param>
        /// <param name="key10">key10</param>
        /// <param name="value10">Value10</param>
        /// <returns>dictionary</returns>
        public static IImmutableDictionary<T, TV> CreateImmutable<T, TV>(T key1, TV value1, T key2, TV value2, T key3, TV value3, T key4, TV value4, T key5, TV value5,
            T key6, TV value6, T key7, TV value7, T key8, TV value8, T key9, TV value9, T key10, TV value10)
        {
            var dictionary = ImmutableDictionary.CreateBuilder<T, TV>();
            dictionary.Put(key1, value1);
            dictionary.Put(key2, value2);
            dictionary.Put(key3, value3);
            dictionary.Put(key4, value4);
            dictionary.Put(key5, value5);
            dictionary.Put(key6, value6);
            dictionary.Put(key7, value7);
            dictionary.Put(key8, value8);
            dictionary.Put(key9, value9);
            dictionary.Put(key10, value10);
            return dictionary.ToImmutable();
        }

        # endregion QuickImmutableDictionaryCreate

        /// <summary>
        /// Creates key-obj entry.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of obj</typeparam>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>entry</returns>
        public static KeyValuePair<T, TV> Entry<T, TV>(T key, TV value)
        {
            return new KeyValuePair<T, TV>(key: key, value: value);
        }

        /// <summary>
        /// Creates empty dictionary instance.
        /// </summary>
        /// <typeparam name="T">Type of key</typeparam>
        /// <typeparam name="TV">Type of obj</typeparam>
        /// <returns>empty dictionary</returns>
        public static IDictionary<T, TV> Empty<T, TV>()
        {
            return new Dictionary<T, TV>();
        }
    }
}
