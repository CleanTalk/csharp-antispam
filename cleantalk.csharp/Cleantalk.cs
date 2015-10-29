using System;
using System.Linq;
using System.Net;
using System.Text;
using cleantalk.csharp.Enums;
using cleantalk.csharp.Helpers;

namespace cleantalk.csharp
{
    [Serializable]
    public class Cleantalk : ICleartalk
    {
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

        public Cleantalk()
        {
            DebugLevel = 0;
            ServerChange = false;
            StayOnServer = false;
            ServerUrl = Constants.ServerUrl;
        }

        /// <summary>
        ///     Function checks whether it is possible to publish the message
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CleantalkResponse CheckMessage(CleantalkRequest request)
        {
            return SendData(request, MethodType.check_message);
        }

        /// <summary>
        ///     Function checks whether it is possible to publish the message
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CleantalkResponse CheckNewUser(CleantalkRequest request)
        {
            return SendData(request, MethodType.check_newuser);
        }

        /// <summary>
        ///     Function sends the results of manual moderation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CleantalkResponse SendFeedback(CleantalkRequest request)
        {
            return SendData(request, MethodType.send_feedback);
        }

        /// <summary>
        ///     Processing response data after action
        /// </summary>
        /// <returns></returns>
        private static CleantalkResponse Postprocessing(CleantalkResponse response)
        {
            response.Comment = ConvertHelper.ConvertIso88591ToUtf8(response.Comment);
            response.ErrStr = ConvertHelper.ConvertIso88591ToUtf8(response.ErrStr);

            return response;
        }

        /// <summary>
        ///     Processing request data before action
        /// </summary>
        /// <param name="request"></param>
        /// <param name="methodType"></param>
        /// <returns></returns>
        private static CleantalkRequest Preprocessing(CleantalkRequest request, MethodType methodType)
        {
            if (string.IsNullOrWhiteSpace(request.AuthKey))
            {
                throw new ArgumentNullException("AuthKey is empty");
            }

            switch (methodType)
            {
                case MethodType.check_message:
                    //nothing to do
                    break;
                case MethodType.check_newuser:
                    if (string.IsNullOrWhiteSpace(request.SenderNickname))
                    {
                        throw new ArgumentNullException("SenderNickname is empty");
                    }

                    if (string.IsNullOrWhiteSpace(request.SenderEmail))
                    {
                        throw new ArgumentNullException("SenderEmail is empty");
                    }

                    break;
                case MethodType.send_feedback:
                    if (string.IsNullOrWhiteSpace(request.Feedback))
                    {
                        throw new ArgumentNullException("Feedback is empty");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException("methodType", methodType, null);
            }

            request.MethodName = methodType.ToString();

            return request;
        }

        /// <summary>
        ///     Send data to the web server
        /// </summary>
        /// <returns></returns>
        private CleantalkResponse SendData(CleantalkRequest request, MethodType methodType)
        {
            string response;
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                request.AllHeaders = webClient.Headers.Keys
                    .Cast<string>()
                    .Aggregate(string.Empty, (current, key) => current + key + ": " + webClient.Headers[key] + Environment.NewLine);

                var postData = WebHelper.JsonSerialize(Preprocessing(request, methodType));
                response = webClient.UploadString(ServerUrl, postData);
            }

            var result = WebHelper.JsonDeserialize<CleantalkResponse>(response);

            return Postprocessing(result);
        }
    }
}