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
        public int addRecord(int CId, decimal TotalPrice, DateTime CreateDate, DateTime TripStart, string CreditCard)
        {
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                try
                {
                    int newid = context.Bookings.Count() + 1;
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
                    return newid;

                }
                catch (Exception)
                {

                    throw new SystemException("Can not add booking");
                }
            }
        }

        public MBooking getRecord(int id, bool getAssociation)
        {
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                try
                {
                    Booking b = context.Bookings.Find(id);
                    MBooking booking = buildBooking(b);
                    if (getAssociation)
                    {
                        //TODO
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
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
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
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
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
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                foreach (Booking b in context.Bookings)
                {
                    MBooking booking = buildBooking(b);
                    if (getAssociation)
                    {
                        //TODO
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
                cId = b.cId, //TODO generate new customer
                totalPrice = b.totalPrice,
                creaditCard = b.creaditCard,
                createDate = b.createDate,
                tripStart = b.tripStart
            };
            return booking;
        }
    }
}
