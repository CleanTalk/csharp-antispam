using System.Diagnostics;
using System.Monads;
using cleantalk.csharp.Helpers;
using NUnit.Framework;

namespace cleantalk.csharp.test
{
    [TestFixture]
    public class CheckNewUserTests
    {
        private ICleartalk _cleantalk;

        [Test]
        public void CheckNewSpamUserTest()
        {
            var req1 = new CleantalkRequest(TestConstants.AuthKey)
            {
                SenderIp = "91.207.4.193",
                SenderEmail = "stop_email@example.com",
                SenderNickname = "Hacker",
                IsJsEnable = 1
            };

            var res1 = _cleantalk.CheckNewUser(req1);
            Debug.WriteLine("req1=" + WebHelper.JsonSerialize(req1));
            Debug.WriteLine("res1=" + WebHelper.JsonSerialize(res1));

            Assert.IsNotNull(res1);
            Assert.IsNotNullOrEmpty(res1.Id);
            Assert.AreEqual(0, res1.IsInactive);
            Assert.IsFalse(res1.IsAllow.With(x => x.Value));
            Assert.IsNotNullOrEmpty(res1.Comment);
        }

        [Test]
        public void CheckNewUserTest()
        {
            var req1 = new CleantalkRequest(TestConstants.AuthKey)
            {
                SenderIp = "91.207.4.192",
                SenderEmail = "keanu8dh.smith@gmail.com",
                SenderNickname = "Mike",
                IsJsEnable = 1,
                SubmitTime = 12
            };
            var res1 = _cleantalk.CheckNewUser(req1);
            Debug.WriteLine("req1=" + WebHelper.JsonSerialize(req1));
            Debug.WriteLine("res1=" + WebHelper.JsonSerialize(res1));

            Assert.IsNotNull(res1);
            Assert.IsNotNullOrEmpty(res1.Id);
            Assert.AreEqual(0, res1.IsInactive);
            Assert.IsTrue(res1.IsAllow.With(x => x.Value));
            Assert.IsNotNullOrEmpty(res1.Comment);
        }

        [SetUp]
        protected void SetUp()
        {
            _cleantalk = new Cleantalk();
        }
    }
}