using System;
using System.Runtime.Serialization;

namespace cleantalk.csharp
{
    [DataContract(Name = "sender_info")]
    public class SenderInfo
    {
        [DataMember(Name = "request_submit_time")]
        public long RequestSubmitTime { get; set; }

        [DataMember(Name = "cms_lang")]
        public string CmsLang { get; set; }

        [DataMember(Name = "REFFERRER")]
        public string Refferrer { get; set; }

        [DataMember(Name = "USER_AGENT")]
        public string UserAgent { get; set; }

        [DataMember(Name = "profile")]
        public bool Profile { get; set; }

        [DataMember(Name = "sender_url")]
        public string SenderUrl { get; set; }

        public SenderInfo()
        {
            this.RequestSubmitTime = DateTime.Now.ToFileTimeUtc();
            this.CmsLang = String.Empty;
            this.Refferrer = String.Empty;
            this.UserAgent = String.Empty;
        }
    }
}
