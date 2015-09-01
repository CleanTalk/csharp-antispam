﻿using NUnit.Framework;

namespace cleantalk.csharp.test
{
    [TestFixture]
    public class CheckMessageTests
    {
        private ICleartalk _cleantalk;

        [SetUp]
        protected void SetUp()
        {
            _cleantalk = new Cleantalk();
        }

        [Test]
        public void NotSpamMessageTest()
        {
            var req1 = new CleantalkRequest(TestConstants.AuthKey)
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

            var res1 = _cleantalk.CheckMessage(req1);

            Assert.IsNotNull(res1);
            Assert.IsNotNullOrEmpty(res1.Id);

            Assert.IsTrue(res1.IsAllow);
            Assert.IsNotNullOrEmpty(res1.Comment);
        }

        [Test]
        public void SpamMessageTest()
        {
            var req1 = new CleantalkRequest(TestConstants.AuthKey)
            {
                Message = "stop_word",
                Example = "Example",
                ResponseLang = "en",
                SenderInfo = WebHelper.JsonSerialize(new SenderInfo
                {
                    CmsLang = "en",
                    Refferrer = "http://www.bbc.co.uk/sport",
                    UserAgent = "Opera/9.80 (Windows NT 6.1; WOW64) Presto/2.12.388 Version/12.12",
                    Profile = false
                }),
                SenderIp = "91.207.4.192",
                SenderEmail = "stop_email@example.com",
                SenderNickname = "Hacker",
                IsAllowLinks = 0,
                IsEnableJs = 1,
                SubmitTime = 12,
                StoplistCheck = 1
            };

            var res1 = _cleantalk.CheckMessage(req1);

            Assert.IsNotNull(res1);
            Assert.IsNotNullOrEmpty(res1.Id);

            Assert.IsFalse(res1.IsAllow);
            Assert.IsNotNullOrEmpty(res1.Comment);
        }
    }
}