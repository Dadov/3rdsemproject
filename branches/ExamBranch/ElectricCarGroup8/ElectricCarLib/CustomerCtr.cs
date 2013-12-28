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
    public class CustomerCtr
    {
        IDCustomer dbCustomer = new DCustomer();
        IDDiscountGroup dbDiscountGroup = new DDiscountGroup();

        public int add(string fName, string lName,
            string address, string country, string phone, string email, // ICollection<MLogInfo> logInfos,
            int discountGroupId, string payStatus)
        {
            MDiscountGroup dg = dbDiscountGroup.getRecord(discountGroupId, false);
            //cannot put empty list at Customer creation, because it requires Person ID
            //ICollection<MLogInfo> lis = new List<MLogInfo>();
            return dbCustomer.addNewRecord(fName, lName, address, country, phone, email, dg, payStatus);
        }

        public MCustomer get(int id, Boolean getAssociation)
        {
            return dbCustomer.getRecord(id, true);
        }

        public void delete(int id)
        {
            dbCustomer.deleteRecord(id);
        }

        public void update(int id, string fName, string lName,
            string address, string country, string phone, string email, ICollection<MLogInfo> logInfos,
            int discountGroupId, string payStatus)
        {
            MDiscountGroup dg = dbDiscountGroup.getRecord(discountGroupId, false);
            dbCustomer.updateRecord(id, fName, lName, address, country, phone, email, logInfos, dg, payStatus); 
        }

        public List<MCustomer> getAll()
        {
            return dbCustomer.getAllRecord();
        }

        public List<string> getAllInfo()
        {
            return dbCustomer.getAllInfo();
        }
    }
}
