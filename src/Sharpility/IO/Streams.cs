using System.IO;
using System.Text;

namespace Sharpility.IO
{
    /// <summary>
    /// Utility for streams.
    /// </summary>
    public static class Streams
    {
        /// <summary>
        /// Read stream content as string.
        /// Stream is not closed.
        /// </summary>
        /// <param name="stream">stream</param>
        /// <param name="encoding">encoding, Encoding.Default if not provided</param>
        /// <param name="bufferSize">bufferSize, 1024 if not provided</param>
        /// <returns>stream content</returns>
        public static string ReadAll(Stream stream, Encoding encoding = null, int bufferSize = 1024)
        {
            using (var reader = new StreamReader(
                stream: stream, 
                encoding: encoding ?? Encoding.Default,
                detectEncodingFromByteOrderMarks: true,
                bufferSize: bufferSize,
                leaveOpen: true))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
