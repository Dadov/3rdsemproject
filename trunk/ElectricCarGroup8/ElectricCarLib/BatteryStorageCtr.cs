using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarDB;
using ElectricCarModelLayer;

namespace ElectricCarLib
{
    class BatteryStorageCtr
    {
        public int addNewRecord(int btID, int sID)
        {
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            int id = dbStorage.addNewRecord(btID,sID);
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
            dbStorage.deleteRecord(id);
        }

        public void updateRecord(int id, int btid, int sID)
        {
            IDBBatteryStorage dbStorage = new DBBatteryStorage();
            dbStorage.updateRecord(id, btid, sID);
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
            bool validate = true;
            //TODO validate whether the booking can be placed in period for the station and battery type
            return validate;
        }

        public bool validateUpdateBookingForStation(int sId, int btId, int updateQuantity, DateTime time)
        {
            bool validate = true;
            //TODO validate whether the update can be placed in period for the station and battery type
            return validate;
        }

        public bool addBookingForStation(int sId, int btId, int quantity, DateTime time)
        {
            bool success = true;
            //TODO calculate and update the numbers in period for spcific battery type with quantity  
            return success;
        }

        public bool deleteBookingForStation(int sId, int btId, int quantity, DateTime time)
        {
            bool success = true;
            //TODO calculate and update the numbers in period for spcific battery type with quantity  
            return success;
        }

        public bool updateBookingForStation(int sId, int btId, int updateQuantity, DateTime time)
        {
            bool success = true;
            //TODO calculate and update the numbers in period for spcific battery type with quantity 
            return success;
        }
    }
}
