using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ElectricCarDB;
using ElectricCarModelLayer;

namespace ElectricCarLib
{
    class BookingCtr
    {
        public void addBooking() 
        {
            using (TransactionScope trsaction = new TransactionScope())
            {
                
            }
        }
    }
}
