using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarDB;
using System.Transactions;
using System.Data;
using System.Data.Objects;
using System.Data.Entity;

namespace ElectricCarLib
{
    class BatteryTypeCtr:IDisposable
    {

        public void addNewRecord(int id, string name, string producer, decimal capacity, decimal exchangeCost, int storageNumber)
        {
            IDBBatteryType dbBatteryType = new DBBatteryType();
            dbBatteryType.addNewRecord(id, name, producer, capacity, exchangeCost, storageNumber);

        }

        public MBatteryType getRecord(int id, Boolean getAssociation)
        {
            IDBBatteryType dbBatteryType = new DBBatteryType();
            return dbBatteryType.getRecord(id, true);
        }

        public void deleteRecord(int id)
        {
            IDBBatteryType dbBatteryType = new DBBatteryType();
            dbBatteryType.deleteRecord(id);
        }

        public void updateRecord(int id, string name, string producer, decimal capacity, decimal exchangeCost, int storageNumber)
        {
            IDBBatteryType dbBatteryType = new DBBatteryType();
            dbBatteryType.updateRecord(id, name, producer, capacity, exchangeCost, storageNumber);
        }

        public List<MBatteryType> getAllRecord(Boolean getAssociation)
        {
            IDBBatteryType dbBatteryType = new DBBatteryType();
            return dbBatteryType.getAllRecord(true); 
        }

        public List<string> getAllInfo()
        {
            IDBBatteryType dbBatteryType = new DBBatteryType();
            return dbBatteryType.getAllInfo();
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
    }
}
