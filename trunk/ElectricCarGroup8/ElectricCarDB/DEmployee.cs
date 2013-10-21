using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    public class DEmployee : IDEmployee
    {
        public int addNewRecord(EmployeePosition position, string fName, string lName, string address, string country, string phone, string email, ICollection<MLogInfo> logInfos, string payStatus)
        {
            throw new NotImplementedException();
        }

        public MEmployee getRecord(int id, bool retrieveAssociation)
        {
            throw new NotImplementedException();
        }

        public void deleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public void updateRecord(int id, EmployeePosition position, string fName, string lName, string address, string country, string phone, string email, ICollection<MLogInfo> logInfos, string payStatus)
        {
            throw new NotImplementedException();
        }

        public List<MEmployee> getAllRecord()
        {
            throw new NotImplementedException();
        }

        public List<string> getAllInfo()
        {
            throw new NotImplementedException();
        }

        public static MEmployee buildMEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
