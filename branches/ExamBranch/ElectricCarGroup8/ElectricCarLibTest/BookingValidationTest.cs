using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarModelLayer;
using ElectricCarDB;
using ElectricCarLib;

namespace ElectricCarLibTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class BookingValidationTest
    {
        public BookingValidationTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestValidateBooking1()
        {
            IDStation dbStation = new DStation();
            IDBatteryType dbType = new DBatteryType();
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            IDPeriod dbPeriod = new DBPeriod();
            int sid = dbStation.addNewRecord("name", "address", "country", "state");
            int btid = dbType.addNewRecord("name", "producer", 10, 10);
            int bsid = dbStorage.addNewRecord(btid, sid, 10);
            MBatteryStorage storage = dbStorage.getRecord(bsid, true);
            try
            {
                
                MPeriod firstPeriod = new MPeriod();
                firstPeriod.bookedBatteryNumber = 6;
                firstPeriod.initBatteryNumber = 10;
                firstPeriod.time = DateTime.Today;
                dbPeriod.addNewRecord(bsid, firstPeriod.time, firstPeriod.initBatteryNumber, firstPeriod.bookedBatteryNumber);
                storage.periods.Add(firstPeriod);
                MPeriod testPeriod = new MPeriod();
                testPeriod.time = DateTime.Today.AddHours(10);
                testPeriod.initBatteryNumber = 10;
                testPeriod.bookedBatteryNumber = 4;
                dbPeriod.addNewRecord(bsid, testPeriod.time, testPeriod.initBatteryNumber, testPeriod.bookedBatteryNumber);
                storage.periods.Add(testPeriod);
                MPeriod lastPeriod = new MPeriod();
                lastPeriod.time = DateTime.Today.AddHours(20);
                lastPeriod.initBatteryNumber = 10;
                lastPeriod.bookedBatteryNumber = 5;
                dbPeriod.addNewRecord(bsid, lastPeriod.time, lastPeriod.initBatteryNumber, lastPeriod.bookedBatteryNumber);
                storage.periods.Add(lastPeriod);
                BatteryStorageCtr storageCtr = new BatteryStorageCtr();
                if (storageCtr.validateBookingForStation(sid, btid, 1, DateTime.Today.AddHours(10)))
                {
                    Assert.AreEqual(2, 1);
                }
                else
                {
                    Assert.AreEqual(1, 1); //it should fail
                }
                if (storageCtr.validateUpdateBookingForStation(sid, btid, 1, DateTime.Today.AddHours(10)))
                {
                    Assert.AreEqual(2, 1);
                }
                else
                {
                    Assert.AreEqual(1, 1); //it should fail
                }
            }
            finally
            {
                foreach (MPeriod p in storage.periods)
                {
                    dbPeriod.deleteRecord(bsid, p.time);
                }
                dbStorage.deleteRecord(bsid);
                dbType.deleteRecord(btid);
                dbStation.deleteRecord(sid);
            }
        }
        [TestMethod]
        public void TestValidateBooking2()
        {
            IDStation dbStation = new DStation();
            IDBatteryType dbType = new DBatteryType();
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            IDPeriod dbPeriod = new DBPeriod();
            int sid = dbStation.addNewRecord("name", "address", "country", "state");
            int btid = dbType.addNewRecord("name", "producer", 10, 10);
            int bsid = dbStorage.addNewRecord(btid, sid, 10);
            MBatteryStorage storage = dbStorage.getRecord(bsid, true);
            try
            {

                MPeriod firstPeriod = new MPeriod();
                firstPeriod.bookedBatteryNumber = 5;
                firstPeriod.initBatteryNumber = 10;
                firstPeriod.time = DateTime.Today;
                dbPeriod.addNewRecord(bsid, firstPeriod.time, firstPeriod.initBatteryNumber, firstPeriod.bookedBatteryNumber);
                storage.periods.Add(firstPeriod);
                MPeriod testPeriod = new MPeriod();
                testPeriod.time = DateTime.Today.AddHours(10);
                testPeriod.initBatteryNumber = 10;
                testPeriod.bookedBatteryNumber = 4;
                dbPeriod.addNewRecord(bsid, testPeriod.time, testPeriod.initBatteryNumber, testPeriod.bookedBatteryNumber);
                storage.periods.Add(testPeriod);
                MPeriod lastPeriod = new MPeriod();
                lastPeriod.time = DateTime.Today.AddHours(20);
                lastPeriod.initBatteryNumber = 10;
                lastPeriod.bookedBatteryNumber = 6;
                dbPeriod.addNewRecord(bsid, lastPeriod.time, lastPeriod.initBatteryNumber, lastPeriod.bookedBatteryNumber);
                storage.periods.Add(lastPeriod);
                BatteryStorageCtr storageCtr = new BatteryStorageCtr();
                if (storageCtr.validateBookingForStation(sid, btid, 1, DateTime.Today.AddHours(10)))
                {
                    Assert.AreEqual(2, 1);
                }
                else
                {
                    Assert.AreEqual(1, 1); //it should fail
                }
                if (storageCtr.validateUpdateBookingForStation(sid, btid, 1, DateTime.Today.AddHours(10)))
                {
                    Assert.AreEqual(2, 1);
                }
                else
                {
                    Assert.AreEqual(1, 1); //it should fail
                }
            }
            finally
            {
                foreach (MPeriod p in storage.periods)
                {
                    dbPeriod.deleteRecord(bsid, p.time);
                }
                dbStorage.deleteRecord(bsid);
                dbType.deleteRecord(btid);
                dbStation.deleteRecord(sid);
            }
        }
        [TestMethod]
        public void TestValidateBooking3()
        {
            IDStation dbStation = new DStation();
            IDBatteryType dbType = new DBatteryType();
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            IDPeriod dbPeriod = new DBPeriod();
            int sid = dbStation.addNewRecord("name", "address", "country", "state");
            int btid = dbType.addNewRecord("name", "producer", 10, 10);
            int bsid = dbStorage.addNewRecord(btid, sid, 10);
            MBatteryStorage storage = dbStorage.getRecord(bsid, true);
            try
            {

                MPeriod firstPeriod = new MPeriod();
                firstPeriod.bookedBatteryNumber = 5;
                firstPeriod.initBatteryNumber = 10;
                firstPeriod.time = DateTime.Today;
                dbPeriod.addNewRecord(bsid, firstPeriod.time, firstPeriod.initBatteryNumber, firstPeriod.bookedBatteryNumber);
                storage.periods.Add(firstPeriod);
                MPeriod testPeriod = new MPeriod();
                testPeriod.time = DateTime.Today.AddHours(10);
                testPeriod.initBatteryNumber = 10;
                testPeriod.bookedBatteryNumber = 4;
                dbPeriod.addNewRecord(bsid, testPeriod.time, testPeriod.initBatteryNumber, testPeriod.bookedBatteryNumber);
                storage.periods.Add(testPeriod);
                MPeriod lastPeriod = new MPeriod();
                lastPeriod.time = DateTime.Today.AddHours(20);
                lastPeriod.initBatteryNumber = 10;
                lastPeriod.bookedBatteryNumber = 5;
                dbPeriod.addNewRecord(bsid, lastPeriod.time, lastPeriod.initBatteryNumber, lastPeriod.bookedBatteryNumber);
                storage.periods.Add(lastPeriod);
                BatteryStorageCtr storageCtr = new BatteryStorageCtr();
                if (storageCtr.validateBookingForStation(sid, btid, 1, DateTime.Today.AddHours(10)))
                {
                    Assert.AreEqual(true, storageCtr.addBookingForStation(sid,btid,1,DateTime.Today.AddHours(10)));
                }
                else
                {
                    Assert.AreEqual(2, 1); //it should pass
                }
                if (storageCtr.validateUpdateBookingForStation(sid, btid, 1, DateTime.Today.AddHours(10)))
                {
                    Assert.AreEqual(2, 1);
                }
                else
                {
                    Assert.AreEqual(1, 1); //it shouldn't pass
                }
            }
            finally
            {
                foreach (MPeriod p in storage.periods)
                {
                    dbPeriod.deleteRecord(bsid, p.time);
                }
                dbStorage.deleteRecord(bsid);
                dbType.deleteRecord(btid);
                dbStation.deleteRecord(sid);
            }
        }
        [TestMethod]
        public void TestValidateBookingUpdateDelete()
        {
            IDStation dbStation = new DStation();
            IDBatteryType dbType = new DBatteryType();
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            IDPeriod dbPeriod = new DBPeriod();
            int sid = dbStation.addNewRecord("name", "address", "country", "state");
            int btid = dbType.addNewRecord("name", "producer", 10, 10);
            int bsid = dbStorage.addNewRecord(btid, sid, 10);
            MBatteryStorage storage = dbStorage.getRecord(bsid, true);
            try
            {

                MPeriod firstPeriod = new MPeriod();
                firstPeriod.bookedBatteryNumber = 5;
                firstPeriod.initBatteryNumber = 10;
                firstPeriod.time = DateTime.Today;
                dbPeriod.addNewRecord(bsid, firstPeriod.time, firstPeriod.initBatteryNumber, firstPeriod.bookedBatteryNumber);
                storage.periods.Add(firstPeriod);
                MPeriod testPeriod = new MPeriod();
                testPeriod.time = DateTime.Today.AddHours(10);
                testPeriod.initBatteryNumber = 10;
                testPeriod.bookedBatteryNumber = 4;
                dbPeriod.addNewRecord(bsid, testPeriod.time, testPeriod.initBatteryNumber, testPeriod.bookedBatteryNumber);
                storage.periods.Add(testPeriod);
                MPeriod lastPeriod = new MPeriod();
                lastPeriod.time = DateTime.Today.AddHours(20);
                lastPeriod.initBatteryNumber = 10;
                lastPeriod.bookedBatteryNumber = 5;
                dbPeriod.addNewRecord(bsid, lastPeriod.time, lastPeriod.initBatteryNumber, lastPeriod.bookedBatteryNumber);
                storage.periods.Add(lastPeriod);
                BatteryStorageCtr storageCtr = new BatteryStorageCtr();
                if (storageCtr.validateBookingForStation(sid, btid, 1, DateTime.Today.AddHours(10)))
                {
                    Assert.AreEqual(true, storageCtr.updateBookingForStation(sid, btid, 1, DateTime.Today.AddHours(10)));
                }
                else
                {
                    Assert.AreEqual(2, 1); //it should pass
                }
                if (storageCtr.validateUpdateBookingForStation(sid, btid, 1, DateTime.Today.AddHours(10)))
                {
                    Assert.AreEqual(2, 1);
                }
                else
                {
                    Assert.AreEqual(1, 1); //it shouldn't pass
                }
                Assert.AreEqual(true, storageCtr.deleteBookingForStation(sid, btid, 1, DateTime.Today.AddHours(10)));
            }
            finally
            {
                foreach (MPeriod p in storage.periods)
                {
                    dbPeriod.deleteRecord(bsid, p.time);
                }
                dbStorage.deleteRecord(bsid);
                dbType.deleteRecord(btid);
                dbStation.deleteRecord(sid);
            }
        }
    }
}
