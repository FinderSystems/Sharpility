
using System;
using System.Collections.Specialized;
using Sharpility.Base;

namespace Sharpility.Extensions
{
    /// <summary>
    /// Extensions of NameValueCollection.
    /// </summary>
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// Returns int value from NameValueCollection. If property not set returns given default.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="defaultValue">Property default value</param>
        /// <returns>Property int value or default</returns>
        public static int IntValue(this NameValueCollection source, string propertyName, int defaultValue)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var value = source[propertyName];
            return string.IsNullOrEmpty(value) ? defaultValue : int.Parse(value);
        }

        /// <summary>
        /// Returns int value from NameValueCollection. If property not set returns null.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="propertyName">Property name</param>
        /// <returns>Property int value or null</returns>
        public static int? IntValue(this NameValueCollection source, string propertyName)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var value = source[propertyName];
            return string.IsNullOrEmpty(value) ? (int?)null : int.Parse(value);
        }

        /// <summary>
        /// Returns boolean value from NameValueCollection.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="propertyName">Property name</param>
        /// <returns>Property boolean value</returns>
        public static bool BoolValue(this NameValueCollection source, string propertyName)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var value = source[propertyName];
            return "true".Equals(value, StringComparison.InvariantCultureIgnoreCase) ||
                   "1".Equals(value);
        }

        /// <summary>
        /// Returns boolean value from NameValueCollection. If not property not set returns given default.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="defaultValue">Property default value</param>
        /// <returns>Property boolean value or default</returns>
        public static bool BoolValue(this NameValueCollection source, string propertyName, bool defaultValue)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var value = source[propertyName];
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            return "true".Equals(value, StringComparison.InvariantCultureIgnoreCase) ||
                   "1".Equals(value);
        }

        /// <summary>
        /// Returns TimeSpan value from NameValueCollection. If property not set returns null.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="propertyName">Property name</param>
        /// <returns>Property TimeSpan value or null</returns>
        public static TimeSpan? TimeSpanValue(this NameValueCollection source, string propertyName)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var value = source[propertyName];
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return TimeSpan.Parse(value);
        }

        /// <summary>
        /// Returns TimeSpan value from NameValueCollection. If not property set returns given default.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="defaultValue">Default property value</param>
        /// <returns>Property TimeSpan value or default</returns>
        public static TimeSpan TimeSpanValue(this NameValueCollection source, string propertyName, TimeSpan defaultValue)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var value = source[propertyName];
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            return TimeSpan.Parse(value);
        }
    }
}
