using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ElectricCarWCF
{
    [ServiceContract]
    public interface IElectricCar
    {
        #region People

        [OperationContract]
        string getME();

        #endregion 

        #region Booking


        #endregion

        #region Stations
        [OperationContract]
        List<Station> getAllStations();

        [OperationContract]
        void addStation(string name, string address, string country, string state);

        [OperationContract]
        Station getStation(int id);

        [OperationContract]
        void updateStation(int id, string name, string address, string country, string state);

        [OperationContract]
        void deleteStation(int id);

        [OperationContract]
        List<Station> getNaborStations(int id);

        [OperationContract]
        bool addNaborStations(int id1, int id2, decimal distance);

        [OperationContract]
        bool updateNaborStations(int id1, int id2, decimal distance);

        [OperationContract]
        bool deleteNaborStations(int id1, int id2);

        [OperationContract]
        List<string> getStates();
        
        #endregion

        #region Batteries

        #endregion
    }
}
