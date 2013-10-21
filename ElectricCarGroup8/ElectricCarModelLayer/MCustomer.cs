using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class MCustomer : MPerson
    {
        public MCustomer() { }
        public MCustomer(MDiscountGroup discountGroup, int id, string fName, string lName,
            string address, string country, string phone, string email, ICollection<MLogInfo> logInfos,
            string payStatus)
            : base(id, fName, lName, address, country, phone, email, logInfos, payStatus) 
        {
            DiscountGroup = discountGroup;
        }

        MDiscountGroup DiscountGroup { get; set; }
    }
}
