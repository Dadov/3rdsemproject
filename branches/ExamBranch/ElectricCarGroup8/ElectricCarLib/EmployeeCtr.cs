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

        public int add(string fName, string lName,
            string address, string country, string phone, string email, // ICollection<MLogInfo> logInfos,
            int sId, EmployeePosition position)
        {
            // TODO: station id from StationCtr

            // cannot be empty list at Employee creation, because it requires Person ID
            // ICollection<MLogInfo> lis = new List<MLogInfo>();
            return dbEmployee.addNewRecord(fName, lName, address, country, phone, email, sId, position);
        }

        public MEmployee get(int id, Boolean getAssociation)
        {
            return dbEmployee.getRecord(id, true);
        }

        public void delete(int id)
        {
            dbEmployee.deleteRecord(id);
        }

        public void update(int id,string fName, string lName,
            string address, string country, string phone, string email, ICollection<MLogInfo> logInfos,
            int sId,  EmployeePosition position)
        {
            dbEmployee.updateRecord(id, fName, lName, address, country, phone, email, logInfos, sId, position);
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
