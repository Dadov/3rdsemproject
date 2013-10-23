using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    public interface IDPeriod
    {
        int addNewRecord(int bsID, DateTime time, int init, int cust, int future);
        MPeriod getRecord(int bsID, DateTime time, Boolean getAssociation);
        void deleteRecord(int bsID, DateTime time);
        void updateRecord(int bsID, DateTime time, int init, int cust, int future);
        List<MPeriod> getAllRecord(Boolean getAssociation);
        List<MPeriod> getStoragePeriods(int bsID, Boolean getAssociation);
        List<string> getAllInfo();
    }
}
