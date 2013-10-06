using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarLib
{
    class TimeEstimate
    {
        private static double carAveSpeed = 70; //km/h

        public double driveHourForDistance(double distance) 
        {
            double driveHour = distance/70;
            return Convert.ToDouble(driveHour.ToString("C2"));
        }

        //TODO create realistic estimate arrive time later, take into account breaks and sleep
        public DateTime arriveTime(DateTime start, double distance)
        {
            DateTime arrive = start.AddHours(driveHourForDistance(distance));
            return arrive;
        }

    }
}
