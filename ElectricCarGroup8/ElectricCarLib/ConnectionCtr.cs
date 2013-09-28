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
            IDBConnection dbConnection = new DBConnection();
            dbConnection.addNewRecord(id1, id2, dist, time);

        }

        public MConnection getRecord(int id1, int id2, Boolean getAssociation)
        {
            IDBConnection dbConnection = new DBConnection();
            return dbConnection.getRecord(id1, id2, false);//TODO do not get association at the moment
        }

        public void deleteRecord(int id1, int id2)
        {
            IDBConnection dbConnection = new DBConnection();
            dbConnection.deleteRecord(id1, id2);
        }

        public void updateRecord(int id1, int id2, decimal dist, decimal time)
        {
            IDBConnection dbConnection = new DBConnection();
            dbConnection.updateRecord(id1, id2, dist, time);
        }

        public List<MConnection> getAllRecord(Boolean getAssociation)
        {
            IDBConnection dbConnection = new DBConnection();
            return dbConnection.getAllRecord(false); // TODO do not get association at the moment
        }

        public List<string> getAllInfo()
        {
            IDBConnection dbConnection = new DBConnection();
            return dbConnection.getAllInfo();
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
