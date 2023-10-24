// ReSharper disable InconsistentNaming

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Web;
using cleantalk.csharp.Enums;
using cleantalk.csharp.Helpers;
using cleantalk.csharp.Request;
using cleantalk.csharp.Response;

namespace cleantalk.csharp
{
    [Serializable]
    public class Cleantalk : ICleantalk
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
        ///     Work url ttl
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

        public Cleantalk(string serverUrl = Constants.ServerUrl)
        {
            DebugLevel = 0;
            ServerChange = false;
            StayOnServer = false;
            ServerUrl = serverUrl;
        }

        /// <summary>
        ///     Function checks whether it is possible to publish the message
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CleantalkResponse CheckMessage(CleantalkRequest request)
        {
            var result = SendData<CleantalkResponse>(request, MethodType.check_message);
            result.ConvertProperties();

            return result;
        }

        /// <summary>
        ///     Function checks whether it is possible to publish the message
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CleantalkResponse CheckNewUser(CleantalkRequest request)
        {
            var result = SendData<CleantalkResponse>(request, MethodType.check_newuser);
            result.ConvertProperties();

            return result;
        }

        /// <summary>
        ///     Function sends the results of manual moderation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CleantalkResponse SendFeedback(CleantalkRequest request)
        {
            var result =  SendData<CleantalkResponse>(request, MethodType.send_feedback);
            result.ConvertProperties();

            return result;
        }

        /// <summary>
        ///     Send data to the web server
        /// </summary>
        /// <returns></returns>
        private TResponse SendData<TResponse>(CleantalkRequestBase request, MethodType methodType)
        {
            var customRestrictedHeaders = new[] { "Content-Length", "Connection", "Cookie" };

            string response;
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;

                //get headers from httpContext
                var context = HttpContext.Current;
                if (context != null)
                {
                    var headers = context.Request.Headers;
                    foreach (var kvp in headers.Keys.Cast<string>()
                                        .Select(x => new { key = x, value = headers[x] })
                                        .Where(x =>
                                            !customRestrictedHeaders.Contains(x.key) &&
                                            !WebHeaderCollection.IsRestricted(x.key)))
                    {
                        webClient.Headers.Add(kvp.key, kvp.value);
                    }
                }

                webClient.Headers[HttpRequestHeader.ContentType] = @"application/x-www-form-urlencoded";

                request.AllHeaders = WebHelper.HeadersSerialize(webClient.Headers);
                request.ValidateAndInit(methodType);

                var postData = WebHelper.JsonSerialize(request);
                response = webClient.UploadString(ServerUrl, postData);
            }

            return WebHelper.JsonDeserialize<TResponse>(response);
        }

        /// <summary>
        ///     Send data to the web server
        /// </summary>
        /// <returns></returns>
        private SpamCheckResponse SendData(SpamCheckRequest request)
        {
            var customRestrictedHeaders = new[] { "Content-Length", "Connection", "Cookie" };

            string response;
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;

                //get headers from httpContext
                var context = HttpContext.Current;
                if (context != null)
                {
                    var headers = context.Request.Headers;
                    foreach (var kvp in headers.Keys.Cast<string>()
                                 .Select(x => new { key = x, value = headers[x] })
                                 .Where(x =>
                                     !customRestrictedHeaders.Contains(x.key) &&
                                     !WebHeaderCollection.IsRestricted(x.key)))
                    {
                        webClient.Headers.Add(kvp.key, kvp.value);
                    }
                }

                webClient.Headers[HttpRequestHeader.ContentType] = @"application/x-www-form-urlencoded";

                var uriBuilder = new UriBuilder(ServerUrl);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["method_name"] = MethodType.spam_check.ToString();
                query["auth_key"] = request.AuthKey;
                query["ip"] = request.ip;
                query["email"] = request.email; 
                //query["ip4_SHA256"] = request.ip4_SHA256;
                //query["ip6_SHA256"] = request.ip6_SHA256;
                //query["date"] = request.date;
                //query["email_SHA256"] = request.email_SHA256;

                uriBuilder.Query = query.ToString();

                //var postData = WebHelper.JsonSerialize(request);
                response = webClient.DownloadString(uriBuilder.Uri);
                //using (var stream = webClient.OpenRead(uriBuilder.Uri))
                //{
                //    var reader = new StreamReader(stream);
                //    response = reader.ReadToEnd();
                //}

            }

            return WebHelper.JsonDeserialize<SpamCheckResponse>(response);
        }

        /// <summary>
        /// This <see href="https://cleantalk.org/help/api-spam-check">method</see> should be used for bulk checks of IP, Email for spam activity.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SpamCheckResponse SpamCheck(SpamCheckRequest request)
        {
            var result = SendData(request);

            return result;
        }
    }
}