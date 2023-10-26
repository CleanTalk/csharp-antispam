using System.Diagnostics;
using cleantalk.csharp.Helpers;
using NUnit.Framework;

namespace cleantalk.csharp.test
{
    [TestFixture]
    public class IpInfoTests
    {
        [SetUp]
        protected void SetUp()
        {
            _cleantalk = new Cleantalk(Constants.SpamCheckServerUrl);
        }

        private ICleantalk _cleantalk;

        [Test]
        public void IpInfoTest()
        {
            var res1 = _cleantalk.IpInfoCheck(TestConstants.AuthKey, "8.8.8.8");
            Debug.WriteLine("res1=" + WebHelper.JsonSerialize(res1));

            Assert.IsNotNull(res1);
            Assert.IsNotEmpty(res1.Data);
        }

        [Test]
        public void IpInfoMultipleRecordsTest()
        {
            var res1 = _cleantalk.IpInfoCheck(TestConstants.AuthKey, "8.8.8.8", "213.239.245.253");
            Debug.WriteLine("res1=" + WebHelper.JsonSerialize(res1));

            Assert.IsNotNull(res1);
            Assert.IsNotEmpty(res1.Data);
        }
    }
}