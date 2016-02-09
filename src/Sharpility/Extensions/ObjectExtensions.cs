
using System;
using Sharpility.Base;
using Sharpility.Util;

namespace Sharpility.Extensions
{
    /// <summary>
    /// Extensions of object class.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Checks object equality by comparing properties.
        /// Collections are supported.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="that">compared object</param>
        /// <returns>true if objects properties are equals</returns>
        public static bool EqualsByProperties(this object source, object that)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var type = source.GetType();
            if (that != null && that.GetType() == type)
            {
                var thisProperties = Reflections.Properties(source);
                var thatProperties = Reflections.Properties(that);
                foreach (var key in thisProperties.OrderedKeys)
                {
                    var thisPropertyValue = thisProperties.GetIfPresent(key);
                    var thatPropertyValue = thatProperties.GetIfPresent(key);
                    if (!Objects.Equal(thisPropertyValue, thatPropertyValue))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks object equality by comparing fields.
        /// Collections are supported.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="that">compared object</param>
        /// <returns>true if objects fields are equals</returns>
        public static bool EqualsByFields(this object source, object that)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var type = source.GetType();
            if (that != null && that.GetType() == type)
            {
                var thisFields = Reflections.Fields(source);
                var thatFields = Reflections.Fields(that);
                foreach (var key in thisFields.OrderedKeys)
                {
                    var thisFieldValue = thisFields.GetIfPresent(key);
                    var thatFieldValue = thatFields.GetIfPresent(key);
                    if (!Objects.Equal(thisFieldValue, thatFieldValue))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks object equality by comparing properties and fields.
        /// Collections are supported.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="that">compared object</param>
        /// <returns>true if objects properties and fields are equals</returns>
        public static bool EqualsByMembers(this object source, object that)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return EqualsByProperties(source, that) && EqualsByFields(source, that);
        }

        /// <summary>
        /// Generates object hashCode from properties.
        /// Collections are supported.
        /// </summary>
        /// <param name="source">this</param>
        /// <returns>hashCode</returns>
        public static int PropertiesHash(this object source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var properties = Reflections.Properties(source);
            var hashCode = 1;
            foreach (var key in properties.OrderedKeys)
            {
                var value = properties.GetIfPresent(key);
                hashCode = 31 * hashCode + Objects.HashCode(value);
            }
            return hashCode;
        }

        /// <summary>
        /// Generated object hashCode from fields.
        /// Collections are supported.
        /// </summary>
        /// <param name="source">this</param>
        /// <returns>hashCode</returns>
        public static int FieldsHash(this object source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var fields = Reflections.Fields(source);
            var hashCode = 1;
            foreach (var key in fields.OrderedKeys)
            {
                var value = fields.GetIfPresent(key);
                hashCode = 31 * hashCode + Objects.HashCode(value);
            }
            return hashCode;
        }

        /// <summary>
        /// Generated object hashCode from properties and fields.
        /// Collections are supported.
        /// </summary>
        /// <param name="source">this</param>
        /// <returns>hashCode</returns>
        public static int MembersHash(this object source)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var hashCode = source.PropertiesHash();
            hashCode = 31 * hashCode + source.FieldsHash();
            return hashCode;
        }

        /// <summary>
        /// Generate toString implementation using object properties.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="skipNulls">true if null properties should be excluded, false by default</param>
        /// <returns>toString</returns>
        public static string PropertiesToString(this object source, bool skipNulls = false)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return ToStringHelper.Of(source)
                .AddProperties()
                .SkipNulls(skipNulls)
                .ToString();
        }

        /// <summary>
        /// Generate toString implementation using object fields.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="skipNulls">true if null properties should be excluded, false by default</param>
        /// <returns>toString</returns>
        public static string FieldsToString(this object source, bool skipNulls = false)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return ToStringHelper.Of(source)
                .AddFields()
                .SkipNulls(skipNulls)
                .ToString();
        }

        /// <summary>
        /// Generate toString implementation using object properties and fields.
        /// </summary>
        /// <param name="source">this</param>
        /// <param name="skipNulls">true if null properties should be excluded, false by default</param>
        /// <returns>toString</returns>
        public static string MembersToString(this object source, bool skipNulls = false)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            return ToStringHelper.Of(source)
                .AddMembers()
                .SkipNulls(skipNulls)
                .ToString();
        }
    }
}
