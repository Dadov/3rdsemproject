using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    //to return the route that has been quaried before and store max 10 route lists
    public class RouteCache
    {
        private static RouteCache instance;
        public Dictionary<string, List<List<PathStop>>> routes { get; set; } //TODO can seperate the list based on battery type
        public Queue<string> queue { get; set; }
        private static int maxStoredRoute = 10;
        private RouteCache() 
        { 
            routes = new Dictionary<string,List<List<PathStop>>>();
            queue = new Queue<string>();
        }

        public static RouteCache Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RouteCache();
                }
                return instance;
            }
        }

        public List<List<PathStop>> getRoute(int sId, int eId) //start station Id and end station Id
        {
            List<List<PathStop>> route = new List<List<PathStop>>();
            string key = sId + "," + eId;
            if (routes.Keys.Contains(key))
            {
                route = routes[key];
            }
            return route;
        }

        public void insertRoute(int sId, int eId, List<List<PathStop>> route)
        {
            string key = sId + "," + eId;
            if (routes.Count == maxStoredRoute)
            {
                string oldest = queue.Dequeue();
                routes.Remove(oldest);
            }
            routes.Add(key, route);
            queue.Enqueue(key);
        }
    }
}
