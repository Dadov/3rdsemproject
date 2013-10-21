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

        public MCustomer(int id, string fName, string lName, string address, string country,
            string phone, string email, ICollection<MLogInfo> logInfos,
            PType pType, MDiscountGroup discountGroup, string payStatus)
            : base(id, fName, lName, address, country, phone, email, logInfos, pType) 
        {
            DiscountGroup = discountGroup;
            PaymentStatus = payStatus;
        }

        string PaymentStatus { get; set; }
        MDiscountGroup DiscountGroup { get; set; }
    }
}
