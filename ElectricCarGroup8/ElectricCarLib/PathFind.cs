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
        public static LinkedList<PathStop> shortestPathWithFibonacci(Dictionary<int, Dictionary<int, decimal>> adjListWithWeight, int startSID, int endSID)
        {
            FibonacciHeap Q = new FibonacciHeap();
            Dictionary<int, FibonacciNode> QCopy = new Dictionary<int, FibonacciNode>();
            List<int> R = new List<int>();
            List<FibonacciNode> S = new List<FibonacciNode>();
            //initialize start node
            FibonacciNode start = Q.insert(startSID);
            start.MinPathValue = 0;
            QCopy.Add(startSID, start);

            while (Q.numberOfNodes != 0)
            {
                FibonacciNode u = Q.extractMinNode();
                R.Add(u.StationID);
                S.Add(u);
                foreach (int vId in adjListWithWeight[u.StationID].Keys)
                {
                    FibonacciNode v = null;
                    if (!Q.stationIDs.Contains(vId))
	                {
                         v= Q.insert(vId);
                         QCopy.Add(vId, v);
	                }
                    else
                    {
                        v = QCopy[vId];
                        
                    }
                    decimal w = adjListWithWeight[u.StationID][vId];
                    relax(u, v, w, Q, QCopy);
                }
            }

            LinkedList<PathStop> route = new LinkedList<PathStop>();
            if (R.Contains(endSID))
	        {
                route = buildRoute(S, endSID);
            }
            else
            {
                route = null;
            }
            return route;
        }

        public static void relax(FibonacciNode u, FibonacciNode v, decimal w, FibonacciHeap Q, Dictionary<int, FibonacciNode> QCopy)
        {
            if (v.MinPathValue > u.MinPathValue + w)
            {
                Q.DecreasingKey(Q.root, v, u.MinPathValue + w);
                QCopy[v.StationID].MinPathValue = u.MinPathValue + w;
                v.lastStop = u;
            }
        }

        //return a path consists of station id and drivehour from each station to start station. 
        public static LinkedList<PathStop> buildRoute(List<FibonacciNode> S, int endSID)
        {
            LinkedList<PathStop> route = new LinkedList<PathStop>();
            FibonacciNode endNode = S.Find(node => node.StationID == endSID);
            FibonacciNode lastStop = endNode;
            while (lastStop != null)
            {
                PathStop stop = new PathStop() { stationID = lastStop.StationID, driveHour = lastStop.MinPathValue };
                route.AddFirst(stop);
                lastStop = lastStop.lastStop;
            }
            return route;
        }



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

            while (queue.Count != 0)
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

            //return the least stops path
            if (reachedStations.Contains(destination))
            {
                MStation parent = destination;
                while (parent != null)//could be a bug
                {
                    leastStopsPath.Add(parent);
                    parent = station_parent[parent];
                }
                leastStopsPath.Reverse();
                numOfStops = leastStopsPath.Count; //including start station and end station
                
            }

            return leastStopsPath;
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
