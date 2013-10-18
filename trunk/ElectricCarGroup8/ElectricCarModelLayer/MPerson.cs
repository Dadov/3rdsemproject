using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public abstract class MPerson
    {
        // constructor
        public MPerson(int id, string fName, string lName, string address, string country,
            string phone, string email, MLogInfo logInfo, string payStatus)
        {
            ID = id;
            FName = fName;
            LName = lName;
            Address = address;
            Country = country;
            Phone = phone;
            Email = email;
            PayStatus = payStatus;
        }
        public MPerson() { }
        // properties
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public MLogInfo LogInfo { get; set; }
        // TODO: what is this for?
        public string PayStatus { get; set; }
    }
}
