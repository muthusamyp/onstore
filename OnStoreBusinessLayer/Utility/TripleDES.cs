using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using KnowledgeAdventure.Foundation.Utility;

namespace KnowledgeAdventure.Foundation.Utility.Crypto
{
    /// <summary>
    /// Utility methods for the 3DES encryption algorithm.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")]
    public static class TripleDES
    {
        /// <summary>
        /// Encrypts the unicode.
        /// </summary>
        /// <param name="plaintext">The plaintext.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string EncryptUnicode(string plaintext, string key)
        {
            Check.NotEmpty(key, "key");

            if (string.IsNullOrEmpty(plaintext))
            {
                return null;
            }

            TripleDESCryptoServiceProvider desProvider = CreateProviderUnicode(key);
            ICryptoTransform desEncrypt = desProvider.CreateEncryptor();

            byte[] buffer = UnicodeEncoding.Unicode.GetBytes(plaintext);

            return Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
        }

        /// <summary>
        /// Decrypts the unicode.
        /// </summary>
        /// <param name="ciphertext">The ciphertext.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string DecryptUnicode(string ciphertext, string key)
        {
            Check.NotEmpty(key, "key");

            if (string.IsNullOrEmpty(ciphertext))
            {
                return null;
            }

            TripleDESCryptoServiceProvider desProvider = CreateProviderUnicode(key);
            ICryptoTransform desEncrypt = desProvider.CreateDecryptor();

            byte[] buffer = Convert.FromBase64String(ciphertext);

            return UnicodeEncoding.Unicode.GetString(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
        }

        /// <summary>
        /// Creates the provider unicode.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static TripleDESCryptoServiceProvider CreateProviderUnicode(string key)
        {
            TripleDESCryptoServiceProvider desProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();

            desProvider.Key = md5Provider.ComputeHash(UnicodeEncoding.Unicode.GetBytes(key));
            desProvider.Mode = CipherMode.ECB;

            return desProvider;
        }

        /// <summary>
        /// Returns a base 64 encoded encrypted string for this plaintext with this key.
        /// </summary>
        /// <param name="plaintext">The plaintext.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string EncryptASCII(string plaintext, string key)
        {
            Check.NotEmpty(key, "key");

            if (string.IsNullOrEmpty(plaintext))
            {
                return null;
            }

            TripleDESCryptoServiceProvider desProvider = CreateProviderASCII(key);
            ICryptoTransform desEncrypt = desProvider.CreateEncryptor();

            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(plaintext);

            return Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
        }

        /// <summary>
        /// Returns the plaintext for this base 64 encoded ciphertext for this key.
        /// </summary>
        /// <param name="ciphertext">The ciphertext.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string DecryptASCII(string ciphertext, string key)
        {
            Check.NotEmpty(key, "key");

            if (string.IsNullOrEmpty(ciphertext))
            {
                return null;
            }

            TripleDESCryptoServiceProvider desProvider = CreateProviderASCII(key);
            ICryptoTransform desEncrypt = desProvider.CreateDecryptor();

            byte[] buffer = Convert.FromBase64String(ciphertext);
            return ASCIIEncoding.ASCII.GetString(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
        }

        /// <summary>
        /// Creates the provider ASCII.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static TripleDESCryptoServiceProvider CreateProviderASCII(string key)
        {
            TripleDESCryptoServiceProvider desProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();

            desProvider.Key = md5Provider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            desProvider.Mode = CipherMode.ECB;

            return desProvider;
        }
    }
}
