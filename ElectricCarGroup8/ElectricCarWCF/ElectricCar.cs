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
            throw new NotImplementedException();
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

        public List<Station> getNaborStations(int id)
        {
            throw new NotImplementedException();
        }

        public bool addNaborStations(int id1, int id2, decimal distance)
        {
            throw new NotImplementedException();
        }

        public bool updateNaborStations(int id1, int id2, decimal distance)
        {
            throw new NotImplementedException();
        }

        public bool deleteNaborStations(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public List<string> getStates()
        {
            StationCtr sCtr = new StationCtr();
            return sCtr.getStates();
        }

        #endregion
    }
}
