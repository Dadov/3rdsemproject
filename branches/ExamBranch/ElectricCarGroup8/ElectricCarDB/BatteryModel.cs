using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    public partial class Battery
    {
       
        public override string ToString()
        {
            return "id: " + Convert.ToString(Id) +
                " name: " + state +
                " battery type ID: " + Convert.ToString(btId);
        }
    }
    
}
