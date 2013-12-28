using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarLib
{
    public class MBattery
    {
        public int id { get; set; }
        public string state { get; set; }
        public virtual MBatteryType batteryType { get; set; }
        

        public override string ToString()
        {
            return "id: " + Convert.ToString(id) +
                " name: " + state +
                " battery typee ID: " + Convert.ToString(batteryType.id);
        }
    }
    
}
