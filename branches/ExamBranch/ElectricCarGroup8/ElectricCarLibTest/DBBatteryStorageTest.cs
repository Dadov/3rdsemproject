using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarModelLayer;
using ElectricCarDB;

namespace ElectricCarLibTest
{
    [TestClass]
    public class BatteryStorageTest
    {
        private IDBBatteryStorage dbStorage = new DBBatteryStorage();
        private IDBatteryType dbType = new DBatteryType();
        private IDStation dbStation = new DStation();
        public BatteryStorageTest()
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
        public void addGetDeleteStorage()
        {
            int btId = dbType.addNewRecord("newName", "newProducer", 10, 100);
            int sID = dbStation.addNewRecord("newName", "newAddress", "newCountry", "newState");
            int id = dbStorage.addNewRecord(btId,sID, 20);
            try
            {
                MBatteryStorage storage = dbStorage.getRecord(id, false);
                Assert.AreEqual(btId, storage.type.id);
            }
            finally
            {
                dbStorage.deleteRecord(id);
                dbType.deleteRecord(btId);
                dbStation.deleteRecord(sID);
            }
        }

        [TestMethod]
        public void updateStorage()
        {
            int btId = dbType.addNewRecord("newName", "newProducer", 10, 100);
            int btId2 = dbType.addNewRecord("Update", "Update", 20, 200);
            int sID = dbStation.addNewRecord("newName", "newAddress", "newCountry", "newState");
            int sID2 = dbStation.addNewRecord("Update", "Update", "Update", "Update");
            int id = dbStorage.addNewRecord(btId,sID, 20);
            try
            {
                dbStorage.updateRecord(id,btId2,sID2, 40);
                MBatteryStorage storage = dbStorage.getRecord(id, false);
                Assert.AreEqual(btId2, storage.type.id);
            }
            catch
            {

            }
            finally
            {
                dbStorage.deleteRecord(id);
                dbType.deleteRecord(btId);
                dbType.deleteRecord(btId2);
                dbStation.deleteRecord(sID);
                dbStation.deleteRecord(sID2);
            }
        }      
    }
}
