using System.Collections.Generic;
using System.IO;
using System.Text;
using NFluent;
using NUnit.Framework;
using Sharpility.Extensions;
using Sharpility.IO;

namespace Sharpility.Tests.IO
{
    [TestFixture]
    public class StreamsTests
    {
        [Test, TestCaseSource("ReadAllStreamTestCases")]
        public void ShouldReadAllFromStreamWithoutClose(string content, Encoding encoding)
        {
            // given
            Stream stream = new MemoryStream(content.ToBytes(encoding));

            // when
            var readContent = Streams.ReadAll(stream, encoding);
            Streams.ReadAll(stream, encoding);  // stream should not be closed

            // then
            Check.That(readContent).IsEqualTo(content);
        }

        private static IEnumerable<ITestCaseData> ReadAllStreamTestCases()
        {
            const string value = "alice has cancer";
            yield return new TestCaseData(value, null)
                .SetName("Should read all stream content without specified encoding");

            yield return new TestCaseData(value, Encoding.Default)
                .SetName("Should read all stream content using Encdoing.Default");

            yield return new TestCaseData(value, Encoding.UTF8)
                .SetName("Should read all stream content using Encdoing.UTF8");

            yield return new TestCaseData(value, Encoding.ASCII)
                .SetName("Should read all stream content using Encdoing.ASCII");
        }
    }
}
