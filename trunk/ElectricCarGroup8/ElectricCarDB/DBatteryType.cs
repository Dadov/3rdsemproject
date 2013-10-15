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
    public class DBatteryType : IDBatteryType
    {
        public int addNewRecord(string name, string producer, decimal capacity, decimal exchangeCost, int storageNumber)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    int newid = context.BatteryTypes.Count() + 1;
                    context.BatteryTypes.Add(new BatteryType()
                    {
                        Id = newid,
                        name = name,
                        producer = producer,
                        capacity = capacity,
                        exchangeCost = exchangeCost,
                        storageNumber = storageNumber
                    });
                    context.SaveChanges();
                    return newid;
                }
                catch (Exception)
                {
                    throw new SystemException("Can not add battery type");
                }
             }
        }

        public MBatteryType getRecord(int id, bool getAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    BatteryType bt = context.BatteryTypes.Find(id);
                    MBatteryType batteryType = buildBatteryType(bt);
                    if (getAssociation)
                    {
                        //TODO get stationCtr to retreive station info
                    }

                    return batteryType;
                }
                catch (Exception e)
                {
                    throw new System.NullReferenceException("Can not find battery type", e);
                    //throw new SystemException("Can not find battery type");
                }
            }
        }

        public void deleteRecord(int id)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                BatteryType btToDelete = context.BatteryTypes.Find(id);
                if (btToDelete != null)
                {
                    context.Entry(btToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find battery type");
                    //throw new SystemException("Can not find battery type");
                }
            }
        }

        public void updateRecord(int id, string name, string producer, decimal capacity, decimal exchangeCost, int storageNumber)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                BatteryType btToUpdate = context.BatteryTypes.Find(id);
                if (btToUpdate != null)
                {
                    btToUpdate.name = name;
                    btToUpdate.producer = producer;
                    btToUpdate.capacity = capacity;
                    btToUpdate.exchangeCost = exchangeCost;
                    btToUpdate.storageNumber = storageNumber;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find battery type");
                    //throw new SystemException("Can not find battery type");
                }
            }
        }

        public List<MBatteryType> getAllRecord(bool getAssociation)
        {
            List<MBatteryType> types = new List<MBatteryType>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (BatteryType bt in context.BatteryTypes)
                {
                    MBatteryType batteryType = buildBatteryType(bt);
                    if (getAssociation)
                    {
                        //TODO
                    }
                    types.Add(batteryType);
                }
            }
            return types;
        }

        public List<string> getAllInfo()
        {
            List<string> info = new List<string>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (BatteryType bt in context.BatteryTypes)
                {
                    info.Add(bt.ToString());
                }
            }
            return info;
        }

        public MBatteryType buildBatteryType(BatteryType bt)
        {
            MBatteryType batteryType = new MBatteryType()
            {
                id = bt.Id,
                name = bt.name,
                producer = bt.producer,
                capacity = bt.capacity,
                exchangeCost = bt.exchangeCost,
                storageNumber = (int) bt.storageNumber
            };
            return batteryType;
        }
    }
}
