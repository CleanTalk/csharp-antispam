using System.Diagnostics;
using cleantalk.csharp.Helpers;
using cleantalk.csharp.Request;
using NUnit.Framework;

namespace cleantalk.csharp.test
{
    [TestFixture]
    public class SpamCheckTests
    {
        private ICleantalk _cleantalk;

        [Test]
        public void SpamCheckTest()
        {
            // string ip, string email, string date, string email_SHA256, string ip4_SHA256, string ip6_SHA256
            var req1 = new SpamCheckRequest(TestConstants.SpamCheckAuthKey)
            {
                email = "stop_email@example.com",
                ip = "127.0.0.1"
            };
            var res1 = _cleantalk.SpamCheck(req1);
            Debug.WriteLine("req1=" + WebHelper.JsonSerialize(req1));
            Debug.WriteLine("res1=" + WebHelper.JsonSerialize(res1));

            Assert.IsNotNull(res1);
        }

        [SetUp]
        protected void SetUp()
        {
            _cleantalk = new Cleantalk(Constants.SpamCheckServerUrl);
        }
    }
}