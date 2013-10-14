using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarDB
{
    class DCustomer : IDCustomer
    {
        public int addNewRecord(ElectricCarModelLayer.MDiscountGroup discountGroup, string fName, string lName, string address, string country, string phone, string email, ElectricCarModelLayer.MLogInfo logInfo, string payStatus)
        {
            throw new NotImplementedException();
        }

        public ElectricCarModelLayer.MCustomer getRecord(int id, bool retrieveAssociation)
        {
            throw new NotImplementedException();
        }

        public void deleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public void updateRecord(int id, ElectricCarModelLayer.MDiscountGroup discountGroup, string fName, string lName, string address, string country, string phone, string email, ElectricCarModelLayer.MLogInfo logInfo, string payStatus)
        {
            throw new NotImplementedException();
        }

        public List<ElectricCarModelLayer.MCustomer> getAllRecord()
        {
            throw new NotImplementedException();
        }

        public List<string> getAllInfo()
        {
            throw new NotImplementedException();
        }
    }
}
