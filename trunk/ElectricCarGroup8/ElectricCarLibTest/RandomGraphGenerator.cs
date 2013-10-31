using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarLibTest
{
    class RandomGraphGenerator
    {
        int nodeSize;
        double density;
        int minValueForWeight;
        int maxValueForWeight;
        List<int> nodes;
        Dictionary<int, Dictionary<int, decimal>> nodesWithAllEdges;
        public RandomGraphGenerator(int size, double d, int min, int max)
        {
            nodeSize = size;
            density = d;
            minValueForWeight = min;
            maxValueForWeight = max;
            nodes = new List<int>();
            nodesWithAllEdges = new Dictionary<int, Dictionary<int, decimal>>();
            createNodes();
            createAllEdges();
        }

        public Dictionary<int, Dictionary<int, decimal>> getAdjList()
        {
            Dictionary<int, Dictionary<int, decimal>> adjListWithWeight = new Dictionary<int, Dictionary<int, decimal>>();
            //init
            foreach (int nodeId in nodes)
            {
                Dictionary<int, decimal> list = new Dictionary<int, decimal>();
                adjListWithWeight.Add(nodeId, list);
            }

            Random r = new Random();

            foreach (int id in nodes)
            {
                Dictionary<int, decimal> selectedEdges = new Dictionary<int, decimal>();
                Dictionary<int, decimal> allEdges = nodesWithAllEdges[id];
                var selected = from pair in allEdges where r.NextDouble() < density select pair;
                selectedEdges = selected.ToDictionary(pair => pair.Key, pair => pair.Value);

                foreach (var pair in selectedEdges)
                {
                    adjListWithWeight[id].Add(pair.Key, pair.Value);//add edges to current node
                    adjListWithWeight[pair.Key].Add(id, pair.Value);//add edges to edge connected node
                }
            }

            return adjListWithWeight;
        }


        private void createNodes()
        {
            for (int i = 1; i < nodeSize +1; i++)
            {
                nodes.Add(i);
            }
        }

        private void createAllEdges()
        {
            Random r = new Random();
            foreach (int id in nodes)
            {
                Dictionary<int, decimal> edges = new Dictionary<int, decimal>();
                foreach (int id2 in nodes)
                {
                    if (id2 != id && id2 > id)
                    {
                        edges.Add(id2, Convert.ToDecimal(r.Next(minValueForWeight, maxValueForWeight)));
                    }
                }
                nodesWithAllEdges.Add(id, edges);
            }

        }
    }
}
