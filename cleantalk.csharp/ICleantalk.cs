// ReSharper disable InconsistentNaming

using cleantalk.csharp.Request;
using cleantalk.csharp.Response;

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
        /// <param name="request"></param>
        /// <returns></returns>
        SpamCheckResponse SpamCheck(SpamCheckRequest request);

        /// <summary>
        ///     This <see href="https://cleantalk.org/help/api-ip-info-country-code">method</see>. Country code by IP address.
        /// </summary>
        /// <param name="authKey"></param>
        /// <param name="ipList"></param>
        /// <returns></returns>
        IpInfoResponse IpInfoCheck(string authKey, params string[] ipList);
    }
}