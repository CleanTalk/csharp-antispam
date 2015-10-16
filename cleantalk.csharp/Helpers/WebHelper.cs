using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace cleantalk.csharp.Helpers
{
    public static class WebHelper
    {
        /// <summary>
        ///     Compresses input byte array
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
        ///     Deserialize object from json string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string json)
        {
            if (json == null) return default(T);

            var obj = Activator.CreateInstance<T>();
            var decodedStr = HttpUtility.HtmlDecode(json);
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(decodedStr)))
            {
                var serializer = new DataContractJsonSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(ms);
            }

            return obj;
        }

        /// <summary>
        ///     Serialize object to json string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string JsonSerialize<T>(T obj)
        {
            if (obj == null) return null;

            var serializer = new DataContractJsonSerializer(obj.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                var retVal = Encoding.UTF8.GetString(ms.ToArray());

                return retVal;
            }
        }
    }
}