using System;
using System.Runtime.Serialization;

namespace cleantalk.csharp
{
    [DataContract]
    public class CleantalkResponse
    {
        public CleantalkResponse()
        {
            this.Comment = String.Empty;
            this.ErrStr = String.Empty;
            this.Sms = String.Empty;
            this.SmsErrorText = String.Empty;
        }

        /**
         *  Is stop words
         * @var int
         */
        [DataMember(Name = "stop_words")]
        public int StopWords { get; set; }

        /**
         * Cleantalk comment
         * @var string
         */
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        /**
         * Is blacklisted
         * @var int
         */
        [DataMember(Name = "blacklisted")]
        public int Blacklisted { get; set; }

        /**
         * Is allow, 1|0
         * @var int
         */
        [DataMember(Name = "allow")]
        public bool IsAllow { get; set; }

        /**
         * Request ID
         * @var int
         */
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /**
         * Request errno
         * @var int
         */
        [DataMember(Name = "errno")]
        public int ErrNo { get; set; }

        /**
         * Error string
         * @var string
         */
        [DataMember(Name = "errstr")]
        public string ErrStr { get; set; }

        /**
         * Is fast submit, 1|0
         * @var string
         */
        [DataMember(Name = "fast_submit")]
        public bool IsFastSubmit { get; set; }

        /**
         * Is spam comment
         * @var string
         */
        [DataMember(Name = "spam")]
        public bool IsSpam { get; set; }

        /**
         * Is JS
         * @var type 
         */
        [DataMember(Name = "js_disabled")]
        public bool IsJs { get; set; }

        /**
         * Sms check
         * @var type 
         */
        [DataMember(Name = "sms_allow")]
        public bool IsSmsAllow { get; set; }

        /**
         * Sms code result
         * @var type 
         */
        [DataMember(Name = "sms")]
        public string Sms { get; set; }

        /**
         * Sms error code
         * @var type 
         */
        [DataMember(Name = "sms_error_code")]
        public int SmsErrorCode { get; set; }

        /**
         * Sms error code
         * @var type 
         */
        [DataMember(Name = "sms_error_text")]
        public string SmsErrorText { get; set; }

        /**
         * Stop queue message, 1|0
         * @var int  
         */
        [DataMember(Name = "stop_queue")]
        public bool StopQueue { get; set; }

        /**
         * Account should be deactivated after registration, 1|0
         * @var int  
         */
        [DataMember(Name = "inactive")]
        public bool IsInactive { get; set; }

        /// <summary>
        /// Processing response data after action
        /// </summary>
        /// <returns></returns>
        public CleantalkResponse DoPostprocessing()
        {
            if (this._isProcessed)
            {
                return this;
            }

            this.Comment = WebHelper.ConvertIso88591ToUtf8(this.Comment);
            this.ErrStr = WebHelper.ConvertIso88591ToUtf8(this.ErrStr);
            this.Sms = WebHelper.ConvertIso88591ToUtf8(this.Sms);
            this.SmsErrorText = WebHelper.ConvertIso88591ToUtf8(this.SmsErrorText);

            this._isProcessed = true;

            return this;
        }

        private bool _isProcessed = false;
    }
}
