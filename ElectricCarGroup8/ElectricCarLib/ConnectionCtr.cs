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
        public void addNewRecord(int id1, int id2, decimal dist, decimal time) 
        {
            IDConnection dbConnection = new DConnection();
            dbConnection.addNewRecord(id1, id2, dist, time);

        }

        public MConnection getRecord(int id1, int id2, Boolean getAssociation)
        {
            IDConnection dbConnection = new DConnection();
            return dbConnection.getRecord(id1, id2, false);//TODO do not get association at the moment
        }

        public void deleteRecord(int id1, int id2)
        {
            IDConnection dbConnection = new DConnection();
            dbConnection.deleteRecord(id1, id2);
        }

        public void updateRecord(int id1, int id2, decimal dist, decimal time)
        {
            IDConnection dbConnection = new DConnection();
            dbConnection.updateRecord(id1, id2, dist, time);
        }

        public List<MConnection> getAllRecord(Boolean getAssociation)
        {
            IDConnection dbConnection = new DConnection();
            return dbConnection.getAllRecord(false); // TODO do not get association at the moment
        }

        public List<string> getAllInfo()
        {
            IDConnection dbConnection = new DConnection();
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
