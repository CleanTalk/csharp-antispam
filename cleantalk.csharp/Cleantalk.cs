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
            this.DebugLevel = 0;
            this.ServerChange = false;
            this.StayOnServer = false;
            this.ServerUrl = Constants.ServerUrl;
        }

        /**
         * Debug level
         * @var int
         */
        public int DebugLevel { get; set; }

        /**
         * Cleantalk server url
         * @var string
         */
        public string ServerUrl { get; set; }

        /**
         * Last work url
         * @var string
         */
        public string WorkUrl { get; set; }

        /**
         * WOrk url ttl
         * @var int
         */
        public int ServerTtl { get; set; }

        /**
         * Time work_url changer
         * @var int
         */
        public DateTime ServerChanged { get; set; }

        /**
         * Flag is change server url
         * @var bool
         */
        public bool ServerChange { get; set; }

        /**
         * Use TRUE when need stay on server. Example: send feedback
         * @var bool
         */
        public bool StayOnServer { get; set; }

        /**
        * Maximum data size in bytes
        * @var int
        */
        private const int dataMaxSise = 32768;

        /**
        * Data compression rate 
        * @var int
        */
        private const int compressRate = 6;

        /**
        * Server connection timeout in seconds 
        * @var int
        */
        private const int serverTimeout = 2;

        /// <summary>
        /// Function checks whether it is possible to publish the message
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CleantalkResponse CheckMessage(CleantalkRequest request)
        {
            var postData = WebHelper.JsonSerialize(request.DoPreprocessing(MethodType.check_message));
            var response = SendPostData(postData);
            var result = WebHelper.JsonDeserialize<CleantalkResponse>(response);

            return result.DoPostprocessing();
        }

        /// <summary>
        /// Function checks whether it is possible to publish the message
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CleantalkResponse CheckNewUser(CleantalkRequest request)
        {
            var postData = WebHelper.JsonSerialize(request.DoPreprocessing(MethodType.check_newuser));
            var response = SendPostData(postData);
            var result = WebHelper.JsonDeserialize<CleantalkResponse>(response);

            return result.DoPostprocessing();
        }

        /// <summary>
        /// Function sends the results of manual moderation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CleantalkResponse SendFeedback(CleantalkRequest request)
        {
            var postData = WebHelper.JsonSerialize(request.DoPreprocessing(MethodType.send_feedback));
            var response = SendPostData(postData);
            var result = WebHelper.JsonDeserialize<CleantalkResponse>(response);

            return result.DoPostprocessing();
        }

        /// <summary>
        /// Send post data to the web server
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
                response = webClient.UploadString(this.ServerUrl, postData);
            }

            return response;
        }
    }
}
