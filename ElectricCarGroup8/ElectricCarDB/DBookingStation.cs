using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;
using System.Data.Entity;
using System.Transactions;
using System.Data;
using System.Data.Objects;

namespace ElectricCarDB
{
    public class DBookingStation: IDBookingStation
    {
        public void addNewRecord(int BId, int SId, DateTime Time)
        {
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                try
                {
                    context.Booking_Station.Add(new Booking_Station() 
                    {
                        bId = BId,
                        sId = SId,
                        bookedTime = Time
                    });
                    context.SaveChanges();

                }
                catch (Exception)
                {

                    throw new SystemException("Can not add association between station and booking");
                }
            }
        }

        public MBookingStation getRecord(int bId, int sId, bool getAssociation)
        {
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                try
                {
                    Booking_Station bs = context.Booking_Station.Find(bId, sId);
                    MBookingStation b_s = buildBookingStation(bs);
                    if (getAssociation)
                    {
                        //TODO
                    }
                    return b_s;
                }
                catch (Exception)
                {

                    throw new System.NullReferenceException("Can not find association between boooking and station");
                }
            }
        }

        public void deleteRecord(int bId, int sId)
        {
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                try 
	            {	        
		            Booking_Station bsToDelete = context.Booking_Station.Find(bId, sId);
                    if (bsToDelete != null)
                    {
                        context.Entry(bsToDelete).State = EntityState.Deleted;
                        context.SaveChanges();
                    }
	            }
	            catch (Exception)
	            {
		
		            throw new System.NullReferenceException("Can not find association between booking and station");
	            }
            }
        }

        public void updateRecord(int bId, int sId, DateTime time)
        {
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                Booking_Station bsToDate = context.Booking_Station.Find(bId, sId);
                if (bsToDate != null)
                {
                    bsToDate.bookedTime = time;
                    context.SaveChanges();
                }
                else
                {
                    throw new System.NullReferenceException("Can not find association between booking and station");
                }
            }
        }

        public List<MBookingStation> getAllStationsForBooking(int bId, bool getAssociation)
        {
            List<MBookingStation> bss = new List<MBookingStation>();
            using(ElectricCarEntities2 context = new ElectricCarEntities2())
	        {
                var items = from item in context.Booking_Station where item.bId == bId select item;
                Booking_Station[] b_ss = items.ToArray<Booking_Station>();
                foreach (Booking_Station item in b_ss)
                {
                    bss.Add(buildBookingStation(item));
                    if (getAssociation)
                    {
                        //TODO
                    }
                }
	        }
            return bss;
            
        }

        public List<MBookingStation> getAllBookingsForStation(int sId, bool getAssociation)
        {
            List<MBookingStation> bss = new List<MBookingStation>();
            using (ElectricCarEntities2 context = new ElectricCarEntities2())
            {
                var items = from item in context.Booking_Station where item.sId == sId select item;
                Booking_Station[] b_ss = items.ToArray<Booking_Station>();
                foreach (Booking_Station item in b_ss)
                {
                    bss.Add(buildBookingStation(item));
                    if (getAssociation)
                    {
                        //TODO
                    }
                }
            }
            return bss;
        }

        public MBookingStation buildBookingStation(Booking_Station bs)
        {
            MBookingStation b_s = new MBookingStation()
            {
                Station = new MStation(){Id = bs.sId},
                Booking = new MBooking(){Id = bs.bId},
                bookedTime = bs.bookedTime
            };
            return b_s;
        }
    }
}
