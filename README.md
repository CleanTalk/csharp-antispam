csharp-antispam
===============

CleanTalk service API for C#. It is invisible protection from spam, no captchas, no puzzles, no animals, and no math.

## Actual API documentation
  * [check_message](https://cleantalk.org/help/api-check-message) - Check IPs, Emails and messages for spam activity
  * [check_newuser](https://cleantalk.org/help/api-check-newuser) - Check registrations of new users

## How does the API stop spam?
The API uses several simple tests to stop spammers.
  * Spambot signatures.
  * Blacklist checks by Email, IP, website domain names.
  * Javascript availability.
  * Comment submit time.
  * Relevance test for the comment.

## How does the API work?
API sends the comment's text and several previous approved comments to the server. The server evaluates the relevance of the comment's text on the topic, tests for spam and finally provides a solution - to publish or to put in manual moderation queue of comments. If a comment is placed in manual moderation queue, the plugin adds a rejection explanation to the text of the comment.

## Requirements

   * [.Net Framework 4.5](https://dot.net)
   * CleanTalk account https://cleantalk.org/register?product=anti-spam

## SPAM test for text comment sample

```c#

    public const string AuthKey = "auth key";

    [TestFixture]
    public class CheckMessageTests
    {
        private ICleantalk _cleantalk;

        [Test]
        public void NotSpamMessageTest()
        {
            var req1 = new CleantalkRequest(AuthKey)
            {
                Message = "This is a great storm!",
                SenderInfo = new SenderInfo
                {
                    Refferrer = "https://www.bbc.co.uk/sport",
                    UserAgent = "Opera/9.80 (Windows NT 6.1; WOW64) Presto/2.12.388 Version/12.12"
                },
                SenderIp = "91.207.4.192",
                SenderEmail = "keanu8dh@gmail.com",
                SenderNickname = "Mike",
                IsJsEnable = 1,
                SubmitTime = 15
            };

            Debug.WriteLine("req1=" + WebHelper.JsonSerialize(req1));
            var res1 = _cleantalk.CheckMessage(req1);
            Debug.WriteLine("res1=" + WebHelper.JsonSerialize(res1));
            Assert.IsNotNull(res1);
            Assert.IsNotNullOrEmpty(res1.Id);
            Assert.AreEqual(0, res1.IsInactive);
            Assert.IsTrue(res1.IsAllow.GetValueOrDefault());
            Assert.IsNotNullOrEmpty(res1.Comment);
        }
      }

```

## API Response description
API returns response object:
  * allow (0|1) - allow to publish or not, in other words spam or ham.
  * comment (string) - server comment for requests.
  * id (string MD5 HEX hash) - unique request idenifier.
  * errno (int) - error number. errno will be 0 if request is successful.
  * errtstr (string) - comment explaining the error. errstr will be `null` if request is successful.
  
