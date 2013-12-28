using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarLib
{
    interface IDBPeriod
    {
        int addNewRecord(int bsID, DateTime time, int init, int cust, int future);
        MPeriod getRecord(int bsID, Boolean getAssociation);
        void deleteRecord(int bsID);
        void updateRecord(int bsID, DateTime time, int init, int cust, int future);
        List<MPeriod> getAllRecord(Boolean getAssociation);
        List<string> getAllInfo();
    }
}
