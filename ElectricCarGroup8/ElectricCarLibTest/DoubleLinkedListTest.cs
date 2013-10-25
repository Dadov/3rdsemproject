using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarModelLayer;

namespace ElectricCarLibTest
{
    [TestClass]
    public class DoubleLinkedListTest
    {
        [TestMethod]
        public void AddDoubleLinkedList()
        {
            DoubleLinkedList dll = new DoubleLinkedList();
            FibonacciNode n1= new FibonacciNode(){StationID = 1}; 
            FibonacciNode n2= new FibonacciNode(){StationID = 2};
            FibonacciNode n3 = new FibonacciNode() { StationID = 3 };
            dll.Add(n1);
            Assert.AreEqual(1, dll.head.StationID);
            Assert.AreEqual(1, dll.head.RightNode.StationID);
            Assert.AreEqual(1, dll.head.LeftNode.StationID);

            dll.Add(n2);
            Assert.AreEqual(1, dll.head.StationID);
            Assert.AreEqual(2, dll.head.RightNode.StationID);
            Assert.AreEqual(1, dll.head.RightNode.RightNode.StationID);

            dll.Add(n3);

            Assert.AreEqual(1, dll.head.StationID);
            Assert.AreEqual(2, dll.head.RightNode.StationID);
            Assert.AreEqual(3, dll.head.RightNode.RightNode.StationID);
            Assert.AreEqual(1, dll.head.LeftNode.RightNode.StationID);
        }

        [TestMethod]
        public void DeleteDoubleLinkedList()
        {
            DoubleLinkedList dll = new DoubleLinkedList();
            FibonacciNode n1 = new FibonacciNode() { StationID = 1 };
            FibonacciNode n2 = new FibonacciNode() { StationID = 2 };
            FibonacciNode n3 = new FibonacciNode() { StationID = 3 };
            dll.Add(n1);
            dll.Add(n2);
            dll.Add(n3);

            Assert.AreEqual(1, dll.head.StationID);
            Assert.AreEqual(2, dll.head.RightNode.StationID);
            Assert.AreEqual(3, dll.head.RightNode.RightNode.StationID);
            Assert.AreEqual(1, dll.head.LeftNode.RightNode.StationID);

            dll.Delete(n3);
            Assert.AreEqual(1, dll.head.StationID);
            Assert.AreEqual(2, dll.head.RightNode.StationID);
            Assert.AreEqual(1, dll.head.LeftNode.RightNode.StationID);

            dll.Delete(n1);
            Assert.AreEqual(2, dll.head.StationID);
            Assert.AreEqual(2, dll.head.RightNode.StationID);
        }
    }
}
