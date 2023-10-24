// ReSharper disable InconsistentNaming

using System;
using System.Runtime.Serialization;
using cleantalk.csharp.Enums;

namespace cleantalk.csharp.Request
{
    [DataContract]
    public class CleantalkRequestBase
    {
        public CleantalkRequestBase(string authKey)
        {
            AuthKey = authKey;
        }

        /// <summary>
        ///     Auth key
        ///     @var string
        /// </summary>
        [DataMember(Name = "auth_key")]
        public string AuthKey { get; set; }

        /// <summary>
        ///     Method name
        ///     @var string
        /// </summary>
        [DataMember(Name = "method_name")]
        public string MethodName { get; set; }

        /// <summary>
        ///     All http request headers
        ///     @var string
        /// </summary>
        [DataMember(Name = "all_headers")]
        public string AllHeaders { get; set; }

        /// <summary>
        ///     Processing request data before action
        /// </summary>
        /// <param name="methodType"></param>
        /// <returns></returns>
        public virtual void ValidateAndInit(MethodType methodType)
        {
            if (string.IsNullOrWhiteSpace(AuthKey)) throw new ArgumentNullException("AuthKey is empty");

            MethodName = methodType.ToString();
        }
    }
}