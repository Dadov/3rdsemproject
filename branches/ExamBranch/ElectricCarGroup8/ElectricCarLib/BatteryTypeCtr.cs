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
using ElectricCarModelLayer;

namespace ElectricCarLib
{
    public class BatteryTypeCtr:IDisposable
    {

        public int addNewRecord(string name, string producer, decimal capacity, decimal exchangeCost)
        {
            IDBatteryType dbBatteryType = new DBatteryType();
            return dbBatteryType.addNewRecord(name, producer, capacity, exchangeCost);

        }

        public MBatteryType getRecord(int id, Boolean getAssociation)
        {
            IDBatteryType dbBatteryType = new DBatteryType();
            return dbBatteryType.getRecord(id, true);
        }

        public void deleteRecord(int id)
        {
            IDBatteryType dbBatteryType = new DBatteryType();
            dbBatteryType.deleteRecord(id);
        }

        public void updateRecord(int id, string name, string producer, decimal capacity, decimal exchangeCost)
        {
            IDBatteryType dbBatteryType = new DBatteryType();
            dbBatteryType.updateRecord(id, name, producer, capacity, exchangeCost);
        }

        public List<MBatteryType> getAllRecord(Boolean getAssociation)
        {
            IDBatteryType dbBatteryType = new DBatteryType();
            return dbBatteryType.getAllRecord(getAssociation); 
        }

        public List<string> getAllInfo()
        {
            IDBatteryType dbBatteryType = new DBatteryType();
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
