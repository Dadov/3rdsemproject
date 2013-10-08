using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using ElectricCarModelLayer;

namespace ElectricCarLib
{
    public class PathFind
    {
        public static List<MStation> leastStopsPath(Dictionary<MStation, LinkedList<MStation>> adjList, MStation start, MStation destination, out int numOfStops)
        {
            
            
            //keep track of visited nodes
            HashSet<MStation> reachedStations = new HashSet<MStation>();
            Queue<MStation> queue = new Queue<MStation>();
            Dictionary<MStation, int> station_distance = new Dictionary<MStation, int>();
            Dictionary<MStation, MStation> station_parent = new Dictionary<MStation, MStation>();

            if (start == null)
            {
                throw new SystemException("The start station does not exist.");
            }

            reachedStations.Add(start);
            station_distance.Add(start, 0);
            station_parent.Add(start, null);

            queue.Enqueue(start);

            while(queue.Count  != 0)
            {
                MStation u = queue.Dequeue();
                if (u.Id == destination.Id)
                {
                    break;
                }
                else
                {
                    foreach (MStation station in adjList[u])
                    {
                        if (!reachedStations.Contains(station))
                        {
                            reachedStations.Add(station);

                            station_distance.Add(station, station_distance[u] + 1);
                            station_parent.Add(station, u);

                            queue.Enqueue(station);
                        }
                    }
                }
            }

            List<MStation> leastStopsPath = new List<MStation>();
            numOfStops = 0;
            if (reachedStations.Contains(destination))
            {
                MStation parent = destination;
                while (parent != null)
                {
                    leastStopsPath.Add(parent);
                    parent = station_parent[parent];
                }
                leastStopsPath.Reverse();
                numOfStops = leastStopsPath.Count; //including start station and end station
            }    
            
            return leastStopsPath;//include start and end stations
        }

        public static int breathFirstSearch(Dictionary<MStation, LinkedList<MStation>> adjList, MStation start, MStation destination)
        {
            int distance = 0;

            HashSet<MStation> reachedStations = new HashSet<MStation>();
            Queue<MStation> queue = new Queue<MStation>();
            Dictionary<MStation, int> station_distance = new Dictionary<MStation, int>();
            Dictionary<MStation, MStation> station_parent = new Dictionary<MStation, MStation>();

            if (start == null)
            {
                throw new SystemException("The start station does not exist.");
            }

            reachedStations.Add(start);
            station_distance.Add(start, 0);
            station_parent.Add(start, null);

            queue.Enqueue(start);

            while (queue.Count != 0)
            {
                MStation u = queue.Dequeue();
                foreach (MStation station in adjList[u])
                {
                    if (!reachedStations.Contains(station))
                    {
                        reachedStations.Add(station);

                        station_distance.Add(station, station_distance[u] + 1);
                        station_parent.Add(station, u);

                        queue.Enqueue(station);
                    }
                }
                
            }

            if (station_distance.ContainsKey(destination))
            {
                distance = station_distance[destination];
            }

            return distance; //does not include start station
        }

    }
}
