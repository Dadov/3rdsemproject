using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class MConnection
    {

        public Nullable<decimal> distance { get; set; }
        public Nullable<decimal> driveHour { get; set; }

        public virtual MStation Station1 { get; set; }
        public virtual MStation Station2 { get; set; }

        public override string ToString()
        {
            return "Station1Id: " + Convert.ToString(Station1.Id) +
                " Station2Id:" + Convert.ToString(Station2.Id) +
                " Distane: " + Convert.ToString(distance) +
                " DriveHour: " + Convert.ToString(driveHour);
        }
    }
}
