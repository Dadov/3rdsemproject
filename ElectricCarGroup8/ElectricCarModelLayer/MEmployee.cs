using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class MEmployee : MPerson
    {
        public MEmployee(EmployeePosition position, int id, string fName, string lName,
            string address, string country, string phone, string email, ICollection<MLogInfo> logInfos, 
            string payStatus)
            : base(id, fName, lName, address, country, phone, email, logInfos, payStatus)
        {
            Position = position;
        }

        EmployeePosition Position { get; set; }
    }

    public enum EmployeePosition
    {
        // TODO: define possible positions
        Boss, LiftBoy
    }
}
