using System;
using cleantalk.csharp.Helpers;
using cleantalk.csharp.Request;

namespace cleantalk.csharp.console
{
    public class Program
    {
        //TODO: set auth_key
        public const string AuthKey = "key1";

        //TODO: set spam_check auth_key
        public const string SpamCheckAuthKey = "key2";

        public static void Main(string[] args)
        {
            var cleantalk = new Cleantalk(Constants.SpamCheckServerUrl);

            var request = new SpamCheckRequest(SpamCheckAuthKey)
            {
                email = "stop_email@example.com",
                ip = "127.0.0.1"
            };
            var result = cleantalk.SpamCheck(request);

            Console.WriteLine(WebHelper.JsonSerialize(result));
        }
    }
}