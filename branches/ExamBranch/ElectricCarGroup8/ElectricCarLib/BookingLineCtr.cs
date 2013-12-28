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
    public class BookingLineCtr
    {
        private DBookingLine dbBL = new DBookingLine();
        public void addBookingLine(int bId, int btId, int sId, int quantity, decimal price, DateTime time)
        {
            dbBL.addRecord(bId, btId, sId, quantity, price, time);
        }

        public void deleteBookingLine(int bId, int btId, int sId)
        {
            dbBL.deleteRecord(bId, btId, sId);
        }

        public void updateBookingLine(int bId, int btId, int sId, int quantity, decimal price, DateTime time)
        {
            dbBL.updateRecord(bId, btId, sId, quantity, price, time);
        }

        public MBookingLine getBookingLine(int bId, int btId, int sId, bool getAssociation)
        {
            return dbBL.getRecord(bId, btId, sId, getAssociation);
        }

        public List<MBookingLine> getAllBLForBooking(int bId, bool getAssociation)
        {
            return dbBL.getBookingLinesForBooking(bId, getAssociation);
        }

        public void updateAllBLForBooking(int bId, List<MBookingLine> bls)
        {
            dbBL.updateAllBookingLineForBooking(bId, bls);
        }

        public void deleteAllBLForBooking(int bId)
        {
            dbBL.deleteAllBookingLineForBooking(bId);
        }

        public void insertAllBLForBooking(int bId, List<MBookingLine> bls)
        {
            dbBL.insertAllBookingLineForBooking(bId, bls);
        }

        public List<MBookingLine> getBookingLinesForStation(int sId, bool association)
        {
            return dbBL.getBookingLinesForStation(sId, association);
        }

        public List<MBookingLine> getBookingLinesForDateInStation(int sId, DateTime date, bool association)
        {
            return dbBL.getBookingLinesForDateInStation(sId, date, association);
        }
    }
}
