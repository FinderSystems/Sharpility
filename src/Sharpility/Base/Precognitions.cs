using System;
using System.Collections;
using System.Collections.Generic;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Base
{
    public static class Precognitions
    {
        public static void Evaluate(bool condition, string errorMessage)
        {
            Evaluate(condition, () => new ArgumentException(errorMessage));
        }

        public static void Evaluate(Func<bool> condition, string errorMessage)
        {
            Evaluate(condition, () => new ArgumentException(errorMessage));
        }

        public static void Evaluate(bool condition, Func<Exception> exception)
        {
            if (!condition)
            {
                throw exception();
            }
        }

        public static void Evaluate(Func<bool> condition, Func<Exception> exception)
        {
            if (!condition())
            {
                throw exception();
            }
        }

        public static void IsNotNull(object value, string errorMessage)
        {
            IsNotNull(value != null, () => new ArgumentException(errorMessage));
        }

        public static void IsNotNull(object value, Func<Exception> exception)
        {
            Evaluate(value != null, exception);
        }

        public static void IsNull(object value, string errorMessage)
        {
            IsNull(value, () => new ArgumentException(errorMessage));
        }

        public static void IsNull(object value, Func<Exception> exception)
        {
            Evaluate(value == null, exception);
        }

        public static void IsNotEmpty(string value, string message)
        {
            IsNotEmpty(value, () => new ArgumentException(message));
        }

        public static void IsNotEmpty(string value, Func<Exception> exception)
        {
            Evaluate(!string.IsNullOrEmpty(value), exception);
        }

        public static void IsNotEmpty<T>(IEnumerable<T> collection, string errorMessage)
        {
            Evaluate(collection.IsNotEmpty(), () => new ArgumentException(errorMessage));
        }

        public static void IsNotEmpty<T>(IEnumerable<T> collection, Func<Exception> exception)
        {
            Evaluate(collection.IsNotEmpty(), exception);
        }

        public static void IsEmpty<T>(IEnumerable<T> collection, string errorMessage)
        {
            Evaluate(collection.IsEmpty(), () => new ArgumentException(errorMessage));
        }

        public static void IsEmpty<T>(IEnumerable<T> collection, Func<Exception> exception)
        {
            Evaluate(collection.IsEmpty(), exception);
        }

        public static void IsSingleton<T>(IEnumerable<T> collection, string errorMessage)
        {
            Evaluate(collection.IsSingleton(), () => new ArgumentException(errorMessage));
        }

        public static void IsSingleton<T>(IEnumerable<T> collection, Func<Exception> exception)
        {
            Evaluate(collection.IsSingleton(), exception);
        }

        public static void EvaluateEnum<T>(string enumValue)
            where T : struct, IConvertible
        {
            EvaluateEnum<T>(enumValue, availableValues => new ArgumentException(
                String.Format("Invalid enum value: '{0}' for {1} expected one of {2}", 
                enumValue, typeof(T), Strings.ToString(availableValues))));
        }

        public static void EvaluateEnum<T>(string enumValue, string message)
            where T : struct, IConvertible
        {
            EvaluateEnum<T>(enumValue, () => new ArgumentException(message));
        }

        public static void EvaluateEnum<T>(string enumValue, Func<Exception> exception)
            where T : struct, IConvertible
        {
            EvaluateEnum<T>(enumValue, availableValues => exception());
        }

        public static void EvaluateEnum<T>(string enumValue, Converter<IEnumerator, Exception> exception)
            where T : struct, IConvertible
        {
            try
            {
                Enums.ValueOf<T>(enumValue);
            }
            catch (ArgumentException)
            {
                var types = Enum.GetValues(typeof(T));
                throw exception(types.GetEnumerator());
            }
        }
    }
}
