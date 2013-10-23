using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarModelLayer;
using ElectricCarDB;

namespace ElectricCarLibTest
{
    /// <summary>
    /// Summary description for DBPeriodTest
    /// </summary>
    [TestClass]
    public class DBPeriodTest
    {
        private IDPeriod dbPeriod = new DBPeriod();
        private IDBBatteryStorage dbStorage = new DBBatteryStorage();
        private IDBatteryType dbType = new DBatteryType();
        private IDStation dbStation = new DStation();
        public DBPeriodTest()
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
        public void addGetDeletePeriod()
        {
            int btId = dbType.addNewRecord("newName", "newProducer", 10, 100, 20);
            int sID = dbStation.addNewRecord("newName", "newAddress", "newCountry", "newState");
            int bsID =  dbStorage.addNewRecord(btId,sID);
            DateTime time = DateTime.Today;
            int id = dbPeriod.addNewRecord(bsID,time, 10, 5, 1);
            try
            {
                MPeriod period = dbPeriod.getRecord(id,time, true);
                Assert.AreEqual(DateTime.Today, period.time);
                Assert.AreEqual(10, period.initBatteryNumber);
                Assert.AreEqual(5, period.bookedBatteryNumber);
                Assert.AreEqual(1, period.futureBatteryNumber);
            }
           
            finally
            {
                dbPeriod.deleteRecord(id, time);
                dbStorage.deleteRecord(bsID);
                dbType.deleteRecord(btId);
                dbStation.deleteRecord(sID);        
        }
        }

        [TestMethod]
        public void updatePeriod()
        {
            int btId = dbType.addNewRecord("newName", "newProducer", 10, 100, 20);
            int sID = dbStation.addNewRecord("newName", "newAddress", "newCountry", "newState");
            int bsID = dbStorage.addNewRecord(btId, sID);
            int id = dbPeriod.addNewRecord(bsID, DateTime.Today, 10, 5, 1);
            DateTime time = DateTime.Today;
            try
            {
                dbPeriod.updateRecord(bsID, time, 20, 10, 2);
                MPeriod period = dbPeriod.getRecord(id, time, true);
                Assert.AreEqual(DateTime.Today, period.time);
                Assert.AreEqual(20, period.initBatteryNumber);
                Assert.AreEqual(10, period.bookedBatteryNumber);
                Assert.AreEqual(2, period.futureBatteryNumber);
            }
            catch
            {
            }
            finally
            {
                dbPeriod.deleteRecord(id, time);
                dbStorage.deleteRecord(bsID);
                dbType.deleteRecord(btId);
                dbStation.deleteRecord(sID);
            }
        }
    }
}
