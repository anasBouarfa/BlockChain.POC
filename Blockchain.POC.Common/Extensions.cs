using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.IO;
using System.Configuration;
using Blockchain.POC.Common;

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

        public static string EncodeNumber(this int num)
        {
            if (num < 1)
                return "";
            int[] nums = new int[9];
            int pos = 0;
            var numbers = "2589314706";

            while (!(num == 0))
            {
                nums[pos] = num % numbers.Length;
                num /= numbers.Length;
                pos += 1;
            }

            string result = "";
            foreach (int numIndex in nums)
                result = numbers[numIndex].ToString() + result;

            return result;
        }

        public static int DecodeToNumber(this string str)
        {
            if (str.Length != 9)
                return -1;
            long num = 0;
            var numbers = "2589314706";
            foreach (char ch in str)
            {
                num *= numbers.Length;
                num += numbers.IndexOf(ch);
            }
            if (num < 1)
                return 0;
            return System.Convert.ToInt32(num);
        }

        public static bool IsNumeric(this string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return regex.IsMatch(text);
        }

        public static string GetJsonHash(this object text)
        {
            return JsonConvert.SerializeObject(text).GetHash();
        }

        public static string Encrypt(this string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(EncrytionConstants.EncryptionKey, EncrytionConstants.InitializationVector);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public static string Decrypt(this string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(EncrytionConstants.EncryptionKey, EncrytionConstants.InitializationVector);
            byte[] inputbuffer = Convert.FromBase64String(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }

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
