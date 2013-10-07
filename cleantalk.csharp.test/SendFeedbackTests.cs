using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace cleantalk.csharp.test
{
    [TestFixture]
    public class SendFeedbackTests
    {
        const string AuthKey = "y3ateqen";

        private ICleartalk _cleantalk;

        [SetUp]
        protected void SetUp()
        {
            this._cleantalk = new Cleantalk();
        }

        [Test]
        public void SendFeedbackTest()
        {
            var req1 = new CleantalkRequest(AuthKey)
            {
                ResponseLang = "en",
                Feedback = "42a7ae178d1cfc4eee6431ce30ecc567:1;1e63750c2fce77b5199594e0d0ea2de7:0;e9687ba3f6751e218d7ef1476b8f72a9:0;d05b8dd0e0d8fad37eb0f2d0d3f42f03:1;"
            };

            var res1 = _cleantalk.SendFeedback(req1);

            Assert.IsNotNull(res1);
            Assert.IsNotNullOrEmpty(res1.Id);
            Assert.IsTrue(res1.IsAllow);
            Assert.IsNotNullOrEmpty(res1.Comment);
        }
    }
}
