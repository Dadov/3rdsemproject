using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class MLogInfo
    {
        public MLogInfo()
        {
            //
        }

        public MLogInfo(int id, string loginName, string password)
        {
            ID = id;
            LoginName = loginName;
            Password = password;
        }

        public int ID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
}
