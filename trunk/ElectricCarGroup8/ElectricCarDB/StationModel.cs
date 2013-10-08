using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarDB
{
    public partial class Station
    {
        public virtual LinkedList<Station> naborStations { get; set; }
        public virtual List<Connection> Connections { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
