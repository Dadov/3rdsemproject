using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    public interface IDBookingLine
    {
        void addRecord(int bId, int btId, int sId, int quantity, decimal price, DateTime time);
        void updateRecord(int bId, int btId, int sId, int quantity, decimal price, DateTime time);
        void deleteRecord(int bId, int btId, int sId);
        MBookingLine getRecord(int bId, int btId, int sId, bool getAssociation);
        List<MBookingLine> getBookingLinesForBooking(int bookingid, bool getAssociation);
        void deleteAllBookingLineForBooking(int bookingid);
        void updateAllBookingLineForBooking(int bookingid, List<MBookingLine> bls);
        void insertAllBookingLineForBooking(List<MBookingLine> bls);
        
    }
}
