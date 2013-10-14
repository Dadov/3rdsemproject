using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class MDiscountGroup
    {
        public MDiscountGroup(int id, string name, int discount) 
        {
            ID = id;
            Name = name;
            Discount = discount;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        // TODO: consider data type
        public int Discount { get; set; }
    }
}
