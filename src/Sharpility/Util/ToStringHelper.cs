using System.Collections;
using System.Collections.Generic;
using System.Text;
using Sharpility.Collections;
using Sharpility.Extensions;

namespace Sharpility.Util
{
    public sealed class ToStringHelper
    {
        private readonly object type;
        private readonly OrderedHashImmutableDictionary<string, object>.Builder fields =
            OrderedHashImmutableDictionary<string, object>.CreateBuilder();

        private bool skipNulls;
        private bool generateToStringOfSubProperties;
        private bool generateToStringOfSubFields;

        private ToStringHelper(object type)
        {
            this.type = type;
        }

        public static ToStringHelper Of(object type)
        {
            return new ToStringHelper(type);
        }

        public ToStringHelper Add(string name, object value)
        {
            fields.Put(name, value);
            return this;
        }

        public ToStringHelper AddProperties(bool includeBase = false)
        {
            var properties = Reflections.Properties(type, includeBase);
            foreach (var key in properties.OrderedKeys)
            {
                Add(key, properties.GetIfPresent(key));
            }
            return this;
        }

        public ToStringHelper AddFields(bool includeBase = false)
        {
            var fields = Reflections.Fields(type, includeBase);
            foreach (var key in fields.OrderedKeys)
            {
                Add(key, fields.GetIfPresent(key));
            }
            return this;
        }

        public ToStringHelper AddMembers(bool includeBase = false)
        {
            AddProperties(includeBase);
            AddFields(includeBase);
            return this;
        }
        public ToStringHelper SkipNulls(bool skip = true) 
        {
            skipNulls = skip;
            return this;
        }

        public ToStringHelper GenerateToStringOfSubProperties()
        {
            generateToStringOfSubProperties = true;
            return this;
        }

        public ToStringHelper GenerateToStringOfSubFields()
        {
            generateToStringOfSubFields = true;
            return this;
        }

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
            if (value != null && (generateToStringOfSubProperties || generateToStringOfSubFields))
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
            if (generateToStringOfSubProperties)
            {
                helper.AddProperties()
                    .GenerateToStringOfSubProperties();
            }
            if (generateToStringOfSubFields)
            {
                helper.AddFields()
                    .GenerateToStringOfSubFields();
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
