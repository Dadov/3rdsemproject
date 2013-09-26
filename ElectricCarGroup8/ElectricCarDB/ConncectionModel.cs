using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarDB
{
    public partial class Connection
    {
        public override string ToString()
        {
            return "Station1Id: " + Convert.ToString(sId1) +
                " Station2Id:" + Convert.ToString(sId2) +
                " Distane: " + Convert.ToString(distance) +
                " DriveHour: " + Convert.ToString(driveHour);
        }
    }
}
