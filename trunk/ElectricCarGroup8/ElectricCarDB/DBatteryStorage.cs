using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;
using System.Transactions;
using System.Data;
using System.Data.Objects;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ElectricCarDB
{
     public class DBBatteryStorage : IDBBatteryStorage
    {
        private DBPeriod dbPeriod = new DBPeriod();
        private DBatteryType dbType = new DBatteryType();

        public int addNewRecord(int btID, int sID)
        {
             // althought 'Required' is supposed to be default
            using (TransactionScope transaction = new TransactionScope((TransactionScopeOption.Required)))
            {
                try
                {
                    int newid = -1;
                    using (ElectricCarEntities context = new ElectricCarEntities())
                    {
                        bool failed = true;
                        do
                        {
                        try
                        {
                            try
                            {
                                var max = context.BatteryStorages.Max(bs => bs.Id);
                                newid = (int)max + 1;
                            }
                            catch (Exception)
                            {
                                newid = 1;
                            }
                            context.BatteryStorages.Add(new BatteryStorage()
                            {
                                Id = newid,
                                sId = sID,
                                btId = btID
                            });
                            context.SaveChanges();
                            failed = false;
                        }
                       catch (DbUpdateConcurrencyException e)
                            {
                                // just in case, shouldn't be needed to set explicitly failed
                                failed = true;
                                e.Entries.Single().Reload();
                                Console.WriteLine("DbUpdateConcurrencyException with message: " +
                                    e.Message + "\n\n was handled and trying again");
                            }
                        } while (failed);
                    }
                    transaction.Complete();
                    return newid;
                    }
                catch (TransactionAbortedException e)
                {
                    throw new SystemException("Cannot finish transaction for adding Battery Storage " +
                       " with an error " + e.Message);
                }

            }
        }

        public MBatteryStorage getRecord(int id, bool getAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    BatteryStorage bs = context.BatteryStorages.Find(id);
                    MBatteryStorage storage = buildStorage(bs);
                    if (getAssociation)
                    {
                        storage.type = dbType.getRecord(storage.type.id, true);
                    }

                    return storage;
                }
                catch (Exception e)
                {
                    throw new System.NullReferenceException("Can not find battery storage", e);
                    //throw new SystemException("Can not find battery storage");
                }
            }
        }

        public MBatteryStorage getRecord(int btid, int sid, bool getAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    BatteryStorage s = context.BatteryStorages.Where(bs => bs.btId == btid && bs.sId==sid).FirstOrDefault();
                    MBatteryStorage storage = buildStorage(s);
                    return storage;
                }
                catch (Exception e)
                {
                    throw new System.NullReferenceException("Can not find battery storage", e);
                    //throw new SystemException("Can not find battery storage");
                }
            }
        }

        public MBatteryStorage getRecordByType(int btid, bool getAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                MBatteryStorage storage = new MBatteryStorage();
                try
                {
                    BatteryStorage s = context.BatteryStorages.Where(bs => bs.btId == btid).FirstOrDefault();
                    storage = buildStorage(s);
                    return storage;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public void deleteRecord(int id)
        {
             using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try 
                {
                bool success = false;
                using(TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                    BatteryStorage storToDelete = context.BatteryStorages.Find(id);
                    context.Entry(storToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                    success = true;
                    }
                        catch(Exception)
                        {
                            throw new System.NullReferenceException("Can not delete battery storage. Delete battery type first.");
                            //throw new SystemException("Can not find battery type");
                        }
                        if (success)
                        {
                            scope.Complete();
                        }
                       
                    }
                }
                    catch (TransactionAbortedException e)
                    {
                        throw new SystemException("Cannot finish transaction for deleting BatteryType " +
                           " with an error " + e.Message);
                    }
                }
            }

        public void deleteRecordByType(int btID)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    bool success = false;
                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            BatteryStorage storToDelete = (BatteryStorage) context.BatteryStorages.Where(bt=>btID==bt.btId).FirstOrDefault();
                            context.Entry(storToDelete).State = EntityState.Deleted;
                            context.SaveChanges();
                            success = true;
                        }
                        catch (Exception)
                        {
                            throw new System.NullReferenceException("Can not delete battery storage. Delete battery type first.");
                            //throw new SystemException("Can not find battery type");
                        }
                        if (success)
                        {
                            scope.Complete();
                        }

                    }
                }
                catch (TransactionAbortedException e)
                {
                    throw new SystemException("Cannot finish transaction for deleting BatteryType " +
                       " with an error " + e.Message);
                }
            }
        }

        public void updateRecord(int id, int btID, int sID)
        {
             using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    bool success = false;
                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                    BatteryStorage storToUpdate = context.BatteryStorages.Find(id);
                       storToUpdate.btId = btID;
                       storToUpdate.sId = sID;
                       context.SaveChanges();
                       success = true;
                        }
                        catch (Exception)
                        {
                            throw new System.NullReferenceException("Can not find battery type");
                            //throw new SystemException("Can not find battery type");
                        }
                        if (success)
                        {
                            scope.Complete();
                        }
                    }
                }
                catch (Exception)
                {
                    throw new SystemException("Can not open transaction scope");
                }
            }
        }

        public List<MBatteryStorage> getStationStorages(int sID)
        {
            List<MBatteryStorage> storages = new List<MBatteryStorage>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (BatteryStorage bs in context.BatteryStorages)
                {
                    if (bs.sId == sID)
                    {
                        MBatteryStorage storage = buildStorage(bs);
                        storage.type = dbType.getRecord(storage.type.id, true);
                        storages.Add(storage);
                    }
                }
            }
            return storages;
        }

        public List<MBatteryStorage> getAllRecord(bool getAssociation)
        {
            List<MBatteryStorage> storages = new List<MBatteryStorage>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (BatteryStorage bs in context.BatteryStorages)
                {
                    MBatteryStorage storage = buildStorage(bs);
                    if (getAssociation)
                    {
                        storage.type = dbType.getRecord(storage.type.id, true);
                    }
                    storages.Add(storage);
                }
            }
            return storages;
        }

        public List<string> getAllInfo()
        {
            List<string> info = new List<string>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (BatteryStorage bs in context.BatteryStorages)
                {
                    info.Add(bs.ToString());
                }
            }
            return info;
        }

        public MBatteryStorage buildStorage(BatteryStorage bs)
        {
            MBatteryStorage storage = new MBatteryStorage()
            {
                id = bs.Id,
                type = new MBatteryType() { id = (int)bs.btId },
                periods = dbPeriod.getStoragePeriods(bs.Id, true),
            };
            return storage;
        }
        
    }
}
