﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using IGL.Common.Tests.Helpers;
using System.Collections.Specialized;

namespace IGL.Common.Tests
{
    [TestClass]
    public class EncryptTests
    {
        [TestMethod]
        public void GameEventEncryptionTest()
        {
#if !FAKES_NOT_SUPPORTED
            using (Microsoft.QualityTools.Testing.Fakes.ShimsContext.Create())
            {
                Faker.FakeOut();
#endif
                var event1 = SampleGenerator.GameEventTest1();
                var packet = SampleGenerator.GamePacketTest1();

                packet.GameEvent = event1;

                // is the content encrypted
                Assert.IsTrue(packet.Content.Contains("76561198024856042") == false);

                Assert.AreEqual(event1.EventId, packet.GameEvent.EventId);
                Assert.AreEqual(event1.GameId, packet.GameEvent.GameId);

                Assert.AreEqual(event1.Properties["stat_avg_level_1"], packet.GameEvent.Properties["stat_avg_level_1"]);
#if !FAKES_NOT_SUPPORTED
            }
#endif
        }
    }
}