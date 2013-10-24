using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using ElectricCarModelLayer;
using ElectricCarLib;

namespace ElectricCarLibTest
{
    [TestClass]
    public class PathFindTest
    {
        [TestMethod]
        public void shortestPathWithFibonacciLinearGraph()
        {
            
        }

        [TestMethod]
        public void shortestPathWithFibonacciTreeGraph()
        {

        }

        [TestMethod]
        public void shortestPathWithFibonacciUndirectedGraph()
        {

        }

        [TestMethod]
        public void buildRoute()
        {
            
        }

        [TestMethod]
        public void relax()
        {

        }
        
        [TestMethod]
        public void leastStopsPathLinearDataStructure()
        {
            Dictionary<MStation, LinkedList<MStation>> adjList = new Dictionary<MStation, LinkedList<MStation>>();
            MStation s = new MStation() { Id = 1 };
            MStation r = new MStation() { Id = 2 };
            MStation v = new MStation() { Id = 3 };
            MStation w = new MStation() { Id = 4 };
            MStation t = new MStation() { Id = 5 };
            MStation x = new MStation() { Id = 6 };
            MStation y = new MStation() { Id = 7 };
            MStation u = new MStation() { Id = 8 };

            LinkedList<MStation> adjS = new LinkedList<MStation>();
            LinkedList<MStation> adjR = new LinkedList<MStation>();
            LinkedList<MStation> adjV = new LinkedList<MStation>();
            LinkedList<MStation> adjW = new LinkedList<MStation>();
            LinkedList<MStation> adjT = new LinkedList<MStation>();
            LinkedList<MStation> adjX = new LinkedList<MStation>();
            LinkedList<MStation> adjY = new LinkedList<MStation>();
            LinkedList<MStation> adjU = new LinkedList<MStation>();

            adjList.Add(s, adjS);
            adjList.Add(r, adjR);
            adjList.Add(y, adjY);
            adjList.Add(t, adjT);
            adjList.Add(x, adjX);
            adjList.Add(v, adjV);
            adjList.Add(u, adjU);
            adjList.Add(w, adjW);

            adjS.AddFirst(r);
            adjR.AddFirst(v);
            adjV.AddFirst(w);
            adjW.AddFirst(t);
            adjT.AddFirst(x);
            adjX.AddFirst(y);
            adjY.AddFirst(u);

            List<MStation> path = new List<MStation>();
            int stops1 = 0;
            int stops2 = 0;
            int stops3 = 0;
            int stops4 = 0;
            int stops5 = 0;
            int stops6 = 0;
            int stops7 = 0;
            int stops8 = 0;

            List<MStation> path1 = PathFind.leastStopsPath(adjList, s, r, out stops1);
            List<MStation> path2 = PathFind.leastStopsPath(adjList, s, v, out stops2);
            List<MStation> path3 = PathFind.leastStopsPath(adjList, s, w, out stops3);
            List<MStation> path4 = PathFind.leastStopsPath(adjList, s, t, out stops4);
            List<MStation> path5 = PathFind.leastStopsPath(adjList, s, x, out stops5);
            List<MStation> path6 = PathFind.leastStopsPath(adjList, s, y, out stops6);
            List<MStation> path7 = PathFind.leastStopsPath(adjList, s, u, out stops7);
            List<MStation> path8 = PathFind.leastStopsPath(adjList, w, y, out stops8);

            Assert.AreEqual(PathFind.breathFirstSearch(adjList, s, r), stops1 - 1);
            Assert.AreEqual(PathFind.breathFirstSearch(adjList, s, v), stops2 - 1);
            Assert.AreEqual(PathFind.breathFirstSearch(adjList, s, w), stops3 - 1);
            Assert.AreEqual(PathFind.breathFirstSearch(adjList, s, t), stops4 - 1);
            Assert.AreEqual(PathFind.breathFirstSearch(adjList, s, x), stops5 - 1);
            Assert.AreEqual(PathFind.breathFirstSearch(adjList, s, y), stops6 - 1);
            Assert.AreEqual(PathFind.breathFirstSearch(adjList, s, u), stops7 - 1);
            Assert.AreEqual(PathFind.breathFirstSearch(adjList, w, y), stops8 - 1);

            Assert.AreEqual(1, path1[0].Id);
            Assert.AreEqual(2, path1[1].Id);

            Assert.AreEqual(1, path2[0].Id);
            Assert.AreEqual(2, path2[1].Id);
            Assert.AreEqual(3, path2[2].Id);

            Assert.AreEqual(1, path3[0].Id);
            Assert.AreEqual(2, path3[1].Id);
            Assert.AreEqual(3, path3[2].Id);
            Assert.AreEqual(4, path3[3].Id);

            Assert.AreEqual(1, path4[0].Id);
            Assert.AreEqual(2, path4[1].Id);
            Assert.AreEqual(3, path4[2].Id);
            Assert.AreEqual(4, path4[3].Id);
            Assert.AreEqual(5, path4[4].Id);

            Assert.AreEqual(1, path5[0].Id);
            Assert.AreEqual(2, path5[1].Id);
            Assert.AreEqual(3, path5[2].Id);
            Assert.AreEqual(4, path5[3].Id);
            Assert.AreEqual(5, path5[4].Id);
            Assert.AreEqual(6, path5[5].Id);

            Assert.AreEqual(1, path6[0].Id);
            Assert.AreEqual(2, path6[1].Id);
            Assert.AreEqual(3, path6[2].Id);
            Assert.AreEqual(4, path6[3].Id);
            Assert.AreEqual(5, path6[4].Id);
            Assert.AreEqual(6, path6[5].Id);
            Assert.AreEqual(7, path6[6].Id);

            Assert.AreEqual(1, path7[0].Id);
            Assert.AreEqual(2, path7[1].Id);
            Assert.AreEqual(3, path7[2].Id);
            Assert.AreEqual(4, path7[3].Id);
            Assert.AreEqual(5, path7[4].Id);
            Assert.AreEqual(6, path7[5].Id);
            Assert.AreEqual(7, path7[6].Id);
            Assert.AreEqual(8, path7[7].Id);

            Assert.AreEqual(4, path8[0].Id);
            Assert.AreEqual(5, path8[1].Id);
            Assert.AreEqual(6, path8[2].Id);
            Assert.AreEqual(7, path8[3].Id);
           
        }

        [TestMethod]
        public void leastStopsPathTreeDataStructure()
        {
            Dictionary<MStation, LinkedList<MStation>> adjList = new Dictionary<MStation, LinkedList<MStation>>();
            MStation s = new MStation() { Id = 1 };
            MStation r = new MStation() { Id = 2 };
            MStation v = new MStation() { Id = 3 };
            MStation w = new MStation() { Id = 4 };
            MStation t = new MStation() { Id = 5 };
            MStation x = new MStation() { Id = 6 };
            MStation y = new MStation() { Id = 7 };
            MStation u = new MStation() { Id = 8 };

            LinkedList<MStation> adjS = new LinkedList<MStation>();
            LinkedList<MStation> adjR = new LinkedList<MStation>();
            LinkedList<MStation> adjV = new LinkedList<MStation>();
            LinkedList<MStation> adjW = new LinkedList<MStation>();
            LinkedList<MStation> adjT = new LinkedList<MStation>();
            LinkedList<MStation> adjX = new LinkedList<MStation>();
            LinkedList<MStation> adjY = new LinkedList<MStation>();
            LinkedList<MStation> adjU = new LinkedList<MStation>();

            adjList.Add(s, adjS);
            adjList.Add(r, adjR);
            adjList.Add(y, adjY);
            adjList.Add(t, adjT);
            adjList.Add(x, adjX);
            adjList.Add(v, adjV);
            adjList.Add(u, adjU);
            adjList.Add(w, adjW);

            adjS.AddFirst(r);
            adjS.AddLast(v);

            adjR.AddFirst(w);
            adjR.AddLast(t);

            adjV.AddFirst(x);
            adjV.AddLast(y);

            adjT.AddFirst(u);

            adjY.AddFirst(v);
            adjV.AddLast(s);

            int stops1 = 0;
            int stops2 = 0;
            int stops3 = 0;
            int stops4 = 0;
            int stops5 = 0;
            int stops6 = 0;

            List<MStation> path1 = PathFind.leastStopsPath(adjList, s, r, out stops1);
            List<MStation> path2 = PathFind.leastStopsPath(adjList, s, w, out stops2);
            List<MStation> path3 = PathFind.leastStopsPath(adjList, s, u, out stops3);
            List<MStation> path4 = PathFind.leastStopsPath(adjList, s, x, out stops4);
            List<MStation> path5 = PathFind.leastStopsPath(adjList, x, y, out stops5);
            List<MStation> path6 = PathFind.leastStopsPath(adjList, y, s, out stops6);

            Assert.AreEqual(stops1 - 1, PathFind.breathFirstSearch(adjList, s, r));
            Assert.AreEqual(1, path1[0].Id);
            Assert.AreEqual(2, path1[1].Id);

            Assert.AreEqual(stops2 - 1, PathFind.breathFirstSearch(adjList, s, w));
            Assert.AreEqual(1, path2[0].Id);
            Assert.AreEqual(2, path2[1].Id);
            Assert.AreEqual(4, path2[2].Id);
           
            Assert.AreEqual(stops3 - 1, PathFind.breathFirstSearch(adjList, s, u));
            Assert.AreEqual(1, path3[0].Id);
            Assert.AreEqual(2, path3[1].Id);
            Assert.AreEqual(5, path3[2].Id);
            Assert.AreEqual(8, path3[3].Id);

            Assert.AreEqual(stops4 - 1, PathFind.breathFirstSearch(adjList, s, x));
            Assert.AreEqual(1, path4[0].Id);
            Assert.AreEqual(3, path4[1].Id);
            Assert.AreEqual(6, path4[2].Id);
            
            Assert.AreEqual(stops5, PathFind.breathFirstSearch(adjList, x, y));
            Assert.AreEqual(0, path5.Count);

            Assert.AreEqual(stops6 - 1, PathFind.breathFirstSearch(adjList, y, s));
            Assert.AreEqual(7, path6[0].Id);
            Assert.AreEqual(3, path6[1].Id);
            Assert.AreEqual(1, path6[2].Id);

        }

        [TestMethod]
        public void leastStopsPathCyclicDataStructure()
        {
            Dictionary<MStation, LinkedList<MStation>> adjList = new Dictionary<MStation, LinkedList<MStation>>();
            MStation s = new MStation() { Id = 1 };
            MStation r = new MStation() { Id = 2 };
            MStation v = new MStation() { Id = 3 };
            MStation w = new MStation() { Id = 4 };
            MStation t = new MStation() { Id = 5 };
            MStation x = new MStation() { Id = 6 };
            MStation y = new MStation() { Id = 7 };
            MStation u = new MStation() { Id = 8 };

            LinkedList<MStation> adjS = new LinkedList<MStation>();
            LinkedList<MStation> adjR = new LinkedList<MStation>();
            LinkedList<MStation> adjV = new LinkedList<MStation>();
            LinkedList<MStation> adjW = new LinkedList<MStation>();
            LinkedList<MStation> adjT = new LinkedList<MStation>();
            LinkedList<MStation> adjX = new LinkedList<MStation>();
            LinkedList<MStation> adjY = new LinkedList<MStation>();
            LinkedList<MStation> adjU = new LinkedList<MStation>();

            adjS.AddFirst(w);
            adjS.AddLast(r);

            adjR.AddFirst(v);
            adjR.AddLast(s);

            adjW.AddFirst(t);
            adjW.AddLast(x);
            adjW.AddLast(s);

            adjX.AddFirst(t);
            adjX.AddLast(w);
            adjX.AddLast(y);

            adjT.AddLast(w);
            adjT.AddLast(x);
            adjT.AddLast(u);

            adjU.AddFirst(t);
            adjU.AddLast(x);
            adjU.AddLast(y);

            adjY.AddFirst(x);
            adjY.AddLast(u);

            adjV.AddFirst(r);

            adjList.Add(s, adjS);
            adjList.Add(r, adjR);
            adjList.Add(y, adjY);
            adjList.Add(t, adjT);
            adjList.Add(x, adjX);
            adjList.Add(v, adjV);
            adjList.Add(u, adjU);
            adjList.Add(w, adjW);

            int stops1 = 0;
            int stops2 = 0;
            int stops3 = 0;
            int stops4 = 0;
            int stops5 = 0;
            int stops6 = 0;
            int stops7 = 0;
            int stops8 = 0;
            int stops9 = 0;

            List<MStation> path1 = PathFind.leastStopsPath(adjList, s, r, out stops1);
            List<MStation> path2 = PathFind.leastStopsPath(adjList, s, v, out stops2);
            List<MStation> path3 = PathFind.leastStopsPath(adjList, s, w, out stops3);
            List<MStation> path4 = PathFind.leastStopsPath(adjList, s, t, out stops4);
            List<MStation> path5 = PathFind.leastStopsPath(adjList, s, x, out stops5);
            List<MStation> path6 = PathFind.leastStopsPath(adjList, s, u, out stops6);
            List<MStation> path7 = PathFind.leastStopsPath(adjList, s, y, out stops7);
            List<MStation> path8 = PathFind.leastStopsPath(adjList, u, v, out stops8);
            List<MStation> path9 = PathFind.leastStopsPath(adjList, w, y, out stops9);

            Assert.AreEqual(1, PathFind.breathFirstSearch(adjList, s, r));
            Assert.AreEqual(1, path1[0].Id);
            Assert.AreEqual(2, path1[1].Id);

            Assert.AreEqual(2, PathFind.breathFirstSearch(adjList, s, v));
            Assert.AreEqual(1, path2[0].Id);
            Assert.AreEqual(2, path2[1].Id);
            Assert.AreEqual(3, path2[2].Id);

            Assert.AreEqual(1, PathFind.breathFirstSearch(adjList, s, w));
            Assert.AreEqual(1, path3[0].Id);
            Assert.AreEqual(4, path3[1].Id);
            
            Assert.AreEqual(2, PathFind.breathFirstSearch(adjList, s, t));
            Assert.AreEqual(1, path4[0].Id);
            Assert.AreEqual(4, path4[1].Id);
            Assert.AreEqual(5, path4[2].Id);

            Assert.AreEqual(2, PathFind.breathFirstSearch(adjList, s, x));
            Assert.AreEqual(1, path5[0].Id);
            Assert.AreEqual(4, path5[1].Id);
            Assert.AreEqual(6, path5[2].Id);

            Assert.AreEqual(3, PathFind.breathFirstSearch(adjList, s, u));
            Assert.AreEqual(1, path6[0].Id);
            Assert.AreEqual(4, path6[1].Id);
            Assert.AreEqual(5, path6[2].Id);
            Assert.AreEqual(8, path6[3].Id);

            Assert.AreEqual(3, PathFind.breathFirstSearch(adjList, s, y));
            Assert.AreEqual(1, path7[0].Id);
            Assert.AreEqual(4, path7[1].Id);
            Assert.AreEqual(6, path7[2].Id);
            Assert.AreEqual(7, path7[3].Id);

            Assert.AreEqual(5, PathFind.breathFirstSearch(adjList, u, v));
            Assert.AreEqual(8, path8[0].Id);
            Assert.AreEqual(5, path8[1].Id);
            Assert.AreEqual(4, path8[2].Id);
            Assert.AreEqual(1, path8[3].Id);
            Assert.AreEqual(2, path8[4].Id);
            Assert.AreEqual(3, path8[5].Id);

            Assert.AreEqual(2, PathFind.breathFirstSearch(adjList, w, y));
            Assert.AreEqual(4, path9[0].Id);
            Assert.AreEqual(6, path9[1].Id);
            Assert.AreEqual(7, path9[2].Id);
        }

        [TestMethod]
        public void breathFirstSearchLinearDataStructure()
        {
            Dictionary<MStation, LinkedList<MStation>> adjList = new Dictionary<MStation, LinkedList<MStation>>();
            MStation s = new MStation() { Id = 1 };
            MStation r = new MStation() { Id = 2 };
            MStation v = new MStation() { Id = 3 };
            MStation w = new MStation() { Id = 4 };
            MStation t = new MStation() { Id = 5 };
            MStation x = new MStation() { Id = 6 };
            MStation y = new MStation() { Id = 7 };
            MStation u = new MStation() { Id = 8 };

            LinkedList<MStation> adjS = new LinkedList<MStation>();
            LinkedList<MStation> adjR = new LinkedList<MStation>();
            LinkedList<MStation> adjV = new LinkedList<MStation>();
            LinkedList<MStation> adjW = new LinkedList<MStation>();
            LinkedList<MStation> adjT = new LinkedList<MStation>();
            LinkedList<MStation> adjX = new LinkedList<MStation>();
            LinkedList<MStation> adjY = new LinkedList<MStation>();
            LinkedList<MStation> adjU = new LinkedList<MStation>();

            adjList.Add(s, adjS);
            adjList.Add(r, adjR);
            adjList.Add(y, adjY);
            adjList.Add(t, adjT);
            adjList.Add(x, adjX);
            adjList.Add(v, adjV);
            adjList.Add(u, adjU);
            adjList.Add(w, adjW);

            adjS.AddFirst(r);
            adjR.AddFirst(v);
            adjV.AddFirst(w);
            adjW.AddFirst(t);
            adjT.AddFirst(x);
            adjX.AddFirst(y);
            adjY.AddFirst(u);

            Assert.AreEqual(1, PathFind.breathFirstSearch(adjList, s, r));
            Assert.AreEqual(2, PathFind.breathFirstSearch(adjList, s, v));
            Assert.AreEqual(3, PathFind.breathFirstSearch(adjList, s, w));
            Assert.AreEqual(4, PathFind.breathFirstSearch(adjList, s, t));
            Assert.AreEqual(5, PathFind.breathFirstSearch(adjList, s, x));
            Assert.AreEqual(6, PathFind.breathFirstSearch(adjList, s, y));
            Assert.AreEqual(7, PathFind.breathFirstSearch(adjList, s, u));
            Assert.AreEqual(3, PathFind.breathFirstSearch(adjList, w, y));

        }

        [TestMethod]
        public void breathFirstSearchTreeDataStructure()
        {
            Dictionary<MStation, LinkedList<MStation>> adjList = new Dictionary<MStation, LinkedList<MStation>>();
            MStation s = new MStation() { Id = 1 };
            MStation r = new MStation() { Id = 2 };
            MStation v = new MStation() { Id = 3 };
            MStation w = new MStation() { Id = 4 };
            MStation t = new MStation() { Id = 5 };
            MStation x = new MStation() { Id = 6 };
            MStation y = new MStation() { Id = 7 };
            MStation u = new MStation() { Id = 8 };

            LinkedList<MStation> adjS = new LinkedList<MStation>();
            LinkedList<MStation> adjR = new LinkedList<MStation>();
            LinkedList<MStation> adjV = new LinkedList<MStation>();
            LinkedList<MStation> adjW = new LinkedList<MStation>();
            LinkedList<MStation> adjT = new LinkedList<MStation>();
            LinkedList<MStation> adjX = new LinkedList<MStation>();
            LinkedList<MStation> adjY = new LinkedList<MStation>();
            LinkedList<MStation> adjU = new LinkedList<MStation>();

            adjList.Add(s, adjS);
            adjList.Add(r, adjR);
            adjList.Add(y, adjY);
            adjList.Add(t, adjT);
            adjList.Add(x, adjX);
            adjList.Add(v, adjV);
            adjList.Add(u, adjU);
            adjList.Add(w, adjW);

            adjS.AddFirst(r);
            adjS.AddLast(v);

            adjR.AddFirst(w);
            adjR.AddLast(t);

            adjV.AddFirst(x);
            adjV.AddLast(y);

            adjT.AddFirst(u);

            adjY.AddFirst(v);
            adjV.AddLast(s);
            Assert.AreEqual(1, PathFind.breathFirstSearch(adjList, s, r));
            Assert.AreEqual(2, PathFind.breathFirstSearch(adjList, s, w));
            Assert.AreEqual(3, PathFind.breathFirstSearch(adjList, s, u));
            Assert.AreEqual(2, PathFind.breathFirstSearch(adjList, s, x));
            Assert.AreEqual(0, PathFind.breathFirstSearch(adjList, x, y));
            Assert.AreEqual(2, PathFind.breathFirstSearch(adjList, y, s));
        }

        [TestMethod]
        public void breathFirstSearchCyclicDataStructure()
        {
            Dictionary<MStation, LinkedList<MStation>> adjList = new Dictionary<MStation, LinkedList<MStation>>();
            MStation s = new MStation() { Id = 1 };
            MStation r = new MStation() { Id = 2 };
            MStation v = new MStation() { Id = 3 };
            MStation w = new MStation() { Id = 4 };
            MStation t = new MStation() { Id = 5 };
            MStation x = new MStation() { Id = 6 };
            MStation y = new MStation() { Id = 7 };
            MStation u = new MStation() { Id = 8 };

            LinkedList<MStation> adjS = new LinkedList<MStation>();
            LinkedList<MStation> adjR = new LinkedList<MStation>();
            LinkedList<MStation> adjV = new LinkedList<MStation>();
            LinkedList<MStation> adjW = new LinkedList<MStation>();
            LinkedList<MStation> adjT = new LinkedList<MStation>();
            LinkedList<MStation> adjX = new LinkedList<MStation>();
            LinkedList<MStation> adjY = new LinkedList<MStation>();
            LinkedList<MStation> adjU = new LinkedList<MStation>();

            adjS.AddFirst(w);
            adjS.AddLast(r);
            
            adjR.AddFirst(v);
            adjR.AddLast(s);
            
            adjW.AddFirst(t);
            adjW.AddLast(x);
            adjW.AddLast(s);

            adjX.AddFirst(t);
            adjX.AddLast(w);
            adjX.AddLast(y);

            adjT.AddLast(w);
            adjT.AddLast(x);
            adjT.AddLast(u);

            adjU.AddFirst(t);
            adjU.AddLast(x);
            adjU.AddLast(y);

            adjY.AddFirst(x);
            adjY.AddLast(u);

            adjV.AddFirst(r);

            adjList.Add(s, adjS);
            adjList.Add(r, adjR);
            adjList.Add(y, adjY);
            adjList.Add(t, adjT);
            adjList.Add(x, adjX);
            adjList.Add(v, adjV);
            adjList.Add(u, adjU);
            adjList.Add(w, adjW);
            
            Assert.AreEqual(1, PathFind.breathFirstSearch(adjList, s, r));
            Assert.AreEqual(2, PathFind.breathFirstSearch(adjList, s, v));
            Assert.AreEqual(1, PathFind.breathFirstSearch(adjList, s, w));
            Assert.AreEqual(2, PathFind.breathFirstSearch(adjList, s, t));
            Assert.AreEqual(2, PathFind.breathFirstSearch(adjList, s, x));
            Assert.AreEqual(3, PathFind.breathFirstSearch(adjList, s, u));
            Assert.AreEqual(3, PathFind.breathFirstSearch(adjList, s, y));
            Assert.AreEqual(5, PathFind.breathFirstSearch(adjList, u, v));
            Assert.AreEqual(2, PathFind.breathFirstSearch(adjList, w, y));
        }
    }
}
