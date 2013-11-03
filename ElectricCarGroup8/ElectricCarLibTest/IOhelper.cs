using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ElectricCarLibTest
{
    public static class IOhelper
    {
        public static void writeDataToFile(string filepath, string message, Dictionary<int, Dictionary<int, decimal>> list)
        {

            using (StreamWriter file = new StreamWriter(filepath))
            {

                file.WriteLine(message);

                foreach (int i in list.Keys)
                {


                    file.WriteLine(i);
                    foreach (int j in list[i].Keys)
                    {
                        if (list[i].Count != 0)
                        {
                            file.WriteLine(j + "," + list[i][j]);
                        }
                        else
                        {
                            file.WriteLine("No edge");
                        }

                    }
                    file.WriteLine("");

                }
            }
        }

        public static Dictionary<int, Dictionary<int, decimal>> readDataFromFile(string filepath)
        {
            Dictionary<int, Dictionary<int, decimal>> adjList = new Dictionary<int, Dictionary<int, decimal>>();

            using (StreamReader sr = new StreamReader(filepath))
            {

                String[] line = sr.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                int nodeId = 0;
                for (int i = 1; i < line.Length - 1; i++)
                {
                    string t = line[i].Trim();

                    if (t != "")
                    {
                        String[] pair = t.Split(new string[] { "," }, StringSplitOptions.None);
                        if (pair.Length == 1)
                        {
                            Dictionary<int, decimal> list = new Dictionary<int, decimal>();
                            nodeId = Convert.ToInt32(t);
                            adjList.Add(nodeId, list);
                        }
                        else
                        {

                            if (pair[0].Equals("No") && pair[1].Equals("edge"))
                            {

                            }
                            else
                            {
                                adjList[nodeId].Add(Convert.ToInt32(pair[0]), Convert.ToDecimal(pair[1]));
                            }
                        }
                    }
                }
            }
            return adjList;
        }

        public static void outPutGraphData(string path, Dictionary<int, Dictionary<int, decimal>> adjList, int startId, int endId)
        {
            
            using (StreamWriter file = new StreamWriter(path))
            {
                file.WriteLine("<?xml version='1.0' encoding='UTF-8'?>");
                file.WriteLine("<gexf xmlns='http://www.gexf.net/1.2draft' version='1.2'>");
                file.WriteLine("<meta lastmodifieddate='2009-03-20'>");
                file.WriteLine("<creator>Gexf.net</creator>");
                file.WriteLine("<description>A hello world! file</description>");
                file.WriteLine("</meta>");
                file.WriteLine("<graph mode='static' defaultedgetype='undirected'>");
                file.WriteLine("<attributes class='node' mode='static'>");
                file.WriteLine("<attribute id='0' title='selected' type='integer'>");
                file.WriteLine("<default>1</default>");
                file.WriteLine("</attribute>");
                file.WriteLine("</attributes>");
                //add nodes
                file.WriteLine("<nodes>");
                foreach (int id in adjList.Keys)
                {
                    if (id == startId || id == endId)
                    {
                        file.WriteLine("<node id='" + id + "' label='" + id + "'>");
                        file.WriteLine("<attvalues>");
                        file.WriteLine("<attvalue for='0' value='100'/>");
                        file.WriteLine("</attvalues>");
                        file.WriteLine("</node>");
                    }
                    else
                    {
                        file.WriteLine("<node id='" + id + "' label='" + id + "'/>");
                    }

                }
                file.WriteLine("</nodes>");

                //add edges
                int edgeId = 0;
                file.WriteLine("<edges>");
                foreach (int id in adjList.Keys)
                {
                    foreach (int id2 in adjList[id].Keys)
                    {
                        if (id2 > id)
                        {
                            file.WriteLine("<edge id='" + edgeId + "' source='" + id + "' target='" + id2 + "' weight='" + adjList[id][id2] + "'/>");
                            edgeId++;
                        }
                    }
                }

                file.WriteLine("</edges>");
                file.WriteLine("</graph>");
                file.WriteLine("</gexf>");
            }
        }
    }
}
