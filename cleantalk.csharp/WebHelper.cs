using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace cleantalk.csharp
{
    public static class WebHelper
    {
        /// <summary>
        /// Converts input string to byte array
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
        /// Compresses input byte array
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] CompressData(byte[] data)
        {
            using (var writer = new MemoryStream())
            {
                using (var gzip = new GZipStream(writer, CompressionMode.Compress))
                {
                    gzip.Write(data, 0, data.Length);
                }

                return writer.ToArray();
            }
        }

        /// <summary>
        /// Convert input string from ISO8859-1 to UTF8
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ConvertIso88591ToUtf8(string input)
        {
            return String.IsNullOrWhiteSpace(input) ?
                String.Empty :
                Encoding.UTF8.GetString(Encoding.GetEncoding("ISO8859-1").GetBytes(input));
        }

        /// <summary>
        /// Encodes byte array to base64 string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Base64Encode(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        #region JSON serialize/deserialize

        /// <summary>
        /// Serialize object to json string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string JsonSerialize<T>(T obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                var retVal = Encoding.UTF8.GetString(ms.ToArray());

                return retVal;
            }
        }

        /// <summary>
        /// Deserialize object from json string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string json)
        {
            var obj = Activator.CreateInstance<T>();
            var decodedStr = HttpUtility.HtmlDecode(json);
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(decodedStr)))
            {
                var serializer = new DataContractJsonSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(ms);
            }

            return obj;
        }

        #endregion
    }
}
