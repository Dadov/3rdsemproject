using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using ElectricCarLib;

namespace ElectricCarLibTest
{
    [TestClass]
    public class GraphAlgorithmsEffciencyTest
    {
        //[TestMethod]
        public void GenerateTestDataSet()
        {

            string path = Directory.GetCurrentDirectory();
            string suffix = @"bin\Debug";
            char[] trailingChars = suffix.ToCharArray();
            path = path.TrimEnd(trailingChars) + @"\Unpassed data\TestNodeSize";

            for (int i = 0; i < 7; i++)
            {

                Dictionary<int, Dictionary<int, decimal>> adjList = new Dictionary<int, Dictionary<int, decimal>>();
                int size = (int)Math.Pow(10, i + 1); //size of random graph can be ajusted, when size>1000 it will generate OutOfMemoryException
                string pathToTxt = path + size + ".txt";
                string pathToGexf = path + size + ".gexf";
                Random r = new Random();
                int startId = r.Next(1, size + 1);
                int endId = r.Next(1, size + 1);
                double density = 0.2; //density of random graph can be adjusted
                RandomGraphGenerator graph = new RandomGraphGenerator(size, density, 1, 300);
                adjList = graph.getAdjList();

                IOhelper.writeDataToFile(pathToTxt, "StartId: " + startId + "  EndId: " + endId, adjList);
                IOhelper.outPutGraphData(pathToGexf, adjList, startId, endId);
            }


        }

        //[TestMethod]
        public void SPAWithFibonacciEffiencey()
        {
            string path = Directory.GetCurrentDirectory();
            string suffix = @"bin\Debug";
            char[] trailingChars = suffix.ToCharArray();
            path = path.TrimEnd(trailingChars) + @"\Unpassed data\";
            string pathToOutput = path + "SPAWithFibonacciDataStructure.txt";
            StreamWriter file = new StreamWriter(pathToOutput);

            file.WriteLine("#Input size(nodes number)" + "\t" + "#AverageOf3RunningTime");
            for (int j = 0; j < 3; j++)
            {
                
                int size = (int)Math.Pow(10, j + 1);
                string dataPath = path + "TestNodeSize" + size + ".txt";

                Dictionary<int, Dictionary<int, decimal>> list = IOhelper.readDataFromFile(dataPath);
                DateTime start;
                TimeSpan timeItTook;
                if (j== 0)
	            {
		            start = DateTime.Now;
                    PathFind.shortestPathWithFibonacci(list, 9, 8);
                    timeItTook = DateTime.Now - start;
	            } else if (j== 1)
	            {
		            start = DateTime.Now;
                    PathFind.shortestPathWithFibonacci(list, 100, 37);
                    timeItTook = DateTime.Now - start;
	            } else 
	            {
		            start = DateTime.Now;
                    PathFind.shortestPathWithFibonacci(list, 910, 325);
                    timeItTook = DateTime.Now - start;
	            }
                
                double milliseconds = timeItTook.TotalMilliseconds;
                file.WriteLine(size + "\t" + milliseconds);
            }

            file.Close();
        }

        //[TestMethod]
        public void SPAWithoutFibonacciEffiencey()
        {
            string path = Directory.GetCurrentDirectory();
            string suffix = @"bin\Debug";
            char[] trailingChars = suffix.ToCharArray();
            path = path.TrimEnd(trailingChars) + @"\Unpassed data\";
            string pathToOutput = path + "SPAWithoutFibonacciDataStructure.txt";
            StreamWriter file = new StreamWriter(pathToOutput);

            file.WriteLine("#Input size(nodes number)" + "\t" + "#AverageOf3RunningTime");
            for (int j = 0; j < 3; j++)
            {

                int size = (int)Math.Pow(10, j + 1);
                string dataPath = path + "TestNodeSize" + size + ".txt";

                Dictionary<int, Dictionary<int, decimal>> list = IOhelper.readDataFromFile(dataPath);
                DateTime start;
                TimeSpan timeItTook;
                if (j == 0)
                {
                    start = DateTime.Now;
                    PathFind.shortestPathWithoutFibonacci(list, 9, 8);
                    timeItTook = DateTime.Now - start;
                }
                else if (j == 1)
                {
                    start = DateTime.Now;
                    PathFind.shortestPathWithoutFibonacci(list, 100, 37);
                    timeItTook = DateTime.Now - start;
                }
                else
                {
                    start = DateTime.Now;
                    PathFind.shortestPathWithoutFibonacci(list, 910, 325);
                    timeItTook = DateTime.Now - start;
                }

                double milliseconds = timeItTook.TotalMilliseconds;
                file.WriteLine(size + "\t" + milliseconds);
            }

            file.Close();
        }

        //[TestMethod]
        public void BFSStopInEndDestinationEfficiency()
        {
            string path = Directory.GetCurrentDirectory();
            string suffix = @"bin\Debug";
            char[] trailingChars = suffix.ToCharArray();
            path = path.TrimEnd(trailingChars) + @"\Unpassed data\";
            string pathToOutput = path + "BFSStopInEndDestination.txt";
            StreamWriter file = new StreamWriter(pathToOutput);

            file.WriteLine("#Input size(nodes number)" + "\t" + "#AverageOf3RunningTime");
            for (int j = 0; j < 3; j++)
            {

                int size = (int)Math.Pow(10, j + 1);
                string dataPath = path + "TestNodeSize" + size + ".txt";

                Dictionary<int, Dictionary<int, decimal>> list = IOhelper.readDataFromFile(dataPath);
                DateTime start;
                TimeSpan timeItTook;
                if (j == 0)
                {
                    start = DateTime.Now;
                    PathFind.leastStopsPathWithIds(list, 9, 8);
                    timeItTook = DateTime.Now - start;
                }
                else if (j == 1)
                {
                    start = DateTime.Now;
                    PathFind.leastStopsPathWithIds(list, 100, 37);
                    timeItTook = DateTime.Now - start;
                }
                else
                {
                    start = DateTime.Now;
                    PathFind.leastStopsPathWithIds(list, 910, 325);
                    timeItTook = DateTime.Now - start;
                }

                double milliseconds = timeItTook.TotalMilliseconds;
                file.WriteLine(size + "\t" + milliseconds);
            }

            file.Close();
        }

        [TestMethod]
        public void BFSEfficiency()
        {
            string path = Directory.GetCurrentDirectory();
            string suffix = @"bin\Debug";
            char[] trailingChars = suffix.ToCharArray();
            path = path.TrimEnd(trailingChars) + @"\Unpassed data\";
            string pathToOutput = path + "BFS.txt";
            StreamWriter file = new StreamWriter(pathToOutput);

            file.WriteLine("#Input size(nodes number)" + "\t" + "#AverageOf3RunningTime");
            for (int j = 0; j < 3; j++)
            {

                int size = (int)Math.Pow(10, j + 1);
                string dataPath = path + "TestNodeSize" + size + ".txt";

                Dictionary<int, Dictionary<int, decimal>> list = IOhelper.readDataFromFile(dataPath);
                DateTime start;
                TimeSpan timeItTook;
                if (j == 0)
                {
                    start = DateTime.Now;
                    PathFind.breathFirstSearchWithIds(list, 9, 8);
                    timeItTook = DateTime.Now - start;
                }
                else if (j == 1)
                {
                    start = DateTime.Now;
                    PathFind.breathFirstSearchWithIds(list, 100, 37);
                    timeItTook = DateTime.Now - start;
                }
                else
                {
                    start = DateTime.Now;
                    PathFind.breathFirstSearchWithIds(list, 910, 325);
                    timeItTook = DateTime.Now - start;
                }

                double milliseconds = timeItTook.TotalMilliseconds;
                file.WriteLine(size + "\t" + milliseconds);
            }

            file.Close();
        }
    }
}
