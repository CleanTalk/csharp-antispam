using System.Linq;
using System.Runtime.Serialization;
using cleantalk.csharp.Enums;
using cleantalk.csharp.Helpers;

namespace cleantalk.csharp
{
    [DataContract]
    public class CleantalkResponse
    {
        /// <summary>
        ///     Is stop words
        ///     @var int
        /// </summary>
        [DataMember(Name = "stop_words")]
        public int? StopWords { get; set; }

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
        public int? Blacklisted { get; set; }

        /// <summary>
        ///     Is allow, 1|0
        ///     @var int
        /// </summary>
        [DataMember(Name = "allow")]
        public bool? IsAllow { get; set; }

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
        public int? ErrNo { get; set; }

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
        public bool? IsSpam { get; set; }

        /// <summary>
        ///     Is JS
        ///     @var type
        /// </summary>
        [DataMember(Name = "js_disabled")]
        public int? IsJsDisabled { get; set; }

        /// <summary>
        ///     Stop queue message, 1|0
        ///     @var int
        /// </summary>
        [DataMember(Name = "stop_queue")]
        public int? StopQueue { get; set; }

        /// <summary>
        ///     Account should be deactivated after registration, 1|0
        ///     @var int
        /// </summary>
        [DataMember(Name = "inactive")]
        public int? IsInactive { get; set; }

        /// <summary>
        ///     Account status
        ///     @var int
        /// </summary>
        [DataMember(Name = "account_status")]
        public int? AccountStatus { get; set; }

        /// <summary>
        ///     Feedback receive flag, 1|0
        ///     @var int
        /// </summary>
        [DataMember(Name = "received")]
        public bool? Received { get; set; }

        [DataMember(Name = "codes")]
        public string CodesString
        {
            get
            {
                return Codes == null || !Codes.Any()
                    ? null
                    : Codes
                        .Select(x => x.ToString())
                        .Aggregate((x, y) => x + " " + y);
            }

            set
            {
                if (string.IsNullOrEmpty(value)) Codes = null;
                Codes = value.Split(' ')
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(x => x.ToEnum<AnswerCodeType>())
                    .Where(x => x != null)
                    .Select(x => x.Value)
                    .ToArray();
            }
        }

        [IgnoreDataMember]
        public AnswerCodeType[] Codes { get; private set; }

        public CleantalkResponse()
        {
            Comment = string.Empty;
            ErrStr = string.Empty;
        }
    }
}