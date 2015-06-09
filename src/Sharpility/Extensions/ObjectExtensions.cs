
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
        /// <param name="obj">this</param>
        /// <param name="that">compared object</param>
        /// <returns>true if objects properties are equals</returns>
        public static bool EqualsByProperties(this object obj, object that)
        {
            var type = obj.GetType();
            if (that != null && that.GetType() == type)
            {
                var thisProperties = Reflections.Properties(obj);
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
        /// <param name="obj">this</param>
        /// <param name="that">compared object</param>
        /// <returns>true if objects fields are equals</returns>
        public static bool EqualsByFields(this object obj, object that)
        {
            var type = obj.GetType();
            if (that != null && that.GetType() == type)
            {
                var thisFields = Reflections.Fields(obj);
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
        /// <param name="obj">this</param>
        /// <param name="that">compared object</param>
        /// <returns>true if objects properties and fields are equals</returns>
        public static bool EqualsByMembers(this object obj, object that)
        {
            return EqualsByProperties(obj, that) && EqualsByFields(obj, that);
        }

        /// <summary>
        /// Generates object hashCode from properties.
        /// Collections are supported.
        /// </summary>
        /// <param name="obj">this</param>
        /// <returns>hashCode</returns>
        public static int PropertiesHash(this object obj)
        {
            var properties = Reflections.Properties(obj);
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
        /// <param name="obj">this</param>
        /// <returns>hashCode</returns>
        public static int FieldsHash(this object obj)
        {
            var fields = Reflections.Fields(obj);
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
        /// <param name="obj">this</param>
        /// <returns>hashCode</returns>
        public static int MembersHash(this object obj)
        {
            var hashCode = obj.PropertiesHash();
            hashCode = 31 * hashCode + obj.FieldsHash();
            return hashCode;
        }

        /// <summary>
        /// Generate toString implementation using object properties.
        /// </summary>
        /// <param name="obj">this</param>
        /// <param name="skipNulls">true if null properties should be excluded, false by default</param>
        /// <returns>toString</returns>
        public static string PropertiesToString(this object obj, bool skipNulls = false)
        {
            return ToStringHelper.Of(obj)
                .AddProperties()
                .SkipNulls(skipNulls)
                .ToString();
        }

        /// <summary>
        /// Generate toString implementation using object fields.
        /// </summary>
        /// <param name="obj">this</param>
        /// <param name="skipNulls">true if null properties should be excluded, false by default</param>
        /// <returns>toString</returns>
        public static string FieldsToString(this object obj, bool skipNulls = false)
        {
            return ToStringHelper.Of(obj)
                .AddFields()
                .SkipNulls(skipNulls)
                .ToString();
        }

        /// <summary>
        /// Generate toString implementation using object properties and fields.
        /// </summary>
        /// <param name="obj">this</param>
        /// <param name="skipNulls">true if null properties should be excluded, false by default</param>
        /// <returns>toString</returns>
        public static string MembersToString(this object obj, bool skipNulls = false)
        {
            return ToStringHelper.Of(obj)
                .AddMembers()
                .SkipNulls(skipNulls)
                .ToString();
        }
    }
}
