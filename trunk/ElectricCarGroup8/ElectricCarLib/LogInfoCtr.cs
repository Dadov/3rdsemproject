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
    public class LogInfoCtr
    {
        IDLogInfo dbLogInfo = new DLogInfo();

        public int add(string loginName, string password, int personId)
        {
            return dbLogInfo.addNewRecord(loginName, password, personId);
        }

        public MLogInfo get(int id, Boolean getAssociation)
        {
            return dbLogInfo.getRecord(id, true);
        }

        public void delete(int id)
        {
            dbLogInfo.deleteRecord(id);
        }

        public void update(int id, string loginName, string password, int personId)
        {
            dbLogInfo.updateRecord(id, loginName, password, personId);
        }

        public List<MLogInfo> getAll()
        {
            return dbLogInfo.getAllRecord();
        }

        public List<string> getAllInfo()
        {
            return dbLogInfo.getAllInfo();
        }
    }
}
