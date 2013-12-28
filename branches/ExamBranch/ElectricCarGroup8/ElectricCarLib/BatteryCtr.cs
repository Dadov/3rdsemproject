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
    public class BatteryCtr : IDisposable
    {

        public int addNewRecord(string state, int btid)
        {
            IDBattery dbBattery = new DBattery();
            int id = dbBattery.addNewRecord(state, btid);
            return id;
        }

        public MBattery getRecord(int id, Boolean getAssociation)
        {
            IDBattery dbBattery = new DBattery();
            return dbBattery.getRecord(id, true);
        }

        public void deleteRecord(int id)
        {
            IDBattery dbBattery = new DBattery();
            dbBattery.deleteRecord(id);
        }

        public void updateRecord(int id, string state, int btid)
        {
            IDBattery dbBattery = new DBattery();
            dbBattery.updateRecord(id, state, btid);
        }

        public List<MBattery> getAllRecord(Boolean getAssociation)
        {
            IDBattery dbBattery = new DBattery();
            return dbBattery.getAllRecord(true);
        }

        public List<string> getAllInfo()
        {
            IDBattery dbBattery = new DBattery();
            return dbBattery.getAllInfo();
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
