using System;
using System.Runtime.Serialization;

namespace cleantalk.csharp
{
    [DataContract]
    public class CleantalkRequest
    {
        public const string Version = "0.7";

        public CleantalkRequest(string authKey)
        {
            this.Message = String.Empty;
            this.Example = String.Empty;
            this.AuthKey = authKey;
            this.Agent = String.Empty;
            this.ResponseLang = String.Empty;
            this.SenderEmail = String.Empty;
            this._senderInfo = new SenderInfo();
            this.SenderIp = String.Empty;
            this.SenderNickname = String.Empty;
            this.PostInfo = String.Empty;
            this.Feedback = String.Empty;
            this.Phone = String.Empty;
        }

        private SenderInfo _senderInfo;

        /**
         * User message
         * @var string
         */
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /**
         * Post example with last comments
         * @var string
         */
        [DataMember(Name = "base_text")]
        public string Example { get; set; }

        /**
         * Auth key
         * @var string
         */
        [DataMember(Name = "auth_key")]
        public string AuthKey { get; set; }

        /**
         * Engine
         * @var string
         */
        [DataMember(Name = "agent")]
        public string Agent { get; set; }

        /**
         * Is check for stoplist,
         * valid are 0|1
         * @var int
         */
        [DataMember(Name = "ct_stop_words")]
        public bool StoplistCheck { get; set; }

        /**
         * Language server response,
         * valid are 'en' or 'ru'
         * @var string
         */
        [DataMember(Name = "response_lang")]
        public string ResponseLang { get; set; }

        /**
         * User IP
         * @var strings
         */
        [DataMember(Name = "session_ip")]
        public string SenderIp { get; set; }

        /**
         * User email
         * @var strings
         */
        [DataMember(Name = "user_email")]
        public string SenderEmail { get; set; }

        /**
         * User nickname
         * @var string
         */
        [DataMember(Name = "user_name")]
        public string SenderNickname { get; set; }

        /**
         * Sender info JSON string
         * @var string
         */
        [DataMember(Name = "sender_info")]
        public string SenderInfo
        {
            get
            {
                var result = WebHelper.JsonSerialize(this._senderInfo);

                return String.IsNullOrEmpty(result) ? String.Empty : result;
            }

            set { this._senderInfo = WebHelper.JsonDeserialize<SenderInfo>(value); }
        }

        /**
         * Post info JSON string
         * @var string
         */
        [DataMember(Name = "post_info")]
        public string PostInfo { get; set; }

        /**
         * Is allow links, email and icq,
         * valid are 1|0
         * @var int
         */
        [DataMember(Name = "ct_links")]
        public bool IsAllowLinks { get; set; }

        /**
         * Time form filling
         * @var int
         */
        [DataMember(Name = "submit_time")]
        public int SubmitTime { get; set; }

        /**
         * Is enable Java Script,
         * valid are 0|1|2
         * Status:
         *  null - JS html code not inserted into phpBB templates
         *  0 - JS disabled at the client browser
         *  1 - JS enabled at the client broswer
         * @var int
         */
        [DataMember(Name = "js_on")]
        public bool IsEnableJs { get; set; }

        /**
         * user time zone
         * @var string
         */
        [DataMember(Name = "tz")]
        public int TimeZone { get; set; }

        /**
         * Feedback string,
         * valid are 'requset_id:(1|0)'
         * @var string
         */
        [DataMember(Name = "feedback")]
        public string Feedback { get; set; }

        /**
         * Phone number
         * @var type 
         */
        [DataMember(Name = "phone")]
        public string Phone { get; set; }

        /**
       * Method name
       * @var string
       */
        [DataMember(Name = "method_name")]
        public string MethodName { get; set; }

        /// <summary>
        /// Processing request data before action
        /// </summary>
        /// <param name="methodType"></param>
        /// <returns></returns>
        public CleantalkRequest DoPreprocessing(MethodType methodType)
        {
            if (this._isProcessed)
            {
                return this;
            }

            if (String.IsNullOrWhiteSpace(this.AuthKey))
            {
                throw new ArgumentException("AuthKey is empty");
            }

            switch (methodType)
            {
                case MethodType.check_message:
                    this.Message = WebHelper.Base64Encode(WebHelper.CompressData(WebHelper.StringToUtf8(this.Message)));
                    this.Example = WebHelper.Base64Encode(WebHelper.CompressData(WebHelper.StringToUtf8(this.Example)));
                    break;
                case MethodType.check_newuser:
                    if (String.IsNullOrWhiteSpace(this.SenderNickname))
                    {
                        throw new ArgumentException("SenderNickname is empty");
                    }

                    if (String.IsNullOrWhiteSpace(this.SenderEmail))
                    {
                        throw new ArgumentException("SenderEmail is empty");
                    }

                    break;
                case MethodType.send_feedback:
                    if (String.IsNullOrWhiteSpace(this.Feedback))
                    {
                        throw new ArgumentException("Feedback is empty");
                    }

                    break;
            }

            this.MethodName = methodType.ToString();
            this._isProcessed = true;

            return this;
        }

        private bool _isProcessed = false;
    }
}
