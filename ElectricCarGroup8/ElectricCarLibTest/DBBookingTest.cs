using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ElectricCarModelLayer;
using ElectricCarDB;

namespace ElectricCarLibTest
{
    /// <summary>
    /// Summary description for DBBooking
    /// </summary>
    [TestClass]
    public class DBBookingTest
    {
        public DBBookingTest()
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

        private IDBooking dbBooking = new DBooking();
        private IDStation dbStation = new DStation();
        private IDBatteryType dbBT = new DBatteryType();
        private IDBBatteryStorage dbBS = new DBBatteryStorage();
        private DCustomer dbCust = new DCustomer();
        private DDiscountGroup dbDG = new DDiscountGroup();
        [TestMethod]
        public void AddGetDeleteBooking()
        {
            DateTime createTime = DateTime.Now;
            DateTime trip = createTime.AddDays(60);
            int dgId = dbDG.addNewRecord("Regular", 0);
            MDiscountGroup dg = dbDG.getRecord(dgId, false);
            int cId = dbCust.addNewRecord("May", "Smith", "Denmark", "Denmark", "12345678", "h@hotmail.com", dg, "Payed");

            int bookingId = dbBooking.addRecord(cId, 100, createTime, trip, "Payed");
            try
            {
                MBooking booking = dbBooking.getRecord(bookingId, false);
                Assert.AreEqual(1, booking.cId);
                Assert.AreEqual(100, booking.totalPrice);
                Assert.AreEqual(createTime, booking.createDate);
                Assert.AreEqual(trip, booking.tripStart);
                Assert.AreEqual("1234456", booking.creaditCard);


            }
            catch (Exception)
            {

            }
            finally
            {
                dbBooking.deleteRecord(bookingId);
                dbCust.deleteRecord(cId);
                dbDG.deleteRecord(dgId);
            }  

        }
        [TestMethod]
        public void updateBooking()
        {
            DateTime createTime = DateTime.Today;
            DateTime trip = createTime.AddDays(60);
            DateTime updateCreateTime = createTime.AddDays(10);
            DateTime updateTrip = createTime.AddDays(40);
            int dgId = dbDG.addNewRecord("Regular", 0);
            MDiscountGroup dg = dbDG.getRecord(dgId, false);
            int cId = dbCust.addNewRecord("May", "Smith", "Denmark", "Denmark", "12345678", "h@hotmail.com", dg, "Payed");

            int bookingId = dbBooking.addRecord(cId, 100, createTime, trip, "1234456");
            try
            {
                dbBooking.updateRecord(bookingId, 2, 200, updateCreateTime, updateTrip, "654321");
                MBooking booking = dbBooking.getRecord(bookingId, false);
                Assert.AreEqual(2, booking.cId);
                Assert.AreEqual(200, booking.totalPrice);
                Assert.AreEqual(updateCreateTime, booking.createDate);
                Assert.AreEqual(updateTrip, booking.tripStart);
                Assert.AreEqual("654321", booking.creaditCard);
            }
            catch (Exception)
            {

            }
            finally
            {
                dbBooking.deleteRecord(bookingId);
                dbCust.deleteRecord(cId);
                dbDG.deleteRecord(dgId);
            }  

        }
    }
}
