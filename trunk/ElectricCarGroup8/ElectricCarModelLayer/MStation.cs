using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    //TODO complete the class
    public class MStation
    {
        public MStation()
        {
            this.naboStations = new Dictionary<MStation, decimal>();
        }

        public int Id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public State state { get; set; }
        public List<MBatteryStorage> storages { get; set; }

        public Dictionary<MStation, decimal> naboStations { get; set; }

    }
}
