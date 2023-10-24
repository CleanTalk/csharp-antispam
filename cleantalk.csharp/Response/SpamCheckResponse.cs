// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace cleantalk.csharp.Response
{
    [DataContract]
    public class SpamCheckResponse
    {
        [DataMember(Name = "data")]
        public Dictionary<string, List<CheckedResults>> Data { get; set; }

        public class CheckedResults
        {
            /// <summary>
            /// the marker that defines the record status in the blacklists 0|1 (shows if the record is blacklisted right now)
            /// </summary>
            [DataMember(EmitDefaultValue = false)] 
            public bool appears { get; set; }

            /// <summary>
            /// address sha256 hash
            /// </summary>
            [DataMember(EmitDefaultValue = false)] 
            public string sha256 { get; set; }

            /// <summary>
            /// special net-type if any (there might be new types in the future)
            /// </summary>
            [DataMember(EmitDefaultValue = false)] 
            public NetworkType? network_type { get; set; }

            /// <summary>
            /// a rating of spam activity from 0 to 100%. 100% means certain spam (the ratio of blocked requests to all).
            /// The "spam_rate" parameter can have a value from 0 to 1: "spam_rate": "1" — 100% of requests were spam, "spam_rate": "0.75" — 75% of requests were blocked as spam
            /// </summary>
            [DataMember(EmitDefaultValue = false)] 
            public decimal? spam_rate { get; set; }

            /// <summary>
            /// date and time of the first spam activity
            /// </summary>
            [DataMember(EmitDefaultValue = false)] 
            public DateTime? submitted { get; set; }

            /// <summary>
            /// date and time of the last status update
            /// </summary>
            [DataMember(EmitDefaultValue = false)] 
            public DateTime? updated { get; set; }

            /// <summary>
            /// letter country code of the IP, in ISO 3166-1 alpha-2
            /// </summary>
            [DataMember(EmitDefaultValue = false)]
            public string country { get; set; }

            /// <summary>
            /// check email for existence (0 - not exists, 1 - exists, null - Empty status, the address is not in our database)
            /// </summary>
            [DataMember(EmitDefaultValue = false)] 
            public bool? exists { get; set; }

            /// <summary>
            /// check email for disposable (0 - normal, 1 - disposable, null - Empty status, the address is not in our database)
            /// </summary>
            [DataMember(EmitDefaultValue = false)] 
            public bool? disposable_email { get; set; }

            /// <summary>
            /// is a number of websites that reported spam activity of the record. It can be from 0 up to 9999 (shows total activity from the first time the record was caught)
            /// </summary>
            [DataMember(EmitDefaultValue = false)] 
            public int frequency { get; set; }

            /// <summary>
            /// is a number of spam requests from the address that were blocked by Anti-Spam in the last 24 hours.
            /// </summary>
            [DataMember(EmitDefaultValue = false)] 
            public int spam_frequency_24h { get; set; }

            /// <summary>
            ///  IP address found in Anti-Apam blacklist(0 - not found, 1 - found)
            /// </summary>
            [DataMember(EmitDefaultValue = false)] 
            public bool in_antispam { get; set; }

            /// <summary>
            /// the previous Anti-Apam blacklist status. It can show was the record blacklisted or not (0 - wasn't blacklisted, 1 - was blacklisted, NULL - no change)
            /// </summary>
            [DataMember(EmitDefaultValue = false)] 
            public bool? in_antispam_previous { get; set; }

            /// <summary>
            /// the date of changing Anti-Apam blacklist status (NULL - no change)
            /// </summary>
            [DataMember(EmitDefaultValue = false)] 
            public DateTime? in_antispam_updated { get; set; }

            /// <summary>
            /// IP address found in security blacklist (brute-force) (0 - not found, 1 - found)
            /// </summary>
            [DataMember(EmitDefaultValue = false)] 
            public bool in_security { get; set; }

            /// <summary>
            /// number of domains found on IPv4 address.
            /// </summary>
            [DataMember(EmitDefaultValue = false)] 
            public int domains_count { get; set; }

            /// <summary>
            /// list of hosted domains/sites on IPv4 address. Method shows first 1000 domains.
            /// </summary>
            [DataMember(EmitDefaultValue = false)] 
            public string domains_list { get; set; }
        }
    }

    public enum NetworkType
    {
        hosting,
        @public,
        paid_vpn,
        tor,
        unknown
    }
}