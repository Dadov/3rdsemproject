using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarLib
{
    public interface IDBBooking
    {
        int addRecord(int cId, decimal totalPrice, DateTime createDate, DateTime tripStart, string creditCard);
        MBooking getRecord(int id, bool getAssociation);
        void updateRecord(int id, int cId, decimal totalPrice, DateTime createDate, DateTime tripStart, string creditCard);
        void deleteRecord(int id);
        List<MBooking> getAllRecord(bool getAssociation);
    }
}
