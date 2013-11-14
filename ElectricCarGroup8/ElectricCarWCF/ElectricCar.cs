using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ElectricCarLib;
using ElectricCarModelLayer;

namespace ElectricCarWCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class ElectricCar : IElectricCar
    {
        #region People

        public string getME()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Statons
        public List<Station> getAllStations()
        {
            using (StationCtr sCtr = new StationCtr())
            {
                List<Station> ss = new List<Station>();
                List<MStation> stations = sCtr.getAllStation();
                foreach (MStation item in stations)
                {
                    Station s = new Station() { Id = item.Id, Name = item.name, Address = item.address, Country = item.country, State = item.state.ToString() };
                    ss.Add(s);
                    
                }
                return ss;
            }
        }

        public Station getStation(int id)
        {
            StationCtr sCtr = new StationCtr();
            MStation s = sCtr.getStation(id, false);
            Station ns = new Station();
            if (s != null)
            {
                ns.Id = s.Id;
                ns.Name = s.name;
                ns.State = s.state.ToString();
                ns.Country = s.country;
                ns.Address = s.address;
            }
            
            return ns;
        }

        public void addStation(string name, string address, string country, string state)
        {
            StationCtr sCtr = new StationCtr();
            sCtr.addStation(name, address, country, state);
        }

        public void updateStation(int id, string name, string address, string country, string state)
        {
            StationCtr sCtr = new StationCtr();
            sCtr.updateStation(id, name, address, country, state);
        }

        public void deleteStation(int id)
        {
            StationCtr sCtr = new StationCtr();
            sCtr.deleteStation(id);
        }

        public List<NaborStation> getNaborStations(int id)
        {
            StationCtr sCtr = new StationCtr();
            List<NaborStation> nbs = new List<NaborStation>();
            List<MConnection> cs = sCtr.getNaborStations(id);
            foreach (MConnection c in cs)
            {
                NaborStation n = new NaborStation();
                if (c.Station1 != null)
                {
                    n.Id = c.Station1.Id;
                    n.Name = c.Station1.name;
                    n.Address = c.Station1.address;
                }
                else
                {
                    n.Id = c.Station2.Id;
                    n.Name = c.Station2.name;
                    n.Address = c.Station2.address;
                }
                n.Distance = Convert.ToDouble(c.distance);
                n.DriveHour = Convert.ToDouble(c.driveHour);
                nbs.Add(n);
            }
            return nbs;
        }

        public void addNaborStation(int id1, int id2, decimal distance, decimal drivehour)
        {
            StationCtr sCtr = new StationCtr();
            sCtr.addConnection(id1, id2, distance, drivehour);
        }

        public void updateNaborStation(int id1, int id2, decimal distance, decimal driveHour)
        {
            StationCtr sCtr = new StationCtr();
            sCtr.updateConnection(id1, id2, distance, driveHour);
        }

        public void deleteNaborStation(int id1, int id2)
        {
            StationCtr sCtr = new StationCtr();
            sCtr.deleteConnection(id1, id2);
        }

        public List<string> getStates()
        {
            StationCtr sCtr = new StationCtr();
            return sCtr.getStates();
        }

        #endregion
    }
}
