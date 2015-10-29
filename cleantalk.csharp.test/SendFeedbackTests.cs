using System.Diagnostics;
using System.Monads;
using cleantalk.csharp.Helpers;
using NUnit.Framework;

namespace cleantalk.csharp.test
{
    [TestFixture]
    public class SendFeedbackTests
    {
        private ICleartalk _cleantalk;

        [Test]
        public void SendFeedbackTest()
        {
            //send message1
            var req1 = new CleantalkRequest(TestConstants.AuthKey)
            {
                Message = "bla-bla-bla",
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
            var res1 = _cleantalk.CheckMessage(req1);
            Debug.WriteLine("req1=" + WebHelper.JsonSerialize(req1));
            Debug.WriteLine("res1=" + WebHelper.JsonSerialize(res1));

            Assert.AreEqual(0, res1.IsInactive);
            Assert.IsTrue(res1.IsAllow.With(x => x.Value));

            //send message2
            var req2 = new CleantalkRequest(TestConstants.AuthKey)
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
            var res2 = _cleantalk.CheckMessage(req2);
            Debug.WriteLine("req2=" + WebHelper.JsonSerialize(req2));
            Debug.WriteLine("res2=" + WebHelper.JsonSerialize(res2));

            Assert.AreEqual(0, res2.IsInactive);
            Assert.IsTrue(res2.IsAllow.With(x => x.Value));

            //send feedback
            var feedbackReq = new CleantalkRequest(TestConstants.AuthKey)
            {
                Feedback = string.Format("{0}:1;{1}:0;", res1.Id, res2.Id)
            };
            var feedbackResp = _cleantalk.SendFeedback(feedbackReq);
            Debug.WriteLine("feedbackReq=" + WebHelper.JsonSerialize(feedbackReq));
            Debug.WriteLine("feedbackResp=" + WebHelper.JsonSerialize(feedbackResp));

            Assert.IsNotNull(feedbackResp);
            Assert.IsNotNullOrEmpty(feedbackResp.Comment);
            Assert.IsTrue(feedbackResp.Received.With(x => x.Value));
        }

        [SetUp]
        protected void SetUp()
        {
            _cleantalk = new Cleantalk();
        }
    }
}