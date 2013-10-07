using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarLib;
using System.Collections;
using System.Collections.Generic;

namespace ElectricCarLibTest
{
    [TestClass]
    public class PathFindTest
    {
        
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

            PathFind.leastStopsPath(adjList, s, r, out stops1);
            PathFind.leastStopsPath(adjList, s, v, out stops2);
            PathFind.leastStopsPath(adjList, s, w, out stops3);
            PathFind.leastStopsPath(adjList, s, t, out stops4);
            PathFind.leastStopsPath(adjList, s, x, out stops5);
            PathFind.leastStopsPath(adjList, s, y, out stops6);
            PathFind.leastStopsPath(adjList, s, u, out stops7);
            PathFind.leastStopsPath(adjList, w, y, out stops8);

            Assert.AreEqual(PathFind.breathFirstSearch(adjList, s, r), stops1 - 1);
            Assert.AreEqual(PathFind.breathFirstSearch(adjList, s, v), stops2 - 1);
            Assert.AreEqual(PathFind.breathFirstSearch(adjList, s, w), stops3 - 1);
            Assert.AreEqual(PathFind.breathFirstSearch(adjList, s, t), stops4 - 1);
            Assert.AreEqual(PathFind.breathFirstSearch(adjList, s, x), stops5 - 1);
            Assert.AreEqual(PathFind.breathFirstSearch(adjList, s, y), stops6 - 1);
            Assert.AreEqual(PathFind.breathFirstSearch(adjList, s, u), stops7 - 1);
            Assert.AreEqual(PathFind.breathFirstSearch(adjList, w, y), stops8 - 1);
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

            PathFind.leastStopsPath(adjList, s, r, out stops1);
            PathFind.leastStopsPath(adjList, s, w, out stops2);
            PathFind.leastStopsPath(adjList, s, u, out stops3);
            PathFind.leastStopsPath(adjList, s, x, out stops4);
            PathFind.leastStopsPath(adjList, x, y, out stops5);
            PathFind.leastStopsPath(adjList, y, s, out stops6);

            Assert.AreEqual(stops1 - 1, PathFind.breathFirstSearch(adjList, s, r));
            Assert.AreEqual(stops2 - 1, PathFind.breathFirstSearch(adjList, s, w));
            Assert.AreEqual(stops3 - 1, PathFind.breathFirstSearch(adjList, s, u));
            Assert.AreEqual(stops4 - 1, PathFind.breathFirstSearch(adjList, s, x));
            Assert.AreEqual(stops5, PathFind.breathFirstSearch(adjList, x, y));
            Assert.AreEqual(stops6 - 1, PathFind.breathFirstSearch(adjList, y, s));
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

            PathFind.leastStopsPath(adjList, s, r, out stops1);
            PathFind.leastStopsPath(adjList, s, v, out stops2);
            PathFind.leastStopsPath(adjList, s, w, out stops3);
            PathFind.leastStopsPath(adjList, s, t, out stops4);
            PathFind.leastStopsPath(adjList, s, x, out stops5);
            PathFind.leastStopsPath(adjList, s, u, out stops6);
            PathFind.leastStopsPath(adjList, s, y, out stops7);
            PathFind.leastStopsPath(adjList, u, v, out stops8);
            PathFind.leastStopsPath(adjList, w, y, out stops9);

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
