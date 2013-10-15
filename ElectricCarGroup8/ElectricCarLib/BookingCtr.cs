using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ElectricCarDB;
using ElectricCarModelLayer;

namespace ElectricCarLib
{
    class BookingCtr
    {
        private DBooking dbBooking = new DBooking();
        private BookingStationCtr bsCtr = new BookingStationCtr();
        private BookingLineCtr blCtr = new BookingLineCtr();
        public void addBooking(MBooking booking) 
        {
            using (TransactionScope trsaction = new TransactionScope())
            {
                //TODO validate period for specific battery type

                int bId = addBookingRecord(booking.cId.Value, booking.totalPrice.Value, booking.createDate.Value, booking.tripStart.Value, booking.creaditCard);
                blCtr.insertAllBLForBooking(booking.bookinglines);
                bsCtr.insertAllBSForBooking(bId, booking.bookingstations);
            }
        }

        public MBooking getBooking(int id)
        {
            MBooking b = dbBooking.getRecord(id, true);
            return b;
        }

        public void deleteBooking(int id)
        {
            blCtr.deleteAllBLForBooking(id);
            bsCtr.deleteAllBSForBooking(id);
            dbBooking.deleteRecord(id);
        }

        public void updateBooking(MBooking booking)
        {
            using (TransactionScope trsaction = new TransactionScope())
            {
                //TODO validate whether update is valid

                updateBookingRecord(booking.Id, booking.cId.Value, booking.totalPrice.Value, booking.createDate.Value, booking.tripStart.Value, booking.creaditCard);
                blCtr.updateAllBLForBooking(booking.Id, booking.bookinglines);
                bsCtr.updateAllBSForBooking(booking.Id, booking.bookingstations);
            }
        }

        public int addBookingRecord(int cId, decimal price, DateTime createDate, DateTime tripStart, string creditCard)
        {
            return dbBooking.addRecord(cId, price, createDate, tripStart, creditCard);
        }

        public void deleteBookingRecord(int id)
        {
            dbBooking.deleteRecord(id);
        }

        public void updateBookingRecord(int bId, int cId, decimal price, DateTime createDate, DateTime tripStart, string creditCard)
        {
            dbBooking.updateRecord(bId, cId, price, createDate, tripStart, creditCard);
        }
    }
}
