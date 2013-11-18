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
    public class DBPeriod:IDPeriod
    {
        public int addNewRecord(int bsID, DateTime time, int init, int cust)
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
                            context.Periods.Add(new Period()
                            {
                                bsId = bsID,
                                time = time,
                                avaiNumber = init,
                                custBookNumber = cust
                            });
                            newid = bsID;
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
                    throw new SystemException("Cannot finish transaction for adding Period " +
                       " with an error " + e.Message);
                }

            }
           }

        public MPeriod getRecord(int bsID,DateTime time, bool getAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    Object[] key = {bsID, time};
                    Period p = context.Periods.Find(key);
                    MPeriod period = buildPeriod(p);
                    if (getAssociation)
                    {
                        //TODO get stationCtr to retreive station info
                    }

                    return period;
                }
                catch (Exception e)
                {
                    throw new System.NullReferenceException("Can not find period", e);
                    //throw new SystemException("Can not find period");
                }
            }
        }

        public void deleteRecord(int bsID, DateTime time)
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
                    Object[] key = { bsID, time };
                    Period perToDelete = context.Periods.Find(key);
                        context.Entry(perToDelete).State = EntityState.Deleted;
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
                catch (TransactionAbortedException e)
                {
                    throw new SystemException("Cannot finish transaction for deleting BatteryType " +
                       " with an error " + e.Message);
                }
            }
        }

        public void updateRecord(int bsID, DateTime time, int init)
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
                Object[] key = { bsID, time };
                Period perToUpdate = context.Periods.Find(key);
                
                    perToUpdate.time = time;
                    perToUpdate.avaiNumber = init;
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

        public void updateRecord(int bsID, DateTime time, int init, int cust)
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
                            Object[] key = { bsID, time };
                            Period perToUpdate = context.Periods.Find(key);

                            perToUpdate.time = time;
                            perToUpdate.avaiNumber = init;
                            perToUpdate.custBookNumber = cust;
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

        public List<MPeriod> getAllRecord(bool getAssociation)
        {
            List<MPeriod> periods = new List<MPeriod>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (Period p in context.Periods)
                {
                    MPeriod period = buildPeriod(p);
                    periods.Add(period);
                }
            }
            return periods;
        }
        public List<MPeriod> getStoragePeriods(int bsID, Boolean getAssociation)
        {
            List<MPeriod> periods = new List<MPeriod>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (Period p in context.Periods)
                {
                    if (p.bsId == bsID)
                    {
                        MPeriod period = buildPeriod(p);
                        periods.Add(period);
                    }
                }
            }
            return periods;
        }

        public List<string> getAllInfo()
        {
            List<string> info = new List<string>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (Period p in context.Periods)
                {
                    info.Add(p.ToString());
                }
            }
            return info;
        }
        public MPeriod buildPeriod(Period p)
        {
            MPeriod period = new MPeriod()
            {
                time = p.time,
                initBatteryNumber = (int) p.avaiNumber,
                bookedBatteryNumber = (int) p.custBookNumber,
            };
            return period;
        }
    }
}
