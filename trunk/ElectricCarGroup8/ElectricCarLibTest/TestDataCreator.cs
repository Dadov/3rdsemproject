using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ElectricCarDB;
using ElectricCarModelLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElectricCarLibTest
{
    [TestClass]
    public class TestDataCreator
    {
        [TestMethod]
        public void Main()
        {
            List<Station> stations = new List<Station>();
            List<Connection> conns = new List<Connection>();
            IDBatteryType dbType = new DBatteryType();
            int sId1 = 0;
            int i = 1;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string[] lines = System.IO.File.ReadAllLines(@"\TestNodeSize50.txt");
            lines[0] = "";
            foreach (string line in lines)
            {
                int x;
                int[] numbers = (from Match m in Regex.Matches(line, @"\d+") select int.Parse(m.Value)).ToArray();
                string digits = new String(line.TakeWhile(Char.IsDigit).ToArray());
                int neighbor;
                int.TryParse(digits, out neighbor);
                if (int.TryParse(line, out x))
                {
                    Station station = new Station();
                    station.Id = x;
                    station.name = "Station_" + x;
                    station.state = "Open";
                    station.address = "Address" + x;
                    station.country = "Denmark";
                    stations.Add(station);
                    sId1 = x;
                }
                else if(line != "" && x<neighbor)
                {
                    Connection conn = new Connection();
                    conn.sId1 = sId1;
                    conn.sId2 = neighbor;
                    conn.distance = numbers[1];
                    decimal drive = (decimal)numbers[1] / (decimal)70;
                    conn.driveHour = Math.Round(drive, 1);
                    conns.Add(conn);
                }
            }
            List<string> output = new List<string>();
            List<string> storages = new List<string>();
            List<string> periods = new List<string>();
            foreach (Station station in stations)
            {

                string text = "insert into Station values ('" + station.Id + "', '" + station.name + "', '" + station.address + "', '" + station.country + "', '" + station.state + "')";
                output.Add(text);
                foreach (MBatteryType bt in dbType.getAllRecord(true))
                {
                    string s = "insert into BatteryStorage values ('" + i + "', '" + bt.id + "', '" +station.Id + "',"+ "10)";
                    storages.Add(s);
                    string p = "insert into Period values ('" + i + "', '" + DateTime.Today + "', 10, 0)";
                    periods.Add(p);
                    i++;
                }
            }

            foreach (Connection conn in conns)
            {
                decimal d = conn.driveHour.Value;
                string withDot = d.ToString(CultureInfo.InvariantCulture);
                string text = "insert into Connection values ('" + conn.sId1 + "', '" + conn.sId2 + "', '" + conn.distance + "', '" + withDot + "')";
                output.Add(text);
            }
            output.AddRange(storages);
            output.AddRange(periods);
            System.IO.File.WriteAllLines(@"\TestNode50.txt", output);
        }
    }
}
