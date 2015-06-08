using System.Collections;
using System.Collections.Generic;
using System.Text;
using Sharpility.Collections;
using Sharpility.Extensions;

namespace Sharpility.Util
{
    /// <summary>
    /// Utility for building toString representation of object.
    /// </summary>
    public sealed class ToStringHelper
    {
        private readonly object type;
        private readonly OrderedHashImmutableDictionary<string, object>.Builder fields =
            OrderedHashImmutableDictionary<string, object>.CreateBuilder();

        private bool skipNulls;
        private bool generateToStringOfProperties;
        private bool generateToStringOfFields;

        private ToStringHelper(object type)
        {
            this.type = type;
        }

        /// <summary>
        /// Creates new ToStringHelper for given object
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>ToStringHelper</returns>
        public static ToStringHelper Of(object obj)
        {
            return new ToStringHelper(obj);
        }

        /// <summary>
        /// Adds entry as 'name:value' in generated toString.
        /// </summary>
        /// <param name="name">Printed name</param>
        /// <param name="value">Printed value</param>
        /// <returns>this</returns>
        public ToStringHelper Add(string name, object value)
        {
            fields.Put(name, value);
            return this;
        }

        /// <summary>
        /// Adds all object properties as entries in generated toString.
        /// </summary>
        /// <param name="includeBase">Should include base class properties, default: False</param>
        /// <returns>this</returns>
        public ToStringHelper AddProperties(bool includeBase = false)
        {
            var properties = Reflections.Properties(type, includeBase);
            foreach (var key in properties.OrderedKeys)
            {
                Add(key, properties.GetIfPresent(key));
            }
            return this;
        }

        /// <summary>
        /// Adds all object fields as entries in generated toString.
        /// </summary>
        /// <param name="includeBase">Should include base class properties, default: False</param>
        /// <returns>this</returns>
        public ToStringHelper AddFields(bool includeBase = false)
        {
            var fields = Reflections.Fields(type, includeBase);
            foreach (var key in fields.OrderedKeys)
            {
                Add(key, fields.GetIfPresent(key));
            }
            return this;
        }

        /// <summary>
        /// Adds all object properties and fields as entries in generated toString.
        /// </summary>
        /// <param name="includeBase">Should include base class properties and fields, default: False</param>
        /// <returns>this</returns>
        public ToStringHelper AddMembers(bool includeBase = false)
        {
            AddProperties(includeBase);
            AddFields(includeBase);
            return this;
        }

        /// <summary>
        /// Skips printing null values.
        /// </summary>
        /// <param name="skip">Should skip printing null values, default: True</param>
        /// <returns>this</returns>
        public ToStringHelper SkipNulls(bool skip = true) 
        {
            skipNulls = skip;
            return this;
        }

        /// <summary>
        /// Enables toString generation of object properties.
        /// </summary>
        /// <returns>this</returns>
        public ToStringHelper GenerateToStringOfProperties()
        {
            generateToStringOfProperties = true;
            return this;
        }

        /// <summary>
        /// Enables toString generation of object fields.
        /// </summary>
        /// <returns>this</returns>
        public ToStringHelper GenerateToStringOfFields()
        {
            generateToStringOfFields = true;
            return this;
        }
        
        /// <summary>
        /// Builds object toString representation.
        /// </summary>
        /// <returns>toString</returns>
        public override string ToString()
        {
            var dictionary = fields.Build();
            var builder = new StringBuilder(type.GetType().Name);
            builder.Append(" {");
            foreach (var fieldName in dictionary.OrderedKeys)
            {
                var value = dictionary.GetIfPresent(fieldName);
                if (value != null || !skipNulls)
                {
                    var valueStrings = ValueToString(value);
                    builder.AppendFormat("{0}:{1}, ", fieldName, valueStrings);
                }
            }
            var result = builder.ToString();
            return result.EndsWith(", ") ? result.Substring(0, result.Length - 2) + "}" : result + "}";
        }


        private string ValueToString(object value)
        {
            return ShouldGenerateToString(value) ? GenerateToStringOf(value) : Strings.ToString(value);
        }

        private bool ShouldGenerateToString(object value)
        {
            if (value != null && (generateToStringOfProperties || generateToStringOfFields))
            {
                var type = value.GetType();
                return !type.IsPrimitive && !(value is string);
            }
            return false;
        }

        private string GenerateToStringOf(object obj)
        {
            if (obj is IEnumerable)
            {
                return GenerateToStringOfEnumerable((IEnumerable) obj);
            }
            else
            {
                return GenerateToStringOfObject(obj);
            }
        }

        private string GenerateToStringOfObject(object obj)
        {
            var helper = Of(obj);
            if (generateToStringOfProperties)
            {
                helper.AddProperties()
                    .GenerateToStringOfProperties();
            }
            if (generateToStringOfFields)
            {
                helper.AddFields()
                    .GenerateToStringOfFields();
            }
            if (skipNulls)
            {
                helper.SkipNulls();
            }
            return helper.ToString();
        }

        private string GenerateToStringOfEnumerable(IEnumerable enumerable)
        {
            var list = new List<string>();
            foreach (var obj in enumerable)
            {
                list.Add(GenerateToStringOf(obj));
            }
            return Strings.ToString(list);
        }
    }
}
