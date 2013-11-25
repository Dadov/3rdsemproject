using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    public interface IDBatteryType
    {
        int addNewRecord(string name, string producer, decimal capacity, decimal exchangeCost);
        MBatteryType getRecord(int id, Boolean getAssociation);
        void deleteRecord(int id);
        void updateRecord(int id, string name, string producer, decimal capacity, decimal exchangeCost);
        List<MBatteryType> getAllRecord(Boolean getAssociation);
        List<string> getAllInfo();
    }
}
