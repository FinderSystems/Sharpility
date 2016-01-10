using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Sharpility.Collections;

namespace Sharpility.Util
{
    public static class Reflections
    {
        /// <summary>
        /// Returns type of object invoking this method.
        /// </summary>
        /// <param name="framesToSkip">Frames to skip, default: 1</param>
        /// <param name="excludedType">Excluded type</param>
        /// <returns>Type</returns>
        public static Type CurrentType(int framesToSkip = 1, Type excludedType = null)
        {
            Type declaringType;
            do
            {
                var frame = new StackFrame(skipFrames: framesToSkip, fNeedFileInfo: false);
                var method = frame.GetMethod();
                declaringType = method.DeclaringType;
                framesToSkip++;
            } while (ShouldContinueToLookForType(declaringType, excludedType));
            return declaringType;
        }

        private static bool ShouldContinueToLookForType(Type declaringType, Type excludedType)
        {
            if (declaringType == excludedType || declaringType == typeof(Reflections))
            {
                return true;
            }
            return declaringType != null
                && declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase);
        }

        private static bool ShouldContinueToLookForType(Type declaringType)
        {
            return declaringType != null && declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns ordered dictionary with object properties.
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="includeBase">Should include properties from object base class, default: false</param>
        /// <returns>Ordered dictionary with object properties</returns>
        public static OrderedImmutableDictionary<string, object> Properties(object obj, bool includeBase = false)
        {
            var builder = OrderedHashImmutableDictionary<string, object>.Builder();
            const BindingFlags options = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            AppendProperties(obj, obj.GetType(), builder, options, includeBase);
            return builder.Build();
        }

        private static void AppendProperties(object obj, Type type, OrderedHashImmutableDictionary<string, object>.OrderedHashImmutableDictionaryBuilder orderedHashImmutableDictionaryBuilder,
            BindingFlags options, bool includeBase)
        {
            if (type != null)
            {
                var properties = type.GetProperties(options);
                foreach (var property in properties)
                {
                    var value = property.GetValue(obj);
                    orderedHashImmutableDictionaryBuilder.Put(property.Name, value);
                }
                if (includeBase)
                {
                    AppendProperties(obj, type.BaseType, orderedHashImmutableDictionaryBuilder, BindingFlags.NonPublic | BindingFlags.Instance, true);
                }
            }
        }

        /// <summary>
        /// Returns ordered dictionary with object fields.
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="includeBase">Should include fields from object base class, default: false</param>
        /// <returns>Ordered dictionary with object fields</returns>
        public static OrderedImmutableDictionary<string, object> Fields(object obj, bool includeBase = false)
        {
            var builder = OrderedHashImmutableDictionary<string, object>.Builder();
            const BindingFlags options = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            AppendFields(obj, obj.GetType(), builder, options, includeBase);
            return builder.Build();
        }

        private static void AppendFields(object obj, Type type, OrderedHashImmutableDictionary<string, object>.OrderedHashImmutableDictionaryBuilder orderedHashImmutableDictionaryBuilder,
           BindingFlags options, bool includeBase)
        {
            if (type != null)
            {
                var fields = type.GetFields(options);
                foreach (var field in fields.Where(f => !f.Name.EndsWith("k__BackingField")))
                {
                    var value = field.GetValue(obj);
                    orderedHashImmutableDictionaryBuilder.Put(field.Name, value);
                }
                if (includeBase)
                {
                    AppendFields(obj, type.BaseType, orderedHashImmutableDictionaryBuilder, BindingFlags.NonPublic | BindingFlags.Instance, true);
                }
            }
        }

        /// <summary>
        /// Returns calling method name.
        /// </summary>
        /// <returns>method name</returns>
        public static string CallingMethodName()
        {
            var frame = new StackFrame(skipFrames: 2);
            var method = frame.GetMethod();
            return method != null ? method.Name : null;
        }
    }
}
