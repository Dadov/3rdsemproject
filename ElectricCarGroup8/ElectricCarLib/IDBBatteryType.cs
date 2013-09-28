using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarLib
{
    interface IDBBatteryType
    {
        void addNewRecord(int id, string name, string producer, decimal capacity, decimal exchangeCost, int storageNumber);
        MBatteryType getRecord(int id, Boolean getAssociation);
        void deleteRecord(int id);
        void updateRecord(int id, string name, string producer, decimal capacity, decimal exchangeCost, int storageNumber);
        List<MBatteryType> getAllRecord(Boolean getAssociation);
        List<string> getAllInfo();
    }
}
