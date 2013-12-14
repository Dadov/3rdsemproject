using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data;
using System.Data.Objects;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    public class DLogInfo : IDLogInfo
    {
        public int addNewRecord(string loginName, string password, int personId)
        {
            // althought 'Required' is supposed to be default
            using (TransactionScope transaction = new TransactionScope((TransactionScopeOption.Required)))
            {
                try
                {
                    int newId = -1;
                    using (ElectricCarEntities context = new ElectricCarEntities())
                    {
                            try
                            {
                                int max;
                                try {
                                    max = context.LoginInfoes.Max(li => li.Id);
                                } catch {
                                    max = 0;
                                }
                                newId = max + 1;
                                context.LoginInfoes.Add(new LoginInfo()
                                {
                                    Id = newId,
                                    name = loginName,
                                    password = password,
                                    pId = personId
                                });
                                context.SaveChanges();
                            }
                            catch (Exception e)
                            {
                                throw new SystemException("Cannot add new Log Info with an error: " + e.Message);
                            }
                    }
                    transaction.Complete();
                    return newId;
                }
                catch (TransactionAbortedException e)
                {
                    throw new SystemException("Cannot finish transaction for adding Login Info " +
                       " with an error " + e.Message);
                }
            }
        }

        public MLogInfo getRecord(int id, bool retrieveAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    LoginInfo li = context.LoginInfoes.Find(id);
                    MLogInfo mli = buildMLogInfo(li);
                    if (retrieveAssociation)
                    {
                        // TODO: solve with entity framework somehow, or manually
                    }
                    return mli;
                }
                catch (Exception e)
                {
                    throw new System.NullReferenceException("Cannot get Login Info with id: " + id +
                        " with an error " + e.Message);
                }
            }
        }

        public void deleteRecord(int id)
        {
            using (TransactionScope transaction = new TransactionScope((TransactionScopeOption.Required)))
            {
                try
                {
                    using (ElectricCarEntities context = new ElectricCarEntities())
                    {
                        try
                        {
                            LoginInfo li = context.LoginInfoes.Find(id);
                            if (li != null)
                            {
                                context.Entry(li).State = EntityState.Deleted;
                                context.SaveChanges();
                            }
                        }
                        catch (Exception e)
                        {
                            throw new SystemException("Cannot delete Login Info " + id + " record " +
                                " with an error " + e.Message);
                        }
                    }
                    transaction.Complete();
                }
                catch (TransactionAbortedException e)
                {
                    throw new SystemException("Cannot finish transaction for deleting Login Info " +
                       " with an error " + e.Message);
                }
            }
        }

        public void updateRecord(int id, string loginName, string password, int personId)
        {
            using (TransactionScope transaction = new TransactionScope((TransactionScopeOption.Required)))
            {
                try
                {
                    using (ElectricCarEntities context = new ElectricCarEntities())
                    {
                        try
                        {
                            LoginInfo li = context.LoginInfoes.Find(id);
                            li.name = loginName;
                            li.password = password;
                            li.pId = personId;
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            throw new SystemException("Cannot update Login Info " + id + " record " +
                                " with an error " + e.Message);
                        }
                    }
                    transaction.Complete();
                }
                catch (TransactionAbortedException e)
                {
                    throw new SystemException("Cannot finish transaction for updating Login Info " +
                       " with an error " + e.Message);
                }
            }
        }

        public void updateRecord(int id, string loginName, string password)
        {
            using (TransactionScope transaction = new TransactionScope((TransactionScopeOption.Required)))
            {
                try
                {
                    using (ElectricCarEntities context = new ElectricCarEntities())
                    {
                        try
                        {
                            LoginInfo li = context.LoginInfoes.Find(id);
                            li.name = loginName;
                            li.password = password;
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            throw new SystemException("Cannot update Login Info " + id + " record " +
                                " with an error " + e.Message);
                        }
                    }
                    transaction.Complete();
                }
                catch (TransactionAbortedException e)
                {
                    throw new SystemException("Cannot finish transaction for updating Login Info " +
                       " with an error " + e.Message);
                }
            }
        }

        public List<MLogInfo> getAllRecord()
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                List<MLogInfo> logInfos = new List<MLogInfo>();
                try
                {
                    foreach (LoginInfo li in context.LoginInfoes)
                    {
                        logInfos.Add(DLogInfo.buildMLogInfo(li));
                    }
                    return logInfos;
                }
                catch (Exception e)
                {
                    throw new SystemException("Cannot get all Login Infos " +
                       " with an error " + e.Message);
                }
            }
        }

        public List<string> getAllInfo()
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                List<string> logInfos = new List<string>();
                try
                {
                    foreach (LoginInfo li in context.LoginInfoes)
                    {
                        logInfos.Add(li.ToString());
                    }
                    return logInfos;
                }
                catch (Exception e)
                {
                    throw new SystemException("Cannot get all Log Infos info info info " +
                       " with an error " + e.Message);
                }
            }
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

        public static ICollection<LoginInfo> buildLogInfos(ICollection<MLogInfo> mLogInfos, ICollection<LoginInfo> logInfos)
        {
            // TODO: fixed HashSet definition taken from Person entity
            foreach (LoginInfo li in logInfos)
            {
                // only password and login name can be changed
                li.password =  mLogInfos.Where(mli => mli.ID == li.Id).FirstOrDefault().Password;
                li.name = mLogInfos.Where(mli => mli.ID == li.Id).FirstOrDefault().LoginName;
            }
            return logInfos;
        }
    }
}
