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

namespace ElectricCarDB
{
     public class DBBatteryStorage : IDBBatteryStorage
    {
        private DBPeriod dbPeriod = new DBPeriod();
        private DBatteryType dbType = new DBatteryType();

        public int addNewRecord(int btID, int sID)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    int newid = context.BatteryStorage.Count() + 1;
                    context.BatteryStorage.Add(new BatteryStorage()
                    {
                        Id = newid,
                        sId = sID,
                        btId = btID
                    });
                    context.SaveChanges();
                    return newid;
                }
                catch (Exception)
                {
                    throw new SystemException("Can not add battery storage");
                }

            }
        }

        public MBatteryStorage getRecord(int id, bool getAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    BatteryStorage bs = context.BatteryStorage.Find(id);
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

        public void deleteRecord(int id)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                BatteryStorage storToDelete = context.BatteryStorage.Find(id);
                if (storToDelete != null)
                {
                    context.Entry(storToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find battery storage");
                    //throw new SystemException("Can not find battery storage");
                }
            }
        }

        public void updateRecord(int id, int btID, int sID)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                BatteryStorage storToUpdate = context.BatteryStorage.Find(id);
                if (storToUpdate != null)
                {
                    storToUpdate.btId = btID;
                    storToUpdate.sId = sID;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find battery storage");
                    //throw new SystemException("Can not find battery storage");
                }
            }
        }

        public List<MBatteryStorage> getStationStorages(int sID)
        {
            List<MBatteryStorage> storages = new List<MBatteryStorage>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (BatteryStorage bs in context.BatteryStorage)
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
                foreach (BatteryStorage bs in context.BatteryStorage)
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
                foreach (BatteryStorage bs in context.BatteryStorage)
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
                periods = dbPeriod.getStoragePeriods(bs.Id),
            };
            return storage;
        }
        
    }
}
