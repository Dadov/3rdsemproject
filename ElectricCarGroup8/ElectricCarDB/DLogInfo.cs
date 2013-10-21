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

        public static  MLogInfo buildMLogInfo(LoginInfo logInfo)
        {
            return new MLogInfo(logInfo.Id, logInfo.name, logInfo.password);
        }

        public static ICollection<MLogInfo> buildMlogInfos(ICollection<LoginInfo> logInfos)
        {
            // TODO: fixed HashSet definition taken from Person entity
            ICollection<MLogInfo> mLogInfos = new HashSet<MLogInfo>();
            foreach (LoginInfo li in logInfos) 
            {
                mLogInfos.Add(buildMLogInfo(li));
            }
            return mLogInfos;
        }

        public static LoginInfo buildLogInfo(MLogInfo mLogInfo)
        {
            return new LoginInfo()
            {
                Id = mLogInfo.ID,
                name = mLogInfo.LoginName,
                password = mLogInfo.Password
            };
        }

        public static ICollection<LoginInfo> buildLogInfos(ICollection<MLogInfo> mLogInfos)
        {
            // TODO: fixed HashSet definition taken from Person entity
            ICollection<LoginInfo> logInfos = new HashSet<LoginInfo>();
            foreach (MLogInfo mli in mLogInfos)
            {
                logInfos.Add(buildLogInfo(mli));
            }
            return logInfos;
        }
    }
}
