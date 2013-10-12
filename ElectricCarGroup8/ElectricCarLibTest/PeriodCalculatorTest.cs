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
        private MPeriod period = new MPeriod();
        private MBatteryStorage storage = new MBatteryStorage();
        private PeriodCalculator pCalc = new PeriodCalculator();

        public PeriodCalculatorTest()
        {
            int btId = dbType.addNewRecord("newName", "newProducer", 10, 100, 20); //capacity equals 10 hours
            int sID = dbStation.addNewRecord("newName", "newAddress", "newCountry", "newState");
            int bsID = dbStorage.addNewRecord(btId,sID);
            dbPeriod.addNewRecord(bsID, DateTime.Today,10,5,1); // initial =10 custom = 5 future = 1
            period = dbPeriod.getRecord(bsID, DateTime.Today, true);
            storage = dbStorage.getRecord(bsID, true);
        }
        [TestMethod]
        public void TestGetTime()
        {
            DateTime newTime = pCalc.getTime(storage);
            Assert.AreEqual(newTime, DateTime.Today.AddHours(10));
        }
        [TestMethod]
        public void TestGetInitNumber()
        {
            int init = pCalc.getInitNumber(storage);
            Assert.AreEqual(init, 5);
        }
        [TestMethod]
        public void TestGetPeriod()
        {
            MPeriod firstPeriod = pCalc.getPeriod(storage);
            storage = dbStorage.getRecord(storage.id, true);
            MPeriod secondPeriod = pCalc.getPeriod(storage);
            Assert.AreEqual(DateTime.Today.AddHours(20), secondPeriod.time);
            Assert.AreEqual(9, secondPeriod.initBatteryNumber);
            Assert.AreEqual(1, firstPeriod.bookedBatteryNumber);
        }
    }
}
