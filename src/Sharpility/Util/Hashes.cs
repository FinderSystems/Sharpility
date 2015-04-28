using System;
using System.Security.Cryptography;
using System.Text;
using Sharpility.Extensions;

namespace Sharpility.Util
{
    public static class Hashes
    {
        public static string Md5(string value, Encoding encoding = null)
        {
            if (value == null)
            {
                return null;
            }
            var bytesToHash = value.ToBytes(encoding);
            var hash = MD5.Create().ComputeHash(bytesToHash);
            return ToHexString(hash);
        }

        public static string Sha1(string value, Encoding encoding = null)
        {
            if (value == null)
            {
                return null;
            }
            var bytesToHash = value.ToBytes(encoding);
            var hash = SHA1.Create().ComputeHash(bytesToHash);
            return ToHexString(hash);
        }

        public static string Sha256(string value, Encoding encoding = null)
        {
            if (value == null)
            {
                return null;
            }
            var bytesToHash = value.ToBytes(encoding);
            var hash = SHA256.Create().ComputeHash(bytesToHash);
            return ToHexString(hash);
        }

        private static string ToHexString(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
