using System.Collections.Generic;

namespace Sharpility.Util
{
    public static class KeyValuePairs
    {
        public static KeyValuePair<TKey, TValue> Of<TKey, TValue>(TKey key, TValue value)
        {
            return new KeyValuePair<TKey, TValue>(key, value);
        } 
    }
}
