using NFluent;
using NUnit.Framework;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class HashesTests
    {
        [Test]
        public void ShouldReturnNullMd5HashForNullString()
        {
            // given
            const string value = null;

            // when
            var checksum = Hashes.Md5(value);

            // then
            Check.That(checksum).IsNull();
        }

        [Test]
        public void ShouldReturnMd5Hash()
        {
            // given
            const string value = "test";

            // when
            var checksum = Hashes.Md5(value);

            // then
            Check.That(checksum).IsEqualTo("098f6bcd4621d373cade4e832627b4f6");
        }

        [Test]
        public void ShouldReturnNullSha1HashForNullString()
        {
            // given
            const string value = null;

            // when
            var checksum = Hashes.Sha1(value);

            // then
            Check.That(checksum).IsNull();
        }

        [Test]
        public void ShouldReturnSha1Hash()
        {
            // given
            const string value = "test";

            // when
            var checksum = Hashes.Sha1(value);

            // then
            Check.That(checksum).IsEqualTo("a94a8fe5ccb19ba61c4c0873d391e987982fbbd3");
        }
    }
}
