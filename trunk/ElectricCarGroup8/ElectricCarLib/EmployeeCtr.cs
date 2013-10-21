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
    public class EmployeeCtr
    {
        IDEmployee dbEmployee = new DEmployee();

        public int add(EmployeePosition position, string fName, string lName,
            string address, string country, string phone, string email, // ICollection<MLogInfo> logInfos,
            int sId)
        {
            ICollection<MLogInfo> lis = new List<MLogInfo>();
            //empty list at employee creation
            return dbEmployee.addNewRecord(position, fName, lName, address, country, phone, email, lis, sId);
        }

        public MEmployee get(int id, Boolean getAssociation)
        {
            return dbEmployee.getRecord(id, true);
        }

        public void delete(int id)
        {
            dbEmployee.deleteRecord(id);
        }

        public void update(int id, EmployeePosition position, string fName, string lName,
            string address, string country, string phone, string email, ICollection<MLogInfo> logInfos,
            int sId)
        {
            dbEmployee.updateRecord(id, position, fName, lName, address, country, phone, email, logInfos, sId);
        }

        public List<MEmployee> getAll()
        {
            return dbEmployee.getAllRecord();
        }

        public List<string> getAllInfo()
        {
            return dbEmployee.getAllInfo();
        }
    }
}
