using System;
using System.Data;
using Sharpility.Base;

namespace Sharpility.Extensions
{
    /// <summary>
    /// Extensions for IDbCommand interface.
    /// </summary>
    public static class DbCommandExtensions
    {
        /// <summary>
        /// Sets command named parameter value.
        /// </summary>
        /// <param name="source">DB command</param>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Paramater value</param>
        public static void SetParameter(this IDbCommand source, string name, object value)
        {
            Preconditions.IsNotNull(source, () => new ArgumentNullException("source"));
            var parameter = source.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            source.Parameters.Add(parameter);
        }
    }
}
