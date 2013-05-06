using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace CryptoProviders
{
    public class AesCryptoProvider
    {
        private RijndaelManaged _cryptoProvider;
        private int _iterations = 10;        
    
        private string _salt = "Put here your salt value!";

        public AesCryptoProvider(string key)
        {
            _cryptoProvider = new RijndaelManaged
                {
                    BlockSize = 256,
                    KeySize = 256,
                    IV = GenerateIVFromKey(key),
                    Key = GeneratePaddedKey(key)
                };
        }

        public string EncryptString(string plainText)
        {
            var rijndaelEncryptor = _cryptoProvider.CreateEncryptor();
            byte[] cipherBytes;

            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, rijndaelEncryptor, CryptoStreamMode.Write))
            {
                var plainBytes = Encoding.UTF8.GetBytes(plainText);
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();
                cipherBytes = memoryStream.ToArray();
            }

            return Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
        }

        public string DecryptString(string cipherText)
        {
            ICryptoTransform rijndaelDecryptor = _cryptoProvider.CreateDecryptor();
            string plainText = String.Empty;

            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, rijndaelDecryptor, CryptoStreamMode.Write))
            {
                var cipherBytes = Convert.FromBase64String(cipherText);
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] plainBytes = memoryStream.ToArray();
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }

            return plainText;
        }

        private byte[] GeneratePaddedKey(string key)
        {
            var derivedBytes = new Rfc2898DeriveBytes(key, Encoding.UTF8.GetBytes(_salt), _iterations);
            return derivedBytes.GetBytes(32);
        }

        private byte[] GenerateIVFromKey(string key)
        {
            var derivedBytes = new Rfc2898DeriveBytes(key.Reverse().ToString(), Encoding.UTF8.GetBytes(_salt), _iterations);
            return derivedBytes.GetBytes(32);
        }
    }
}
