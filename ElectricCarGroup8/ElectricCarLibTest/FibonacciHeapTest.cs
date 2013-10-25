using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarModelLayer;

namespace ElectricCarLibTest
{
    [TestClass]
    public class FibonacciHeapTest
    {
        [TestMethod]
        public void insertNode()
        {
            FibonacciHeap heap = new FibonacciHeap();
            heap.insert(1);
            
            Assert.AreEqual(1, heap.numberOfNodes);
            Assert.AreEqual(1, heap.root.head.StationID);
            Assert.AreEqual(1, heap.minNode.StationID);

            heap.insert(2);
            Assert.AreEqual(2, heap.numberOfNodes);
            Assert.AreEqual(1, heap.root.head.StationID);
            Assert.AreEqual(1, heap.minNode.StationID);

            Assert.AreEqual(2, heap.root.head.RightNode.StationID);
            Assert.AreEqual(1, heap.minNode.StationID);

        }

        [TestMethod]
        public void extractMinNode()
        {
            FibonacciHeap heap = new FibonacciHeap();
            FibonacciNode n1 = heap.insert(1);
            n1.MinPathValue = 5;
            FibonacciNode n2 = heap.insert(2);
            n2.MinPathValue = 10;
            FibonacciNode n3 = heap.insert(3);
            n3.MinPathValue = 15;

            Assert.AreEqual(3, heap.numberOfNodes);

            FibonacciNode min = heap.extractMinNode();
            Assert.AreEqual(1, min.StationID);
            Assert.AreEqual(5, Convert.ToInt32(min.MinPathValue));
            Assert.AreEqual(2, heap.numberOfNodes);
            Assert.AreEqual(2, heap.minNode.StationID);

            FibonacciNode min2 = heap.extractMinNode();
            Assert.AreEqual(2, min2.StationID);
            Assert.AreEqual(10, Convert.ToInt32(min2.MinPathValue));
            Assert.AreEqual(1, heap.numberOfNodes);
            Assert.AreEqual(3, heap.minNode.StationID);

            FibonacciNode min3 = heap.extractMinNode();
            Assert.AreEqual(3, min3.StationID);
            Assert.AreEqual(15, Convert.ToInt32(min3.MinPathValue));
            Assert.AreEqual(0, heap.numberOfNodes);
            Assert.AreEqual(null, heap.minNode);
        }

        [TestMethod]
        public void consolidateTree()
        {
            FibonacciHeap heap = new FibonacciHeap();
            FibonacciNode n1 = heap.insert(1);
            FibonacciNode n2 = heap.insert(2);
            n1.MinPathValue = 23;
            n2.MinPathValue = 7;
            heap.consolidateTree(heap.root);

            Assert.AreEqual(2, heap.minNode.StationID);
            Assert.AreEqual(n2, n1.Parent);
            Assert.AreEqual(1, n2.Degree);
            Assert.AreEqual(0, n1.Degree);
            Assert.AreEqual(n1, n2.Child);

            FibonacciNode n3 = heap.insert(3);
            n3.MinPathValue = 2;
            heap.consolidateTree(heap.root);

            Assert.AreEqual(3, heap.minNode.StationID);
            Assert.AreEqual(0, n3.Degree);
            Assert.AreEqual(null, n3.Parent);
            Assert.AreEqual(null, n3.Child);
            Assert.AreEqual(n2, n1.Parent);
            Assert.AreEqual(1, n2.Degree);
            Assert.AreEqual(0, n1.Degree);
            Assert.AreEqual(n1, n2.Child);
        }

        [TestMethod]
        public void Link()
        {
            FibonacciHeap heap1 = new FibonacciHeap();
            DoubleLinkedList root1 = new DoubleLinkedList();
            FibonacciNode y = new FibonacciNode() { StationID = 1, MinPathValue = 10 };
            FibonacciNode x = new FibonacciNode() { StationID = 2, MinPathValue = 5 };
            root1.Add(y);
            root1.Add(x);

            heap1.Link(root1, y, x);

            Assert.AreEqual(2, root1.head.StationID);
            Assert.AreEqual(1, x.Child.StationID);
            Assert.AreEqual(1, x.Degree);
            Assert.AreEqual(2, root1.head.RightNode.StationID);

            FibonacciHeap heap2 = new FibonacciHeap();
            DoubleLinkedList root2 = new DoubleLinkedList();
            FibonacciNode z = new FibonacciNode() { StationID = 3, MinPathValue = 10 };
            x.Child = z;
            z.Parent = x;
            x.Degree = 1;
            root1.Add(y);
            root1.Add(x);

            heap2.Link(root2, y, x);

            Assert.AreEqual(2, root1.head.StationID);
            Assert.AreEqual(3, x.Child.StationID);
            Assert.AreEqual(2, x.Degree);
            Assert.AreEqual(2, root1.head.RightNode.StationID);
            Assert.AreEqual(3, x.Child.StationID);
            Assert.AreEqual(1, x.Child.RightNode.StationID);
        }

        [TestMethod]
        public void DecreasingKey()
        {
            FibonacciHeap heap = new FibonacciHeap();
            FibonacciNode x = heap.insert(1);
            x.MinPathValue = 10;
            heap.DecreasingKey(heap.root, x, 5);

            Assert.AreEqual(5, Convert.ToInt32(x.MinPathValue));
            Assert.AreEqual(1, heap.root.Size);

            FibonacciNode y = heap.insert(2);
            y.MinPathValue = 26;
            y.Parent = x;
            x.Child = y;
            heap.DecreasingKey(heap.root, y, 20);
            Assert.AreEqual(20, Convert.ToInt32(y.MinPathValue));
            Assert.AreEqual(2, heap.root.Size);
        }

        [TestMethod]
        public void Cut()
        {
            FibonacciHeap heap = new FibonacciHeap();
            DoubleLinkedList list = new DoubleLinkedList();
            FibonacciNode y = new FibonacciNode() { StationID = 1, MinPathValue = 10 };
            FibonacciNode x = new FibonacciNode() { StationID = 2, MinPathValue = 20 };
            x.Parent = y;
            y.Child = x;
            list.Add(y);

            heap.Cut(list, x, y);

            Assert.AreEqual(null, x.Parent);
            Assert.AreEqual(false, x.Mark);
            Assert.AreEqual(1, list.head.StationID);
            Assert.AreEqual(2, list.head.RightNode.StationID);

        }

        [TestMethod]
        public void CascadingCut()
        {
            FibonacciHeap heap = new FibonacciHeap();
            DoubleLinkedList list = new DoubleLinkedList();
            FibonacciNode y = new FibonacciNode() { StationID = 1, MinPathValue = 10 };
            FibonacciNode x = new FibonacciNode() { StationID = 2, MinPathValue = 20, Mark = true };
            FibonacciNode z = new FibonacciNode() { StationID = 3, MinPathValue = 21, Mark = true };
            x.Parent = y;
            y.Child = x;
            z.Parent = x;
            x.Child = z;
            list.Add(y);

            heap.CascadingCut(list, z);

            Assert.AreEqual(null, x.Child);
            Assert.AreEqual(null, y.Child);
            Assert.AreEqual(null, x.Parent);
            Assert.AreEqual(null, z.Parent);
            Assert.AreEqual(1, list.head.StationID);
            Assert.AreEqual(3, list.head.RightNode.StationID);
            Assert.AreEqual(2, list.head.RightNode.RightNode.StationID);

        }

        [TestMethod]
        public void calculateArraySize()
        {
            FibonacciHeap heap = new FibonacciHeap();
            Assert.AreEqual(4, heap.calculateArraySize(15));
            Assert.AreEqual(3, heap.calculateArraySize(7));
            Assert.AreEqual(4, heap.calculateArraySize(8));

        }
    }
}
