using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarLib
{
    public class MBooking
    {
        public MBooking()
        {
            //this.BookingLine = new HashSet<BookingLine>();
            //this.Station = new HashSet<Station>();
        }
        public int Id { get; set; }
        public Nullable<int> cId { get; set; }
        public Nullable<decimal> totalPrice { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> tripStart { get; set; }
        public string creaditCard { get; set; }

        //public virtual Customer Customer { get; set; }
        //public virtual ICollection<BookingLine> BookingLine { get; set; }
        //public virtual ICollection<Station> Station { get; set; }
    }
}
