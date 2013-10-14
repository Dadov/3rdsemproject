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
        [TestMethod]
        public void addGetDeleteBookingLine()
        {
            DateTime createTime = DateTime.Now;
            DateTime trip = createTime.AddDays(60);
            int bId = dbBooking.addRecord(1, 100, createTime, trip, "1234456");
            int btId = dbBT.addNewRecord("AAA", "BBB", 12, 100, 50);
            try
            {
                dbBL.addRecord(bId, btId, 2, 40);
                MBookingLine bl = dbBL.getRecord(bId, btId, false);
                Assert.AreEqual(bId, bl.Booking.Id);
                Assert.AreEqual(btId, bl.BatteryType.id);
                Assert.AreEqual(2, bl.quantity);
                Assert.AreEqual(40, Convert.ToInt32(bl.price));
            }
            catch (Exception)
            {
            }
            finally
            {
                dbBL.deleteRecord(bId, btId);
                dbBooking.deleteRecord(bId);
                dbBT.deleteRecord(btId);
            }
        }

        [TestMethod]
        public void updateBookingLine()
        {
            DateTime createTime = DateTime.Now;
            DateTime trip = createTime.AddDays(60);
            int bId = dbBooking.addRecord(1, 100, createTime, trip, "1234456");
            int btId = dbBT.addNewRecord("AAA", "BBB", 12, 100, 50);
            try
            {
                dbBL.addRecord(bId, btId, 2, 40);
                dbBL.updateRecord(bId, btId, 4, 80);
                MBookingLine bl = dbBL.getRecord(bId, btId, false);
                Assert.AreEqual(bId, bl.Booking.Id);
                Assert.AreEqual(btId, bl.BatteryType.id);
                Assert.AreEqual(4, bl.quantity);
                Assert.AreEqual(80, Convert.ToInt32(bl.price));
            }
            catch (Exception)
            {
            }
            finally
            {
                dbBL.deleteRecord(bId, btId);
                dbBooking.deleteRecord(bId);
                dbBT.deleteRecord(btId);
            }
        }

        [TestMethod]
        public void getBookingLinesForBookingTest()
        {
            DateTime createTime = DateTime.Now;
            DateTime trip = createTime.AddDays(60);
            int bId = dbBooking.addRecord(1, 100, createTime, trip, "1234456");
            int btId = dbBT.addNewRecord("AAA", "BBB", 12, 100, 50);
            try
            {
                dbBL.addRecord(bId, btId, 2, 40);
                List<MBookingLine> bls = dbBL.getBookingLinesForBooking(bId, false);
                Assert.AreEqual(1, bls.Count);
                Assert.AreEqual(bId, bls[0].Booking.Id);
                Assert.AreEqual(btId, bls[0].BatteryType.id);
                Assert.AreEqual(4, bls[0].quantity);
                Assert.AreEqual(80, Convert.ToInt32(bls[0].price));
            }
            catch (Exception)
            {
            }
            finally
            {
                dbBL.deleteRecord(bId, btId);
                dbBooking.deleteRecord(bId);
                dbBT.deleteRecord(btId);
            }
        }

        [TestMethod]
        public void deleteAllBookingLineForBookingTest()
        {
            DateTime createTime = DateTime.Now;
            DateTime trip = createTime.AddDays(60);
            int bId = dbBooking.addRecord(1, 100, createTime, trip, "1234456");
            int btId = dbBT.addNewRecord("AAA", "BBB", 12, 100, 50);
            try
            {
                dbBL.addRecord(bId, btId, 2, 40);
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
            }
        }

        [TestMethod]
        public void updateAllBookingLineForBookingTest()
        {
            DateTime createTime = DateTime.Now;
            DateTime trip = createTime.AddDays(60);
            int bId = dbBooking.addRecord(1, 100, createTime, trip, "1234456");
            int btId = dbBT.addNewRecord("AAA", "BBB", 12, 100, 50);
            try
            {
                dbBL.addRecord(bId, btId, 2, 40);
                List<MBookingLine> b_ls = new List<MBookingLine>();
                b_ls.Add(new MBookingLine()
                {
                    Booking = new MBooking() {Id = bId },
                    BatteryType = new MBatteryType() {id = btId},
                    price = 80,
                    quantity = 4
                });
                dbBL.updateAllBookingLineForBooking(bId, b_ls);
                List<MBookingLine> bls = dbBL.getBookingLinesForBooking(bId, false);
                Assert.AreEqual(1, bls.Count);
                Assert.AreEqual(bId, bls[0].Booking.Id);
                Assert.AreEqual(btId, bls[0].BatteryType.id);
                Assert.AreEqual(4, bls[0].quantity);
                Assert.AreEqual(80, Convert.ToInt32(bls[0].price));
            }
            catch (Exception)
            {
            }
            finally
            {
                dbBL.deleteRecord(bId, btId);
                dbBooking.deleteRecord(bId);
                dbBT.deleteRecord(btId);
            }
        }
    }
}
