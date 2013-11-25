using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarDB
{
    public partial class Period
    {
        
        public override string ToString()
        {
            return "time: " + Convert.ToString(time) +
               "Initially available battery number: " + Convert.ToString(avaiNumber) +
               "Number of batteries booked by customers: " + Convert.ToString(custBookNumber);
        }
    }
}
