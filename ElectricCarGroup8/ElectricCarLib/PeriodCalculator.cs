using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;
using ElectricCarDB;

namespace ElectricCarLib
{
    public class PeriodCalculator
    {
        private IDPeriod dbPeriod = new DBPeriod();
        private IDBBatteryStorage dbStorage = new DBBatteryStorage();
      
        //This method adds hours according to the capacity of given battery type
        public DateTime getTime(MBatteryStorage storage)
        {
            int count = storage.periods.Count;
            DateTime firstPeriod = storage.periods[count-1].time;
            int capacity = (int) storage.type.capacity;
            DateTime secondPeriod = firstPeriod.AddHours(capacity);
            return secondPeriod;
        }

        //This method will return number of batteries of given type for one storage
        public int getInitNumber(MBatteryStorage storage)
        {
            List<MPeriod> pers = storage.periods;
            int count = pers.Count;
            //(an = bn-2 + an-1 - bn-1) 
            int bn2 = 0;
            if (count >= 2) bn2 = pers[count - 2].bookedBatteryNumber;
            int an1 = pers[count - 1].initBatteryNumber;
            int bn1 = pers[count - 1].bookedBatteryNumber;
            int init =  bn2 + an1 - bn1;
            return init;
        }

        public int setBookedBatteries(MBatteryStorage storage)
        {
            return storage.periods[storage.periods.Count - 1].futureBatteryNumber;
        }

        public MPeriod createPeriod(MBatteryStorage storage)
        {
            storage = dbStorage.getRecord(storage.id, true);
            MPeriod newPeriod = new MPeriod();
            newPeriod.time = getTime(storage);
            newPeriod.initBatteryNumber = getInitNumber(storage);
            newPeriod.bookedBatteryNumber = setBookedBatteries(storage);
            dbPeriod.addNewRecord(storage.id, newPeriod.time, newPeriod.initBatteryNumber, newPeriod.bookedBatteryNumber, newPeriod.futureBatteryNumber);
            return newPeriod;
        }

        //This method returns period for given booking
        public MPeriod getBookingPeriod(MBatteryStorage storage, DateTime time)
        {
            List<MPeriod> periods = dbPeriod.getStoragePeriods(storage.id,true);
            MPeriod lastPeriod = periods[periods.Count - 1];
            if (time.CompareTo(lastPeriod.time) > 0)//if time of booking is earlier or in the same time then time of last period
            {
                while (time.CompareTo(lastPeriod.time) > 0) //while time of booking is earlier or in the same time then time of last period
                {
                    lastPeriod = createPeriod(storage);//create new period
                }
            }
            else //if time of booking is later as time of last period
            {
                for (int x = periods.Count - 1; x >= 1; x--) //for periods from last to first
                {
                    MPeriod next = periods[x]; //last created period
                    MPeriod curr = periods[x - 1]; //second last period
                    if ((time.CompareTo(curr) >= 0) & (time.CompareTo(next) < 0)) //if time of booking is later then current and earlier then next period
                    {
                        return curr;
                    }
                }
             }
            return lastPeriod;               
        }

        public MPeriod getPreviousPeriod(MBatteryStorage storage, MPeriod current)
        {
           List<MPeriod> periods = dbPeriod.getStoragePeriods(storage.id, true);
           int x = periods.Count;
            bool found = false;
            MPeriod previous = new MPeriod();
            while(!found || x<0)
           {
               MPeriod period = periods[x-1];
               if (period.time == current.time)
               {
                   found = true;
                   previous = periods[x - 2];
               }
               x--;
           }
            return previous;
        }

    }
}
