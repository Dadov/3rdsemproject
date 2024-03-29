﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarModelLayer;
using ElectricCarDB;

namespace ElectricCarLibTest
{
    [TestClass]
    public class DBBatteryTest
    {
        private IDBattery dbBattery = new DBattery();
        private IDBatteryType dbType = new DBatteryType();
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
        public void addGetDeleteBattery()
        {
            int btId = dbType.addNewRecord("newName", "newProducer", 10, 100);
            int id = dbBattery.addNewRecord("newState", btId);
            try
            {
                MBattery battery = dbBattery.getRecord(id, false);
                Assert.AreEqual("newState",battery.state);
            }
            finally
            {
                dbBattery.deleteRecord(id);
                dbType.deleteRecord(btId);
                
            }
        }

        [TestMethod]
        public void updateBattery()
        {
            int btId = dbType.addNewRecord("newName", "newProducer", 10, 100);
            int btId2 = dbType.addNewRecord("Update", "Update", 20, 200);
            int id = dbBattery.addNewRecord("newState", btId);
            try
            {
                dbBattery.updateRecord(id, "Update", btId2);
                MBattery battery = dbBattery.getRecord(id, false);
                Assert.AreEqual("Update", battery.state);
            }
            catch
            {

            }
            finally
            {
                dbBattery.deleteRecord(id);
                dbType.deleteRecord(btId);
                dbType.deleteRecord(btId2);
            }
        }      
    }
}
