
using Sharpility.Collections;

namespace Sharpility.Util
{
    public static class CompositeKeys
    {
        public static CompositeKey<T, TV> Of<T, TV>(T primary, TV secondary)
        {
            return new CompositeKey<T, TV>(primary, secondary);
        }
    }
}
