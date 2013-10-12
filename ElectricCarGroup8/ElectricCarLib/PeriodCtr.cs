using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;
using ElectricCarDB;

namespace ElectricCarLib
{
    class PeriodCtr:IDisposable
    {
        public int addNewRecord(int bsID, DateTime time, int init, int cust, int future)
        {
            IDPeriod dbPeriod = new DBPeriod();
            int id = dbPeriod.addNewRecord(bsID,time,init,cust,future);
            return id;
        }

        public MPeriod getRecord(int id, DateTime time,  Boolean getAssociation)
        {
            IDPeriod dbPeriod = new DBPeriod();
            return dbPeriod.getRecord(id,time, true);
        }

        public void deleteRecord(int id, DateTime time)
        {
            IDPeriod dbPeriod = new DBPeriod();
            dbPeriod.deleteRecord(id, time);
        }

        public void updateRecord(int bsID, DateTime time, int init, int cust, int future)
        {
            IDPeriod dbPeriod = new DBPeriod();
            dbPeriod.updateRecord(bsID, time, init, cust, future);
        }

        public List<MPeriod> getAllRecord(Boolean getAssociation)
        {
            IDPeriod dbPeriod = new DBPeriod();
            return dbPeriod.getAllRecord(true);
        }

        public List<string> getAllInfo()
        {
            IDPeriod dbPeriod = new DBPeriod();
            return dbPeriod.getAllInfo();
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
