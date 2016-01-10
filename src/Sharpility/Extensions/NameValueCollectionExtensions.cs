
using System;
using System.Collections.Specialized;

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
        /// <param name="settings">this</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="defaultValue">Property default value</param>
        /// <returns>Property int value or default</returns>
        public static int IntValue(this NameValueCollection settings, string propertyName, int defaultValue)
        {
            var value = settings[propertyName];
            return string.IsNullOrEmpty(value) ? defaultValue : int.Parse(value);
        }

        /// <summary>
        /// Returns int value from NameValueCollection. If property not set returns null.
        /// </summary>
        /// <param name="settings">this</param>
        /// <param name="propertyName">Property name</param>
        /// <returns>Property int value or null</returns>
        public static int? IntValue(this NameValueCollection settings, string propertyName)
        {
            var value = settings[propertyName];
            return string.IsNullOrEmpty(value) ? (int?)null : int.Parse(value);
        }

        /// <summary>
        /// Returns boolean value from NameValueCollection.
        /// </summary>
        /// <param name="settings">this</param>
        /// <param name="propertyName">Property name</param>
        /// <returns>Property boolean value</returns>
        public static bool BoolValue(this NameValueCollection settings, string propertyName)
        {
            var value = settings[propertyName];
            return "true".Equals(value, StringComparison.InvariantCultureIgnoreCase) ||
                   "1".Equals(value);
        }

        /// <summary>
        /// Returns boolean value from NameValueCollection. If not property not set returns given default.
        /// </summary>
        /// <param name="settings">this</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="defaultValue">Property default value</param>
        /// <returns>Property boolean value or default</returns>
        public static bool BoolValue(this NameValueCollection settings, string propertyName, bool defaultValue)
        {
            var value = settings[propertyName];
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
        /// <param name="settings">this</param>
        /// <param name="propertyName">Property name</param>
        /// <returns>Property TimeSpan value or null</returns>
        public static TimeSpan? TimeSpanValue(this NameValueCollection settings, string propertyName)
        {
            var value = settings[propertyName];
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return TimeSpan.Parse(value);
        }

        /// <summary>
        /// Returns TimeSpan value from NameValueCollection. If not property set returns given default.
        /// </summary>
        /// <param name="settings">this</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="defaultValue">Default property value</param>
        /// <returns>Property TimeSpan value or default</returns>
        public static TimeSpan TimeSpanValue(this NameValueCollection settings, string propertyName, TimeSpan defaultValue)
        {
            var value = settings[propertyName];
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            return TimeSpan.Parse(value);
        }
    }
}
