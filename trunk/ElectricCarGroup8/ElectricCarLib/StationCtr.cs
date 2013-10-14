using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarDB;
using ElectricCarModelLayer;
using System.Transactions;
using System.Data;
using System.Data.Objects;
using System.Data.Entity;

namespace ElectricCarLib
{
    class StationCtr
    {
        private IDStation dbStation = new DStation();
        public void addStation(string name, string address, string country, string state)
        {
            dbStation.addNewRecord(name, address, country, state);
        }

        public MStation getStation(int id, bool getAssociation)
        {
            return dbStation.getRecord(id, false);
        }

        public void deleteStation(int id)
        {
            dbStation.deleteRecord(id);
        }

        public void updateStation(int id, string name, string address, string country, string state)
        {
            dbStation.updateRecord(id, name, address, country, state);
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
