using System.Diagnostics;
using System.Monads;
using cleantalk.csharp.Helpers;
using NUnit.Framework;

namespace cleantalk.csharp.test
{
    [TestFixture]
    public class CheckMessageTests
    {
        private ICleartalk _cleantalk;

        [Test]
        public void NotSpamMessageTest()
        {
            var req1 = new CleantalkRequest(TestConstants.AuthKey)
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

        [Test]
        public void StopEmailMessageTest()
        {
            var req1 = new CleantalkRequest(TestConstants.AuthKey)
            {
                Message = "good words",
                SenderInfo = new SenderInfo
                {
                    Refferrer = "http://www.bbc.co.uk/sport",
                    UserAgent = "Opera/9.80 (Windows NT 6.1; WOW64) Presto/2.12.388 Version/12.12"
                },
                SenderIp = "91.207.4.192",
                SenderEmail = "stop_email@example.com",
                SenderNickname = "Hacker",
                IsJsEnable = 1,
                SubmitTime = 12
            };

            Debug.WriteLine("req1=" + WebHelper.JsonSerialize(req1));
            var res1 = _cleantalk.CheckMessage(req1);
            Debug.WriteLine("res1=" + WebHelper.JsonSerialize(res1));

            Assert.IsNotNull(res1);
            Assert.IsNotNullOrEmpty(res1.Id);
            Assert.AreEqual(0, res1.IsInactive);
            Assert.IsFalse(res1.IsAllow.With(x => x.Value));
            Assert.IsNotNullOrEmpty(res1.Comment);
        }

        [Test]
        public void StopWordMessageTest()
        {
            var req1 = new CleantalkRequest(TestConstants.AuthKey)
            {
                Message = "stop_word",
                SenderInfo = new SenderInfo
                {
                    Refferrer = "http://www.bbc.co.uk/sport",
                    UserAgent = "Opera/9.80 (Windows NT 6.1; WOW64) Presto/2.12.388 Version/12.12"
                },
                PostInfo = new PostInfo
                {
                    CommentType = "feedback"
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
            Assert.IsFalse(res1.IsAllow.With(x => x.Value));
            Assert.IsTrue(res1.IsSpam.With(x => x.Value));
            Assert.IsNotNullOrEmpty(res1.Comment);
        }

        [SetUp]
        protected void SetUp()
        {
            _cleantalk = new Cleantalk();
        }
    }
}