// ReSharper disable InconsistentNaming

namespace cleantalk.csharp
{
    public interface ICleantalk
    {
        /// <summary>
        ///     Function checks whether it is possible to publish the message
        ///     @param CleantalkRequest $request
        ///     @return type
        /// </summary>
        CleantalkResponse CheckMessage(CleantalkRequest request);

        /// <summary>
        ///     Function checks whether it is possible to publish the message
        ///     @param CleantalkRequest $request
        ///     @return type
        /// </summary>
        CleantalkResponse CheckNewUser(CleantalkRequest request);

        /// <summary>
        ///     Function sends the results of manual moderation
        ///     @param CleantalkRequest $request
        ///     @return type
        /// </summary>
        CleantalkResponse SendFeedback(CleantalkRequest request);

        /// <summary>
        /// This method should be used for bulk checks of IP, Email for spam activity.
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="email"></param>
        /// <param name="date"></param>
        /// <param name="email_SHA256"></param>
        /// <param name="ip4_SHA256"></param>
        /// <param name="ip6_SHA256"></param>
        /// <returns></returns>
        SpamCheckResponse SpamCheck(string ip, string email, string date, string email_SHA256, string ip4_SHA256, string ip6_SHA256);

        //TODO: method ip_info
    }
}