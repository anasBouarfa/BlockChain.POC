using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using System.Text.RegularExpressions;

namespace System
{
    public static class Extensions
    {
        public static string GetHash(this byte[] bytes)
        {
            try
            {
                if (bytes != null && bytes.Length > 0)
                {
                    return Convert.ToBase64String(SHA256.Create().ComputeHash(bytes));
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public static bool IsNumbers(this string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return regex.IsMatch(text);
        }

        public static string GetJsonHash(this object text)
        {
            return JsonConvert.SerializeObject(text).GetHash();
        }

        //public static string Encrypt(this string chainCharacters, string key)
        //{
        //    if (chainCharacters == null)
        //    {
        //        throw new ArgumentNullException(nameof(chainCharacters));
        //    }

        //    if (key == null)
        //    {
        //        throw new ArgumentNullException(nameof(key));
        //    }

        //    var md5Algorithm = MD5CryptoServiceProvider.Create();

        //    byte[] secretKey = md5Algorithm.ComputeHash(Encoding.UTF8.GetBytes(key));

        //    var bcEngine = new AesCryptoServiceProvider();

        //    var aa =AesCryptoServiceProvider.Create();

        //    Org.BouncyCastle.Crypto.Engines.AesEngine()

        //    aa.CreateEncryptor(secretKey, secretKey).


        //    symmetricKeyAlgorithmProvider.Init(true, new CipherPa)

        //    symmetricKeyAlgorithmProvider.CreateEncryptor().


        //    ISymmetricKeyAlgorithmProvider symmetricKeyAlgorithmProvider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.TripleDesEcbPkcs7);

        //    using (ICryptographicKey cryptographicKey = symmetricKeyAlgorithmProvider.CreateSymmetricKey(secretKey))
        //    {
        //        using (ICryptoTransform iCryptoTransform = WinRTCrypto.CryptographicEngine.CreateEncryptor(cryptographicKey))
        //        {
        //            byte[] input = Encoding.UTF8.GetBytes(chainCharacters);
        //            byte[] output = iCryptoTransform.TransformFinalBlock(input, 0, input.Length);

        //            return Convert.ToBase64String(output, 0, output.Length);
        //        }
        //    }
        //}

        public static string GetHash(this string text)
        {
            try
            {
                if (!text.IsNullOrWhitespace())
                {
                    return Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(text)));
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public static bool IsNullOrWhitespace(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }
    }
}
