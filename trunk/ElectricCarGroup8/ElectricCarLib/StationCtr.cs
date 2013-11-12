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
    public class StationCtr:IDisposable
    {
        private IDStation dbStation = new DStation();
        private IDConnection dbConnection = new DConnection();
        public void addStation(string name, string address, string country, string state)
        {
            dbStation.addNewRecord(name, address, country, state);
        }

        public List<MStation> getAllStation()
        {
            List<MStation> stations = dbStation.getAllRecord(false);
            return stations;
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

        

        public Dictionary<MStation, Dictionary<MStation, decimal>> adjListWithWeight()
        {
            Dictionary<MStation, Dictionary<MStation, decimal>> adjList = new Dictionary<MStation, Dictionary<MStation, decimal>>();
            List<MStation> stations = dbStation.getAllRecord(false);
            foreach (MStation s in stations)
            {
                Dictionary<MStation, decimal> naborStations = dbStation.getNaborStationsWithDriveHour(s.Id);
                adjList.Add(s, naborStations);
            }
            return adjList;
        }

        public Dictionary<MStation, LinkedList<MStation>> adjListWithoutWeight()
        {
            Dictionary<MStation, LinkedList<MStation>> adjList = new Dictionary<MStation, LinkedList<MStation>>();
            List<MStation> stations = dbStation.getAllRecord(false);
            foreach (MStation s in stations)
            {
                LinkedList<MStation> naborStations = dbStation.getNaborStationsWithoutDriveHour(s.Id);
                adjList.Add(s, naborStations);
            }
            return adjList;
        }

        public List<string> getStates()
        {
            List<string> states = new List<string>();
            foreach (var item in Enum.GetValues(typeof(State)))
            {
                states.Add(item.ToString());
            }
            return states;
        }

        public void Dispose()
        {
            
        }
    }
}
