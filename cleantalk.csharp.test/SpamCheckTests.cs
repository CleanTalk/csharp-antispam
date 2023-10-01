﻿using System.Diagnostics;
using cleantalk.csharp.Helpers;
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
            var req1 = new CleantalkRequest(TestConstants.AuthKey)
            {
                SenderIp = "91.207.4.193",
                SenderEmail = "stop_email@example.com",
                SenderNickname = "Hacker",
                //IsJsEnable = 1, redundant if use event_token
                EventToken = "f32f32f32f32f32f32f32f32f32f32a2",
            };

            var res1 = _cleantalk.SpamCheck(req1);
            Debug.WriteLine("req1=" + WebHelper.JsonSerialize(req1));
            Debug.WriteLine("res1=" + WebHelper.JsonSerialize(res1));

            Assert.IsNotNull(res1);
            Assert.IsNotNullOrEmpty(res1.Id);
            Assert.AreEqual(0, res1.IsInactive);
            Assert.IsFalse(res1.IsAllow.GetValueOrDefault());
            Assert.IsNotNullOrEmpty(res1.Comment);
        }

        [SetUp]
        protected void SetUp()
        {
            _cleantalk = new Cleantalk();
        }
    }
}