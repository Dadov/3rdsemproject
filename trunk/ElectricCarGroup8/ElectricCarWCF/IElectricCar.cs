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

        #region Select path and Booking
        [OperationContract]
        List<Booking> getAllBookings();

        [OperationContract]
        Booking getBooking(int id);
        //search paths

        //add booking

        //update booking

        //delete booking

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
        List<NaborStation> getNaborStations(int id);

        [OperationContract]
        void addNaborStation(int id1, int id2, decimal distance, decimal drivehour);

        [OperationContract]
        void updateNaborStation(int id1, int id2, decimal distance, decimal driveHour);

        [OperationContract]
        void deleteNaborStation(int id1, int id2);

        [OperationContract]
        List<string> getStates();
        
        #endregion

        #region Batteries

        #endregion
    }
}
