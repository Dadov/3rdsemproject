using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarLib
{
    public class PathFindCtr
    {
        private StationCtr sCtr = new StationCtr();
        private static int numberOfPaths = 10;
        
        public List<List <PathStop>> findRoutes(int sId1, int sId2, DateTime startTime, decimal batteryLimit)
        {
            List<List<PathStop>> paths = new List<List<PathStop>>();
            Dictionary<int, Dictionary<int, decimal>> adjListInLimit = sCtr.getAdjListWithBatteryLimitForDistance(batteryLimit);
            if (RouteCache.Instance.queue.Contains(sId1+ "," + sId2))
            {
                paths = RouteCache.Instance.getRoute(sId1, sId2);
            }
            else
            {
                paths = FindPath.getKShortestPath(adjListInLimit, sId1, sId2, numberOfPaths);
                RouteCache.Instance.insertRoute(sId1, sId2, paths);
            }

            if (paths.Count!=0)
            {
                foreach (List <PathStop> p in paths)
                {
                    DateTime time = startTime;
                    for (int i = 0; i < p.Count; i++)
                    {
                        if (i == 0)
                        {
                            p[i].distance = 0;
                            p[i].driveHour = 0;
                        }
                        else
                        {
                            p[i].distance = adjListInLimit[p[i - 1].stationID][p[i].stationID];
                            p[i].driveHour = Convert.ToDecimal(EstimateTime.driveHourForDistance(p[i].distance));
                            
                        }
                        p[i].station = sCtr.getStation(p[i].stationID, false);
                        
                    }
                }
            }
            return paths;
        }

        public List<Dictionary<MStation, DateTime>> findRoutes(int sId1, int sId2, DateTime startTime)
        {
            List<Dictionary<MStation, DateTime>> route = new List<Dictionary<MStation, DateTime>>();
            MStation startStation = sCtr.getStation(sId1, false);
            MStation endStation = sCtr.getStation(sId2, false);
            Dictionary<MStation, Dictionary<MStation, decimal>> adjListWithWeight = sCtr.adjListWithWeight();

            Dictionary<MStation, LinkedList<MStation>> adjListWithoutWeight = sCtr.adjListWithoutWeight();
            
            int stops = 0;
            //return path with least stops
            List<MStation> path = FindPath.leastStopsPath(adjListWithoutWeight, startStation, endStation, out stops);
            Dictionary<MStation, DateTime> leastStopPath = new Dictionary<MStation, DateTime>();
            leastStopPath.Add(path[0], startTime);
            DateTime time = startTime;
            for (int i = 1; i < path.Count; i++)
            {
                double h = Convert.ToDouble((adjListWithWeight[path[i - 1]])[path[i]]);
                time = time.AddHours(h);
                leastStopPath.Add(path[i], time);
            }

            route.Add(leastStopPath);

            return route;
        }
    }
}
