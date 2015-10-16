using System.Runtime.Serialization;
using cleantalk.csharp.Helpers;

namespace cleantalk.csharp
{
    [DataContract]
    public class CleantalkRequest
    {
        /// <summary>
        ///     User message
        ///     @var string
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        ///     Auth key
        ///     @var string
        /// </summary>
        [DataMember(Name = "auth_key")]
        public string AuthKey { get; set; }

        /// <summary>
        ///     User IP
        ///     @var strings
        /// </summary>
        [DataMember(Name = "sender_ip")]
        public string SenderIp { get; set; }

        /// <summary>
        ///     User email
        ///     @var strings
        /// </summary>
        [DataMember(Name = "sender_email")]
        public string SenderEmail { get; set; }

        /// <summary>
        ///     User nickname
        ///     @var string
        /// </summary>
        [DataMember(Name = "sender_nickname")]
        public string SenderNickname { get; set; }

        /// <summary>
        ///     Sender info JSON string
        ///     @var string
        /// </summary>
        [DataMember(Name = "sender_info")]
        public string SenderInfoString
        {
            get { return WebHelper.JsonSerialize(SenderInfo); }
            set { SenderInfo = WebHelper.JsonDeserialize<SenderInfo>(value); }
        }

        [IgnoreDataMember]
        public SenderInfo SenderInfo { get; set; }

        /// <summary>
        ///     Post info JSON string
        ///     @var string
        /// </summary>
        [DataMember(Name = "post_info")]
        public string PostInfoString
        {
            get { return WebHelper.JsonSerialize(PostInfo); }
            set { PostInfo = WebHelper.JsonDeserialize<PostInfo>(value); }
        }

        [IgnoreDataMember]
        public PostInfo PostInfo { get; set; }

        /// <summary>
        ///     Time form filling (seconds from start form filling till the form POST)
        ///     @var int
        /// </summary>
        [DataMember(Name = "submit_time")]
        public int? SubmitTime { get; set; }

        /// <summary>
        ///     Is enable Java Script (site visitor has JavaScript)
        ///     valid are 0|1|2
        ///     Status:
        ///     null - JS html code not inserted into phpBB templates
        ///     0 - JS disabled at the client browser
        ///     1 - JS enabled at the client browser
        ///     @var int
        /// </summary>
        [DataMember(Name = "js_on")]
        public int? IsJsEnable { get; set; }

        /// <summary>
        ///     Feedback string,
        ///     valid are 'requset_id:(1|0)'
        ///     @var string
        /// </summary>
        [DataMember(Name = "feedback")]
        public string Feedback { get; set; }

        /// <summary>
        ///     Method name
        ///     @var string
        /// </summary>
        [DataMember(Name = "method_name")]
        public string MethodName { get; set; }

        public CleantalkRequest(string authKey)
        {
            AuthKey = authKey;
        }
    }
}