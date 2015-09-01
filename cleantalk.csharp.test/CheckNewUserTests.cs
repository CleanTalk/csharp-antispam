using NUnit.Framework;

namespace cleantalk.csharp.test
{
    [TestFixture]
    public class CheckNewUserTests
    {
        private ICleartalk _cleantalk;

        [SetUp]
        protected void SetUp()
        {
            _cleantalk = new Cleantalk();
        }

        [Test]
        public void CheckNewUserTest()
        {
            var req1 = new CleantalkRequest(TestConstants.AuthKey)
            {
                ResponseLang = "en",
                Agent = "Opera/9.80 (Windows NT 6.1; WOW64) Presto/2.12.388 Version/12.12",
                SenderIp = "91.207.4.192",
                SenderEmail = "keanu8dh.smith@gmail.com",
                SenderNickname = "Mike",
                IsAllowLinks = 0,
                IsEnableJs = 1,
                SubmitTime = 12
            };

            var res1 = _cleantalk.CheckNewUser(req1);

            Assert.IsNotNull(res1);
            Assert.IsNotNullOrEmpty(res1.Id);

            Assert.IsTrue(res1.IsAllow);
            Assert.IsNotNullOrEmpty(res1.Comment);
        }

        [Test]
        public void CheckNewSpamUserTest()
        {
            var req1 = new CleantalkRequest(TestConstants.AuthKey)
            {
                ResponseLang = "en",
                Agent = "Opera/9.80 (Windows NT 6.1; WOW64) Presto/2.12.388 Version/12.12",
                SenderIp = "91.207.4.193",
                SenderEmail = "stop_email@example.com",
                SenderNickname = "Hacker",
                IsEnableJs = 1,
            };

            var res1 = _cleantalk.CheckNewUser(req1);

            Assert.IsNotNull(res1);
            Assert.IsNotNullOrEmpty(res1.Id);

            Assert.IsFalse(res1.IsAllow);
            Assert.IsNotNullOrEmpty(res1.Comment);
        }
    }
}