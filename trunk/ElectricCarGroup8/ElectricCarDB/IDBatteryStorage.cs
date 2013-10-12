using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    public interface IDBBatteryStorage
    {
        int addNewRecord(int btID, int sID);
        MBatteryStorage getRecord(int id, Boolean getAssociation);
        void deleteRecord(int id);
        void updateRecord(int id, int btID, int sID);
        List<MBatteryStorage> getAllRecord(Boolean getAssociation);
        List<string> getAllInfo();
    }
}
