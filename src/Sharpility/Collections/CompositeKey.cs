using System;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Collections
{
    [Serializable]
    public struct CompositeKey<TPrimaryKey, TSecondaryKey>
    {
        public TPrimaryKey Primary { get; private set; }
        public TSecondaryKey Secondary { get; private set; }

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
