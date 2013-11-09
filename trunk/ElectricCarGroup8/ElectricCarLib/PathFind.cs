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

        public static List<List<PathStop>> getKShortestPath(Dictionary<int, Dictionary<int, decimal>> adjListWithWeight, int startSID, int endSID, int numberOfPath)
        {
            List<List<PathStop>> kShortestPaths = new List<List<PathStop>>();
            List<List<PathStop>> temporaryPaths = new List<List<PathStop>>();

            for (int k = 0; k < numberOfPath; k++)
            {
                kShortestPaths.Add(shortestPathWithoutFibonacci(adjListWithWeight, startSID, endSID));

                Dictionary<int, Dictionary<int, decimal>> removedEdges = new Dictionary<int, Dictionary<int, decimal>> ();

                for (int i = 0; i < (kShortestPaths[k-1].Count-1); i++)
                {
                    PathStop spurNode = kShortestPaths[k - 1][i];
                    //initiate root path
                    List<PathStop> rootPath = new List<PathStop>();
                    for (int j = 0; j <= i; j++)
                    {
                        rootPath.Add(kShortestPaths[k - 1][j]);
                    }

                    foreach (List<PathStop> p in kShortestPaths)
                    {
                        if (pathIsEqual(rootPath, p))
                        {
                            Dictionary<int, decimal> edge1 = new Dictionary<int,decimal>();
                            edge1.Add(i+1, adjListWithWeight[i][i + 1]);
                            Dictionary<int, decimal> edge2 = new Dictionary<int,decimal>();
                            edge2.Add(i, adjListWithWeight[i+1][i]);
                            removedEdges.Add(i, edge1);
                            removedEdges.Add(i+1, edge2);

                            adjListWithWeight[i].Remove(i + 1);
                            adjListWithWeight[i + 1].Remove(i);
                        }
                    }
                    List<PathStop> spurPath = shortestPathWithoutFibonacci(adjListWithWeight, spurNode.stationID, endSID);
                    rootPath.AddRange(spurPath);
                    if (temporaryPaths.Contains(rootPath))
                    {
                        temporaryPaths.Add(rootPath);
                    }
                }
                temporaryPaths.OrderBy(x => x.Last().driveHour);
                kShortestPaths[k] = temporaryPaths[0];
                temporaryPaths.RemoveAt(0);

                //restore edges
                foreach (int id in removedEdges.Keys)
                {
                    foreach (int item in removedEdges[id].Keys)
                    {
                        adjListWithWeight[id].Add(item, removedEdges[id][item]);
                    }
                }
            }

            
            return kShortestPaths;
        }

        public static bool pathIsEqual(List<PathStop> rootPath, List<PathStop> p)
        {
            bool isEqual = true;

            for (int i = 0; i < rootPath.Count; i++)
            {
                if (rootPath[i].stationID != p[i].stationID)
                {
                    isEqual = false;
                    break;
                }
            }

            return isEqual;
        }

        public static List<List<PathStop>> getAllPossiblePathsBetweenTwoPoints(Dictionary<int, Dictionary<int, decimal>> adjListWithWeight, int startSID, int endSID)
        {
            
            List<List<int>> paths = new List<List<int>>();

            List<List<int>> visitedPaths = new List<List<int>>();
            List<List<int>> nextPaths = new List<List<int>>();

            //initiate
            visitedPaths.Add(new List<int>());
            visitedPaths[0].Add(startSID);

            //bug: evaluate the while condition
            
            while (visitedPaths.Count!=0)
            {
                

                foreach (List<int> path in visitedPaths)
                {
                    

                    foreach (int u in adjListWithWeight[path.Last()].Keys)
                    {
                        int[] pathToArray = new int[path.Count];
                        path.CopyTo(pathToArray);//make a deep copy of the object
                        List<int> pathClone = pathToArray.ToList();

                        if (u == endSID)
                        {
                            pathClone.Add(u);
                            paths.Add(pathClone);
                            
                        }
                        else
                        {
                            if (!path.Contains(u)) //exclude loop situation
                            {
                                pathClone.Add(u);
                                nextPaths.Add(pathClone);
                            }
                        }
                    }
                    
                }
                visitedPaths = nextPaths;
                nextPaths = new List<List<int>>();
            }

            Dictionary<List<int>, decimal> pathWithWeight = new Dictionary<List<int>, decimal>();
            if (paths.Count!=0)
            {
                foreach (List<int> path in paths)
                {
                    decimal totalWeight = 0;
                    for (int i = 0; i < path.Count; i++)
                    {
                        if (i!=0)
                        {
                            totalWeight += adjListWithWeight[path[i-1]][path[i]];
                        }
                        
                    }
                    pathWithWeight.Add(path, totalWeight);
                }
            }

            pathWithWeight.OrderBy(i => i.Value);
            List<List<int>> sorted = pathWithWeight.Keys.ToList();

            //build the ordered paths
            List<List<PathStop>> sortedPaths = new List<List<PathStop>>();
            for (int i = 0; i < sorted.Count; i++)
            {
                List<PathStop> path = new List<PathStop>();
                for (int j = 0; j < sorted[i].Count; j++)
                {
                    if (path.Count==0)
	                {
		                PathStop ps = new PathStop() {stationID = sorted[i][j], driveHour = 0};
                        path.Add(ps);
                    }
                    else
                    {
                        PathStop ps = new PathStop() { stationID = sorted[i][j], driveHour = path[path.Count-1].driveHour + adjListWithWeight[sorted[i][j - 1]][sorted[i][j]] };
                        path.Add(ps);
                    }

                    
                }
                sortedPaths.Add(path);
            }
            
            

            return sortedPaths;
        }
        //return a path with min weight
        public static List<PathStop> shortestPathWithFibonacci(Dictionary<int, Dictionary<int, decimal>> adjListWithWeight, int startSID, int endSID)
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

            List<PathStop> route = new List<PathStop>();
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

        public static List<PathStop> shortestPathWithoutFibonacci(Dictionary<int, Dictionary<int, decimal>> adjListWithWeight, int startSID, int endSID)
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

            List<PathStop> route = new List<PathStop>();
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

        public static List<PathStop> buildRoute(Dictionary<int, decimal> visitedStationIDs_distance, Dictionary<int, int> stationID_parentID, int endSID)
        {
            List<PathStop> route = new List<PathStop>();
            int parentId = endSID;
            while (parentId!=0)
            {
                PathStop stop = new PathStop() { stationID = parentId, driveHour = visitedStationIDs_distance[parentId] };
                route.Add(stop);
                parentId = stationID_parentID[parentId];
            }
            route.Reverse();
            return route;
        }

        
        //return a path consists of station id and drivehour from each station to start station. 
        public static List<PathStop> buildRoute(List<FibonacciNode> S, int endSID)
        {
            List<PathStop> route = new List<PathStop>();
            FibonacciNode endNode = S.Find(node => node.StationID == endSID);
            FibonacciNode lastStop = endNode;
            while (lastStop != null)
            {
                PathStop stop = new PathStop() { stationID = lastStop.StationID, driveHour = lastStop.MinPathValue };
                route.Add(stop);
                lastStop = lastStop.lastStop;
            }
            route.Reverse();
            return route;
        }

        //build route based on BFS and least stops route
        public static List<PathStop> buildRoute(Dictionary<int, int> stationID_stops, Dictionary<int, int> stationID_parentID, int endSID)
        {
            List<PathStop> route = new List<PathStop>();
            int parentId = endSID;
            while (parentId!= 0)
            {
                PathStop stop = new PathStop(){stationID = parentId, driveHour = stationID_stops[parentId]};
                route.Add(stop);
                parentId = stationID_parentID[parentId];
            }
            route.Reverse();
            return route;
        }

        public static List<PathStop> leastStopsPathWithIds(Dictionary<int, Dictionary<int, decimal>> adjListWithWeight, int startSID, int endSID)
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
            List<PathStop> route = new List<PathStop>();
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
