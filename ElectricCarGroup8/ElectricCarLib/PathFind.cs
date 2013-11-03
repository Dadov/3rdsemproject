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
        //return a path with min weight
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
                if (R.Contains(endSID))
                {
                    break;  //if extracted list contains the end station id, stop algorithm
                }
                foreach (int vId in adjListWithWeight[u.StationID].Keys)
                {
                    FibonacciNode v = null;
                    if (!R.Contains(vId)) //if the node is not in extracted list
                    {
                        if (!Q.stationIDs.Contains(vId))//if the node is not reached yet
                        {
                            v = Q.insert(vId);
                            QCopy.Add(vId, v);
                        }
                        else
                        {
                            v = QCopy[vId]; //The node is reached

                        }
                        decimal w = adjListWithWeight[u.StationID][vId];
                        relax(u, v, w, Q, QCopy);
                    }
                    
                }
            }

            LinkedList<PathStop> route = new LinkedList<PathStop>();
            if (R.Contains(endSID))
	        {
                route = buildRoute(S, endSID);
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

        public static LinkedList<PathStop> shortestPathWithoutFibonacci(Dictionary<int, Dictionary<int, decimal>> adjListWithWeight, int startSID, int endSID)
        {
            Dictionary<int, decimal> visitedStationIDs_distance = new Dictionary<int, decimal>(); //keep track of extracted nodes
            Dictionary<int, int> stationID_parentID = new Dictionary<int, int>();
            HashSet<int> reachedStations = new HashSet<int>();
            Dictionary<int, decimal> queue = new Dictionary<int, decimal>();

            //initiate the start
            reachedStations.Add(startSID);
            stationID_parentID.Add(startSID, 0);

            queue.Add(startSID, 0);

            while (queue.Count != 0)
            {
                int uId = getIdWithMinValue(queue);
                visitedStationIDs_distance.Add(uId, queue[uId]);
                queue.Remove(uId);

                if (visitedStationIDs_distance.Keys.Contains(endSID))
                {
                    break; //if extracted list contains the end station id, stop algorithm
                }

                foreach (int vId in adjListWithWeight[uId].Keys)
                {
                    if (!reachedStations.Contains(vId)) //if it is a new node
                    {
                        queue.Add(vId, decimal.MaxValue);
                        reachedStations.Add(vId);

                        decimal w = adjListWithWeight[uId][vId];
                        relax(uId, visitedStationIDs_distance[uId], vId, w, queue, stationID_parentID);
                    }
                    else if (!visitedStationIDs_distance.Keys.Contains(vId)) //if the node is not extracted and still in the queue
                    {                                                        //then do relax
                        decimal w = adjListWithWeight[uId][vId];             
                        relax(uId, visitedStationIDs_distance[uId], vId, w, queue, stationID_parentID);
                    }
                }
            }

            LinkedList<PathStop> route = new LinkedList<PathStop>();
            if (visitedStationIDs_distance.Keys.Contains(endSID))
            {
                route = buildRoute(visitedStationIDs_distance, stationID_parentID, endSID);
            }
            
            return route;
        }

        public static int getIdWithMinValue(Dictionary<int, decimal> q)
        {
           return q.OrderBy(i => i.Value).First().Key; //using quick sort O(NlogN) at average
        }

        public static void relax(int u, decimal uMinVal, int v, decimal w, Dictionary<int, decimal> q, Dictionary<int, int> stationID_parentID)
        {
            if (q[v] > uMinVal + w)
            {
                q[v] = uMinVal + w;
                stationID_parentID[v] = u;
            }
        }

        public static LinkedList<PathStop> buildRoute(Dictionary<int, decimal> visitedStationIDs_distance, Dictionary<int, int> stationID_parentID, int endSID)
        {
            LinkedList<PathStop> route = new LinkedList<PathStop>();
            int parentId = endSID;
            while (parentId!=0)
            {
                PathStop stop = new PathStop() { stationID = parentId, driveHour = visitedStationIDs_distance[parentId] };
                route.AddFirst(stop);
                parentId = stationID_parentID[parentId];
            }
            return route;
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

        //build route based on BFS and least stops route
        public static LinkedList<PathStop> buildRoute(Dictionary<int, int> stationID_stops, Dictionary<int, int> stationID_parentID, int endSID)
        {
            LinkedList<PathStop> route = new LinkedList<PathStop>();
            int parentId = endSID;
            while (parentId!= 0)
            {
                PathStop stop = new PathStop(){stationID = parentId, driveHour = stationID_stops[parentId]};
                route.AddFirst(stop);
                parentId = stationID_parentID[parentId];
            }
            return route;
        }

        public static LinkedList<PathStop> leastStopsPathWithIds(Dictionary<int, Dictionary<int, decimal>> adjListWithWeight, int startSID, int endSID)
        {
            Dictionary<int, int> visitedStationIDs_stops = new Dictionary<int, int>(); //keep track of reached nodes and stops
            Dictionary<int, int> stationID_parentID = new Dictionary<int, int>();
            HashSet<int> reachedStations = new HashSet<int>();
            Queue<int> queue = new Queue<int>();

            //initiate the start
            reachedStations.Add(startSID);
            visitedStationIDs_stops.Add(startSID, 0);
            stationID_parentID.Add(startSID, 0);

            queue.Enqueue(startSID);
            while (queue.Count != 0)
            {
                int u = queue.Dequeue();
                if (u == endSID)
                {
                    break;
                }
                else
                {
                    foreach (int sID in adjListWithWeight[u].Keys)
                    {
                        if (!reachedStations.Contains(sID))
                        {
                            reachedStations.Add(sID);

                            visitedStationIDs_stops.Add(sID, visitedStationIDs_stops[u] + 1);
                            stationID_parentID.Add(sID, u);

                            queue.Enqueue(sID);
                        }
                    }
                }
            }

            //build route
            LinkedList<PathStop> route = new LinkedList<PathStop>();
            if (visitedStationIDs_stops.Keys.Contains(endSID))
            {
                route = buildRoute(visitedStationIDs_stops, stationID_parentID, endSID);
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

        public static int breathFirstSearchWithIds(Dictionary<int, Dictionary<int, decimal>> adjListWithWeight, int startID, int endID)
        {
            int distance = 0;

            HashSet<int> reachedStationIDs = new HashSet<int>();
            Queue<int> queue = new Queue<int>();
            Dictionary<int, int> stationId_stops = new Dictionary<int, int>();

            //initiation
            reachedStationIDs.Add(startID);
            stationId_stops.Add(startID, 0);
            queue.Enqueue(startID);

            //start search
            while (queue.Count != 0)
            {
                int u = queue.Dequeue();
                foreach (int sID in adjListWithWeight[u].Keys)
                {
                    if (!reachedStationIDs.Contains(sID))
                    {
                        reachedStationIDs.Add(sID);
                        stationId_stops.Add(sID, stationId_stops[u] + 1);
                        queue.Enqueue(sID);
                    }
                }

            }

            //check if end station id is in the searched stations
            if (stationId_stops.ContainsKey(endID))
            {
                distance = stationId_stops[endID];
            }
            return distance;
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
