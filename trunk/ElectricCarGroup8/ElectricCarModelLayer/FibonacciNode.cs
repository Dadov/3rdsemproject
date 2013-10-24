using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class FibonacciNode
    {
        public int StationID { get; set; }
        public decimal MinPathValue { get; set; }
        public FibonacciNode LeftNode { get; set; }
        public FibonacciNode RightNode { get; set; }
        public int Degree { get; set; }
        public FibonacciNode Child { get; set; }
        public FibonacciNode Parent { get; set; }
        public bool Mark { get; set; }

        public FibonacciNode lastStop { get; set; }
        public bool Visited { get; set; }
        public bool Added { get; set; }

        public FibonacciNode()
        {
            lastStop = null;
            Degree = 0;
            Parent = null;
            MinPathValue = Convert.ToDecimal(double.PositiveInfinity);
            Mark = false; //newly created node
            LeftNode = this; //node points to itself
            RightNode = this;
        }
    }
}
