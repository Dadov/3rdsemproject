using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    public interface IDDiscountGroup
    {
        int addNewRecord(string name, Nullable<decimal> discount);
        MDiscountGroup getRecord(int id, bool retrieveAssociation);
        void deleteRecord(int id, bool retrieveAssociation);
        void updateRecord(int id, string name, Nullable<decimal> discount);
        List<MDiscountGroup> getAllRecord();
        List<string> getAllInfo();
    }
}
