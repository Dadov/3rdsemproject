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
    public class DBattery:IDBattery
    {
        private IDBatteryType dbType = new DBatteryType();
        public int addNewRecord(string state, int btid)
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
                                
                                try
                                {
                                    var max = context.Batteries.Max(b => b.Id);
                                    newid = (int)max + 1;
                                }
                                catch (Exception)
                                {
                                    newid = 1;
                                }
                                    context.Batteries.Add(new Battery()
                            {
                                Id = newid,
                                state = state,
                                btId = btid
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

        public MBattery getRecord(int id, bool getAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    Battery b = context.Batteries.Find(id);
                    MBattery battery = buildBattery(b);
                    if (getAssociation)
                    {
                        battery.type = dbType.getRecord(battery.type.id, true);
                    }

                    return battery;
                }
                catch (Exception e)
                {
                    throw new System.NullReferenceException("Can not find battery", e);
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
                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                        Battery batToDelete = context.Batteries.Find(id);
                        
                        
                            context.Entry(batToDelete).State = EntityState.Deleted;
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

        public void updateRecord(int id, string state, int btid)
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
                        Battery batToUpdate = context.Batteries.Find(id);
                        
                            batToUpdate.state = state;
                            batToUpdate.btId = btid;
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

        public List<MBattery> getAllRecord(bool getAssociation)
        {
            List<MBattery> batteries = new List<MBattery>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (Battery b in context.Batteries)
                {
                    MBattery battery = buildBattery(b);
                    if (getAssociation)
                    {
                        battery.type = dbType.getRecord(battery.type.id, true);
                    }
                    batteries.Add(battery);
                }
            }
            return batteries;
        }
        public List<MBattery> getTypeBatteries(int btId, bool getAssociation)
        {
            List<MBattery> batteries = new List<MBattery>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (Battery b in context.Batteries)
                {
                    if (b.btId == btId)
                    {
                        MBattery battery = buildBattery(b);
                        battery.type = dbType.getRecord(battery.type.id,true);
                        batteries.Add(battery);
                    }
                }
            }
            return batteries;
        }

        public List<string> getAllInfo()
        {
            List<string> info = new List<string>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (Battery b in context.Batteries)
                {
                    info.Add(b.ToString());
                }
            }
            return info;
        }

        public MBattery buildBattery(Battery b)
        {
            MBattery battery = new MBattery()
            {
                id = b.Id,
                state = b.state,
                type = new MBatteryType { id = b.btId.Value },
            };
            return battery;
        }
    }
}
