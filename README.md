csharp-antispam
===============

CleanTalk service API for C#. It is invisible protection from spam, no captches, no puzzles, no animals and no math.

## Actual API documentation
  * [check_message](https://cleantalk.org/wiki/doku.php?id=check_message) - Check IPs, Emails and messages for spam activity
  * [check_newuser](https://cleantalk.org/wiki/doku.php?id=check_newuser) - Check registrations of new users

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

    public const string AuthKey = "auth key";

    [TestFixture]
    public class CheckMessageTests
    {
        private ICleartalk _cleantalk;

        [Test]
        public void NotSpamMessageTest()
        {
            var req1 = new CleantalkRequest(AuthKey)
            {
                Message = "This is great storm!",
                SenderInfo = new SenderInfo
                {
                    Refferrer = "http://www.bbc.co.uk/sport",
                    UserAgent = "Opera/9.80 (Windows NT 6.1; WOW64) Presto/2.12.388 Version/12.12"
                },
                SenderIp = "91.207.4.192",
                SenderEmail = "keanu8dh.smith@gmail.com",
                SenderNickname = "Mike",
                IsJsEnable = 1,
                SubmitTime = 12
            };

            Debug.WriteLine("req1=" + WebHelper.JsonSerialize(req1));
            var res1 = _cleantalk.CheckMessage(req1);
            Debug.WriteLine("res1=" + WebHelper.JsonSerialize(res1));
            Assert.IsNotNull(res1);
            Assert.IsNotNullOrEmpty(res1.Id);
            Assert.AreEqual(0, res1.IsInactive);
            Assert.IsTrue(res1.IsAllow.With(x => x.Value));
            Assert.IsNotNullOrEmpty(res1.Comment);
        }
      }

```

## API Response description
API returns response object:
  * allow (0|1) - allow to publish or not, in other words spam or ham
  * comment (string) - server comment for requests.
  * id (string MD5 HEX hash) - unique request idenifier.
  * errno (int) - error number. errno == 0 if requests successfull.
  * errtstr (string) - comment for error issue, errstr == null if requests successfull.
  
