using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ElectricCarLib;
using ElectricCarModelLayer;

namespace ElectricCarWCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class ElectricCar : IElectricCar
    {
        #region People

        public string getME()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Booking
        public List<Booking> getAllBookings()
        {
            List<Booking> bookings = new List<Booking>();
            BookingCtr bCtr = new BookingCtr();
            List<MBooking> bs = bCtr.getAllBooking();
            if (bs.Count != 0)
            {
                foreach (MBooking b in bs)
                {
                    Booking bk = new Booking();
                    bk.Id = b.Id;
                    bk.createDate = b.createDate.Value.ToShortTimeString();
                    CustomerTest cust = new CustomerTest() { Id = b.customer.ID, name = b.customer.FName + " " + b.customer.LName };
                    bk.customer = cust;
                    bk.cId = b.customer.ID;
                    bk.payStatus = b.creaditCard;
                    bk.totalPrice = b.totalPrice.Value;
                    bk.tripStart = b.tripStart.Value.ToShortTimeString();
                    if (b.bookinglines.Count != 0)
                    {
                        foreach (MBookingLine bl in b.bookinglines)
                        {
                            BookingLine l = new BookingLine();
                            l.price = bl.price.Value;
                            l.quantity = bl.quantity.Value;
                            l.time = bl.time.Value;
                            bk.bookinglines.Add(l);
                            BatteryTypeTest bt = new BatteryTypeTest();
                            bt.Id = bl.BatteryType.id;
                            bt.name = bl.BatteryType.name;
                            l.BatteryType = bt;
                            Station s = new Station();
                            s.Id = bl.Station.Id;
                            s.Name = bl.Station.name;
                            s.Address = bl.Station.address;
                            s.Country = bl.Station.country;
                            l.station = s;

                        }
                        bk.startStationId = b.bookinglines.First().Station.Id;
                    }
                    bookings.Add(bk);
                }
            }

            return bookings;
        }

        public Booking getBooking(int id)
        {
            Booking bk = new Booking();
            BookingCtr bCtr = new BookingCtr();
            MBooking b = bCtr.getBooking(id, true);
            if (bk != null)
            {
                foreach (MBookingLine bl in b.bookinglines)
                {
                    BookingLine l = new BookingLine();
                    l.price = bl.price.Value;
                    l.quantity = bl.quantity.Value;
                    l.time = bl.time.Value;
                    bk.bookinglines.Add(l);

                    BatteryTypeTest bt = new BatteryTypeTest();
                    bt.Id = bl.BatteryType.id;
                    bt.name = bl.BatteryType.name;
                    l.BatteryType = bt;
                    
                    Station s = new Station();
                    s.Id = bl.Station.Id;
                    s.Name = bl.Station.name;
                    s.Address = bl.Station.address;
                    s.Country = bl.Station.country;
                    l.station = s;

                }
                bk.startStationId = b.bookinglines.First().Station.Id;
            }
            return bk;
        }

        public List<BatteryTypeTest> getAllBatteryType()
        {
            List<BatteryTypeTest> bts = new List<BatteryTypeTest>();
            BatteryTypeCtr btCtr = new BatteryTypeCtr();
            List<MBatteryType> bt = btCtr.getAllRecord(false);
            if (bt.Count !=0)
            {
                foreach (MBatteryType b in bt)
                {
                    BatteryTypeTest btt = new BatteryTypeTest();
                    btt.Id = b.id;
                    btt.name = b.name;
                    btt.price = Convert.ToDouble(b.exchangeCost);
                    bts.Add(btt);
                }
            }

            return bts;
        }

        #endregion

        #region Statons
        public List<Station> getAllStations()
        {
            using (StationCtr sCtr = new StationCtr())
            {
                List<Station> ss = new List<Station>();
                List<MStation> stations = sCtr.getAllStation();
                foreach (MStation item in stations)
                {
                    Station s = new Station() { Id = item.Id, Name = item.name, Address = item.address, Country = item.country, State = item.state.ToString() };
                    ss.Add(s);

                }
                return ss;
            }
        }

        public Station getStation(int id)
        {
            StationCtr sCtr = new StationCtr();
            MStation s = sCtr.getStation(id, false);
            Station ns = new Station();
            if (s != null)
            {
                ns.Id = s.Id;
                ns.Name = s.name;
                ns.State = s.state.ToString();
                ns.Country = s.country;
                ns.Address = s.address;
            }

            return ns;
        }

        public void addStation(string name, string address, string country, string state)
        {
            StationCtr sCtr = new StationCtr();
            sCtr.addStation(name, address, country, state);
        }

        public void updateStation(int id, string name, string address, string country, string state)
        {
            StationCtr sCtr = new StationCtr();
            sCtr.updateStation(id, name, address, country, state);
        }

        public void deleteStation(int id)
        {
            StationCtr sCtr = new StationCtr();
            sCtr.deleteStation(id);
        }

        public List<NaborStation> getNaborStations(int id)
        {
            StationCtr sCtr = new StationCtr();
            List<NaborStation> nbs = new List<NaborStation>();
            List<MConnection> cs = sCtr.getNaborStations(id);
            foreach (MConnection c in cs)
            {
                NaborStation n = new NaborStation();
                if (c.Station1 != null)
                {
                    n.Id = c.Station1.Id;
                    n.Name = c.Station1.name;
                    n.Address = c.Station1.address;
                }
                else
                {
                    n.Id = c.Station2.Id;
                    n.Name = c.Station2.name;
                    n.Address = c.Station2.address;
                }
                n.Distance = Convert.ToDouble(c.distance);
                n.DriveHour = Convert.ToDouble(c.driveHour);
                nbs.Add(n);
            }
            return nbs;
        }

        public void addNaborStation(int id1, int id2, decimal distance, decimal drivehour)
        {
            StationCtr sCtr = new StationCtr();
            sCtr.addConnection(id1, id2, distance, drivehour);
        }

        public void updateNaborStation(int id1, int id2, decimal distance, decimal driveHour)
        {
            StationCtr sCtr = new StationCtr();
            sCtr.updateConnection(id1, id2, distance, driveHour);
        }

        public void deleteNaborStation(int id1, int id2)
        {
            StationCtr sCtr = new StationCtr();
            sCtr.deleteConnection(id1, id2);
        }

        public List<string> getStates()
        {
            StationCtr sCtr = new StationCtr();
            return sCtr.getStates();
        }

        #endregion
    }
}
