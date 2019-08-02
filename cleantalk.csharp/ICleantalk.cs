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
    }
}