﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarLib;

namespace ElectricCarLibTest
{
    class DBBatteryTest
    {
        private DBBattery dbBattery = new DBBattery();
        private IDBBatteryType dbType = new DBBatteryType();
        public DBBatteryTest()
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
            int btId = dbType.addNewRecord("newName", "newProducer", 10, 100, 20);
            int id = dbBattery.addNewRecord("newState", btId);
            try
            {
                MBattery battery = dbBattery.getRecord(id, false);
                Assert.AreEqual("newState",battery.state);
                Assert.AreEqual(btId, battery.batteryType.id);
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
            int btId = dbType.addNewRecord("newName", "newProducer", 10, 100, 20);
            int btId2 = dbType.addNewRecord("Update", "Update", 20, 200, 40);
            int id = dbBattery.addNewRecord("newState", btId);
            try
            {
                dbBattery.updateRecord(id, "Update", btId2);
                MBattery battery = dbBattery.getRecord(id, false);
                Assert.AreEqual("Update", battery.state);
                Assert.AreEqual(btId2, battery.batteryType.id);
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