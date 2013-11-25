using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class MBatteryType
    {
        public int id { get; set; }
        public string name { get; set; }
        public string producer { get; set; }
        public Nullable<decimal> capacity { get; set; }
        public Nullable<decimal> exchangeCost { get; set; }
    }
}
