using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    public class DCustomer : IDCustomer
    {
        public int addNewRecord(MDiscountGroup discountGroup, string fName, string lName, string address, string country, string phone, string email, MLogInfo logInfo, string payStatus)
        {
            throw new NotImplementedException();
        }

        public MCustomer getRecord(int id, bool retrieveAssociation)
        {
            throw new NotImplementedException();
        }

        public void deleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public void updateRecord(int id, MDiscountGroup discountGroup, string fName, string lName, string address, string country, string phone, string email, MLogInfo logInfo, string payStatus)
        {
            throw new NotImplementedException();
        }

        public List<MCustomer> getAllRecord()
        {
            throw new NotImplementedException();
        }

        public List<string> getAllInfo()
        {
            throw new NotImplementedException();
        }

        private MCustomer buildMCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
