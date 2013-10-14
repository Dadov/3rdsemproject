using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    public class DLogInfo : IDLogInfo
    {
        public int addNewRecord(string loginName, string password)
        {
            throw new NotImplementedException();
        }

        public MLogInfo getRecord(int id, bool retrieveAssociation)
        {
            throw new NotImplementedException();
        }

        public void deleteRecord(int id, bool retrieveAssociation)
        {
            throw new NotImplementedException();
        }

        public void updateRecord(int id, string loginName, string password)
        {
            throw new NotImplementedException();
        }

        public List<MLogInfo> getAllRecord()
        {
            throw new NotImplementedException();
        }

        public List<string> getAllInfo()
        {
            throw new NotImplementedException();
        }

        private MLogInfo buildMLogInfo(LoginInfo logInfo)
        {
            throw new NotImplementedException();
        }
    }
}
