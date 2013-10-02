using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarLib;

namespace ElectricCarLib
{
    public interface IDBStation
    {
        int addNewRecord(string name, string address, string country, string state);
        MStation getRecord(int id, bool getAssociation);
        void deleteRecord(int id);
        void updateRecord(int id, string Name, string Address, string Country, string State);
        List<MStation> getAllRecord(bool getAssociation);
        LinkedList<MStation> getNaborStations(int id);
    }
}
