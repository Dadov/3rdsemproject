using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricCarModelLayer;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        [TestMethod]
        public void streessTest()
        {
            long i = 0;
            while (i <= 100)//value can be changed to 1000000(28 min to finish) or others
            {
                //if (i % 100 == 0)
                //{
                    System.Diagnostics.Debug.WriteLine(i + "");
                //}

                extract_minWithRandomNumbers();
                i++;

                
            }
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void extract_minWithRandomNumbers()
        {
            List<int> dataList = new List<int>();
            try
            {
                FibonacciHeap heap = new FibonacciHeap();
                Random r = new Random();
                int ran = 0;
                decimal preValue = 0;
                decimal currentExtractValue = 0;
                int size = 100; //can be adjust it
                for (int i = 0; i < size; i++)
                {
                    FibonacciNode node = heap.insert(i + 1);
                    ran = r.Next(1, 100);
                    heap.DecreasingKey(heap.root, node, ran);
                    dataList.Add(ran);
                    Assert.AreEqual(i + 1, heap.numberOfNodes);
                }

                for (int i = 0; i < size; i++)
                {
                    FibonacciNode node = heap.extractMinNode();
                    Assert.AreEqual(size - (i + 1), heap.numberOfNodes);
                    currentExtractValue = node.MinPathValue;
                    if (currentExtractValue < preValue)
                    {
                        Assert.Fail();
                    }
                    preValue = currentExtractValue;

                }
            }
            catch (IndexOutOfRangeException e)
            {
                writeDataToFile(e.Message, dataList);
                throw new Exception();
            }
            catch (AssertFailedException e)
            {
                writeDataToFile(e.Message, dataList);
                throw new Exception();
            }


        }

        [TestMethod]
        public void runUnpassedData()
        {
            List<int> dataList = readDataFromFile();
            decimal preValue = 0;
            decimal currentExtractValue = 0;
            FibonacciHeap heap = new FibonacciHeap();
            for (int i = 0; i < dataList.Count; i++)
            {
                FibonacciNode node = heap.insert(i+1);
                heap.DecreasingKey(heap.root, node, dataList[i]);
                Assert.AreEqual(i + 1, heap.numberOfNodes);
            }

            dataList.Sort();

            
            List<FibonacciNode> listNode = new List<FibonacciNode>();
            int size = dataList.Count;
            for (int i = 0; i < dataList.Count; i++)
            {
                FibonacciNode node = heap.extractMinNode();
                listNode.Add(node);
                Assert.AreEqual(size - (i + 1), heap.numberOfNodes);
                currentExtractValue = node.MinPathValue;
                if (currentExtractValue < preValue)
                {
                    Assert.Fail();
                }
                preValue = currentExtractValue;

            }

            for (int i = 0; i < dataList.Count; i++)
            {
                Assert.AreEqual(dataList[i], Convert.ToInt32(listNode[i].MinPathValue));
            }
        }

        [TestMethod]
        public List<int> readDataFromFile()
        {
            List<int> dataList = new List<int>();
            string path = Directory.GetCurrentDirectory();
            string suffix = @"bin\Debug";
            char[] trailingChars = suffix.ToCharArray();
            path = path.TrimEnd(trailingChars);
            using (StreamReader sr = new StreamReader(path + @"\Unpassed data\data2.txt"))
            {

                String[] line = sr.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                for (int i = 1; i < line.Length - 1; i++)
                {
                    string t = line[i].Trim();
                    dataList.Add(Convert.ToInt32(t));
                }
            }
            
            
            return dataList;

        }

        public void writeDataToFile(string message, List<int> data)
        {
            string path = Directory.GetCurrentDirectory();
            string suffix = @"bin\Debug";
            char[] trailingChars = suffix.ToCharArray();
            path = path.TrimEnd(trailingChars);
            using (StreamWriter file = new StreamWriter(path + @"\Unpassed data\data5.txt"))
            {

                file.WriteLine(message);
                foreach (int i in data)
                {
                    file.WriteLine(i);
                }
            }
        }
    }
}
