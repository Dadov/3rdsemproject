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
    public class DBattery:IDBattery
    {
        private IDBatteryType dbType = new DBatteryType();
        public int addNewRecord(string state, int btid)
        {
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                try
                {
                    int newid = context.Batteries.Count() + 1;
                    context.Batteries.Add(new Battery()
                    {
                        Id = newid,
                        state = state,
                        btId = btid
                    });
                    context.SaveChanges();
                    return newid;
                }
                catch (Exception)
                {
                    throw new SystemException("Can not add battery");
                }

            }
        }

        public MBattery getRecord(int id, bool getAssociation)
        {
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
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
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                Battery batToDelete = context.Batteries.Find(id);
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
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                Battery batToUpdate = context.Batteries.Find(id);
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
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
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

        public List<string> getAllInfo()
        {
            List<string> info = new List<string>();
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
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
                type = new MBatteryType() { id = (int) b.btId },
            };
            return battery;
        }
    }
}
