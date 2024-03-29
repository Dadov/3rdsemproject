﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarLib
{
    public class EstimateTime
    {

        public static decimal carAveSpeed = 70;//km/h

        public decimal getCarAveSpeed()
        {
            return carAveSpeed;
        }
        
        private ConnectionCtr cCtr = new ConnectionCtr();

        public static double driveHourForDistance(decimal distance) 
        {
            decimal driveHour = distance / carAveSpeed; //driveHour.ToString("C2")
            return Convert.ToDouble(driveHour);
        }


        public Dictionary<MStation, DateTime> estimateArriveTimeForPath(LinkedList<MStation> path, DateTime start)
        {
            Dictionary<MStation, DateTime> estimateArriveTimeForPath = new Dictionary<MStation, DateTime>();
            MStation[] pathToArray = path.ToArray<MStation>();
            estimateArriveTimeForPath.Add(pathToArray[0], start);

            for (int i = 0; i < path.Count; i++)
            {
                decimal distance = (decimal)cCtr.getRecord(pathToArray[i].Id, pathToArray[i+1].Id, false).distance;
                estimateArriveTimeForPath.Add(pathToArray[i + 1], arriveTime(start, distance));
            }

            return estimateArriveTimeForPath;
        }

        //TODO create realistic estimate arrive time later, take into account breaks and sleep
        public DateTime arriveTime(DateTime start, decimal distance)
        {
            DateTime arrive = start.AddHours(driveHourForDistance(distance));
            return arrive;
        }

    }
}
