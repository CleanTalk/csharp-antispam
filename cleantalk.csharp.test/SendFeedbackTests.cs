using System.Diagnostics;
using cleantalk.csharp.Helpers;
using NUnit.Framework;

namespace cleantalk.csharp.test
{
    [TestFixture]
    public class SendFeedbackTests
    {
        private ICleantalk _cleantalk;

        [Test]
        public void SendFeedbackTest()
        {
            //send message1 to collect request_id
            var req1 = new CleantalkRequest(TestConstants.AuthKey)
            {
                Message = "bla-bla-bla oh",
                SenderInfo = new SenderInfo
                {
                    Refferrer = "http://www.bbc.co.uk/sport",
                    UserAgent = "Opera/9.80 (Windows NT 6.1; WOW64) Presto/2.12.388 Version/12.12"
                },
                SenderIp = "91.207.4.192",
                SenderEmail = "keanu8dh@gmail.com",
                SenderNickname = "Mike",
                //IsJsEnable = 1, redundant if use event_token
                EventToken = "f32f32f32f32f32f32f32f32f32f32a2",
                SubmitTime = 15
            };
            var res1 = _cleantalk.CheckMessage(req1);
            Debug.WriteLine("req1=" + WebHelper.JsonSerialize(req1));
            Debug.WriteLine("res1=" + WebHelper.JsonSerialize(res1));

            //send message2 to collect request_id
            var req2 = new CleantalkRequest(TestConstants.AuthKey)
            {
                Message = "This is great storm!!!",
                SenderInfo = new SenderInfo
                {
                    Refferrer = "http://www.bbc.co.uk/sport",
                    UserAgent = "Opera/9.80 (Windows NT 6.1; WOW64) Presto/2.12.388 Version/12.12"
                },
                SenderIp = "91.207.4.192",
                SenderEmail = "keanu8dh@gmail.com",
                SenderNickname = "Mike",
                //IsJsEnable = 1, redundant if use event_token
                EventToken = "f32f32f32f32f32f32f32f32f32f32a2",
                SubmitTime = 15
            };
            var res2 = _cleantalk.CheckMessage(req2);
            Debug.WriteLine("req2=" + WebHelper.JsonSerialize(req2));
            Debug.WriteLine("res2=" + WebHelper.JsonSerialize(res2));

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
            Assert.IsTrue(feedbackResp.Received.GetValueOrDefault());
        }

        [SetUp]
        protected void SetUp()
        {
            _cleantalk = new Cleantalk();
        }
    }
}