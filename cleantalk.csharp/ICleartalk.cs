
namespace cleantalk.csharp
{
    /// <summary>
    /// moderate3, moderate4.cleantalk.ru
    /// </summary>

    public interface ICleartalk
    {
        /**
         * Function checks whether it is possible to publish the message
         * @param CleantalkRequest $request
         * @return type
         */
        CleantalkResponse CheckMessage(CleantalkRequest request);

        /**
         * Function checks whether it is possible to publish the message
         * @param CleantalkRequest $request
         * @return type
         */
        CleantalkResponse CheckNewUser(CleantalkRequest request);

        /**
        * Function sends the results of manual moderation
        *
        * @param CleantalkRequest $request
        * @return type
        */
        CleantalkResponse SendFeedback(CleantalkRequest request);
    }
}
