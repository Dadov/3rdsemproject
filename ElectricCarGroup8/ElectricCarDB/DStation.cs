﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;
using System.Transactions;
using System.Data;
using System.Data.Objects;
using System.Data.Entity;
using System.Collections;

namespace ElectricCarDB
{
    public class DStation : IDStation
    {
        private DBBatteryStorage dbStorage = new DBBatteryStorage();

        public int addNewRecord(string Name, string Address, string Country, string State)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope()) //open transaction
                {
                    int newid = -1;
                    using (ElectricCarEntities context = new ElectricCarEntities())
                    {
                        if (context.Stations.Count() == 0)
                        {
                            newid = 1;
                        }
                        else
                        {
                            newid = context.Stations.Max(x => x.Id) + 1;
                        } 
                        context.Stations.Add(new Station()
                        {
                            Id = newid,
                            name = Name,
                            address = Address,
                            country = Country,
                            state = State
                        });
                        context.SaveChanges();  //update station table
                    }
                    scope.Complete(); //close transaction
                    return newid;
                }

            }
            catch (TransactionAbortedException)
            {
                throw new SystemException("Can not add new station");
            }


        }

        public MStation getRecord(int id, bool getAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    Station s = context.Stations.Find(id);
                    MStation station = buildStation(s);
                    if (getAssociation)
                    {
                        station.storages = dbStorage.getStationStorages(id);
                        station.naboStations = getNaborStationsWithDriveHour(id);
                    }
                    return station;
                }
                catch (Exception)
                {
                    throw new System.NullReferenceException("Can not find nabor station");
                }


            }
        }

        public void deleteRecord(int id)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    Station staToDelete = context.Stations.Find(id);
                    if (staToDelete != null)
                    {
                        context.Entry(staToDelete).State = EntityState.Deleted;
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    throw new System.NullReferenceException("Can not find nabor station");
                }

            }
        }

        public void updateRecord(int id, string Name, string Address, string Country, string State)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                Station staUpToDate = context.Stations.Find(id);
                if (staUpToDate != null)
                {
                    staUpToDate.name = Name;
                    staUpToDate.address = Address;
                    staUpToDate.country = Country;
                    staUpToDate.state = State;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find nabor station");
                }
            }
        }

        public List<MStation> getAllRecord(bool getAssociation)
        {
            List<MStation> stations = new List<MStation>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (Station s in context.Stations)
                {
                    MStation station = buildStation(s);
                    if (getAssociation)
                    {
                        station.storages = dbStorage.getStationStorages(s.Id);
                        station.naboStations = getNaborStationsWithDriveHour(s.Id);
                    }
                    stations.Add(station);
                }
            }
            return stations;
        }

        public List<MConnection> getNaborStations(int id)
        {
            List<MConnection> connections = new List<MConnection>();
            
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                var cs = from c in context.Connections where c.sId1 == id || c.sId2 == id select c;
                foreach (var c in cs)
                {
                    MConnection mc = new MConnection();
                    if (c.sId1 == id)
                    {
                        mc.Station2 = getRecord(c.sId2, false);
                    } else
	                {
                        mc.Station2 = getRecord(c.sId1, false);
	                }
                    mc.distance = c.distance;
                    mc.driveHour = c.driveHour;
                    connections.Add(mc);
                }
            }
            return connections;
        }

        public Dictionary<int, Dictionary<int, decimal>> getAdjListWithBatteryLimitForDistance(decimal batteryLimit)
        {
            Dictionary<int, Dictionary<int, decimal>> adjList = new Dictionary<int,Dictionary<int,decimal>>();
            //return id adjList with limit of batteryLimit
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                var connections = from c in context.Connections where c.distance<= batteryLimit select c;
                foreach (var c in connections)
                {
                    if (!adjList.Keys.Contains(c.sId1))
                    {
                        Dictionary<int, decimal> list = new Dictionary<int, decimal>();
                        list.Add(c.sId2, c.distance.Value);
                        adjList.Add(c.sId1, list);
                    }
                    else
                    {
                        adjList[c.sId1].Add(c.sId2, c.distance.Value);
                    }
                    if (!adjList.Keys.Contains(c.sId2))
                    {
                        Dictionary<int, decimal> list = new Dictionary<int, decimal>();
                        list.Add(c.sId1, c.distance.Value);
                        adjList.Add(c.sId2, list);
                    }
                    else
                    {
                        adjList[c.sId2].Add(c.sId1, c.distance.Value);
                    }

                }
            }
            return adjList;
        }

        public Dictionary<MStation, decimal> getNaborStationsWithDriveHour(int id)
        {
            Dictionary<MStation, decimal> nStations = new Dictionary<MStation, decimal>();
            nStations.Add(getRecord(id, false), 0);
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                var connections = from c in context.Connections where c.sId1 == id || c.sId2 == id select c;
                foreach (var c in connections)
                {
                    MStation sToAdd = new MStation();
                    decimal driveHour = c.driveHour.Value;
                    if (c.sId1 == id)
                    {
                        sToAdd = getRecord(c.sId2, false);
                    }
                    else
                    {
                        sToAdd = getRecord(c.sId1, false);
                    }
                    nStations.Add(sToAdd, driveHour);
                }

            }
            return nStations;
        }

        public LinkedList<MStation> getNaborStationsWithoutDriveHour(int id)
        {
            LinkedList<MStation> nStations = new LinkedList<MStation>();
            nStations.AddFirst(getRecord(id, false));
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                var connections = from c in context.Connections where c.sId1 == id || c.sId2 == id select c;
                foreach (var c in connections)
                {
                    MStation sToAdd = new MStation();

                    if (c.sId1 == id)
                    {
                        sToAdd = getRecord(c.sId2, false);
                    }
                    else
                    {
                        sToAdd = getRecord(c.sId1, false);
                    }
                    nStations.AddLast(sToAdd);
                }

            }
            return nStations;
        }

        public MStation buildStation(Station s)
        {
            MStation station = new MStation()
            {
                Id = s.Id,
                name = s.name,
                address = s.address,
                country = s.country,
                state = (State)Enum.Parse(typeof(State), s.state),
            };
            return station;
        }
    }
}
