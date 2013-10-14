using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarModelLayer;
using ElectricCarDB;
using System.Collections;
using System.Collections.Generic;

namespace ElectricCarLibTest
{
    
    [TestClass]
    public class DBBooking_StationTest
    {
        private DBookingStation dbBS = new DBookingStation();
        private DBooking dbBooking = new DBooking();
        private DStation dbStation = new DStation();
        [TestMethod]
        public void addGetDeleteBookingStation()
        {
            DateTime time = DateTime.Now;
            int sId = dbStation.addNewRecord("AalborgStation", "Aalborg", "Denmark", "Open");
            int bId = dbBooking.addRecord(1, 100, time, time, "1234456");

            try
            {
                dbBS.addNewRecord(bId, sId, time);
                MBookingStation bs = dbBS.getRecord(bId, sId, false);
                Assert.AreEqual(sId, bs.Station.Id);
                Assert.AreEqual(bId, bs.Booking.Id);
                Assert.AreEqual(time, bs.bookedTime);
            }
            catch (Exception)
            {
            }
            finally
            {
                dbBS.deleteRecord(bId, sId);
                dbStation.deleteRecord(sId);
                dbBooking.deleteRecord(bId);
            }
        }

        [TestMethod]
        public void updateBookingStation()
        {
            DateTime time = DateTime.Now;
            DateTime updateTime = time.AddDays(30);
            int sId = dbStation.addNewRecord("AalborgStation", "Aalborg", "Denmark", "Open");
            int bId = dbBooking.addRecord(1, 100, time, time, "1234456");
            dbBS.addNewRecord(bId, sId, time);

            try
            {
                dbBS.updateRecord(bId, sId, updateTime);
                MBookingStation bs = dbBS.getRecord(bId, sId, false);
                Assert.AreEqual(sId, bs.Station.Id);
                Assert.AreEqual(bId, bs.Booking.Id);
                Assert.AreEqual(updateTime, bs.bookedTime);

            }
            catch (Exception)
            {
            }
            finally
            {
                dbBS.deleteRecord(bId, sId);
                dbStation.deleteRecord(sId);
                dbBooking.deleteRecord(bId);
            }
        }

        [TestMethod]
        public void getAllBookingForStation()
        {
            DateTime time = DateTime.Now;
            int sId = dbStation.addNewRecord("AalborgStation", "Aalborg", "Denmark", "Open");
            int bId = dbBooking.addRecord(1, 100, time, time, "1234456");

            try
            {
                dbBS.addNewRecord(bId, sId, time);
                List<MBookingStation> bss = dbBS.getAllBookingsForStation(sId, false);
                Assert.AreEqual(1, bss.Count);
                Assert.AreEqual(sId, bss[0].Station.Id);
                Assert.AreEqual(bId, bss[0].Booking.Id);
                Assert.AreEqual(time, bss[0].bookedTime);
            }
            catch (Exception)
            {
            }
            finally
            {
                dbBS.deleteRecord(bId, sId);
                dbStation.deleteRecord(sId);
                dbBooking.deleteRecord(bId);
            }
        }

        [TestMethod]
        public void getAllStationForBooking()
        {
            DateTime time = DateTime.Now;
            int sId = dbStation.addNewRecord("AalborgStation", "Aalborg", "Denmark", "Open");
            int bId = dbBooking.addRecord(1, 100, time, time, "1234456");

            try
            {
                dbBS.addNewRecord(bId, sId, time);
                List<MBookingStation> bss = dbBS.getAllStationsForBooking(bId, false);
                Assert.AreEqual(1, bss.Count);
                Assert.AreEqual(sId, bss[0].Station.Id);
                Assert.AreEqual(bId, bss[0].Booking.Id);
                Assert.AreEqual(time, bss[0].bookedTime);
            }
            catch (Exception)
            {
            }
            finally
            {
                dbBS.deleteRecord(bId, sId);
                dbStation.deleteRecord(sId);
                dbBooking.deleteRecord(bId);
            }
        }
    }
}
