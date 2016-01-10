using System;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Collections
{
    /// <summary>
    /// Key consisting of primary and secondary key.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key</typeparam>
    /// <typeparam name="TSecondaryKey">Type of secondary key</typeparam>
    [Serializable]
    public struct CompositeKey<TPrimaryKey, TSecondaryKey>
    {
        /// <summary>
        /// Primary key value.
        /// </summary>
        public TPrimaryKey Primary { get; private set; }

        /// <summary>
        /// Secondary key value.
        /// </summary>
        public TSecondaryKey Secondary { get; private set; }

        /// <summary>
        /// Created composite key.
        /// </summary>
        /// <param name="primary">Primary key value</param>
        /// <param name="secondary">Secondary key value</param>
        public CompositeKey(TPrimaryKey primary, TSecondaryKey secondary)
            :this()
        {
            Primary = primary;
            Secondary = secondary;
        }

        public override bool Equals(object obj)
        {
            return this.EqualsByProperties(obj);
        }

        public override int GetHashCode()
        {
            return this.PropertiesHash();
        }

        public override string ToString()
        {
            return Strings.Format("[{0},{1}]", Primary, Secondary);
        }
    }
}
