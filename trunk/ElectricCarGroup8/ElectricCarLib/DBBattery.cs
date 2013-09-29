using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarDB;
using System.Transactions;
using System.Data;
using System.Data.Objects;
using System.Data.Entity;

namespace ElectricCarLib
{
    class DBBattery:IDBBattery
    {
        public void addNewRecord(int id, string state, int btid)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    context.Battery.Add(new Battery()
                    {
                        Id = id,
                        state = state,
                        btId = btid
                    });
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw new SystemException("Can not add battery");
                }

            }
        }

        public MBattery getRecord(int id, bool getAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    Battery b = context.Battery.Find(id);
                    MBattery battery = buildBattery(b);
                    if (getAssociation)
                    {
                        //TODO get stationCtr to retreive station info
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
                Battery batToDelete = context.Battery.Find(id);
                if (batToDelete != null)
                {
                    context.Entry(batToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find battery");
                    //throw new SystemException("Can not find battery");
                }
            }
        }

        public void updateRecord(int id, string state, int btid)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                Battery batToUpdate = context.Battery.Find(id);
                if (batToUpdate != null)
                {
                    batToUpdate.state = state;
                    batToUpdate.btId = btid;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find battery");
                    //throw new SystemException("Can not find battery");
                }
            }
        }

        public List<MBattery> getAllRecord(bool getAssociation)
        {
            List<MBattery> batteries = new List<MBattery>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (Battery b in context.Battery)
                {
                    MBattery battery = buildBattery(b);
                    if (getAssociation)
                    {
                        //TODO
                    }
                    batteries.Add(battery);
                }
            }
            return batteries;
        }

        public List<string> getAllInfo()
        {
            List<string> info = new List<string>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (Battery b in context.Battery)
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
                batteryType = new MBatteryType() { id = (int) b.btId },
            };
            return battery;
        }
    }
}
