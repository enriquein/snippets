using System;
using System.Security.Cryptography;
using System.Text;

//Note: Depends on ByteExtensions.cs
namespace ExtensionMethods
{
    /// <summary>
    /// Static class that provides simpler access to MD5 functions.
    /// </summary>
    public static class MD5Provider
    {
        private static MD5CryptoServiceProvider _hasher = new MD5CryptoServiceProvider();
        private static UTF8Encoding _encoder = new UTF8Encoding();
        private static RNGCryptoServiceProvider _randGenerator = new RNGCryptoServiceProvider();

        /// <summary>
        /// Calculates the MD5 hash for the provided string.
        /// </summary>
        /// <param name="plainText">String that will be hashed.</param>
        /// <returns>A 32 character long string representing the calculated hash.</returns>
        public static string GetMD5String(string plainText)
        {
            return GetMD5String(plainText, null);
        }

        /// <summary>
        /// Calculates the MD5 hash for the provided string and salt.
        /// </summary>
        /// <param name="plainText">String that will be hashed.</param>
        /// <param name="salt">Salt to consider in the calculation.</param>
        /// <returns>A 32 character long string representing the calculated hash.</returns>
        public static string GetMD5String(string plainText, string salt)
        {
            return CalculateMD5(plainText, salt).ToStringEx();
        }

        /// <summary>
        /// Calculates the MD5 hash for the provided string.
        /// </summary>
        /// <param name="plainText">String that will be hashed.</param>
        /// <returns>An array of 16 bytes representing the calculated hash.</returns>
        public static byte[] GetMD5Bytes(string plainText)
        {
            return GetMD5Bytes(plainText, null);
        }

        /// <summary>
        /// Calculates the MD5 hash for the provided string and salt.
        /// </summary>
        /// <param name="plainText">String that will be hashed.</param>
        /// <param name="salt">Salt to consider in the calculation.</param>
        /// <returns>An array of 16 bytes representing the calculated hash.</returns>
        public static byte[] GetMD5Bytes(string plainText, string salt)
        {
            return CalculateMD5(plainText, salt);
        }

        /// <summary>
        /// Provides a random salt value generally used to better randomize
        /// MD5 hash values.
        /// </summary>
        /// <returns>A random string suited for use as a salt value.</returns>
        public static string GetRandomSalt()
        {
            byte[] randomNum = new byte[1];
            _randGenerator.GetBytes(randomNum);
            string randomString = DateTime.Now.ToString("mmssfffttMMddHH") + randomNum[0].ToString();
            return CalculateMD5(randomString, null).ToStringEx();
        }

        private static byte[] CalculateMD5(string plainText, string salt)
        {
            salt = salt ?? "";
            return _hasher.ComputeHash(_encoder.GetBytes(plainText + salt));
        }

        
    }
}