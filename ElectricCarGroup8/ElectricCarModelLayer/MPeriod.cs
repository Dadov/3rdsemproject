using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class MPeriod
    {
        public DateTime time { get; set; }
        public int initBatteryNumber { get; set; }
        public int bookedBatteryNumber { get; set; }
  


        public override string ToString()
        {
            return "id: " + Convert.ToString(time) +
               "Initially available battery number: " + Convert.ToString(initBatteryNumber) +
               "Number of batteries booked by customers: " + Convert.ToString(bookedBatteryNumber);
        }
    }
}
