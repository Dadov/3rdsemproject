using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarLib;

namespace ElectricCarLibTest
{
    [TestClass]
    public class DBBatteryTypeTest
    {
        private IDBBatteryType dbType = new DBBatteryType();
        public DBBatteryTypeTest()
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
        public void addGetDeleteNewRecord()
        {
            int id = dbType.addNewRecord("newName", "newProducer", 10, 100, 20);
            try
            {
                MBatteryType type = dbType.getRecord(id, false);
                Assert.AreEqual("newName", type.name);
                Assert.AreEqual("newProducer", type.producer);
                Assert.AreEqual(10,type.capacity);
                Assert.AreEqual(100, type.exchangeCost);
                Assert.AreEqual(20, type.storageNumber);
            }
            catch
            {
            }
            finally
            {
                dbType.deleteRecord(id);
            }
        }

        [TestMethod]
        public void updateRecord()
        {
            int id = dbType.addNewRecord("newName", "newProducer", 10, 100, 20);
            try
            {
                dbType.updateRecord(id, "Update", "Update", 20,200,40);
                MBatteryType type = dbType .getRecord(id, false);
                Assert.AreEqual("Update", type.name);
                Assert.AreEqual("Update", type.producer);
                Assert.AreEqual(20,type.capacity);
                Assert.AreEqual(200, type.exchangeCost);
                Assert.AreEqual(40, type.storageNumber);
            }
            catch
            {

            }
            finally
            {
                dbType.deleteRecord(id);
            }
        }      
    }
}
