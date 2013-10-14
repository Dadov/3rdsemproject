using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    interface IDEmployee
    {
        int addNewRecord(EmployeePosition position, string fName, string lName,
            string address, string country, string phone, string email, MLogInfo logInfo,
            string payStatus);
        MEmployee getRecord(int id, bool retrieveAssociation);
        void deleteRecord(int id);
        void updateRecord(int id, EmployeePosition position, string fName, string lName,
            string address, string country, string phone, string email, MLogInfo logInfo,
            string payStatus);
        List<MEmployee> getAllRecord();
        List<string> getAllInfo();
    }
}
