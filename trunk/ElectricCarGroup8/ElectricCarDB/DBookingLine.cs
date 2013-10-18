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
        private DBatteryType dbBT = new DBatteryType();
        private DStation dbStation = new DStation();
        public void addRecord(int BId, int BtId, int SId, int Quantity, decimal Price, DateTime Time)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                context.BookingLines.Add(new BookingLine()
                {
                    bId = BId,
                    btId = BtId,
                    sId = SId,
                    quantity = Quantity,
                    price = Price,
                    time = Time
                });
                context.SaveChanges();
            }
        }

        public void updateRecord(int bId, int btId, int sId, int quantity, decimal price, DateTime time)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                BookingLine blToDate = context.BookingLines.Find(bId, btId, sId);
                if (blToDate != null)
                {
                    blToDate.quantity = quantity;
                    blToDate.price = price;
                    blToDate.time = time;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find booking line");
                }
            }
        }

        public void deleteRecord(int bId, int btId, int sId)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try 
	            {
                    BookingLine blToDelete = context.BookingLines.Find(bId, btId, sId);
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

        public MBookingLine getRecord(int bId, int btId, int sId, bool getAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    BookingLine b = context.BookingLines.Find(bId, btId, sId);
                    MBookingLine bl = buildBookingLine(b);
                    if (getAssociation)
                    {
                        bl.BatteryType = dbBT.getRecord(btId, true);
                        bl.Station = dbStation.getRecord(sId, false);
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
                BatteryType = new MBatteryType { id = bl.btId },
                Station = new MStation() { Id = bl.sId },
                quantity = bl.quantity,
                price = bl.price,
                time = bl.time
            };
            return b;
        }

        public List<MBookingLine> getBookingLinesForBooking(int bookingid, bool getAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                List<MBookingLine> blForBooking = new List<MBookingLine>();
                BookingLine[] bls;

                var items = from item in context.BookingLines where item.bId == bookingid select item;
                bls = items.ToArray<BookingLine>();

                foreach (BookingLine bl in bls)
                {
                    MBookingLine boookingLine = buildBookingLine(bl);
                    if (getAssociation)
                    {
                        boookingLine.BatteryType = dbBT.getRecord(bl.btId, true);
                        boookingLine.Station = dbStation.getRecord(bl.sId, false);
                    }
                    blForBooking.Add(boookingLine);
                    
                }
                return blForBooking;
            }
        }

        public void deleteAllBookingLineForBooking(int bookingid)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
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
            using (ElectricCarEntities context = new ElectricCarEntities())
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
                                updateRecord(bl.Station.Id, bl.BatteryType.id, bl.Station.Id, bl.quantity.Value, bl.price.Value, bl.time.Value);
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
                addRecord(bl.Station.Id, bl.BatteryType.id, bl.Station.Id, bl.quantity.Value, bl.price.Value, bl.time.Value);
            }
        }
    }
}
