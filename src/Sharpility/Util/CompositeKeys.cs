
using Sharpility.Collections;

namespace Sharpility.Util
{
    /// <summary>
    /// Utility for CompisiteKey class.
    /// </summary>
    public static class CompositeKeys
    {
        /// <summary>
        /// Creates composite key.
        /// </summary>
        /// <typeparam name="T">Type of primary key</typeparam>
        /// <typeparam name="TV">Type of secondary key</typeparam>
        /// <param name="primary">Primary key value</param>
        /// <param name="secondary">Secondary key value</param>
        /// <returns>CompositeKey</returns>
        public static CompositeKey<T, TV> Of<T, TV>(T primary, TV secondary)
        {
            return new CompositeKey<T, TV>(primary, secondary);
        }
    }
}
