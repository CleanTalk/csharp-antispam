﻿// ReSharper disable InconsistentNaming

using System;
using System.Runtime.Serialization;
using cleantalk.csharp.Enums;
using cleantalk.csharp.Helpers;

namespace cleantalk.csharp.Request
{
    [DataContract]
    public class CleantalkRequest
    {
        public CleantalkRequest(string authKey)
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
        ///     User message
        ///     @var string
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

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
        ///     CleanTalk bot detector event token.
        ///     To get this param:
        ///     1. add a script to the web-page:
        ///     <script src="https://moderate.cleantalk.org/ct-bot-detector-wrapper.js" id="ct_bot_detector-js"></script>
        ///     2. parse the newly added hidden input on the web form, the name atrribute of input is "ct_bot_detector_event_token"
        ///     @var string
        /// </summary>
        [DataMember(Name = "event_token")]
        public string EventToken { get; set; }

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

        [IgnoreDataMember] public SenderInfo SenderInfo { get; set; }

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

        [IgnoreDataMember] public PostInfo PostInfo { get; set; }

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
        ///     valid are 'request_id:(1|0)'
        ///     @var string
        /// </summary>
        [DataMember(Name = "feedback")]
        public string Feedback { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(AuthKey)) throw new ArgumentNullException("AuthKey");

            switch (MethodName.ToEnum<MethodType>())
            {
                case MethodType.check_message:
                    //nothing to do
                    break;
                case MethodType.check_newuser:
                    if (string.IsNullOrWhiteSpace(SenderNickname)) throw new ArgumentNullException("SenderNickname");
                    if (string.IsNullOrWhiteSpace(SenderEmail)) throw new ArgumentNullException("SenderEmail");
                    break;
                case MethodType.send_feedback:
                    if (string.IsNullOrWhiteSpace(Feedback)) throw new ArgumentNullException("Feedback");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("MethodName", MethodName, null);
            }
        }
    }
}