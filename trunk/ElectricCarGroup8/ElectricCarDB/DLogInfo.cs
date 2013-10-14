using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarDB
{
    class DLogInfo : IDLogInfo
    {
        public int addNewRecord(string loginName, string password)
        {
            throw new NotImplementedException();
        }

        public ElectricCarModelLayer.MLogInfo getRecord(int id, bool retrieveAssociation)
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

        public List<ElectricCarModelLayer.MLogInfo> getAllRecord()
        {
            throw new NotImplementedException();
        }

        public List<string> getAllInfo()
        {
            throw new NotImplementedException();
        }
    }
}
