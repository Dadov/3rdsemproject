using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarDB
{
    public partial class BatteryType
    {

        public override string ToString()
        {
            return "id: " + Convert.ToString(Id) +
                " name: " + name +
                " producer: " + producer +
                " capacity:" + Convert.ToString(capacity) +
                " exchnage cost: " + Convert.ToString(exchangeCost);
        }
    }
}
