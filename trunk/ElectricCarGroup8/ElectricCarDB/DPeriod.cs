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
    public class DBPeriod:IDPeriod
    {
        public int addNewRecord(int bsID, DateTime time, int init, int cust, int future)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    context.Period.Add(new Period()
                    {
                        bsId = bsID,
                        time = time,
                        avaiNumber = init,
                        custBookNumber = cust,
                        futureBookNumber = future
                    });
                    context.SaveChanges();
                    return bsID;
                }
                catch (Exception)
                {
                    throw new SystemException("Can not add period");
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
                    Period p = context.Period.Find(key);
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
                Object[] key = { bsID, time };
                Period perToDelete = context.Period.Find(key);
                if (perToDelete != null)
                {
                    context.Entry(perToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find period");
                    //throw new SystemException("Can not find period");
                }
            }
        }

        public void updateRecord(int bsID, DateTime time, int init, int cust, int future)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                Object[] key = { bsID, time };
                Period perToUpdate = context.Period.Find(key);
                if (perToUpdate != null)
                {
                    perToUpdate.time = time;
                    perToUpdate.avaiNumber = init;
                    perToUpdate.custBookNumber = cust;
                    perToUpdate.futureBookNumber = future;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find period");
                    //throw new SystemException("Can not find period");
                }
            }
        }

        public List<MPeriod> getAllRecord(bool getAssociation)
        {
            List<MPeriod> periods = new List<MPeriod>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (Period p in context.Period)
                {
                    MPeriod period = buildPeriod(p);
                    if (getAssociation)
                    {
                        //TODO
                    }
                    periods.Add(period);
                }
            }
            return periods;
        }
        public List<MPeriod> getStoragePeriods(int bsID)
        {
            List<MPeriod> periods = new List<MPeriod>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (Period p in context.Period)
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
                foreach (Period p in context.Period)
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
                futureBatteryNumber = (int) p.futureBookNumber
            };
            return period;
        }
    }
}
