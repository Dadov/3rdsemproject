using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ElectricCarLib
{
    class SystemException: Exception 
    {
        private string p;

        public SystemException(string message, Exception inner): base(message, inner)
        {

        }

        public SystemException(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }

        public override string ToString()
        {
            return p;
        }
    }
}
