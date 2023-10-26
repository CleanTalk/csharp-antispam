// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace cleantalk.csharp.Response
{
    [DataContract]
    public class IpInfoResponse
    {
        [DataMember(Name = "data")] public Dictionary<string, CheckedResults> Data { get; set; }

        [DataContract]
        public class CheckedResults
        {
            /// <summary>
            /// country code
            /// </summary>
            [DataMember(EmitDefaultValue = false)]
            public string country_code { get; set; }

            /// <summary>
            /// country name
            /// </summary>
            [DataMember(EmitDefaultValue = false)]
            public string country_name { get; set; }

            /// <summary>
            /// request handling error
            /// </summary>
            [DataMember(EmitDefaultValue = false)]
            public string error { get; set; }
        }
    }
}