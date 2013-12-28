using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace ElectricCarLibTest
{
    [TestClass]
    public class ConvertDataForBetterPlace
    {
        [TestMethod]
        public void ConvertDataForBetterPlaceToSql()
        {
            convertDataForBetterPlaceXmlToSql();
        }

        public void convertDataForBetterPlaceXmlToSql()
        {
            //load xml
            string path = Directory.GetCurrentDirectory();
            string suffix = @"bin\Debug";
            char[] trailingChars = suffix.ToCharArray();
            path = path.TrimEnd(trailingChars) + @"\DataForBetterPlace\";
            XDocument stationDoc = XDocument.Load(path + "StationXML.xml");
            XDocument connectionDoc = XDocument.Load(path + "ConnectionXML.xml");

            var stations = from RECORD in stationDoc.Descendants("RECORD") select RECORD;
            var connections = from RECORD in connectionDoc.Descendants("RECORD") select RECORD;

            string writPathToStation = path + @"StationSql.txt";
            string writPathToConnection = path + @"StationSql.txt";

            TextWriter tw = new StreamWriter(writPathToStation);

            foreach (var item in stations)
            {
                string id = (string)item.Element("id");
                string name = (string)item.Element("name");
                string longitude = (string)item.Element("longitude");
                string latitude = (string)item.Element("latitude");
                string address = "Station " + name;
                string country = "Denmark";
                string state = "Open";
                tw.WriteLine("insert into Station values ('" + id + "', '" + name + "', '" + address + "', '" + country + "', '" + state + "', '" + longitude + "', '" + latitude + "')");
            }

            string bt1 = @"insert into BatteryType values ('1', 'SmallBattery', 'EnergyFactory', '10', '30')";
            string bt2 = @"insert into BatteryType values ('2', 'MediantBattery', 'EnergyFactory', '12', '40')";
            string bt3 = @"insert into BatteryType values ('3', 'LargeBattery', 'EnergyFactory', '14', '45')";
            tw.WriteLine(bt1);
            tw.WriteLine(bt2);
            tw.WriteLine(bt3);

            int i = 1;

            List<string> storages = new List<string>();
            List<string> periods = new List<string>();
            foreach (var item in stations)
            {
                string id = (string)item.Element("id");
                for (int j = 1; j < 4; j++)
                {
                    string s = "insert into BatteryStorage values ('" + i + "', '" + j + "', '" + id + "'," + "10)";
                    storages.Add(s);
                    string p = "insert into Period values ('" + i + "', '" + DateTime.Today + "', '10', '0')";
                    periods.Add(p);
                    i++;
                }

            }

            List<string> output = new List<string>();
            output.AddRange(storages);
            output.AddRange(periods);
            foreach (string item in output)
            {
                tw.WriteLine(item);
                tw.Flush();
            }
            

           

            foreach (var item in connections)
            {
                string id1 = (string)item.Element("location1");
                string id2 = (string)item.Element("location2");
                string distance = (string)item.Element("distance");
                string time = (string)item.Element("time");
                string[] words = time.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                decimal driveHour = Convert.ToDecimal(words[1]);
                tw.WriteLine("insert into Connection values ('" + id1 + "', '" + id2 + "', '" + distance + "', '" + driveHour + "')");
            }

            tw.Close();



        }
    }
}
