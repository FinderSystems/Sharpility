using System.IO;
using NFluent;
using NUnit.Framework;
using Sharpility.Extensions;
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
        public void ShouldReturnMd5HashFromStream()
        {
            // given
            using (var stream = new MemoryStream("test".ToUtf8Bytes()))
            {

                // when
                var checksum = Hashes.Md5(stream);

                // then
                Check.That(checksum).IsEqualTo("098f6bcd4621d373cade4e832627b4f6");
            }
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

        [Test]
        public void ShouldReturSha1HashFromStream()
        {
            // given
            using (var stream = new MemoryStream("test".ToUtf8Bytes()))
            {

                // when
                var checksum = Hashes.Sha1(stream);

                // then
                Check.That(checksum).IsEqualTo("a94a8fe5ccb19ba61c4c0873d391e987982fbbd3");
            }
        }

        [Test]
        public void ShouldReturnNullSha256HashForNullString()
        {
            // given
            const string value = null;

            // when
            var checksum = Hashes.Sha256(value);

            // then
            Check.That(checksum).IsNull();
        }

        [Test]
        public void ShouldReturnSha256Hash()
        {
            // given
            const string value = "test";

            // when
            var checksum = Hashes.Sha256(value);

            // then
            Check.That(checksum).IsEqualTo("9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08");
        }

        [Test]
        public void ShouldReturSha256HashFromStream()
        {
            // given
            using (var stream = new MemoryStream("test".ToUtf8Bytes()))
            {

                // when
                var checksum = Hashes.Sha256(stream);

                // then
                Check.That(checksum).IsEqualTo("9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08");
            }
        }
    }
}
