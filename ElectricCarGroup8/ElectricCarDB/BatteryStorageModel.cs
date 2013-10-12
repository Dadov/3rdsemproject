using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    public partial class BatteryStorage
    {
        public override string ToString()
        {
            return "Id: " + Convert.ToString(Id) +
               "Battery Type ID: " + Convert.ToString(btId) +
               "Station ID: " + Convert.ToString(sId);
        }
    }
}
