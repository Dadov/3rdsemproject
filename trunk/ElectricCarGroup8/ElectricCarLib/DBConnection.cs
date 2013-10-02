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
    public class DBConnection: IDBConnection
    {
        public void addNewRecord(int id1, int id2, decimal dist, decimal time)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    context.Connection.Add(new Connection()
                    {
                        sId1 = id1,
                        sId2 = id2,
                        distance = dist,
                        driveHour = time
                    });
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw new SystemException("Can not add association between two stations");
                }
                
            }
        }

        public MConnection getRecord(int id1, int id2, bool getAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    Connection c = context.Connection.Find(id1, id2);
                    MConnection connection = buildConnection(c);
                    if (getAssociation)
                    {
                        //TODO get stationCtr to retreive station info
                    }

                    return connection;
                }
                catch (Exception e)
                {
                    throw new System.NullReferenceException("Can not find nabor station", e);
                    //throw new SystemException("Can not find nabor station");
                }
            }
        }

        public void deleteRecord(int id1, int id2)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                Connection conToDelete = context.Connection.Find(id1, id2);
                if (conToDelete != null)
                {
                    context.Entry(conToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find nabor station");
                    //throw new SystemException("Can not find association between these two stations");
                }
            }
        }

        public void updateRecord(int id1, int id2, decimal dist, decimal time)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                Connection conToUpdate = context.Connection.Find(id1, id2);
                if (conToUpdate != null)
                {
                    conToUpdate.distance = dist;
                    conToUpdate.driveHour = time;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find nabor station");
                    //throw new SystemException("Can not find association between these two stations");
                }
            }
        }

        public List<MConnection> getAllRecord(bool getAssociation)
        {
            List<MConnection> connections = new List<MConnection>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (Connection c in context.Connection)
                {
                    MConnection connection = buildConnection(c);
                    if (getAssociation)
                    {
                        //TODO
                    }
                    connections.Add(connection);
                }
            }
            return connections;
        }

        private MConnection buildConnection(Connection c)
        {
            MConnection connection = new MConnection()
            {
                distance = c.distance,
                driveHour = c.driveHour,
                Station1 = new MStation() { Id = c.sId1 },
                Station2 = new MStation() { Id = c.sId2 }
            };
            return connection;
        }

        public List<string> getAllInfo()
        {
            List<string> info = new List<string>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (Connection c in context.Connection)
                {
                    info.Add(c.ToString());
                }
            }
            return info;
        }
    }
}
