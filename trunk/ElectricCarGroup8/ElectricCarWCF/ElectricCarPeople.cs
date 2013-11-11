using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using ElectricCarModelLayer;

namespace ElectricCarWCF
{
    [DataContract]
    public partial class ElectricCarPeople  
    {
        [DataMember]
        public MCustomer cust;

        public string getME()
        {
            return null;
        }


        
    }
}
