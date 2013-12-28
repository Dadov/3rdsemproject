using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarLib
{
    public class MBatteryStorage
    {
        public int id { get; set; }
        public virtual MBatteryType type { get; set; }
        public List<MPeriod> periods { get; set; }
    }
}
