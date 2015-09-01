using System;
using System.Runtime.Serialization;

namespace cleantalk.csharp
{
    [DataContract]
    public class CleantalkResponse
    {
        public CleantalkResponse()
        {
            Comment = String.Empty;
            ErrStr = String.Empty;
            Sms = String.Empty;
            SmsErrorText = String.Empty;
        }

        /// <summary>
        ///     Is stop words
        ///     @var int
        /// </summary>
        [DataMember(Name = "stop_words")]
        public int StopWords { get; set; }

        /// <summary>
        ///     Cleantalk comment
        ///     @var string
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        /// <summary>
        ///     Is blacklisted
        ///     @var int
        /// </summary>
        [DataMember(Name = "blacklisted")]
        public int Blacklisted { get; set; }

        /// <summary>
        ///     Is allow, 1|0
        ///     @var int
        /// </summary>
        [DataMember(Name = "allow")]
        public bool IsAllow { get; set; }

        /// <summary>
        ///     Request ID
        ///     @var int
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        ///     Request errno
        ///     @var int
        /// </summary>
        [DataMember(Name = "errno")]
        public int ErrNo { get; set; }

        /// <summary>
        ///     Error string
        ///     @var string
        /// </summary>
        [DataMember(Name = "errstr")]
        public string ErrStr { get; set; }

        /// <summary>
        ///     Is fast submit, 1|0
        ///     @var string
        /// </summary>
        [DataMember(Name = "fast_submit")]
        public string IsFastSubmit { get; set; }

        /// <summary>
        ///     Is spam comment
        ///     @var string
        /// </summary>
        [DataMember(Name = "spam")]
        public bool IsSpam { get; set; }

        /// <summary>
        ///     Is JS
        ///     @var type
        /// </summary>
        [DataMember(Name = "js_disabled")]
        public bool IsJs { get; set; }

        /// <summary>
        ///     Sms check
        ///     @var type
        /// </summary>
        [DataMember(Name = "sms_allow")]
        public bool IsSmsAllow { get; set; }

        /// <summary>
        ///     Sms code result
        ///     @var type
        /// </summary>
        [DataMember(Name = "sms")]
        public string Sms { get; set; }

        /// <summary>
        ///     Sms error code
        ///     @var type
        /// </summary>
        [DataMember(Name = "sms_error_code")]
        public int SmsErrorCode { get; set; }

        /// <summary>
        ///     Sms error code
        ///     @var type
        /// </summary>
        [DataMember(Name = "sms_error_text")]
        public string SmsErrorText { get; set; }

        /// <summary>
        ///     Stop queue message, 1|0
        ///     @var int
        /// </summary>
        [DataMember(Name = "stop_queue")]
        public bool StopQueue { get; set; }

        /// <summary>
        ///     Account should be deactivated after registration, 1|0
        ///     @var int
        /// </summary>
        [DataMember(Name = "inactive")]
        public bool IsInactive { get; set; }

        /// <summary>
        ///     Account status
        ///     @var int
        /// </summary>
        [DataMember(Name = "account_status")]
        public int AccountStatus { get; set; }

        /// <summary>
        ///     Feedback receive flag, 1|0
        ///     @var int
        /// </summary>
        [DataMember(Name = "received")]
        public bool Received { get; set; }

        /// <summary>
        ///     Processing response data after action
        /// </summary>
        /// <returns></returns>
        public CleantalkResponse Postprocessing()
        {
            if (_isProcessed)
            {
                return this;
            }

            Comment = WebHelper.ConvertIso88591ToUtf8(Comment);
            ErrStr = WebHelper.ConvertIso88591ToUtf8(ErrStr);
            Sms = WebHelper.ConvertIso88591ToUtf8(Sms);
            SmsErrorText = WebHelper.ConvertIso88591ToUtf8(SmsErrorText);

            _isProcessed = true;

            return this;
        }

        private bool _isProcessed;
    }
}