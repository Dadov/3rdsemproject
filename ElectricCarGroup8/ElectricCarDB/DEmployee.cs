using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarDB
{
    class DEmployee : IDEmployee
    {
        public int addNewRecord(ElectricCarModelLayer.EmployeePosition position, string fName, string lName, string address, string country, string phone, string email, ElectricCarModelLayer.MLogInfo logInfo, string payStatus)
        {
            throw new NotImplementedException();
        }

        public ElectricCarModelLayer.MEmployee getRecord(int id, bool retrieveAssociation)
        {
            throw new NotImplementedException();
        }

        public void deleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public void updateRecord(int id, ElectricCarModelLayer.EmployeePosition position, string fName, string lName, string address, string country, string phone, string email, ElectricCarModelLayer.MLogInfo logInfo, string payStatus)
        {
            throw new NotImplementedException();
        }

        public List<ElectricCarModelLayer.MEmployee> getAllRecord()
        {
            throw new NotImplementedException();
        }

        public List<string> getAllInfo()
        {
            throw new NotImplementedException();
        }
    }
}
