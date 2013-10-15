using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarDB;
using ElectricCarModelLayer;
using System.Transactions;
using System.Data;
using System.Data.Objects;
using System.Data.Entity;

namespace ElectricCarLib
{
    class BookingStationCtr
    {
        private DBookingStation dbBS = new DBookingStation();

        public void addBookingStation(int bId, int sId, DateTime time)
        {
            dbBS.addNewRecord(bId, sId, time);
        }

        public void deleteBookingStation(int bId, int sId)
        {
            dbBS.deleteRecord(bId, sId);
        }

        public MBookingStation getBookingStation(int bId, int sId, bool getAssociation)
        {
            return dbBS.getRecord(bId, sId, getAssociation);
        }

        public void updateBookingStation(int bId, int sId, DateTime time)
        {
            dbBS.updateRecord(bId, sId, time);
        }

        public List<MBookingStation> getAllBSForBooking(int bId, bool getAssociation)
        {
            return dbBS.getAllStationsForBooking(bId, getAssociation);
        }

        public List<MBookingStation> getAllBSForStation(int sId, bool getAssociation)
        {
            return dbBS.getAllBookingsForStation(sId, getAssociation);
        }

        public void updateAllBSForBooking(int bId, List<MBookingStation> bss)
        {
            dbBS.updateAllBSForBooking(bId, bss);
        }

        public void insertAllBSForBooking(int bId, List<MBookingStation> bss)
        {
            dbBS.insertAllBSForBooking(bId, bss);
        }

        public void deleteAllBSForBooking(int bId)
        {
            dbBS.deleteAllBSForBooking(bId);
        }
    }
}
