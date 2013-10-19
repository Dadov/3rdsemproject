using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarLib
{
    class PathFindCtr
    {
        private StationCtr sCtr = new StationCtr();
        public List<Dictionary<MStation, DateTime>> findRoutes(int sId1, int sId2, DateTime startTime)
        {
            List<Dictionary<MStation, DateTime>> route = new List<Dictionary<MStation, DateTime>>();
            MStation startStation = sCtr.getStation(sId1, false);
            MStation endStation = sCtr.getStation(sId2, false);
            Dictionary<MStation, Dictionary<MStation, decimal>> adjListWithWeight = sCtr.adjListWithWeight();

            Dictionary<MStation, LinkedList<MStation>> adjListWithoutWeight = sCtr.adjListWithoutWeight();
            
            int stops = 0;
            //return path with least stops
            List<MStation> path = PathFind.leastStopsPath(adjListWithoutWeight, startStation, endStation, out stops);
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
