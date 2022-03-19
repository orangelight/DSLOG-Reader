using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSLOG_Reader_Library;
using System;

namespace DSLOG_Reader_Tests
{
    [TestClass]
    public class ReaderTests
    {
        [TestMethod]
        public void TestReadLog()
        {
            var reader = new DSLOGReader("TestFiles\\2022_02_25 21_25_39 Fri.dslog");
            reader.Read();
            Assert.IsTrue(reader.Version == 4);
        }

        [TestMethod]
        public void TestReadLogRev()
        {
            var reader = new DSLOGReader("TestFiles\\2022_02_27 15_17_59 Sun.dslog");
            reader.Read();
            Assert.IsTrue(reader.Version == 4);
        }

        [TestMethod]
        public void TestReadEvents()
        {
            var reader = new DSEVENTSReader("TestFiles\\2022_02_25 21_25_39 Fri.dsevents");
            reader.Read();
            Assert.IsTrue(reader.Version == 4);
            Assert.IsTrue(reader.Entries[0].Data == "<TagVersion>1 <time> 04.803 <message> ********** Robot program starting ********** ");
        }

        [TestMethod]
        public void TestReadMultiPdpType()
        {
            var reader = new DSLOGReader("TestFiles\\2022_03_19 12_55_56 Sat.dslog");
            reader.Read();
            Assert.IsTrue(reader.Version == 4);
            foreach (var entry in reader.Entries)
            {
                Assert.IsFalse(entry.Watchdog);
                Assert.IsFalse(entry.Brownout);
            }
        }

        [TestMethod]
        public void TestReadMetadata()
        {
            var reader = new DSLOGReader("TestFiles\\2022_02_25 21_25_39 Fri.dslog");
            reader.OnlyReadMetaData();
            Assert.IsTrue(reader.Version == 4);
            var expectedTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(2022, 02, 26, 2, 25, 39, 855), TimeZoneInfo.Local);
            Assert.AreEqual(reader.StartTime, expectedTime);
        }
    }
}
