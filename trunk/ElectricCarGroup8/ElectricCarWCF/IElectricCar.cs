using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ElectricCarModelLayer;

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

        [OperationContract]
        List<BatteryTypeTest> getAllBatteryType();


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

        #region BatteryType
        [OperationContract]
        int addBatteryType(string name, string producer, decimal capacity, decimal exchangeCost, int storageNumber);

        [OperationContract]
        BatteryType getBatteryType(int id);

        [OperationContract]
        void deleteBatteryType(int id);

        [OperationContract]
        void updateBatteryType(int id, string name, string producer, decimal capacity, decimal exchangeCost, int storageNumber);

        [OperationContract]
        List<BatteryType> getAllBatteryTypes();

        [OperationContract]
        List<string> getAllInfoTypes();
        #endregion

        #region BatteryStorage
        [OperationContract]
        int addNewStorage(int btID, int sID);
        
        [OperationContract]
        BatteryStorage getStorage(int id);
      
        [OperationContract]
        void deleteStorage(int id);

        [OperationContract]
        void updateStorage(int id, int btid, int sID);

        [OperationContract]
        List<BatteryStorage> getAllRecord();

        [OperationContract]
        List<string> getAllInfo();
        #endregion

        #region Period
        [OperationContract]
        Period getPeriod(int bsID, DateTime time);

        [OperationContract]
        List<Period> getAllPeriods();

        [OperationContract]
        List<Period> getStoragePeriods(int bsID);

        #endregion

        #endregion
    }
}
