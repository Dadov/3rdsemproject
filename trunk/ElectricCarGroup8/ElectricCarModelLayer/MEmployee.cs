using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class MEmployee : MPerson
    {
        public MEmployee(int id, string fName, string lName, string address, string country,
            string phone, string email, ICollection<MLogInfo> logInfos,
            PType pType, EmployeePosition position, int sId)
            : base(id, fName, lName, address, country, phone, email, logInfos, pType)
        {
            Position = position;
            StationId = sId;
        }

        int StationId { get; set; }
        EmployeePosition Position { get; set; }
    }

    public enum EmployeePosition
    {
        // TODO: define possible positions
        Boss,
        LiftBoy
    }
}
