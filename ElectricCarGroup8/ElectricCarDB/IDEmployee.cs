using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    // interfaces is implicitly public and can only contain public methods
    public interface IDEmployee
    {
        int addNewRecord(EmployeePosition position, string fName, string lName,
            string address, string country, string phone, string email, ICollection<MLogInfo> logInfos,
            int sId);
        MEmployee getRecord(int id, bool retrieveAssociation);
        void deleteRecord(int id);
        void updateRecord(int id, EmployeePosition position, string fName, string lName,
            string address, string country, string phone, string email, ICollection<MLogInfo> logInfos,
            int sId);
        List<MEmployee> getAllRecord();
        List<string> getAllInfo();
    }
}
