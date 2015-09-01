using System;
using NUnit.Framework;

namespace cleantalk.csharp.test
{
    [TestFixture]
    public class SendFeedbackTests
    {
        private ICleartalk _cleantalk;

        [SetUp]
        protected void SetUp()
        {
            _cleantalk = new Cleantalk();
        }

        [Test]
        public void SendFeedbackTest()
        {
            //send message1
            var req1 = new CleantalkRequest(TestConstants.AuthKey)
            {
                Message = "bla-bla-bla",
                Example = "",
                ResponseLang = "en",
                SenderInfo = WebHelper.JsonSerialize(new SenderInfo
                {
                    CmsLang = "en",
                    Refferrer = "http://www.bbc.co.uk/sport",
                    UserAgent = "Opera/9.80 (Windows NT 6.1; WOW64) Presto/2.12.388 Version/12.12",
                    Profile = false
                }),
                SenderIp = "91.207.4.192",
                SenderEmail = "keanu8dh.smith@gmail.com",
                SenderNickname = "Mike",
                IsAllowLinks = 0,
                IsEnableJs = 1,
                SubmitTime = 12,
                StoplistCheck = 0
            };
            var res1 = _cleantalk.CheckMessage(req1);
            Assert.IsTrue(res1.IsAllow);

            //send message2
            var req2 = new CleantalkRequest(TestConstants.AuthKey)
            {
                Message = "This is great storm!",
                Example = "Formula 1 organisers are monitoring Tropical Storm Fitow that is passing through parts of Asia ahead of this weekend's Korean Grand Prix.",
                ResponseLang = "en",
                SenderInfo = WebHelper.JsonSerialize(new SenderInfo
                {
                    CmsLang = "en",
                    Refferrer = "http://www.bbc.co.uk/sport",
                    UserAgent = "Opera/9.80 (Windows NT 6.1; WOW64) Presto/2.12.388 Version/12.12",
                    Profile = false
                }),
                SenderIp = "91.207.4.192",
                SenderEmail = "keanu8dh.smith@gmail.com",
                SenderNickname = "Mike",
                IsAllowLinks = 0,
                IsEnableJs = 1,
                SubmitTime = 12,
                StoplistCheck = 0
            };
            var res2 = _cleantalk.CheckMessage(req2);
            Assert.IsTrue(res2.IsAllow);

            //send feedback
            var feedbackReq = new CleantalkRequest(TestConstants.AuthKey)
            {
                ResponseLang = "en",
                Feedback = String.Format("{0}:1;{1}:0;", res1.Id, res2.Id)
            };

            var feedbackResp = _cleantalk.SendFeedback(feedbackReq);

            Assert.IsNotNull(feedbackResp);
            Assert.IsNotNullOrEmpty(feedbackResp.Comment);
            Assert.IsTrue(feedbackResp.Received);
        }
    }
}