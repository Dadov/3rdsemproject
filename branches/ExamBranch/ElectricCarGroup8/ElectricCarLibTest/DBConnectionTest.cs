using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarDB;
using ElectricCarModelLayer;

namespace ElectricCarLibTest
{
    [TestClass]
    public class DBConnectionTest
    {
        private DStation dbStation = new DStation();
        private DConnection dbConnection = new DConnection();
        [TestMethod]
        public void addGetDeleteConnection()
        {
            int id1 = dbStation.addNewRecord("AalborgStation", "Aalborg", "Denmark", "Open");
            int id2 = dbStation.addNewRecord("BoholmStation", "Boholm", "Denmark", "Open");
            try
            {
                dbConnection.addNewRecord(id1, id2, 300, 7);
                MConnection connection = dbConnection.getRecord(id1, id2, true);
                Assert.AreEqual(id1, connection.Station1.Id);
                Assert.AreEqual("AalborgStation", connection.Station1.name);
                Assert.AreEqual("Aalborg", connection.Station1.address);
                Assert.AreEqual("Denmark", connection.Station1.country);
                Assert.AreEqual(State.Open, connection.Station1.state);

                Assert.AreEqual(id2, connection.Station2.Id);
                Assert.AreEqual("BoholmStation", connection.Station2.name);
                Assert.AreEqual("Boholm", connection.Station2.address);
                Assert.AreEqual("Denmark", connection.Station2.country);
                Assert.AreEqual(State.Open, connection.Station2.state);

                Assert.AreEqual(300, Convert.ToInt32(connection.distance));
                Assert.AreEqual(7, Convert.ToInt32(connection.driveHour));
            }
            catch (Exception)
            {
            }
            finally
            {
                dbConnection.deleteRecord(id1, id2);
                dbStation.deleteRecord(id1);
                dbStation.deleteRecord(id2);
            }
            
        }
        [TestMethod]
        public void updateConnection()
        {
            int id1 = dbStation.addNewRecord("AalborgStation", "Aalborg", "Denmark", "Open");
            int id2 = dbStation.addNewRecord("BoholmStation", "Boholm", "Denmark", "Open");
            try
            {
                dbConnection.addNewRecord(id1, id2, 300, 7);
                dbConnection.updateRecord(id1, id2, 200, 6);
                MConnection connection = dbConnection.getRecord(id1, id2, true);
                Assert.AreEqual(id1, connection.Station1.Id);
                Assert.AreEqual("AalborgStation", connection.Station1.name);
                Assert.AreEqual("Aalborg", connection.Station1.address);
                Assert.AreEqual("Denmark", connection.Station1.country);
                Assert.AreEqual(State.Open, connection.Station1.state);

                Assert.AreEqual(id2, connection.Station2.Id);
                Assert.AreEqual("BoholmStation", connection.Station2.name);
                Assert.AreEqual("Boholm", connection.Station2.address);
                Assert.AreEqual("Denmark", connection.Station2.country);
                Assert.AreEqual(State.Open, connection.Station2.state);

                Assert.AreEqual(200, Convert.ToInt32(connection.distance));
                Assert.AreEqual(6, Convert.ToInt32(connection.driveHour));
            }
            catch (Exception)
            {
            }
            finally
            {
                dbConnection.deleteRecord(id1, id2);
                dbStation.deleteRecord(id1);
                dbStation.deleteRecord(id2);
            }
        }
    }
}
