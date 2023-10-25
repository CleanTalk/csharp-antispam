// ReSharper disable InconsistentNaming

using System.Runtime.Serialization;

namespace cleantalk.csharp.Request
{
    [DataContract]
    public class SpamCheckRequest
    {
        public SpamCheckRequest(string authKey)
        {
            AuthKey = authKey;
        }

        /// <summary>
        ///     Auth key
        ///     @var string
        /// </summary>
        [IgnoreDataMember]
        public string AuthKey { get; set; }

        /// <summary>
        /// Multiple Records Check: you can submit multiple records to test per 1 call, to do that use POST options.
        /// data — string with records to check separated by ','.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string data { get; set; }

        /// <summary>
        ///     IP address to check (IPv4 or IPv6 standard format)
        /// </summary>
        public string ip { get; set; }

        /// <summary>
        ///     e-mail address to check (The result is given for the last 6 months)
        /// </summary>
        public string email { get; set; }

        /// <summary>
        ///     date to check for statistics in YYYY-MM-DD format (It can be applied only to IP addresses)
        /// </summary>
        public string date { get; set; }

        /// <summary>
        ///     email SHA256 hash
        /// </summary>
        public string email_SHA256 { get; set; }

        /// <summary>
        ///     IPv4 address SHA256 hash
        /// </summary>
        public string ip4_SHA256 { get; set; }

        /// <summary>
        ///     IPv6 address SHA256 hash
        /// </summary>
        public string ip6_SHA256 { get; set; }
    }
}