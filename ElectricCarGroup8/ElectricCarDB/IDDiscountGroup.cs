using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    interface IDDiscountGroup
    {
        int addNewRecord(string name, int discount);
        MDiscountGroup getRecord(int id, bool retrieveAssociation);
        void deleteRecord(int id, bool retrieveAssociation);
        void updateRecord(int id, string name, int discount);
        List<MDiscountGroup> getAllRecord();
        List<string> getAllInfo();
    }
}
