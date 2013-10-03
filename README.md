csharp-antispam
===============

CleanTalk service API for C#. It is invisible protection from spam, no captches, no puzzles, no animals and no math.

## How does API stop spam?
API uses several simple tests to stop spammers.
  * Spam bots signatures.
  * Blacklists checks by Email, IP, web-sites domain names.
  * JavaScript availability.
  * Comment submit time.
  * Relevance test for the comment.

## How does API works?
API sends a comment's text and several previous approved comments to the servers. Servers evaluates the relevance of the comment's text on the topic, tests on spam and finaly provides a solution - to publish or put on manual moderation of comments. If a comment is placed on manual moderation, the plugin adds to the text of a comment explaining the reason for the ban server publishing.

## Requirements

   * .Net Framawork 4.5

## SPAM test for text comment sample

```c#
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

```

## API Response description
API returns PHP object:
  * allow (0|1) - allow to publish or not, in other words spam or ham
  * comment (string) - server comment for requests.
  * id (string MD5 HEX hash) - unique request idenifier.
  * errno (int) - error number. errno == 0 if requests successfull.
  * errtstr (string) - comment for error issue, errstr == null if requests successfull.
  
