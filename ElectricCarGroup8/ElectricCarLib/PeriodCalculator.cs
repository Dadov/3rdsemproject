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
        public DateTime getTime(MBatteryStorage storage)
        {
            int count = storage.periods.Count;
            DateTime firstPeriod = storage.periods[count-1].time;
            int capacity = (int) storage.type.capacity;
            DateTime secondPeriod = firstPeriod.AddHours(capacity);
            return secondPeriod;
        }

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

        public MPeriod getPeriod(MBatteryStorage storage)
        {
            MPeriod newPeriod = new MPeriod();
            newPeriod.time = getTime(storage);
            newPeriod.initBatteryNumber = getInitNumber(storage);
            newPeriod.bookedBatteryNumber = setBookedBatteries(storage);
            dbPeriod.addNewRecord(storage.id, newPeriod.time, newPeriod.initBatteryNumber, newPeriod.bookedBatteryNumber, newPeriod.futureBatteryNumber);
            return newPeriod;
        }


    }
}
