using System;
using System.Runtime.Serialization;

namespace cleantalk.csharp
{
    [DataContract]
    public class CleantalkRequest
    {
        public CleantalkRequest(string authKey)
        {
            Message = String.Empty;
            Example = String.Empty;
            AuthKey = authKey;
            Agent = String.Empty;
            ResponseLang = String.Empty;
            SenderEmail = String.Empty;
            _senderInfo = new SenderInfo();
            SenderIp = String.Empty;
            SenderNickname = String.Empty;
            PostInfo = String.Empty;
            Feedback = String.Empty;
            Phone = String.Empty;
        }

        private SenderInfo _senderInfo;

        /// <summary>
        ///     User message
        ///     @var string
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        ///     Post example with last comments
        ///     @var string
        /// </summary>
        [DataMember(Name = "base_text")]
        public string Example { get; set; }

        /// <summary>
        ///     Auth key
        ///     @var string
        /// </summary>
        [DataMember(Name = "auth_key")]
        public string AuthKey { get; set; }

        /// <summary>
        ///     Engine
        ///     @var string
        /// </summary>
        [DataMember(Name = "agent")]
        public string Agent { get; set; }

        /// <summary>
        ///     Is check for stoplist,
        ///     valid are 0|1
        ///     @var int
        /// </summary>
        [DataMember(Name = "ct_stop_words")]
        public int StoplistCheck { get; set; }

        /// <summary>
        ///     Language server response,
        ///     valid are 'en' or 'ru'
        ///     @var string
        /// </summary>
        [DataMember(Name = "response_lang")]
        public string ResponseLang { get; set; }

        /// <summary>
        ///     User IP
        ///     @var strings
        /// </summary>
        [DataMember(Name = "session_ip")]
        public string SenderIp { get; set; }

        /// <summary>
        ///     User email
        ///     @var strings
        /// </summary>
        [DataMember(Name = "user_email")]
        public string SenderEmail { get; set; }

        /// <summary>
        ///     User nickname
        ///     @var string
        /// </summary>
        [DataMember(Name = "user_name")]
        public string SenderNickname { get; set; }

        /// <summary>
        ///     Sender info JSON string
        ///     @var string
        /// </summary>
        [DataMember(Name = "sender_info")]
        public string SenderInfo
        {
            get
            {
                var result = WebHelper.JsonSerialize(_senderInfo);

                return String.IsNullOrEmpty(result) ? String.Empty : result;
            }

            set { _senderInfo = WebHelper.JsonDeserialize<SenderInfo>(value); }
        }

        /// <summary>
        ///     Post info JSON string
        ///     @var string
        /// </summary>
        [DataMember(Name = "post_info")]
        public string PostInfo { get; set; }

        /// <summary>
        ///     Is allow links, email and icq,
        ///     valid are 1|0
        ///     @var int
        /// </summary>
        [DataMember(Name = "ct_links")]
        public int IsAllowLinks { get; set; }

        /// <summary>
        ///     Time form filling (seconds from start form filling till the form POST)
        ///     @var int
        /// </summary>
        [DataMember(Name = "submit_time")]
        public int SubmitTime { get; set; }

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
        public int IsEnableJs { get; set; }

        /// <summary>
        ///     user time zone
        ///     @var string
        /// </summary>
        [DataMember(Name = "tz")]
        public string TimeZone { get; set; }

        /// <summary>
        ///     Feedback string,
        ///     valid are 'requset_id:(1|0)'
        ///     @var string
        /// </summary>
        [DataMember(Name = "feedback")]
        public string Feedback { get; set; }

        /// <summary>
        ///     Phone number
        ///     @var type
        /// </summary>
        [DataMember(Name = "phone")]
        public string Phone { get; set; }

        /// <summary>
        ///     Method name
        ///     @var string
        /// </summary>
        [DataMember(Name = "method_name")]
        public string MethodName { get; set; }

        /// <summary>
        ///     Processing request data before action
        /// </summary>
        /// <param name="methodType"></param>
        /// <returns></returns>
        public CleantalkRequest Preprocessing(MethodType methodType)
        {
            if (_isProcessed)
            {
                return this;
            }

            if (String.IsNullOrWhiteSpace(AuthKey))
            {
                throw new ArgumentException("AuthKey is empty");
            }

            switch (methodType)
            {
                case MethodType.check_message:
                    Message = WebHelper.Base64Encode(WebHelper.CompressData(WebHelper.StringToUtf8(Message)));
                    Example = WebHelper.Base64Encode(WebHelper.CompressData(WebHelper.StringToUtf8(Example)));
                    break;
                case MethodType.check_newuser:
                    if (String.IsNullOrWhiteSpace(SenderNickname))
                    {
                        throw new ArgumentException("SenderNickname is empty");
                    }

                    if (String.IsNullOrWhiteSpace(SenderEmail))
                    {
                        throw new ArgumentException("SenderEmail is empty");
                    }

                    break;
                case MethodType.send_feedback:
                    if (String.IsNullOrWhiteSpace(Feedback))
                    {
                        throw new ArgumentException("Feedback is empty");
                    }

                    break;
            }

            MethodName = methodType.ToString();
            _isProcessed = true;

            return this;
        }

        private bool _isProcessed;
    }
}