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
        CleantalkResponse SpamCheck(CleantalkRequest request);

        //ip_info
    }
}