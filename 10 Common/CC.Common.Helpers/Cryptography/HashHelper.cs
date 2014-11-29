using System.Security.Cryptography;
using System.Text;
using CC.Common.Helpers.Encode;
using CC.Common.Helpers.Instance;

namespace CC.Common.Helpers.Cryptography
{
    public static class HashHelper
    {
        /// <summary>
        /// Calculates MD5 hash from a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToMd5(this byte[] input)
        {
            var _md5Provider = MD5.Create();
            var _bytes = _md5Provider.ComputeHash(input);

            var _hashBuilder = new StringBuilder();

            for (var _byteIndex = 0; _byteIndex < _bytes.Length; _byteIndex++)
            {
                _hashBuilder.Append(_bytes[_byteIndex].ToString("x2"));
            }

            return _hashBuilder.ToString();
        }

        /// <summary>
        /// Calculates MD5 hash from a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToMd5(this string input)
        {
            return input.ToBytes().ToMd5();
        }

        /// <summary>
        /// Verify MD5 hash with a given input string
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool VerifyMd5(this string hash, string input)
        {
            var _inputHash = input.ToMd5();
            return hash.IsEqual(_inputHash);
        }

        /// <summary>
        /// Computes 128 bit Sha1 from string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSha1(this string input)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] _bytes = sha.ComputeHash(input.ToBytes());

            var _hashBuilder = new StringBuilder();

            for (var _byteIndex = 0; _byteIndex < _bytes.Length; _byteIndex++)
            {
                _hashBuilder.Append(_bytes[_byteIndex].ToString("x2"));
            }

            return _hashBuilder.ToString();
        }

        /// <summary>
        /// Verifies Sha1 hash with input
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool VerifySha1(this string hash, string input)
        {
            var _inputHash = input.ToSha1();
            return hash.IsEqual(_inputHash);
        }

        /// <summary>
        /// Generates salt for usage in hash 
        /// generation to increase hash security
        /// </summary>
        /// <param name="saltLen"></param>
        /// <returns></returns>
        public static string GenerateSalt(int saltLen)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[saltLen];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(saltLen);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        /// <summary>
        /// Generates salt with len 8 for usage in hash 
        /// generation to increase hash security
        /// </summary>
        /// <returns></returns>
        public static string GenerateSalt()
        {
            return GenerateSalt(8);
        }
    }
}
