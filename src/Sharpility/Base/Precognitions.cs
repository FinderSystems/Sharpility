using System;
using System.Collections;
using System.Collections.Generic;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Base
{
    /// <summary>
    /// Simple utility for data validation.
    /// </summary>
    public static class Precognitions
    {
        /// <summary>
        /// Evaluates is statement is true and throws ArgumentException with given message if not.
        /// </summary>
        /// <param name="condition">Evaluated statement</param>
        /// <param name="errorMessage">Message thrown in ArgumentException</param>
        public static void Evaluate(bool condition, string errorMessage)
        {
            Evaluate(condition, () => new ArgumentException(errorMessage));
        }

        /// <summary>
        /// Evaluates is callback statement is true and throws ArgumentException with given message if not.
        /// </summary>
        /// <param name="condition">Evaluated statement callback</param>
        /// <param name="errorMessage">Message thrown in ArgumentException</param>
        public static void Evaluate(Func<bool> condition, string errorMessage)
        {
            Evaluate(condition, () => new ArgumentException(errorMessage));
        }

        /// <summary>
        /// Evaluates is statement is true and throws given exception if not.
        /// </summary>
        /// <param name="condition">Evaluated statement</param>
        /// <param name="exception">Supplier of thrown exception</param>
        public static void Evaluate(bool condition, Func<Exception> exception)
        {
            if (!condition)
            {
                throw exception();
            }
        }

        /// <summary>
        /// Evaluates is statement is true and throws given exception if not.
        /// </summary>
        /// <param name="condition">Evaluated statement callback</param>
        /// <param name="exception">Supplier of thrown exception</param>
        public static void Evaluate(Func<bool> condition, Func<Exception> exception)
        {
            if (!condition())
            {
                throw exception();
            }
        }

        /// <summary>
        /// Evaulates is value is not null and throws ArgumentException with given message if not.
        /// </summary>
        /// <param name="value">Evaluated value</param>
        /// <param name="errorMessage">Message thrown in ArgumentException</param>
        public static void IsNotNull(object value, string errorMessage)
        {
            IsNotNull(value, () => new ArgumentException(errorMessage));
        }

        /// <summary>
        /// Evaulates is value is not null and throws exception if not.
        /// </summary>
        /// <param name="value">Evaluated value</param>
        /// <param name="exception">Supplier of thrown exception</param>
        public static void IsNotNull(object value, Func<Exception> exception)
        {
            Evaluate(value != null, exception);
        }

        /// <summary>
        /// Evaulates is value is null and throws ArgumentException with given message if not.
        /// </summary>
        /// <param name="value">Evaluated value</param>
        /// <param name="errorMessage">Message thrown in ArgumentException</param>
        public static void IsNull(object value, string errorMessage)
        {
            IsNull(value, () => new ArgumentException(errorMessage));
        }

        /// <summary>
        /// Evaulates is value is null and throws exception if not.
        /// </summary>
        /// <param name="value">Evaluated value</param>
        /// <param name="exception">Supplier of thrown exception</param>
        public static void IsNull(object value, Func<Exception> exception)
        {
            Evaluate(value == null, exception);
        }

        /// <summary>
        /// Evaulates is string value is not null or empty and throws ArgumentException if not.
        /// </summary>
        /// <param name="value">Evaluated value</param>
        /// <param name="errorMessage">Message thrown in ArgumentException</param>
        public static void IsNotEmpty(string value, string errorMessage)
        {
            IsNotEmpty(value, () => new ArgumentException(errorMessage));
        }

        /// <summary>
        /// Evaulates is string value is not null or empty and throws exception if not.
        /// </summary>
        /// <param name="value">Evaluated value</param>
        /// <param name="exception">Supplier of thrown exception</param>
        public static void IsNotEmpty(string value, Func<Exception> exception)
        {
            Evaluate(!string.IsNullOrEmpty(value), exception);
        }

        /// <summary>
        /// Evaulates is collection is not null or empty and throws ArgumentException if not.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="collection">Evaluated collection</param>
        /// <param name="errorMessage">Message thrown in ArgumentException</param>
        public static void IsNotEmpty<T>(IEnumerable<T> collection, string errorMessage)
        {
           IsNotEmpty(collection, () => new ArgumentException(errorMessage));
        }

        /// <summary>
        /// Evaulates is collection is not null or empty and throws exception if not.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="collection">Evaluated collection</param>
        /// <param name="exception">Supplier of thrown exception</param>
        public static void IsNotEmpty<T>(IEnumerable<T> collection, Func<Exception> exception)
        {
            Evaluate(collection != null && collection.IsNotEmpty(), exception);
        }

        /// <summary>
        /// Evaulates is collection is empty and throws ArgumentException if not.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="collection">Evaluated collection</param>
        /// <param name="errorMessage">Message thrown in ArgumentException</param>
        public static void IsEmpty<T>(IEnumerable<T> collection, string errorMessage)
        {
            IsEmpty(collection, () => new ArgumentException(errorMessage));
        }

        /// <summary>
        /// Evaulates is collection is empty and throws exception if not.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="collection">Evaluated collection</param>
        /// <param name="exception">Supplier of thrown exception</param>
        public static void IsEmpty<T>(IEnumerable<T> collection, Func<Exception> exception)
        {
            Evaluate(collection.IsEmpty(), exception);
        }

        /// <summary>
        /// Evaulates is collection contains only one element and throws ArgumentException if not.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="collection">Evaluated collection</param>
        /// <param name="errorMessage">Message thrown in ArgumentException</param>
        public static void IsSingleton<T>(IEnumerable<T> collection, string errorMessage)
        {
            IsSingleton(collection, () => new ArgumentException(errorMessage));
        }

        /// <summary>
        /// Evaulates is collection contains only one element and throws exception if not.
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="collection">Evaluated collection</param>
        /// <param name="exception">Supplier of thrown exception</param>
        public static void IsSingleton<T>(IEnumerable<T> collection, Func<Exception> exception)
        {
            Evaluate(collection != null && collection.IsSingleton(), exception);
        }

        /// <summary>
        /// Evaluates is string value can be parsed to given enum type.
        /// If enumValue is invalid ArgumentException is thrown.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="enumValue">Evaluated enum value</param>
        public static void EvaluateEnum<T>(string enumValue)
            where T : struct, IConvertible
        {
            EvaluateEnum<T>(enumValue, availableValues => new ArgumentException(
                String.Format("Invalid enum value: '{0}' for {1} expected one of {2}", 
                Strings.ToString(enumValue), typeof(T), Strings.ToString(availableValues))));
        }

        /// <summary>
        /// Evaluates is string value can be parsed to given enum type.
        /// If enumValue is invalid ArgumentException is thrown.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="enumValue">Evaluated enum value</param>
        /// <param name="message">Message thrown in ArgumentException</param>
        public static void EvaluateEnum<T>(string enumValue, string message)
            where T : struct, IConvertible
        {
            EvaluateEnum<T>(enumValue, () => new ArgumentException(message));
        }

        /// <summary>
        /// Evaluates is string value can be parsed to given enum type.
        /// If enumValue is invalid exception is thrown.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="enumValue">Evaluated enum value</param>
        /// <param name="exception">Supplier of thrown exception</param>
        public static void EvaluateEnum<T>(string enumValue, Func<Exception> exception)
            where T : struct, IConvertible
        {
            EvaluateEnum<T>(enumValue, availableValues => exception());
        }

        /// <summary>
        /// Evaluates is string value can be parsed to given enum type.
        /// If enumValue is invalid exception is thrown.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="enumValue">Evaluated enum value</param>
        /// <param name="exception">Supplier of thrown exception with additional enum values enumerator</param>
        public static void EvaluateEnum<T>(string enumValue, Converter<IEnumerator, Exception> exception)
            where T : struct, IConvertible
        {
            if (enumValue == null)
            {
                var types = Enum.GetValues(typeof(T));
                throw exception(types.GetEnumerator());
            }
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
