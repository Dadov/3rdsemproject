﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarWCF
{
    [DataContract]
    class BatteryStorage
    {
        [DataMember]
        public int ID;

        [DataMember]
        public List<Period> periods;

        [DataMember]
        public int typeID;
    }
}