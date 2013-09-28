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
    class BatteryCtr : IDisposable
    {

        public void addNewRecord(int id, string state, int btid)
        {
            IDBBattery dbBattery = new DBBattery();
            dbBattery.addNewRecord(id, state, btid);

        }

        public MBattery getRecord(int id, Boolean getAssociation)
        {
            IDBBattery dbBattery = new DBBattery();
            return dbBattery.getRecord(id, true);
        }

        public void deleteRecord(int id)
        {
            IDBBattery dbBattery = new DBBattery();
            dbBattery.deleteRecord(id);
        }

        public void updateRecord(int id, string state, int btid)
        {
            IDBBattery dbBattery = new DBBattery();
            dbBattery.updateRecord(id, state, btid);
        }

        public List<MBattery> getAllRecord(Boolean getAssociation)
        {
            IDBBattery dbBattery = new DBBattery();
            return dbBattery.getAllRecord(true);
        }

        public List<string> getAllInfo()
        {
            IDBBattery dbBattery = new DBBattery();
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
