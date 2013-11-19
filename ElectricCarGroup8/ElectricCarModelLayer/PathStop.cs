using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class PathStop
    {
        public int stationID { get; set; }
        public decimal distance { get; set; }
        public decimal driveHour { get; set; }
        public MStation station { get; set; }
        
    }
}
