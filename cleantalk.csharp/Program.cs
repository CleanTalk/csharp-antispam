using System;

namespace cleantalk.csharp
{
    public class Program
    {
        public static int Main()
        {
            const string testMessage = "This is test message";
            const string authKey = "your_auth_key";

            ICleartalk c = new Cleantalk();

            //test CheckMessage
            var req1 = new CleantalkRequest(authKey)
                {
                    Message = testMessage,
                    Example = testMessage
                };
            var res1 = c.CheckMessage(req1);
            Console.WriteLine("Result={0}\n\n", WebHelper.JsonSerialize(res1));

            //test CheckNewUser
            var req2 = new CleantalkRequest(authKey)
                {
                    SenderNickname = "testUserNickName",
                    SenderEmail = "testUserNickName@test.ru",
                    IsEnableJs = true
                };
            var res2 = c.CheckNewUser(req2);
            Console.WriteLine("Result={0}\n\n", WebHelper.JsonSerialize(res2));

            //test SendFeedback
            var req3 = new CleantalkRequest(authKey)
                {
                    Feedback = "This is super feedback!"
                };
            var res3 = c.SendFeedback(req3);
            Console.WriteLine("Result={0}\n\n", WebHelper.JsonSerialize(res3));

            return 0;
        }
    }
}
