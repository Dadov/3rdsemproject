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
        Station getStation(int id);

        [OperationContract]
        bool updateStation(Station station);

        [OperationContract]
        bool deleteStation(int id);

        [OperationContract]
        List<Station> getNaborStations(int id);

        [OperationContract]
        bool addNaborStations(int id1, int id2, decimal distance);

        [OperationContract]
        bool updateNaborStations(int id1, int id2, decimal distance);

        [OperationContract]
        bool deleteNaborStations(int id1, int id2);
        
        #endregion

        #region Batteries

        #endregion
    }
}
