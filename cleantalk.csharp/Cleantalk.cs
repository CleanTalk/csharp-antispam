using System;
using System.Net;
using System.Text;

namespace cleantalk.csharp
{
    [Serializable]
    public class Cleantalk : ICleartalk
    {
        public Cleantalk()
        {
            DebugLevel = 0;
            ServerChange = false;
            StayOnServer = false;
            ServerUrl = Constants.ServerUrl;
        }

        /// <summary>
        ///     Debug level
        ///     @var int
        /// </summary>
        public int DebugLevel { get; set; }

        /// <summary>
        ///     Cleantalk server url
        ///     @var string
        /// </summary>
        public string ServerUrl { get; set; }

        /// <summary>
        ///     Last work url
        ///     @var string
        /// </summary>
        public string WorkUrl { get; set; }

        /// <summary>
        ///     WOrk url ttl
        ///     @var int
        /// </summary>
        public int ServerTtl { get; set; }

        /// <summary>
        ///     Time work_url changer
        ///     @var int
        /// </summary>
        public DateTime ServerChanged { get; set; }

        /// <summary>
        ///     Flag is change server url
        ///     @var bool
        /// </summary>
        public bool ServerChange { get; set; }

        /// <summary>
        ///     Use TRUE when need stay on server. Example: send feedback
        ///     @var bool
        /// </summary>
        public bool StayOnServer { get; set; }

        /// <summary>
        ///     Maximum data size in bytes
        ///     @var int
        /// </summary>
        private const int dataMaxSise = 32768;

        /// <summary>
        ///     Data compression rate
        ///     @var int
        /// </summary>
        private const int compressRate = 6;

        /// <summary>
        ///     Server connection timeout in seconds
        ///     @var int
        /// </summary>
        private const int serverTimeout = 2;

        /// <summary>
        ///     Function checks whether it is possible to publish the message
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CleantalkResponse CheckMessage(CleantalkRequest request)
        {
            var postData = WebHelper.JsonSerialize(request.Preprocessing(MethodType.check_message));
            var response = SendPostData(postData);
            var result = WebHelper.JsonDeserialize<CleantalkResponse>(response);

            return result.Postprocessing();
        }

        /// <summary>
        ///     Function checks whether it is possible to publish the message
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CleantalkResponse CheckNewUser(CleantalkRequest request)
        {
            var postData = WebHelper.JsonSerialize(request.Preprocessing(MethodType.check_newuser));
            var response = SendPostData(postData);
            var result = WebHelper.JsonDeserialize<CleantalkResponse>(response);

            return result.Postprocessing();
        }

        /// <summary>
        ///     Function sends the results of manual moderation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CleantalkResponse SendFeedback(CleantalkRequest request)
        {
            var postData = WebHelper.JsonSerialize(request.Preprocessing(MethodType.send_feedback));
            var response = SendPostData(postData);
            var result = WebHelper.JsonDeserialize<CleantalkResponse>(response);

            return result.Postprocessing();
        }

        /// <summary>
        ///     Send post data to the web server
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        private string SendPostData(string postData)
        {
            string response;
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                response = webClient.UploadString(ServerUrl, postData);
            }

            return response;
        }
    }
}