using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarModelLayer;
using ElectricCarDB;
using System.Collections;
using System.Collections.Generic;

namespace ElectricCarLibTest
{
    [TestClass]
    public class DBBookingLineTest
    {
        private DBooking dbBooking = new DBooking();
        private DBookingLine dbBL = new DBookingLine();
        private DBatteryType dbBT = new DBatteryType();
        private DStation dbStation = new DStation();
        [TestMethod]
        public void addGetDeleteBookingLine()
        {
            DateTime createTime = DateTime.Now;
            DateTime trip = createTime.AddDays(60);
            DateTime sTime = createTime.AddDays(60);
            int bId = dbBooking.addRecord(3, 100, createTime, trip, "Payed");
            int btId = dbBT.addNewRecord("AAA", "BBB", 12, 100);
            int sId = dbStation.addNewRecord("AStation", "Aalborg", "Denmark", "Open");
            try
            {
                dbBL.addRecord(bId, btId, sId, 2, 40, sTime);
                MBookingLine bl = dbBL.getRecord(bId, btId, sId, true);
                Assert.AreEqual(btId, bl.BatteryType.id);
                Assert.AreEqual("AAA", bl.BatteryType.name);
                Assert.AreEqual("BBB", bl.BatteryType.producer);
                Assert.AreEqual(12, Convert.ToInt32(bl.BatteryType.capacity));
                Assert.AreEqual(100, Convert.ToInt32(bl.BatteryType.exchangeCost));
                Assert.AreEqual(sId, bl.Station.Id);
                Assert.AreEqual("AStation", bl.Station.name);
                Assert.AreEqual("Aalborg", bl.Station.address);
                Assert.AreEqual("Denmark", bl.Station.country);
                Assert.AreEqual(State.Open, bl.Station.state);
                Assert.AreEqual(4, bl.quantity);
                Assert.AreEqual(80, Convert.ToInt32(bl.price));
                Assert.AreEqual(2, bl.quantity);
                Assert.AreEqual(40, Convert.ToInt32(bl.price));
                Assert.AreEqual(sTime, bl.time);
            }
            catch (Exception)
            {
            }
            finally
            {
                dbBL.deleteRecord(bId, btId, sId);
                dbBooking.deleteRecord(bId);
                dbBT.deleteRecord(btId);
                dbStation.deleteRecord(sId);
            }
        }

        [TestMethod]
        public void updateBookingLine()
        {
            DateTime createTime = DateTime.Now;
            DateTime trip = createTime.AddDays(60);
            DateTime sTime = createTime.AddDays(60);
            DateTime sTime2 = createTime.AddDays(70);
            int bId = dbBooking.addRecord(1, 100, createTime, trip, "1234456");
            int btId = dbBT.addNewRecord("AAA", "BBB", 12, 100);
            int sId = dbStation.addNewRecord("BStation", "Aalborg", "Denmark", "Open");
            try
            {
                dbBL.addRecord(bId, btId, sId, 2, 40, sTime);
                dbBL.updateRecord(bId, btId, sId, 4, 80, sTime2);
                MBookingLine bl = dbBL.getRecord(bId, btId, sId, false);
                Assert.AreEqual(bId, bl.Station.Id);
                Assert.AreEqual(btId, bl.BatteryType.id);
                Assert.AreEqual(sId, bl.Station.Id);
                Assert.AreEqual(4, bl.quantity);
                Assert.AreEqual(80, Convert.ToInt32(bl.price));
                Assert.AreEqual(sTime2, bl.time);

            }
            catch (Exception)
            {
            }
            finally
            {
                dbBL.deleteRecord(bId, btId, sId);
                dbBooking.deleteRecord(bId);
                dbBT.deleteRecord(btId);
                dbStation.deleteRecord(sId);
            }
        }

        [TestMethod]
        public void getBookingLinesForBookingTest()
        {
            DateTime createTime = DateTime.Now;
            DateTime trip = createTime.AddDays(60);
            DateTime sTime = createTime.AddDays(60);
            int bId = dbBooking.addRecord(1, 100, createTime, trip, "1234456");
            int btId = dbBT.addNewRecord("AAA", "BBB", 12, 100);
            int sId = dbStation.addNewRecord("CStation", "Aalborg", "Denmark", "Open");
            try
            {
                dbBL.addRecord(bId, btId, sId, 2, 40, sTime);
                List<MBookingLine> bls = dbBL.getBookingLinesForBooking(bId, true);
                Assert.AreEqual(1, bls.Count);
                Assert.AreEqual(btId, bls[0].BatteryType.id);
                Assert.AreEqual("AAA", bls[0].BatteryType.name);
                Assert.AreEqual("BBB", bls[0].BatteryType.producer);
                Assert.AreEqual(12, Convert.ToInt32(bls[0].BatteryType.capacity));
                Assert.AreEqual(100, Convert.ToInt32(bls[0].BatteryType.exchangeCost));
                Assert.AreEqual(sId, bls[0].Station.Id);
                Assert.AreEqual("CStation", bls[0].Station.name);
                Assert.AreEqual("Aalborg", bls[0].Station.address);
                Assert.AreEqual("Denmark", bls[0].Station.country);
                Assert.AreEqual(State.Open, bls[0].Station.state);
                Assert.AreEqual(4, bls[0].quantity);
                Assert.AreEqual(80, Convert.ToInt32(bls[0].price));
                Assert.AreEqual(sTime, bls[0].time);
            }
            catch (Exception)
            {
            }
            finally
            {
                dbBL.deleteRecord(bId, btId, sId);
                dbBooking.deleteRecord(bId);
                dbBT.deleteRecord(btId);
                dbStation.deleteRecord(sId);
            }
        }

        [TestMethod]
        public void deleteAllBookingLineForBookingTest()
        {
            DateTime createTime = DateTime.Now;
            DateTime trip = createTime.AddDays(60);
            DateTime sTime = createTime.AddDays(60);
            int bId = dbBooking.addRecord(1, 100, createTime, trip, "1234456");
            int btId = dbBT.addNewRecord("AAA", "BBB", 12, 100);
            int sId = dbStation.addNewRecord("DStation", "Aalborg", "Denmark", "Open");
            try
            {
                dbBL.addRecord(bId, btId, sId, 2, 40, sTime);
                dbBL.deleteAllBookingLineForBooking(bId);
                List<MBookingLine> bls = dbBL.getBookingLinesForBooking(bId, false);
                Assert.AreEqual(0, bls.Count);
                
            }
            catch (Exception)
            {
            }
            finally
            {
                
                dbBooking.deleteRecord(bId);
                dbBT.deleteRecord(btId);
                dbStation.deleteRecord(sId);
            }
        }

        [TestMethod]
        public void updateAllBookingLineForBookingTest()
        {
            DateTime createTime = DateTime.Now;
            DateTime trip = createTime.AddDays(60);
            DateTime sTime = createTime.AddDays(60);
            int bId = dbBooking.addRecord(1, 100, createTime, trip, "1234456");
            int btId = dbBT.addNewRecord("AAA", "BBB", 12, 100);
            int sId = dbStation.addNewRecord("AalborgStation", "Aalborg", "Denmark", "Open");
            try
            {
                dbBL.addRecord(bId, btId, sId, 2, 40, sTime);
                List<MBookingLine> b_ls = new List<MBookingLine>();
                b_ls.Add(new MBookingLine()
                {
                    
                    BatteryType = new MBatteryType() {id = btId},
                    Station = new MStation(){Id = sId},
                    price = 80,
                    quantity = 4,
                    time = sTime
                });
                dbBL.updateAllBookingLineForBooking(bId, b_ls);
                List<MBookingLine> bls = dbBL.getBookingLinesForBooking(bId, false);
                Assert.AreEqual(1, bls.Count);
                Assert.AreEqual(btId, bls[0].BatteryType.id);
                Assert.AreEqual(sId, bls[0].Station.Id);
                Assert.AreEqual(4, bls[0].quantity);
                Assert.AreEqual(80, Convert.ToInt32(bls[0].price));
                Assert.AreEqual(sTime, bls[0].time);
            }
            catch (Exception)
            {
            }
            finally
            {
                dbBL.deleteRecord(bId, btId, sId);
                dbBooking.deleteRecord(bId);
                dbBT.deleteRecord(btId);
                dbStation.deleteRecord(sId);
            }
        }
    }
}
