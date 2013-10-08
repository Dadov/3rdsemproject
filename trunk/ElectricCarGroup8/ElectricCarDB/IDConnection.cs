using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;
namespace ElectricCarDB
{
    public interface IDConnection
    {
        void addNewRecord(int id1, int id2, decimal dist, decimal time);
        MConnection getRecord(int id1, int id2, Boolean getAssociation);
        void deleteRecord(int id1, int id2);
        void updateRecord(int id1, int id2, decimal dist, decimal time);
        List<MConnection> getAllRecord(Boolean getAssociation);
        List<string> getAllInfo();
    }
}
