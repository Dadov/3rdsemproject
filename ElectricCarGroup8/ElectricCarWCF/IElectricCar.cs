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

        #region Employees

        [OperationContract]
        void addEmployee(string fname, string lname, string address, string country, string phone,
            string email, string password, int stationId, string position);

        [OperationContract]
        Employee getEmployee(int id);

        [OperationContract]
        List<Employee> getAllEmployees();

        /*
        [OperationContract]
        void updateEmployee(int id, string fname, string lname, string address, string country,
            string phone, string email, List<LogInfo> logInfos, int stationId, string position);
        */
        [OperationContract]
        void updateEmployee(Employee employee);
        
        [OperationContract]
        void deleteEmployee(int id);

        #endregion

        #region Customers

        [OperationContract]
        void addCustomer(string fname, string lname, string address, string country, string phone,
            string email, string password, string payStatus, DiscountGroup discountGroup);

        /*
        [OperationContract]
        void addCustomer(Customer customer);
        */

        [OperationContract]
        Customer getCustomer(int id);

        [OperationContract]
        List<Customer> getAllCustomers();

        /*
        [OperationContract]
        void updateCustomer(int id, string fname, string lname, string address, string country,
            string phone, string email, string password, List<LogInfos> logInfos, string payStatus, 
            DiscountGroup discountGroup);
        */

        [OperationContract]
        void updateCustomer(Customer customer);

        [OperationContract]
        void deleteCustomer(int id);

        #endregion

        #region Log Infos

        [OperationContract]
        void addLogInfo(string loginName, string password, int personId);

        [OperationContract]
        List<LogInfo> getPersonLogInfos(int id);

        /*
        [OperationContract]
        void updateLogInfo(int id, string loginName, string password);
        */
        [OperationContract]
        void updateLogInfo(LogInfo logInfo);

        [OperationContract]
        void deleteLogInfo(int id);

        #endregion

        #region Discount Groups

        [OperationContract]
        void addDiscountGroup(string name, decimal discount);

        [OperationContract]
        DiscountGroup getDiscoutGroup(int id);

        [OperationContract]
        List<DiscountGroup> getAllDiscountGroups();

        /*
        [OperationContract]
        void updateDiscountGroup(int id, string name, decimal discount);
        */
        [OperationContract]
        void updateDiscountGroup(DiscountGroup discountGroup);

        [OperationContract]
        void deleteDiscountGroup(int id);

        #endregion

        #endregion

        #region Select path and Booking
        [OperationContract]
        List<Booking> getAllBookings();

        [OperationContract]
        Booking getBooking(int id);
        //search paths
        [OperationContract]
        List<List<RouteStop>> getRoutes(int startSId, int endSIdint, DateTime tripStart, decimal batteryLimit);

        [OperationContract]
        decimal convertCapacityToDistance(decimal capacity);
        //add booking
        [OperationContract]
        void addBooking(Booking b);
        //update booking
        [OperationContract]
        void updateBooking(Booking b);
        //delete booking
        [OperationContract]
        void deleteBooking(int bId);

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

        [OperationContract]
        List<BookingLine> getBookingLinesForStation(int sId);
        [OperationContract]
        List<BookingLine> getBookingLinesForDateInStation(int sId, DateTime date);
        
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
        void deleteStorageByType(int btID);

        [OperationContract]
        void updateStorage(int id, int btid, int sID);

        [OperationContract]
        List<BatteryStorage> getStationStorages(int sID);

        [OperationContract]
        List<BatteryStorage> getAllStorages();

        [OperationContract]
        List<string> getAllStorageInfo();
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
