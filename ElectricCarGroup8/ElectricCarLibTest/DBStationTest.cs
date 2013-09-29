using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarLib;

namespace ElectricCarLibTest
{
    /// <summary>
    /// Summary description for DBStationTest
    /// </summary>
    [TestClass]
    public class DBStationTest
    {
        private DBStation dbStation = new DBStation();
        public DBStationTest()
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
            int id = dbStation.addNewRecord("BoholmStation", "Boholm", "Denmark", "Open");
            MStation station = dbStation.getRecord(id, false);
            Assert.AreEqual("BoholmStation", station.name);
            Assert.AreEqual("Boholm", station.address);
            Assert.AreEqual("Denmark", station.country);
            Assert.AreEqual("Open", station.state.ToString());
            dbStation.deleteRecord(id);
        }

        [TestMethod]
        public void updateRecord()
        {
            int id = dbStation.addNewRecord("BoholmStation", "Boholm", "Denmark", "Open");
            dbStation.updateRecord(id, "Update", "Update", "Update", "Close");
            MStation station = dbStation.getRecord(id, false);
            Assert.AreEqual("Update", station.name);
            Assert.AreEqual("Update", station.address);
            Assert.AreEqual("Update", station.country);
            Assert.AreEqual("Close", station.state.ToString());
            dbStation.deleteRecord(id);
        }
    }
}
