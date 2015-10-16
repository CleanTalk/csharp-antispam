using System.Runtime.Serialization;

namespace cleantalk.csharp
{
    [DataContract(Name = "post_info")]
    public class PostInfo
    {
        /// <summary>
        ///     HTTP link to the post
        /// </summary>
        [DataMember(Name = "post_url")]
        public string PostUrl { get; set; }

        /// <summary>
        ///     Comment type: trackback, pingback, comment.
        /// </summary>
        [DataMember(Name = "comment_type")]
        public string CommentType { get; set; }
    }
}