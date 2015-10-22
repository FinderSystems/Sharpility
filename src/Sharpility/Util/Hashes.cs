using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Sharpility.Extensions;

namespace Sharpility.Util
{
    /// <summary>
    /// Hashes Utility.
    /// </summary>
    public static class Hashes
    {
        /// <summary>
        /// Generates MD5 hash in hex string.
        /// </summary>
        /// <param name="value">Hashed value</param>
        /// <param name="encoding">Hash encoding, Encoding.Default if not provided</param>
        /// <returns>MD5 hash</returns>
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

        /// <summary>
        /// Generates MD5 hash in hex string.
        /// </summary>
        /// <param name="inputStream">Input stream to hash</param>
        /// <returns>MD5 hash</returns>
        public static string Md5(Stream inputStream)
        {
            var hash = MD5.Create().ComputeHash(inputStream);
            return ToHexString(hash);
        }

        /// <summary>
        /// Generates SHA-1 hash in hex string.
        /// </summary>
        /// <param name="value">Hashed value</param>
        /// <param name="encoding">Hash encoding, Encoding.Default if not provided</param>
        /// <returns>SHA-1 hash</returns>
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

        /// <summary>
        /// Generates SHA-1 hash in hex string.
        /// </summary>
        /// <param name="inputStream">Input stream to hash</param>
        /// <returns>SHA-1 hash</returns>
        public static string Sha1(Stream inputStream)
        {
            var hash = SHA1.Create().ComputeHash(inputStream);
            return ToHexString(hash);
        }

        /// <summary>
        /// Generates SHA-256 hash in hex string.
        /// </summary>
        /// <param name="value">Hashed value</param>
        /// <param name="encoding">Hash encoding, Encoding.Default if not provided</param>
        /// <returns>SHA-256 hash</returns>
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

        /// <summary>
        /// Generates SHA-256 hash in hex string.
        /// </summary>
        /// <param name="inputStream">Input stream to hash</param>
        /// <returns>SHA-256 hash</returns>
        public static string Sha256(Stream inputStream)
        {
            var hash = SHA256.Create().ComputeHash(inputStream);
            return ToHexString(hash);
        }

        private static string ToHexString(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
