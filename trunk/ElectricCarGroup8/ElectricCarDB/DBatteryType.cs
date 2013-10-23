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
    public class DBatteryType : IDBatteryType
    {
        public int addNewRecord(string name, string producer, decimal capacity, decimal exchangeCost, int storageNumber)
        {
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
                            var max= context.BatteryTypes.Max( dg => dg.Id);
                            newid = (int) max + 1;
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
                                failed = false;
                            }
                            // needs to be done:
                            // set 'Concurrency Mode = Fixed' in the property panel for the timestamp in the designer
                            // DbUpdateConcurrencyException or OptimisticConcurrencyException
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
                        throw new SystemException("Cannot finish transaction for adding Battery Type " +
                           " with an error " + e.Message);
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
                try 
                {
                bool success = false;
                using(TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                         BatteryType btToDelete = context.BatteryTypes.Find(id);
                         
                                context.Entry(btToDelete).State = EntityState.Deleted;
                                context.SaveChanges();
                                success = true;
                     }
                        catch(Exception)
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
                    catch (TransactionAbortedException e)
                    {
                        throw new SystemException("Cannot finish transaction for deleting BatteryType " +
                           " with an error " + e.Message);
                    }
                }
            }
        

        public void updateRecord(int id, string name, string producer, decimal capacity, decimal exchangeCost, int storageNumber)
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
                            BatteryType btToUpdate = context.BatteryTypes.Find(id);
                            btToUpdate.name = name;
                            btToUpdate.producer = producer;
                            btToUpdate.capacity = capacity;
                            btToUpdate.exchangeCost = exchangeCost;
                            btToUpdate.storageNumber = storageNumber;
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
