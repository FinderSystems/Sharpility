using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sharpility.Base;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Collections
{
    internal sealed class WildcardCollection: IEnumerable
    {
        public Type GenericType { get; private set; }
        private readonly IEnumerable collection;
        private readonly MethodInfo containsMethod;
        private readonly PropertyInfo countProperty;

        private WildcardCollection(IEnumerable collection, Type genericType, MethodInfo containsMethod, PropertyInfo countProperty)
        {
            GenericType = genericType;
            this.collection = collection;
            this.containsMethod = containsMethod;
            this.countProperty = countProperty;
        }

        internal static WildcardCollection Of(IEnumerable enumerable)
        {
            var enumerableType = enumerable.GetType();
            var genericType = GenericTypeOf(enumerable);
            Precognitions.IsNotNull(genericType, "Enumerable is not generic collection");
            var countProperty = enumerableType.GetProperty(name: "Count");
            var containsMethod = enumerableType.GetMethod(name: "Contains", types: new[] {genericType});
            return new WildcardCollection(collection: enumerable, 
                genericType: genericType,
                containsMethod: containsMethod, 
                countProperty: countProperty);
        }

        private static Type GenericTypeOf(IEnumerable enumerable)
        {
            if (enumerable == null)
            {
                return null;
            }
            var type = enumerable.GetType();
            var genericTypes = type.GetGenericArguments();
            return genericTypes.IsNotEmpty() ? genericTypes[0] : null;
        }

        public IEnumerator GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        internal bool Contains(object element)
        {
            return element != null && element.GetType() == GenericType && 
                (bool)containsMethod.Invoke(obj: collection, parameters: new[] { element });
        }

        internal bool ContainsAll(IEnumerable values)
        {
            foreach (var element in values)
            {
                if (!Contains(element))
                {
                    return false;
                }
            }
            return true;
        }

        public int Count
        {
            get { return (int) countProperty.GetValue(obj: collection); }
        }

        public override string ToString()
        {
            return Strings.ToString(collection);
        }
    }
}
