using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
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
        private IDBBatteryStorage dbBS = new DBBatteryStorage();
        private IDBatteryType dbBT = new DBatteryType();

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
            int id1 = dbStation.addNewRecord("BoholmStation", "Boholm", "Denmark", "Open");
            int id2 = dbStation.addNewRecord("AalborgStation", "Aalborg", "Denmark", "Open");
            //int id3 = dbBT.addNewRecord("SmallBattery", "TODB", 8, 30, 1);
            
            dbConnection.addNewRecord(id1, id2, 300, 7);
            //int id4 = dbBS.addNewRecord(id3, id1);

            try
            {
                MStation station = dbStation.getRecord(id1, true);
                Dictionary<MStation, decimal> adj = station.naboStations;
                ICollection<MStation> naborStations =  (ICollection<MStation>)(adj.Keys);
                IEnumerator ei = naborStations.GetEnumerator();
                MStation naborStation = new MStation();
                if (ei.MoveNext())
	            {
		            naborStation = (MStation)ei.Current;
	            }
                Assert.AreEqual("BoholmStation", station.name);
                Assert.AreEqual("Boholm", station.address);
                Assert.AreEqual("Denmark", station.country);
                Assert.AreEqual("Open", station.state.ToString());

                Assert.AreEqual("AalborgStation", naborStation.name);
                Assert.AreEqual("Aalborg", naborStation.address);
                Assert.AreEqual("Denmark", naborStation.country);
                Assert.AreEqual("Open", naborStation.state.ToString());

                //Assert.AreEqual(id3, station.storages[0].type.id);
                //Assert.AreEqual("SmallBattery", station.storages[0].type.name);
                //Assert.AreEqual("TODB", station.storages[0].type.producer);
                //Assert.AreEqual(8, Convert.ToInt32(station.storages[0].type.capacity));
                //Assert.AreEqual(30, Convert.ToInt32(station.storages[0].type.exchangeCost));
                //Assert.AreEqual(1, Convert.ToInt32(station.storages[0].type.storageNumber));
            }
            catch
            {
            }
            finally
            {
                //dbBS.deleteRecord(id4);
                //dbBT.deleteRecord(id3);
                dbConnection.deleteRecord(id1, id2);
                dbStation.deleteRecord(id2);
                dbStation.deleteRecord(id1);
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
                LinkedList<MStation> stations = dbStation.getNaborStationsWithoutDriveHour(id1);
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
