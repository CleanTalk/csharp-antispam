using System;
using System.Text;

namespace cleantalk.csharp.Helpers
{
    public static class ConvertHelper
    {
        /// <summary>
        ///     Encodes byte array to base64 string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Base64Encode(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        /// <summary>
        ///     Converts input string to byte array
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] StringToUtf8(string str)
        {
            var utf8 = new UTF8Encoding();
            var encodedBytes = utf8.GetBytes(str);

            return encodedBytes;
        }

        /// <summary>
        ///     Convert input string from ISO8859-1 to UTF8
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ConvertIso88591ToUtf8(string input)
        {
            return string.IsNullOrWhiteSpace(input)
                ? string.Empty
                : Encoding.UTF8.GetString(Encoding.GetEncoding("ISO8859-1").GetBytes(input));
        }
    }
}