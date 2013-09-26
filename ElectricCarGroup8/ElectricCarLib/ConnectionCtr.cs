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
    public class ConnectionCtr: IDisposable
    {
        public void addNewRecord(int id1, int id2, decimal dist, decimal time)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                context.Connection.Add(new Connection() {sId1 = id1, sId2 = id2, distance = dist, 
                 driveHour = time});
                context.SaveChanges();
            }
        }

        public MConnection getRecord(int id1, int id2, Boolean getAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                Connection c = context.Connection.Find(id1, id2);
                MConnection connection = buildConnection(c);
                if (getAssociation) {
                    //TODO get stationCtr to retreive station info
                }
                
                return connection;
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
            }
        }

        public List<MConnection> getAllRecord(Boolean getAssociation)
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

        public void printAll()
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (Connection c in context.Connection)
                {
                    Console.WriteLine(c);
                }
            }
            
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            this.Dispose();
        }

        
    }
}
