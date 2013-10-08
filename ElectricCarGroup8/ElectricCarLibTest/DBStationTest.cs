using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ElectricCarModelLayer;
using ElectricCarDB;

namespace ElectricCarLibTest
{
    /// <summary>
    /// Summary description for DBStationTest
    /// </summary>
    [TestClass]
    public class DBStationTest
    {
        private DStation dbStation = new DStation();
        private IDConnection dbConnection = new DConnection();
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
        public void addGetDeleteStation()
        {
            int id = dbStation.addNewRecord("BoholmStation", "Boholm", "Denmark", "Open");
            try
            {
                MStation station = dbStation.getRecord(id, false);
                Assert.AreEqual("BoholmStation", station.name);
                Assert.AreEqual("Boholm", station.address);
                Assert.AreEqual("Denmark", station.country);
                Assert.AreEqual("Open", station.state.ToString());
            }
            catch
            {
            }
            finally
            {
                dbStation.deleteRecord(id);
            }
        }

        [TestMethod]
        public void updateStation()
        {
            int id = dbStation.addNewRecord("BoholmStation", "Boholm", "Denmark", "Open");
            try
            {
                dbStation.updateRecord(id, "Update", "Update", "Update", "Close");
                MStation station = dbStation.getRecord(id, false);
                Assert.AreEqual("Update", station.name);
                Assert.AreEqual("Update", station.address);
                Assert.AreEqual("Update", station.country);
                Assert.AreEqual("Close", station.state.ToString());
            }
            catch
            {

            }
            finally
            {
                dbStation.deleteRecord(id);
            }
        }

        [TestMethod]
        public void getNaborStations()
        {
            int id1 = dbStation.addNewRecord("BoholmStation", "Boholm", "Denmark", "Open");
            int id2 = dbStation.addNewRecord("nabor1", "Aarhus", "Denmark", "Close");
            int id3 = dbStation.addNewRecord("nabor2", "Aalborg", "Denmark", "Open");
            dbConnection.addNewRecord(id1, id2, 200, 2);
            dbConnection.addNewRecord(id1, id3, 300, 3);
            try
            {
                LinkedList<MStation> stations = dbStation.getNaborStations(id1);
                Assert.AreEqual(3, stations.Count);
                MStation startStation = new MStation();
                MStation naborStationId2 = new MStation();
                MStation naborStationId3 = new MStation();
                foreach (MStation c in stations)
                {
                    if (c.Id == id1)
                    {
                        startStation = c;
                    }
                    else if (c.Id == id2)
                    {
                        naborStationId2 = c;
                    }
                    else if (true)
                    {
                        naborStationId3 = c;
                    }
                }
                Assert.AreEqual(id1, startStation.Id);

                Assert.AreEqual(id2, naborStationId2.Id);
                Assert.AreEqual("nabor1", naborStationId2.name);
                Assert.AreEqual("Aarhus", naborStationId2.address);
                Assert.AreEqual("Denmark", naborStationId2.country);
                Assert.AreEqual("Close", naborStationId2.state.ToString());

                Assert.AreEqual(id3, naborStationId3.Id);
                Assert.AreEqual("nabor2", naborStationId2.name);
                Assert.AreEqual("Aalborg", naborStationId2.address);
                Assert.AreEqual("Denmark", naborStationId2.country);
                Assert.AreEqual("Open", naborStationId2.state.ToString());
            }
            catch
            {

            }
            finally
            {

                dbConnection.deleteRecord(id1, id2);
                dbConnection.deleteRecord(id1, id3);
                dbStation.deleteRecord(id1);
                dbStation.deleteRecord(id2);
                dbStation.deleteRecord(id3);
            }
        
        }
    
            
    }
}
