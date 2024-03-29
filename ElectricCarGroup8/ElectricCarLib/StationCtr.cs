﻿using System;
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

        public List<MConnection> getNaborStations(int id)
        {
            return dbStation.getNaborStations(id);
        }

        public void deleteConnection(int sId1, int sId2)
        {
            dbConnection.deleteRecord(sId1, sId2);
        }

        public void addConnection(int sId1, int sId2, decimal distance, decimal driveHour)
        {
            dbConnection.addNewRecord(sId1, sId2, distance, driveHour);
        }

        public bool isConnectionExist(int id1, int id2)
        {
            return dbConnection.isConnectionExist(id1, id2);
        }

        public void updateConnection(int sId1, int sId2, decimal distance, decimal driveHour)
        {
            dbConnection.updateRecord(sId1, sId2, distance, driveHour);
        }

        public Dictionary<int, Dictionary<int, decimal>> getAdjListWithBatteryLimitForDistance(decimal batteryLimit)
        {
            Dictionary<int, Dictionary<int, decimal>> adjList = dbStation.getAdjListWithBatteryLimitForDistance(batteryLimit);
            return adjList;

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
