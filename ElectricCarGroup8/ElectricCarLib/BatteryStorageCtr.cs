using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarDB;
using ElectricCarModelLayer;
using System.Transactions;

namespace ElectricCarLib
{
    public class BatteryStorageCtr
    {
        public int addNewRecord(int btID, int sID, int storageNumber)
        {
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            BatteryTypeCtr bCtr = new BatteryTypeCtr();
            MBatteryStorage stor = dbStorage.getRecordByType(btID, true);
            int id = -1;
            if (stor == null)
            {
                id = dbStorage.addNewRecord(btID, sID, storageNumber);
                PeriodCalculator pCalc = new PeriodCalculator();
                pCalc.createFirstPeriod(id);
            }
            else throw new SystemException("This type is already assigned to a storage");
            return id;
        }

        public MBatteryStorage getRecord(int id, Boolean getAssociation)
        {
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            return dbStorage.getRecord(id, true);
        }

        public void deleteRecord(int id)
        {
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            IDPeriod dbPeriod = new DBPeriod();
            bool success = false;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    dbPeriod.deleteRecord(id);
                    dbStorage.deleteRecord(id);
                    success = true;
                }
                catch (Exception)
                {
                    throw new System.NullReferenceException("Can not delete battery.");
                    //throw new SystemException("Can not find battery type");
                }
                if (success)
                {
                    scope.Complete();
                }
            }
        }

        public void deleteRecordByType(int btID)
        {
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            int bsID = dbStorage.getRecordByType(btID, true).id;
            BatteryTypeCtr bCtr = new BatteryTypeCtr();
            bool success = false;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    deleteRecord(bsID);
                    bCtr.deleteRecord(btID);
                    success = true;
                }
                catch (Exception)
                {
                    throw new System.NullReferenceException("Can not delete battery.");
                    //throw new SystemException("Can not find battery type");
                }
                if (success)
                {
                    scope.Complete();
                }
            }
        }

        public void updateRecord(int id, int btid, int sID, int storageNumber)
        {
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            dbStorage.updateRecord(id, btid, sID, storageNumber);
            IDPeriod dbPeriod = new DBPeriod();
            PeriodCalculator pCalc = new PeriodCalculator();
            dbPeriod.updateRecord(id, pCalc.getBookingPeriod(getRecord(id,true),DateTime.Now).time,pCalc.getInitNumber(getRecord(id,true)));
        }

        public List<MBatteryStorage> getStationStorages(int sID)
        {
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            return dbStorage.getStationStorages(sID);
        }

        public List<MBatteryStorage> getAllRecord(Boolean getAssociation)
        {
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            return dbStorage.getAllRecord(true);
        }


        public List<string> getAllInfo()
        {
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            return dbStorage.getAllInfo();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            this.Dispose();
        }

        public bool validateBookingForStation(int sId, int btId, int quantity, DateTime time)
        {
            bool validate = false;
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            MBatteryStorage storage = dbStorage.getRecord(btId, sId, true);
            PeriodCalculator pCalc = new PeriodCalculator();
            MPeriod period = pCalc.getBookingPeriod(storage,time);
            MPeriod previous = pCalc.getPreviousPeriod(storage, period);
            MPeriod next = pCalc.getNextPeriod(storage, period);
            int booked = period.bookedBatteryNumber + previous.bookedBatteryNumber;
            int available = period.initBatteryNumber - booked;
            int futureBooked = period.bookedBatteryNumber + next.bookedBatteryNumber;
            int futureAvailable = next.initBatteryNumber - futureBooked;
            if (available >= quantity && futureAvailable >= quantity)
            {
                validate = true;
            }
            return validate;
        }

        public bool validateUpdateBookingForStation(int sId, int btId, int updateQuantity, DateTime time)
        {
            bool validate = false;
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            MBatteryStorage storage = dbStorage.getRecord(btId, sId, true);
            PeriodCalculator pCalc = new PeriodCalculator();
            MPeriod period = pCalc.getBookingPeriod(storage, time);
            MPeriod previous = pCalc.getPreviousPeriod(storage, period);
            MPeriod next = pCalc.getNextPeriod(storage, period);
            int booked = period.bookedBatteryNumber + previous.bookedBatteryNumber;
            int available = period.initBatteryNumber - booked;
            int futureBooked = period.bookedBatteryNumber + next.bookedBatteryNumber;
            int futureAvailable = next.initBatteryNumber - futureBooked;
            if (available >= updateQuantity && futureAvailable >= updateQuantity)
            {
                validate = true;
            }
            return validate;
        }

        public bool addBookingForStation(int sId, int btId, int quantity, DateTime time)
        {
            bool success = false;
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            MBatteryStorage storage = dbStorage.getRecord(btId, sId, true);
            IDPeriod dbPeriod = new DBPeriod();
            PeriodCalculator pCalc = new PeriodCalculator();
            try
            {
                MPeriod period = pCalc.getBookingPeriod(storage, time);
                period.bookedBatteryNumber = period.bookedBatteryNumber + quantity;
                dbPeriod.updateRecord(storage.id,period.time,period.initBatteryNumber,period.bookedBatteryNumber);
                success = true;
            }
            catch(Exception e)
            {
                throw new SystemException("Can not add Booking" + e.Message);
            }
            return success;
           }

        public bool deleteBookingForStation(int sId, int btId, int quantity, DateTime time)
        {
            bool success = false;
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            MBatteryStorage storage = dbStorage.getRecord(btId, sId, true);
            IDPeriod dbPeriod = new DBPeriod();
            PeriodCalculator pCalc = new PeriodCalculator();
            try
            {
                MPeriod period = pCalc.getBookingPeriod(storage, time);
                period.bookedBatteryNumber = period.bookedBatteryNumber - quantity;
                dbPeriod.updateRecord(storage.id, period.time, period.initBatteryNumber, period.bookedBatteryNumber);
                success = true;
            }
            catch (Exception)
            {
                throw new SystemException("Can not delete Booking");
            }
            return success;
        }

        public bool updateBookingForStation(int sId, int btId, int updateQuantity, DateTime time)
        {
            bool success = false;
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            MBatteryStorage storage = dbStorage.getRecord(btId, sId, true);
            IDPeriod dbPeriod = new DBPeriod();
            PeriodCalculator pCalc = new PeriodCalculator();
            try
            {
                MPeriod period = pCalc.getBookingPeriod(storage, time);
                period.bookedBatteryNumber = period.bookedBatteryNumber + updateQuantity;
                dbPeriod.updateRecord(storage.id, period.time, period.initBatteryNumber, period.bookedBatteryNumber);
                success = true;
            }
            catch (Exception)
            {
                throw new SystemException("Can not update Booking");
            }
            return success;
        }
    }
}
