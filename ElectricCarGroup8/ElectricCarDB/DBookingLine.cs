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
    public class DBookingLine: IDBookingLine
    {
        public void addRecord(int BId, int BtId, int Quantity, decimal Price)
        {
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                context.BookingLines.Add(new BookingLine()
                {
                    bId = BId,
                    btId = BtId,
                    quantity = Quantity,
                    price = Price
                });
                context.SaveChanges();
            }
        }

        public void updateRecord(int bId, int btId, int quantity, decimal price)
        {
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                BookingLine blToDate = context.BookingLines.Find(bId, btId);
                if (blToDate != null)
                {
                    blToDate.quantity = quantity;
                    blToDate.price = price;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find booking line");
                }
            }
        }

        public void deleteRecord(int bId, int btId)
        {
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                try 
	            {
                    BookingLine blToDelete = context.BookingLines.Find(bId, btId);
                    if (blToDelete != null)
                    {
                        context.Entry(blToDelete).State = EntityState.Deleted;
                        context.SaveChanges();
                    }
	            }
	            catch (Exception)
	            {

                    throw new System.NullReferenceException("Can not find booking line");
	            }
            }
        }

        public MBookingLine getRecord(int bId, int btId, bool getAssociation)
        {
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                try
                {
                    BookingLine b = context.BookingLines.Find(bId, btId);
                    MBookingLine bl = buildBookingLine(b);
                    if (getAssociation)
                    {
                        //TODO get association
                    }
                    return bl;
                }
                catch (Exception)
                {
                    
                    throw new System.NullReferenceException("Can not find booking line");
                }
            }
        }

        public MBookingLine buildBookingLine(BookingLine bl)
        {
            MBookingLine b = new MBookingLine()
            {
                Booking = new MBooking() {Id = bl.bId },
                BatteryType = new MBatteryType { id = bl.btId },
                quantity = bl.quantity,
                price = bl.price,
            };
            return b;
        }

        public List<MBookingLine> getBookingLinesForBooking(int bookingid, bool getAssociation)
        {
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                List<MBookingLine> blForBooking = new List<MBookingLine>();
                BookingLine[] bls;

                var items = from item in context.BookingLines where item.bId == bookingid select item;
                bls = items.ToArray<BookingLine>();

                foreach (BookingLine bl in bls)
                {
                    blForBooking.Add(buildBookingLine(bl));
                    if (getAssociation)
                    {
                        //TODO
                    }
                    
                }
                return blForBooking;
            }
        }

        public void deleteAllBookingLineForBooking(int bookingid)
        {
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                try
                {
                    BookingLine[] blsToDelete;
                    var items = from item in context.BookingLines where item.bId == bookingid select item;
                    blsToDelete = items.ToArray<BookingLine>();

                    if (blsToDelete != null)
                    {
                        foreach (BookingLine item in blsToDelete)
                        {
                            context.Entry(item).State = EntityState.Deleted;
                        }
                        
                    }
                    context.SaveChanges();
                }
                catch (Exception)
                {

                    throw new System.NullReferenceException("Can not find booking line");
                }
            }
        }

        public void updateAllBookingLineForBooking(int bookingid, List<MBookingLine> bls)
        {
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                try
                {
                    bool success = false;
                    using (TransactionScope transaction = new TransactionScope())
                    {
                        try
                        {
                            deleteAllBookingLineForBooking(bookingid);
                            foreach (MBookingLine bl in bls)
                            {
                                updateRecord(bl.Booking.Id, bl.BatteryType.id, bl.quantity.Value, bl.price.Value);
                            }
                            transaction.Complete();
                            success = true;
                        }
                        catch (Exception)
                        {

                            throw new SystemException("Not able finish update for bookingline, please try again");
                        }
                    }

                    if (success)            
                    {
                        // Reset the context since the operation succeeded.
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new SystemException("Not able finish update for bookingline, please try again");
                    }
                    

                    context.SaveChanges();
                }
                catch (Exception)
                {

                    throw new System.NullReferenceException("Can not find booking line");
                }
            }
        }

        public void insertAllBookingLineForBooking(List<MBookingLine> bls)
        {
            foreach (MBookingLine bl in bls)
            {
                addRecord(bl.Booking.Id, bl.BatteryType.id, bl.quantity.Value, bl.price.Value);
            }
        }
    }
}
