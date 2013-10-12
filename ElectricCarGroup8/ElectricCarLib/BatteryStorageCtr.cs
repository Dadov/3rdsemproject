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
    }
}
