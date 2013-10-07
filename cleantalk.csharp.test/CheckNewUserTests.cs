using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace cleantalk.csharp.test
{
    [TestFixture]
    public class CheckNewUserTests
    {
        const string AuthKey = "y3ateqen";

        private ICleartalk _cleantalk;

        [SetUp]
        protected void SetUp()
        {
            this._cleantalk = new Cleantalk();
        }

        [Test]
        public void CheckNewUserTest()
        {
            var req1 = new CleantalkRequest(AuthKey)
            {
                ResponseLang = "en",
                Agent = "Opera/9.80 (Windows NT 6.1; WOW64) Presto/2.12.388 Version/12.12",
                SenderInfo = WebHelper.JsonSerialize(new SenderInfo
                {
                    Refferrer = "http://www.bbc.co.uk/sport",
                    UserAgent = "Opera/9.80 (Windows NT 6.1; WOW64) Presto/2.12.388 Version/12.12",
                }),
                SenderIp = "91.207.4.192",
                SenderEmail = "keanu8dh.smith@gmail.com",
                SenderNickname = "Mike",
                IsAllowLinks = 0,
                IsEnableJs = 1,
                SubmitTime = 12,
                TimeZone = 2
            };

            var res1 = _cleantalk.CheckNewUser(req1);

            Assert.IsNotNull(res1);
            Assert.IsNotNullOrEmpty(res1.Id);
            Assert.IsTrue(res1.IsAllow);
            Assert.IsNotNullOrEmpty(res1.Comment);
        }
    }
}
