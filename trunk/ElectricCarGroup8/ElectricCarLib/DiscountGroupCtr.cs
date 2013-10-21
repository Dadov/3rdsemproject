using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ElectricCarDB;
using ElectricCarModelLayer;

namespace ElectricCarLib
{
    public class DiscountGroupCtr
    {
        IDDiscountGroup dbDiscountGroup = new DDiscountGroup();

        public int add(string name, decimal discount)
        {
            return dbDiscountGroup.addNewRecord(name, discount);
        }

        public MDiscountGroup get(int id, Boolean getAssociation)
        {
            return dbDiscountGroup.getRecord(id, true);
        }

        public void delete(int id)
        {
            dbDiscountGroup.deleteRecord(id);
        }

        public void update(int id, string name, decimal discount)
        {
            dbDiscountGroup.updateRecord(id, name, discount);
        }

        public List<MDiscountGroup> getAll()
        {
            return dbDiscountGroup.getAllRecord();
        }

        public List<string> getAllInfo()
        {
            return dbDiscountGroup.getAllInfo();
        }
    }
}
