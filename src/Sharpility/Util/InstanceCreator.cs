using System;
using System.Linq;
using System.Reflection;

namespace Sharpility.Util
{
    /// <summary>
    /// Utility for creating instance of given type.
    /// Created objects does not require public or empty constructors.
    /// </summary>
    public static class InstanceCreator
    {
        /// <summary>
        /// Creates instance of given type.
        /// </summary>
        /// <typeparam name="T">Type of created object</typeparam>
        /// <returns>Instance of given type</returns>
        public static T CreateIntance<T>()
            where T : class
        {
            var instance = CreateIntance(typeof(T));
            return (T)instance;
        }

        /// <summary>
        /// Creates instance of given type.
        /// </summary>
        /// <param name="type">Type of created object</param>
        ///<returns>Instance of given type</returns>
        public static object CreateIntance(Type type)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var noArgsConstructor = type.GetConstructor(bindingAttr: flags, binder: null,
                types: new Type[0], modifiers: new ParameterModifier[0]);
            if (noArgsConstructor != null)
            {
                return noArgsConstructor.Invoke(new object[0]);
            }
            var constructors = type.GetConstructors(bindingAttr: flags);
            var constructor = constructors.First();
            var parameterTypes = constructor.GetParameters();
            var parameters = new object[parameterTypes.Length];
            for (var i = 0; i < parameterTypes.Length; i++)
            {
                var parameterType = parameterTypes[i].ParameterType;
                parameters[i] = parameterType.IsValueType ? Activator.CreateInstance(parameterType) : null;
            }
            return constructor.Invoke(parameters);
        }
    }
}
