﻿using System;
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
    public class DBStation: IDBStation
    {
        public int addNewRecord(string Name, string Address, string Country, string State)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    int newid = context.Station.Count() + 1;
                    context.Station.Add(new Station()
                    {
                        Id = newid,
                        name = Name,
                        address = Address,
                        country = Country,
                        state = State
                    });
                    context.SaveChanges();
                    return newid;
                }
                catch (Exception)
                {

                    throw new SystemException("Can not add association between two stations");
                }
                

            }
        }

        public MStation getRecord(int id, Boolean getAssocitation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    Station s = context.Station.Find(id);
                    MStation station = buildStation(s);
                    if (getAssocitation)
                    {
                        //TODO get association
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
                    Station staToDelete = context.Station.Find(id);
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
                try
                {
                    Station s = context.Station.Find(id);
                    context.Station.Add(new Station()
                    {
                        name = Name,
                        address = Address,
                        country = Country,
                        state = State
                    });
                    context.SaveChanges();
                }
                catch (Exception)
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
                foreach (Station s in context.Station)
                {
                    MStation station = buildStation(s);
                    if (getAssociation)
                    {
                        //TODO
                    }
                    stations.Add(station);
                }
            }
            return stations;
        }
        public MStation buildStation(Station s)
        {
            MStation station = new MStation()
            {
                Id = s.Id,
                name = s.name,
                address = s.address,
                country = s.country,
                state = s.state
            };
            return station;
        }
    }
}