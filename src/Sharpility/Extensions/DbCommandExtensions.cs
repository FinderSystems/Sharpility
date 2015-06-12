using System.Data;

namespace Sharpility.Extensions
{
    /// <summary>
    /// Extensions for IDbCommand interface.
    /// </summary>
    public static class DbCommandExtensions
    {
        public static void SetParameter(this IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }
    }
}
