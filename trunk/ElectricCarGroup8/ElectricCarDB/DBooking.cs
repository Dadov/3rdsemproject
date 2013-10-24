using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;
using System.Transactions;
using System.Data;
using System.Data.Objects;
using System.Data.Entity;

namespace ElectricCarDB
{
    public class DBooking : IDBooking
    {
        private DBookingLine dbBL = new DBookingLine();
        private DCustomer dbCustomer = new DCustomer();

        public int addRecord(int CId, decimal TotalPrice, DateTime CreateDate, DateTime TripStart, string CreditCard)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    int newid = -1;
                    
                    using (ElectricCarEntities context = new ElectricCarEntities())
                    {
                        newid = context.Bookings.Count() + 1;
                        context.Bookings.Add(new Booking()
                        {
                        Id = newid,
                        cId = CId,
                        totalPrice = TotalPrice,
                        createDate = CreateDate,
                        tripStart = TripStart,
                        creaditCard = CreditCard
                        });
                    context.SaveChanges();
                    }
                    scope.Complete();
                    return newid;
                }
            }
            catch (TransactionAbortedException)
            {

                throw new SystemException("Can not add new booking");
            }
            
        }

        public MBooking getRecord(int id, bool getAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    Booking b = context.Bookings.Find(id);
                    MBooking booking = buildBooking(b);
                    if (getAssociation)
                    {
                        dbBL.getBookingLinesForBooking(id, true);
                        //dbCustomer.getRecord(b.cId, true); //TODO wait for completion of DCustomer
                    }
                    return booking;
                }
                catch (Exception)
                {

                    throw new System.NullReferenceException("Can not find booking");
                }
            }
        }

        public void updateRecord(int id, int cId, decimal totalPrice, DateTime createDate, DateTime tripStart, string creditCard)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                Booking booUpToDate = context.Bookings.Find(id);
                if (booUpToDate != null)
                {
                    booUpToDate.cId = cId;
                    booUpToDate.totalPrice = totalPrice;
                    booUpToDate.createDate = createDate;
                    booUpToDate.tripStart = tripStart;
                    booUpToDate.creaditCard = creditCard;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find booking");
                }
            }
        }

        public void deleteRecord(int id)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    Booking booToDelete = context.Bookings.Find(id);
                    if (booToDelete != null)
                    {
                        context.Entry(booToDelete).State = EntityState.Deleted;
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    throw new System.NullReferenceException("Can not find booking");
                }

            }
        }


        public List<MBooking> getAllRecord(bool getAssociation)
        {
            List<MBooking> bookings = new List<MBooking>();
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                foreach (Booking b in context.Bookings)
                {
                    MBooking booking = buildBooking(b);
                    if (getAssociation)
                    {
                        dbBL.getBookingLinesForBooking(b.Id, true);
                        //dbCustomer.getRecord(b.cId, true);
                    }
                    bookings.Add(booking);
                }
            }
            return bookings;
        }

        private MBooking buildBooking(Booking b)
        {
            MBooking booking = new MBooking()
            {
                Id = b.Id,
                customer = new MCustomer(){ID = b.cId.Value},
                totalPrice = b.totalPrice,
                creaditCard = b.creaditCard,
                createDate = b.createDate,
                tripStart = b.tripStart
            };
            return booking;
        }
    }
}
