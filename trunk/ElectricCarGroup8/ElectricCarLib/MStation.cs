using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarLib
{
    //TODO complete the class
    public class MStation
    {
        public MStation()
        {
            this.Connection = new HashSet<MConnection>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public State state { get; set; }
    

        public virtual ICollection<MConnection> Connection { get; set; }
        
    }
}
