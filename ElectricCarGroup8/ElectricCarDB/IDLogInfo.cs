using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    public interface IDLogInfo
    {
        int addNewRecord(string loginName, string password, int personId);
        MLogInfo getRecord(int id, bool retrieveAssociation);
        void deleteRecord(int id);
        void updateRecord(int id, string loginName, string password, int personId);
        void updateRecord(int id, string loginName, string password);
        List<MLogInfo> getAllRecord();
        List<string> getAllInfo();
    }
}
