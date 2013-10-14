using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class MCustomer : MPerson
    {
        public MCustomer(MDiscountGroup discountGroup, int id, string fName, string lName,
            string address, string country, string phone, string email, MLogInfo logInfo,
            string payStatus)
            : base(id, fName, lName, address, country, phone, email, logInfo, payStatus) 
        {
            DiscountGroup = discountGroup;
        }

        MDiscountGroup DiscountGroup { get; set; }
    }
}
