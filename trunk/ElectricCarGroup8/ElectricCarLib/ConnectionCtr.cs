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

using ElectricCarModelLayer;

namespace ElectricCarLib
{
    public class ConnectionCtr: IDisposable
    {
        private IDConnection dbConnection = new DConnection();
        public void addNewRecord(int id1, int id2, decimal dist, decimal time) 
        {
            
            dbConnection.addNewRecord(id1, id2, dist, time);

        }

        public MConnection getRecord(int id1, int id2, bool getAssociation)
        {
            
            return dbConnection.getRecord(id1, id2, false);//TODO do not get association at the moment
        }

        public void deleteRecord(int id1, int id2)
        {
            
            dbConnection.deleteRecord(id1, id2);
        }

        public void updateRecord(int id1, int id2, decimal dist, decimal time)
        {
            
            dbConnection.updateRecord(id1, id2, dist, time);
        }

        public List<MConnection> getAllRecord(Boolean getAssociation)
        {
            
            return dbConnection.getAllRecord(false); // TODO do not get association at the moment
        }

        public List<string> getAllInfo()
        {
            
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
