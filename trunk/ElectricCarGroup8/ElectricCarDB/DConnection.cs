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
    public class DConnection: IDConnection
    {
        private DStation dbStation = new DStation();
        public void addNewRecord(int id1, int id2, decimal dist, decimal time)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    context.Connections.Add(new Connection()
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

        public bool isConnectionExist(int id1, int id2)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try 
	            {	        
		            getRecord(id1, id2, false);
	            }
	            catch (NullReferenceException)
	            {
                    return false;
	            }

                return true;
            }
        }

        public MConnection getRecord(int id1, int id2, bool getAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    Connection c = context.Connections.Find(id1, id2);
                    MConnection connection = buildConnection(c);
                    if (getAssociation)
                    {
                        connection.Station1 = dbStation.getRecord(connection.Station1.Id, false);
                        connection.Station2 = dbStation.getRecord(connection.Station2.Id, false);
                    }

                    return connection;
                }
                catch (Exception e)
                {
                    throw new System.NullReferenceException("Can not find nabor station", e);
                }
            }
        }

        public void deleteRecord(int id1, int id2)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                Connection conToDelete = context.Connections.Find(id1, id2);
                if (conToDelete != null)
                {
                    context.Entry(conToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find nabor station");
                }
            }
        }

        public void updateRecord(int id1, int id2, decimal dist, decimal time)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                Connection conToUpdate = context.Connections.Find(id1, id2);
                if (conToUpdate != null)
                {
                    conToUpdate.distance = dist;
                    conToUpdate.driveHour = time;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find nabor station");
                }
            }
        }

        public List<MConnection> getAllRecord(bool getAssociation)
        {
            List<MConnection> connections = new List<MConnection>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (Connection c in context.Connections)
                {
                    MConnection connection = buildConnection(c);
                    if (getAssociation)
                    {
                        connection.Station1 = dbStation.getRecord(connection.Station1.Id, false);
                        connection.Station2 = dbStation.getRecord(connection.Station2.Id, false);
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
                foreach (Connection c in context.Connections)
                {
                    info.Add(c.ToString());
                }
            }
            return info;
        }
    }
}
