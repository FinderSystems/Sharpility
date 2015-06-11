using System.Data.SqlClient;
using NFluent;
using NUnit.Framework;
using Sharpility.Extensions;

namespace Sharpility.Tests.Extensions
{
    [TestFixture]
    public class DbCommandExtensionsTests
    {
        [Test]
        public void ShouldExtendDbCommandBySetParameterMethod()
        {
            // given
            var command = new SqlCommand("test");

            // then
            command.SetParameter("id", 15);

            // then
            Check.That(command.Parameters).HasSize(1);
            Check.That(command.Parameters[0].ParameterName).IsEqualTo("id");
            Check.That(command.Parameters[0].Value).IsEqualTo(15);

        }
    }
}
