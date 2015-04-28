using System;
using System.Diagnostics;
using System.Reflection;
using Sharpility.Collections;

namespace Sharpility.Util
{
    public static class Reflections
    {
        public static Type CurrentType()
        {
            Type declaringType;
            var framesToSkip = 1;
            do
            {
                var frame = new StackFrame(skipFrames: framesToSkip, fNeedFileInfo: false);
                var method = frame.GetMethod();
                declaringType = method.DeclaringType;
                framesToSkip++;
            } while (ShouldContinueToLookForType(declaringType));
            return declaringType;
        }

        private static bool ShouldContinueToLookForType(Type declaringType)
        {
            return declaringType != null && declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase);
        }

        public static OrderedImmutableDictionary<string, object> Properties(object obj, bool includeBase = false)
        {
            var builder = OrderedHashImmutableDictionary<string, object>.CreateBuilder();
            const BindingFlags options = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            AppendProperties(obj, obj.GetType(), builder, options, includeBase);
            return builder.Build();
        }

        private static void AppendProperties(object obj, Type type, OrderedHashImmutableDictionary<string, object>.Builder builder,
            BindingFlags options, bool includeBase)
        {
            if (type != null)
            {
                var properties = type.GetProperties(options);
                foreach (var property in properties)
                {
                    var value = property.GetValue(obj);
                    builder.Put(property.Name, value);
                }
                if (includeBase)
                {
                    AppendProperties(obj, type.BaseType, builder, BindingFlags.NonPublic | BindingFlags.Instance, true);
                }
            }
        }

        public static OrderedImmutableDictionary<string, object> Fields(object obj, bool includeBase = false)
        {
            var builder = OrderedHashImmutableDictionary<string, object>.CreateBuilder();
            const BindingFlags options = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            AppendFields(obj, obj.GetType(), builder, options, includeBase);
            return builder.Build();
        }

        private static void AppendFields(object obj, Type type, OrderedHashImmutableDictionary<string, object>.Builder builder,
           BindingFlags options, bool includeBase)
        {
            if (type != null)
            {
                var fields = type.GetFields(options);
                foreach (var field in fields)
                {
                    var value = field.GetValue(obj);
                    builder.Put(field.Name, value);
                }
                if (includeBase)
                {
                    AppendFields(obj, type.BaseType, builder, BindingFlags.NonPublic | BindingFlags.Instance, true);
                }
            }
        }

        public static string CallingMethodName()
        {
            var frame = new StackFrame(skipFrames: 1);
            var method = frame.GetMethod();
            return method != null ? method.Name : null;
        }
    }
}
