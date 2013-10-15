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
    class BookingLineCtr
    {
        private DBookingLine dbBL = new DBookingLine();
        public void addBookingLine(int bId, int btId, int quantity, decimal price)
        {
            dbBL.addRecord(bId, btId, quantity, price);
        }

        public void deleteBookingLine(int bId, int btId)
        {
            dbBL.deleteRecord(bId, btId);
        }

        public void updateBookingLine(int bId, int btId, int quantity, decimal price)
        {
            dbBL.updateRecord(bId, btId, quantity, price);
        }

        public MBookingLine getBookingLine(int bId, int btId, bool getAssociation)
        {
            return dbBL.getRecord(bId, btId, getAssociation);
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

        public void insertAllBLForBooking(List<MBookingLine> bls)
        {
            dbBL.insertAllBookingLineForBooking(bls);
        }
    }
}
