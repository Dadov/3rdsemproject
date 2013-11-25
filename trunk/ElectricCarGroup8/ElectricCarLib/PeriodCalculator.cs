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
            int init = storage.storageNumber;
            return init;
        }

        public void createFirstPeriod(int storageID)
        {
            MBatteryStorage storage = dbStorage.getRecord(storageID, true);
            MPeriod period = new MPeriod();
            period.bookedBatteryNumber = 0;
            period.initBatteryNumber = getInitNumber(storage);
            period.time = DateTime.Today.AddDays(1);
            dbPeriod.addNewRecord(storage.id, period.time, period.initBatteryNumber, period.bookedBatteryNumber);
        }

        public MPeriod createPeriod(MBatteryStorage storage)
        {
            storage = dbStorage.getRecord(storage.id, true);
            MPeriod newPeriod = new MPeriod();
            newPeriod.time = getTime(storage);
            newPeriod.initBatteryNumber = getInitNumber(storage);
            dbPeriod.addNewRecord(storage.id, newPeriod.time, newPeriod.initBatteryNumber, newPeriod.bookedBatteryNumber);
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
                    if ((time.CompareTo(curr.time) >= 0) & (time.CompareTo(next.time) < 0)) //if time of booking is later then current and earlier then next period
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
            while(!found || x>0)
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

        public MPeriod getNextPeriod(MBatteryStorage storage, MPeriod current)
        {
            List<MPeriod> periods = dbPeriod.getStoragePeriods(storage.id, true);
            int x = 0;
            bool found = false;
            MPeriod next = new MPeriod();
            while (!found || x < periods.Count)
            {
                MPeriod period = periods[x];
                if (period.time == current.time)
                {
                    found = true;
                    try
                    {
                        next = periods[x + 1];
                    }
                    catch (Exception)
                    {
                        next = createPeriod(storage);
                    }
                }
                x++;
            }
            return next;
        }

    }
}
