using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    public interface IDBookingStation
    {
        void addNewRecord(int bId, int sId, DateTime time);
        MBookingStation getRecord(int bId, int sId, bool getAssociation);
        void deleteRecord(int bId, int sId);
        void updateRecord(int bId, int sId, DateTime time);
        List<MBookingStation> getAllStationsForBooking(int bId, bool getAssociation);
        List<MBookingStation> getAllBookingsForStation(int sId, bool getAssociation);
        void insertAllBSForBooking(int bId, List<MBookingStation> bss);
        void updateAllBSForBooking(int bId, List<MBookingStation> bss);
        void deleteAllBSForBooking(int bId);
    }
}
