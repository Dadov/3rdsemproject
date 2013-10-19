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
        private BookingLineCtr blCtr = new BookingLineCtr();
        private BatteryStorageCtr bsCtr = new BatteryStorageCtr();
        public void addBooking(MBooking booking) 
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //validate period for specific battery type
                foreach (MBookingLine item in booking.bookinglines)
                {
                    if (!bsCtr.validateBookingForStation(item.Station.Id, item.BatteryType.id, item.quantity.Value, item.time.Value))
                    {
                        throw new SystemException("Booking fail because one of the stations is fully booked");
                    }
                }

                int bId = addBookingRecord(booking.cId.Value, booking.totalPrice.Value, booking.createDate.Value, booking.tripStart.Value, booking.creaditCard);
                //decrease the number in Period
                foreach (MBookingLine item in booking.bookinglines)
                {
                    bsCtr.addBookingForStation(item.Station.Id, item.BatteryType.id, item.quantity.Value, item.time.Value);
                }
                
                blCtr.insertAllBLForBooking(booking.bookinglines);

                scope.Complete();

            }
        }


        public MBooking getBooking(int id)
        {
            MBooking b = dbBooking.getRecord(id, true);
            return b;
        }

        public void deleteBooking(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                MBooking b = getBooking(id);
                foreach (MBookingLine item in b.bookinglines)
                {
                    bsCtr.deleteBookingForStation(item.Station.Id, item.BatteryType.id, item.quantity.Value, item.time.Value);
                }

                blCtr.deleteAllBLForBooking(id);
                dbBooking.deleteRecord(id);
                scope.Complete();
            }
        }

        public void updateBooking(MBooking booking)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //validate whether update is valid
                foreach (MBookingLine item in booking.bookinglines)
                {
                    if (!bsCtr.validateUpdateBookingForStation(item.Station.Id, item.BatteryType.id, item.quantity.Value, item.time.Value))
                    {
                        throw new SystemException("Update booking fail because one of the stations is fully booked");
                    }
                }
                updateBookingRecord(booking.Id, booking.cId.Value, booking.totalPrice.Value, booking.createDate.Value, booking.tripStart.Value, booking.creaditCard);
                foreach (MBookingLine item in booking.bookinglines)
                {
                    bsCtr.updateBookingForStation(item.Station.Id, item.BatteryType.id, item.quantity.Value, item.time.Value);
                }
                blCtr.updateAllBLForBooking(booking.Id, booking.bookinglines);
                scope.Complete();
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
