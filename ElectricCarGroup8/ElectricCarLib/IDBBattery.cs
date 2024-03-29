﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarLib
{
    interface IDBBattery
    {
        int addNewRecord(string state, int btid);
        MBattery getRecord(int id, Boolean getAssociation);
        void deleteRecord(int id);
        void updateRecord(int id, string state, int btid);
        List<MBattery> getAllRecord(Boolean getAssociation);
        List<string> getAllInfo();
    }
}
