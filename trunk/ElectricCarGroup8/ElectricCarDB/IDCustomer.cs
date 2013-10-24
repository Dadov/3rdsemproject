using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    public interface IDCustomer
    {
        int addNewRecord(string fName, string lName, string address, string country, string phone,
             string email, MDiscountGroup discountGroup, string payStatus);
        MCustomer getRecord(int id, bool retrieveAssociation);
        void deleteRecord(int id);
        void updateRecord(int id, string fName, string lName,
            string address, string country, string phone, string email, ICollection<MLogInfo> logInfos,
            MDiscountGroup discountGroup, string payStatus);
        List<MCustomer> getAllRecord();
        List<string> getAllInfo();
    }
}
