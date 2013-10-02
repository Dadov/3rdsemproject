using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarLib
{
    public class MBatteryType
    {
        public int id { get; set; }
        public string name { get; set; }
        public string producer { get; set; }
        public Nullable<decimal> capacity { get; set; }
        public Nullable<decimal> exchangeCost { get; set; }
        public int storageNumber { get; set; }

        public override string ToString()
        {
            return "id: " + Convert.ToString(id) +
                " name: " + name +
                " producer: " + producer +
                " capacity:" + Convert.ToString(capacity) +
                " exchnage cost: " + Convert.ToString(exchangeCost) +
                " storage number: " + Convert.ToString(storageNumber);
        }
    }
}
