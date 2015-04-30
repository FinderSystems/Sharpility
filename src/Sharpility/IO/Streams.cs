using System.IO;

namespace Sharpility.IO
{
    public static class Streams
    {
        public static string ReadAll(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
