using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarModelLayer;
using ElectricCarDB;
using ElectricCarLib;

namespace ElectricCarLibTest
{
    [TestClass]
    public class PeriodCalculatorTest
    {
        private IDBatteryType dbType = new DBatteryType();
        private IDStation dbStation = new DStation();
        private IDBBatteryStorage dbStorage = new DBBatteryStorage();
        private IDPeriod dbPeriod = new DBPeriod();
        private IDBattery dbBattery = new DBattery();
        private MPeriod period = new MPeriod();
        private MBatteryStorage storage = new MBatteryStorage();
        private PeriodCalculator pCalc = new PeriodCalculator();

        public PeriodCalculatorTest()
        {
            
        }
        [TestMethod]
        public void TestGetTime()
        {
            int btId = dbType.addNewRecord("newName", "newProducer", 10, 100); //capacity equals 10 hours
            int sID = dbStation.addNewRecord("newName", "newAddress", "newCountry", "newState");
            int bsID = dbStorage.addNewRecord(btId, sID, 20);
            dbPeriod.addNewRecord(bsID, DateTime.Today, 10, 5); // initial =10 custom = 5 future = 1
            period = dbPeriod.getRecord(bsID, DateTime.Today, true);
            storage = dbStorage.getRecord(bsID, true);
            try
            {
                DateTime newTime = pCalc.getTime(storage);
                Assert.AreEqual(newTime, DateTime.Today.AddHours(10));
            }
            finally
            {
                dbPeriod.deleteRecord(bsID, DateTime.Today);
                dbStorage.deleteRecord(bsID);
                dbStation.deleteRecord(sID);
                dbType.deleteRecord(btId);
            }
            }
        [TestMethod]
        public void TestGetInitNumber()
        {
            int btId = dbType.addNewRecord("newName", "newProducer", 10, 100); //capacity equals 10 hours
            int sID = dbStation.addNewRecord("newName", "newAddress", "newCountry", "newState");
            int bsID = dbStorage.addNewRecord(btId, sID, 20);
            dbPeriod.addNewRecord(bsID, DateTime.Today, 10, 5); // initial =10 custom = 5 future = 1
            period = dbPeriod.getRecord(bsID, DateTime.Today, true);
            storage = dbStorage.getRecord(bsID, true);
            try
            {
                int init = pCalc.getInitNumber(storage);
                Assert.AreEqual(init, 20);
            }
            finally
            {
                dbPeriod.deleteRecord(bsID, DateTime.Today);
                dbStorage.deleteRecord(bsID);
                dbStation.deleteRecord(sID);
                dbType.deleteRecord(btId);
            }
        }
        [TestMethod]
        public void TestGetPeriod()
        {
            int btId = dbType.addNewRecord("newName", "newProducer", 10, 100); //capacity equals 10 hours
            int sID = dbStation.addNewRecord("newName", "newAddress", "newCountry", "newState");
            int bsID = dbStorage.addNewRecord(btId, sID, 20);
            dbPeriod.addNewRecord(bsID, DateTime.Today, 10, 5); // initial =10 custom = 5 future = 1
            period = dbPeriod.getRecord(bsID, DateTime.Today, true);
            storage = dbStorage.getRecord(bsID, true);
            try
            {
                MPeriod firstPeriod = pCalc.createPeriod(storage);
                storage = dbStorage.getRecord(storage.id, true);
                MPeriod secondPeriod = pCalc.createPeriod(storage);
                Assert.AreEqual(DateTime.Today.AddHours(20), secondPeriod.time);
                Assert.AreEqual(20, secondPeriod.initBatteryNumber);
                Assert.AreEqual(0, firstPeriod.bookedBatteryNumber);
            }
            finally
            {
                dbPeriod.deleteRecord(bsID, DateTime.Today);
                dbPeriod.deleteRecord(bsID, DateTime.Today.AddHours(10));
                dbPeriod.deleteRecord(bsID, DateTime.Today.AddHours(20));
                dbStorage.deleteRecord(bsID);
                dbStation.deleteRecord(sID);
                dbType.deleteRecord(btId);
                
                
               
            }
        }
        [TestMethod]
        public void testBookingPeriod()
        {
            
            int btId = dbType.addNewRecord("newName", "newProducer", 10, 100); //capacity equals 10 hours
            int bId = dbBattery.addNewRecord("Charged", btId);
            int sID = dbStation.addNewRecord("newName", "newAddress", "newCountry", "newState");
            int bsID = dbStorage.addNewRecord(btId, sID, 20);
            dbPeriod.addNewRecord(bsID, DateTime.Today, 10,5); // initial =10 custom = 5
            period = dbPeriod.getRecord(bsID, DateTime.Today, true);
            storage = dbStorage.getRecord(bsID, true);
            DateTime time = new DateTime();
            time = DateTime.Today.AddDays(10);
            DateTime secondTime = DateTime.Today.AddDays(5);
            try
            {
               period = pCalc.getBookingPeriod(storage, time);
               MPeriod previous = pCalc.getPreviousPeriod(storage, period);
               Assert.AreEqual(isInPeriod(period, time, storage), true);
               period = pCalc.getBookingPeriod(storage, secondTime);
               Assert.AreEqual(isInPeriod(period, secondTime, storage), true);


            }
            finally
            {
                storage = dbStorage.getRecord(storage.id,true);
                foreach (MPeriod p in storage.periods)
                {
                    dbPeriod.deleteRecord(bsID,p.time);
                }
                dbStorage.deleteRecord(bsID);
                dbStation.deleteRecord(sID);
                dbBattery.deleteRecord(bId);
                dbType.deleteRecord(btId);
            }
        }

        public bool isInPeriod(MPeriod period, DateTime time, MBatteryStorage storage)
        {
            DateTime first = period.time;
            double hours = (double) storage.type.capacity;
            DateTime second = period.time.AddHours(hours);
            if ((time.CompareTo(first) >= 0) & (time.CompareTo(second) < 0))
            {
                return true;
            }
            else return false;
        }
    }
}
