using System;
using System.Runtime.Serialization;

namespace cleantalk.csharp
{
    [DataContract(Name = "sender_info")]
    public class SenderInfo
    {
        [DataMember(Name = "request_submit_time")]
        public long RequestSubmitTime { get; set; }

        /// <summary>
        /// CMS lang (ru or en)
        /// </summary>
        [DataMember(Name = "cms_lang")]
        public string CmsLang { get; set; }

        /// <summary>
        ///  the value of $_SERVER['HTTP_REFERER']
        /// </summary>
        [DataMember(Name = "REFFERRER")]
        public string Refferrer { get; set; }

        /// <summary>
        /// the value of $_SERVER['HTTP_USER_AGENT']
        /// </summary>
        [DataMember(Name = "USER_AGENT")]
        public string UserAgent { get; set; }

        /// <summary>
        /// 0|1, do or not check profile, if value is 1 then skip check of text relevance
        /// </summary>
        [DataMember(Name = "profile")]
        public bool? Profile { get; set; }

        /// <summary>
        ///  sender URL in the form
        /// </summary>
        [DataMember(Name = "sender_url")]
        public string SenderUrl { get; set; }

        public SenderInfo()
        {
            RequestSubmitTime = DateTime.Now.ToFileTimeUtc();
        }
    }
}