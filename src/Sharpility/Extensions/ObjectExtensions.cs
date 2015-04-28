
using Sharpility.Util;

namespace Sharpility.Extensions
{
    public static class ObjectExtensions
    {
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

        public static bool EqualsByMembers(this object obj, object that)
        {
            return EqualsByProperties(obj, that) && EqualsByFields(obj, that);
        }

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

        public static int MembersHash(this object obj)
        {
            var hashCode = obj.PropertiesHash();
            hashCode = 31 * hashCode + obj.FieldsHash();
            return hashCode;
        }

        public static string PropertiesToString(this object obj, bool skipNulls = false)
        {
            return ToStringHelper.Of(obj)
                .AddProperties()
                .SkipNulls(skipNulls)
                .ToString();
        }

        public static string FieldsToString(this object obj, bool skipNulls = false)
        {
            return ToStringHelper.Of(obj)
                .AddFields()
                .SkipNulls(skipNulls)
                .ToString();
        }

        public static string MembersToString(this object obj, bool skipNulls = false)
        {
            return ToStringHelper.Of(obj)
                .AddMembers()
                .SkipNulls(skipNulls)
                .ToString();
        }
    }
}
