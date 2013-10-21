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

        public int add(int discountGroupId, string fName, string lName,
            string address, string country, string phone, string email, // ICollection<MLogInfo> logInfos,
            string payStatus)
        {
            MDiscountGroup dg = dbDiscountGroup.getRecord(discountGroupId, false);
            //empty list at customer creation
            ICollection<MLogInfo> lis = new List<MLogInfo>();
            return dbCustomer.addNewRecord(dg, fName, lName, address, country, phone, email, lis, payStatus);
        }

        public MCustomer get(int id, Boolean getAssociation)
        {
            return dbCustomer.getRecord(id, true);
        }

        public void delete(int id)
        {
            dbCustomer.deleteRecord(id);
        }

        public void update(int id, int discountGroupId, string fName, string lName,
            string address, string country, string phone, string email, ICollection<MLogInfo> logInfos,
            string payStatus)
        {
            MDiscountGroup dg = dbDiscountGroup.getRecord(discountGroupId, false);
            dbCustomer.updateRecord(id, dg, fName, lName, address, country, phone, email, logInfos, payStatus);
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
