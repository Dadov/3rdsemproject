using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ElectricCarModelLayer
{
    public partial class MBattery
    {
        public int id { get; set; }
        public string state { get; set; }
        public virtual MBatteryType type { get; set; } 
        public override string ToString()
        {
            return "id: " + Convert.ToString(id) +
                " name: " + state +
                " battery type ID: " + Convert.ToString(type.id);
        }
    }
    
}
