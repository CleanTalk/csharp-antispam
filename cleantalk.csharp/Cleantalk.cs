// ReSharper disable InconsistentNaming

using System;
using System.Linq;
using System.Net;
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
        private static readonly string[] customRestrictedHeaders = { "Content-Length", "Connection", "Cookie" };

        public Cleantalk(string serverUrl = Constants.ServerUrl)
        {
            DebugLevel = 0;
            ServerChange = false;
            StayOnServer = false;
            ServerUrl = serverUrl;
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

        /// <summary>
        ///     Function checks whether it is possible to publish the message
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CleantalkResponse CheckMessage(CleantalkRequest request)
        {
            var result = SendData(request, MethodType.check_message);
            return result;
        }

        /// <summary>
        ///     Function checks whether it is possible to publish the message
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CleantalkResponse CheckNewUser(CleantalkRequest request)
        {
            var result = SendData(request, MethodType.check_newuser);
            return result;
        }

        /// <summary>
        ///     Function sends the results of manual moderation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CleantalkResponse SendFeedback(CleantalkRequest request)
        {
            var result = SendData(request, MethodType.send_feedback);
            return result;
        }

        /// <summary>
        ///     This <see href="https://cleantalk.org/help/api-spam-check">method</see> should be used for bulk checks of IP, Email
        ///     for spam activity.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SpamCheckResponse SpamCheck(SpamCheckRequest request)
        {
            var result = SendData(request);
            return result;
        }

        /// <summary>
        ///     This <see href="https://cleantalk.org/help/api-ip-info-country-code">method</see>. Country code by IP address.
        /// </summary>
        /// <param name="authKey"></param>
        /// <param name="ipList"></param>
        /// <returns></returns>
        public IpInfoResponse IpInfoCheck(string authKey, params string[] ipList)
        {
            var result = SendData(authKey, ipList);
            return result;
        }

        /// <summary>
        ///     Send POST data to the web server
        /// </summary>
        /// <returns></returns>
        private CleantalkResponse SendData(CleantalkRequest request, MethodType methodType)
        {
            string response;
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                SetWebHeaders(webClient);

                request.AllHeaders = WebHelper.HeadersSerialize(webClient.Headers);
                request.MethodName = methodType.ToString();
                request.Validate();

                var postData = WebHelper.JsonSerialize(request);
                response = webClient.UploadString(ServerUrl, postData);
            }

            var result = WebHelper.JsonDeserialize<CleantalkResponse>(response);
            result.ConvertProperties();

            return result;
        }

        /// <summary>
        ///     Send GET and POST data to the web server for spam_check method
        /// </summary>
        /// <returns></returns>
        private SpamCheckResponse SendData(SpamCheckRequest request)
        {
            string response;
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                SetWebHeaders(webClient);

                var uriBuilder = new UriBuilder(ServerUrl);

                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["method_name"] = MethodType.spam_check.ToString();
                request.AuthKey.Do(x => query["auth_key"] = x);
                request.ip.Do(x => query["ip"] = x);
                request.email.Do(x => query["email"] = x);
                request.ip4_SHA256.Do(x => query["ip4_SHA256"] = x);
                request.ip6_SHA256.Do(x => query["ip6_SHA256"] = x);
                request.date.Do(x => query["date"] = x);
                request.email_SHA256.Do(x => query["email_SHA256"] = x);
                uriBuilder.Query = query.ToString();

                response = string.IsNullOrEmpty(request.data)
                    ? webClient.DownloadString(uriBuilder.Uri)
                    : webClient.UploadString(uriBuilder.Uri, "data=" + request.data);
            }

            var result = WebHelper.JsonDeserialize<SpamCheckResponse>(response);

            return result;
        }

        /// <summary>
        ///     Send GET and POST data to the web server for ip_info method
        /// </summary>
        /// <returns></returns>
        private IpInfoResponse SendData(string authKey, params string[] ipList)
        {
            string response;
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                SetWebHeaders(webClient);

                var uriBuilder = new UriBuilder(ServerUrl);

                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["method_name"] = MethodType.ip_info.ToString();
                query["auth_key"] = authKey;

                var postData = string.Empty;
                if (ipList.Length == 1)
                    query["ip"] = ipList.First();
                else
                    postData = "data=" + string.Join(",", ipList);

                uriBuilder.Query = query.ToString();

                response = string.IsNullOrEmpty(postData)
                    ? webClient.DownloadString(uriBuilder.Uri)
                    : webClient.UploadString(uriBuilder.Uri, postData);
            }

            var result = WebHelper.JsonDeserialize<IpInfoResponse>(response);

            return result;
        }

        private static void SetWebHeaders(WebClient webClient)
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                var headers = context.Request.Headers;
                foreach (var kvp in headers.Keys.Cast<string>()
                             .Select(x => new { key = x, value = headers[x] })
                             .Where(x =>
                                 !customRestrictedHeaders.Contains(x.key) &&
                                 !WebHeaderCollection.IsRestricted(x.key)))
                    webClient.Headers.Add(kvp.key, kvp.value);
            }

            webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        }
    }
}