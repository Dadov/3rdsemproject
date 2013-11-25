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

        #region Employees

        public void addEmployee(string fname, string lname, string address, string country, string phone,
            string email, string password, int stationId, string position)
        {
            EmployeeCtr empCtr = new EmployeeCtr();
            int newEmp = empCtr.add(fname, lname, address, country, phone, email, stationId,
                (EmployeePosition)Enum.Parse(typeof(EmployeePosition), position));
            LogInfoCtr logInfoCtr = new LogInfoCtr();
            // email is used as login name for everyone
            logInfoCtr.add(email, password, newEmp);
        }
        // TODO: change to accept one Employee employee argument
        // public void addEmployee(Employee employee)

        public Employee getEmployee(int id)
        {
            EmployeeCtr empCtr = new EmployeeCtr();
            MEmployee mEmp = empCtr.get(id, false);
            List<LogInfo> logInfos = new List<LogInfo>();
            foreach (MLogInfo logInfo in mEmp.LogInfos)
            {
                logInfos.Add(new LogInfo
                {
                    ID = logInfo.ID,
                    LoginName = logInfo.LoginName,
                    Password = logInfo.Password
                });
            }
            if (mEmp != null)
            {
                Employee emp = new Employee()
                {
                    ID = mEmp.ID,
                    FName = mEmp.FName,
                    LName = mEmp.LName,
                    Address = mEmp.Address,
                    Country = mEmp.Country,
                    Phone = mEmp.Phone,
                    Email = mEmp.Email,
                    LogInfos = logInfos,
                    Position = mEmp.Position.ToString(),
                    StationID = mEmp.StationId
                };
                return emp;
            }
            throw new SystemException("No Employee model was returned.");
        }

        public List<Employee> getAllEmployees()
        {
            EmployeeCtr empCtr = new EmployeeCtr();
            List<MEmployee> mEmps = empCtr.getAll();
            List<Employee> emps = new List<Employee>();
            foreach (MEmployee mEmp in mEmps)
            {
                List<LogInfo> logInfos = new List<LogInfo>();
                foreach (MLogInfo mLogInfo in mEmp.LogInfos)
                {
                    logInfos.Add(new LogInfo
                    {
                        ID = mLogInfo.ID,
                        LoginName = mLogInfo.LoginName,
                        Password = mLogInfo.Password
                    });
                }
                Employee emp = new Employee()
                {
                    ID = mEmp.ID,
                    FName = mEmp.FName,
                    LName = mEmp.LName,
                    Address = mEmp.Address,
                    Country = mEmp.Country,
                    Phone = mEmp.Phone,
                    Email = mEmp.Email,
                    LogInfos = logInfos,
                    Position = mEmp.Position.ToString(),
                    StationID = mEmp.StationId
                };
                emps.Add(emp);
            }
            return emps;
        }

        /*
        public void updateEmployee(int id, string fname, string lname, string address, string country,
            string phone, string email, string password LogInfo logInfo, int stationId, string position)
        {
            throw new NotImplementedException();
        }
        */

        public void updateEmployee(Employee employee)
        {
            /*
            MEmployee mEmp = new MEmployee()
            {
                ID = employee.ID,
                FName = employee.FName,
                LName = employee.LName,
                Address = employee.Address,
                Country = employee.Country,
                Phone = employee.Phone,
                Email = employee.Email,
                // logInfos
                PersonType = (PType) Enum.Parse(typeof(PType),  employee.GetType(),
                Position = (EmployeePosition) Enum.Parse(typeof(EmployeePosition), employee.Position),
                StationId = employee.StationID
            };
            */
            
            EmployeeCtr empCtr = new EmployeeCtr();
            List<MLogInfo> mLogInfos = new List<MLogInfo>();
            foreach (LogInfo logInfo in employee.LogInfos)
            {
                mLogInfos.Add(new MLogInfo
                {
                    ID = logInfo.ID,
                    LoginName = logInfo.LoginName,
                    Password = logInfo.Password
                });

            }
            empCtr.update(employee.ID, employee.FName, employee.LName, employee.Address, employee.Country,
                employee.Phone, employee.Email, mLogInfos, employee.StationID,
                (EmployeePosition)Enum.Parse(typeof(EmployeePosition), employee.Position));
        }

        public void deleteEmployee(int id)
        {
            EmployeeCtr empCtr = new EmployeeCtr();
            empCtr.delete(id);
        }

        #endregion

        #region Customers

        public void addCustomer(string fname, string lname, string address, string country, string phone,
            string email, string password, string payStatus, DiscountGroup discountGroup)
        {
            CustomerCtr custCtr = new CustomerCtr();
            int newCust = custCtr.add(fname, lname, address, country, phone, email, discountGroup.ID, payStatus);
            LogInfoCtr logInfoCtr = new LogInfoCtr();
            // email is used as login name for everyone
            logInfoCtr.add(email, password, newCust);
        }
        /*
        public void addCustmer(Customer customer)
        {
            CustomerCtr custCtr = new CustomerCtr();
            int newCust = custCtr.add(customer.FName, customer.LName, customer.Address, customer.Country, customer.Phone,
                customer.Email, customer.DiscountGroup.ID, customer.PaymentStatus);
            LogInfoCtr logInfoCtr = new LogInfoCtr();
            // email is used as login name for everyone
            
            logInfoCtr.add(customer.Email, customer.LogInfo.Password, newCust);
        }
        */

        public Customer getCustomer(int id)
        {
            CustomerCtr custCtr = new CustomerCtr();
            MCustomer mCust = custCtr.get(id, false);
            List<LogInfo> logInfos = new List<LogInfo>();
            foreach (MLogInfo mLogInfo in mCust.LogInfos)
            {
                logInfos.Add(new LogInfo
                {
                    ID = mLogInfo.ID,
                    LoginName = mLogInfo.LoginName,
                    Password = mLogInfo.Password
                });
            }
            DiscountGroup discoGroup = new DiscountGroup
            {
                ID = mCust.DiscountGroup.ID,
                Name = mCust.DiscountGroup.Name,
                Discount = mCust.DiscountGroup.Discount
            };
            if (mCust != null)
            {
                Customer cust = new Customer()
                {
                    ID = mCust.ID,
                    FName = mCust.FName,
                    LName = mCust.LName,
                    Address = mCust.Address,
                    Country = mCust.Country,
                    Phone = mCust.Phone,
                    Email = mCust.Email,
                    LogInfos = logInfos,
                    PaymentStatus = mCust.PaymentStatus,
                    DiscountGroup = discoGroup
                };
                return cust;
            }
            throw new SystemException("No Customer model was returned.");
        }

        public List<Customer> getAllCustomers()
        {
            CustomerCtr custCtr = new CustomerCtr();
            List<MCustomer> mCusts = custCtr.getAll();
            List<Customer> custs = new List<Customer>();
            foreach (MCustomer mCust in mCusts)
            {
                List<LogInfo> logInfos = new List<LogInfo>();
                foreach (MLogInfo mLogInfo in mCust.LogInfos)
                {
                    logInfos.Add(new LogInfo
                    {
                        ID = mLogInfo.ID,
                        LoginName = mLogInfo.LoginName,
                        Password = mLogInfo.Password
                    });
                }
                DiscountGroup discoGroup = new DiscountGroup
                {
                    ID = mCust.DiscountGroup.ID,
                    Name = mCust.DiscountGroup.Name,
                    Discount = mCust.DiscountGroup.Discount
                };
                Customer cust = new Customer()
                {
                    ID = mCust.ID,
                    FName = mCust.FName,
                    LName = mCust.LName,
                    Address = mCust.Address,
                    Country = mCust.Country,
                    Phone = mCust.Phone,
                    Email = mCust.Email,
                    LogInfos = logInfos,
                    PaymentStatus = mCust.PaymentStatus,
                    DiscountGroup = discoGroup
                };
                custs.Add(cust);
            }
            return custs;
        }

        /*
        public void updateCustomer(int id, string fname, string lname, string address, string country,
            string phone, string email, LogInfo logInfo, string payStatus, DiscountGroup discountGroup)
        {
            throw new NotImplementedException();
        }
        */
        public void updateCustomer(Customer customer)
        {
            CustomerCtr custCtr = new CustomerCtr();
            List<MLogInfo> mLogInfos = new List<MLogInfo>();
            foreach (LogInfo logInfo in customer.LogInfos)
            {
                mLogInfos.Add(new MLogInfo
                {
                    ID = logInfo.ID,
                    LoginName = logInfo.LoginName,
                    Password = logInfo.Password
                });
            }
            custCtr.update(customer.ID, customer.FName, customer.LName, customer.Address, customer.Country,
                customer.Phone, customer.Email, mLogInfos, customer.DiscountGroup.ID, customer.PaymentStatus);
        }

        public void deleteCustomer(int id)
        {
            CustomerCtr custCtr = new CustomerCtr();
            custCtr.delete(id);
        }

        #endregion

        #region Log Infos

        public void addLogInfo(string loginName, string password, int personId)
        {
            LogInfoCtr liCtr = new LogInfoCtr();
            int newLi = liCtr.add(loginName, password, personId);
        }

        public List<LogInfo> getPersonLogInfos(int id)
        {
            CustomerCtr custCtr = new CustomerCtr();
            MCustomer mCust = custCtr.get(id, false);
            if (mCust != null)
            {
                List<LogInfo> logInfos = new List<LogInfo>();
                foreach (MLogInfo mLogInfo in mCust.LogInfos)
                {
                    logInfos.Add(new LogInfo
                    {
                        ID = mLogInfo.ID,
                        LoginName = mLogInfo.LoginName,
                        Password = mLogInfo.Password
                    });
                }
                return logInfos;
                
            }
            EmployeeCtr empCtr = new EmployeeCtr();
            MEmployee mEmp = empCtr.get(id, false);
            if (mEmp != null)
            {
                List<LogInfo> logInfos = new List<LogInfo>();
                foreach (MLogInfo mLogInfo in mEmp.LogInfos)
                {
                    logInfos.Add(new LogInfo
                    {
                        ID = mLogInfo.ID,
                        LoginName = mLogInfo.LoginName,
                        Password = mLogInfo.Password
                    });
                }
                return logInfos;
            }
            throw new SystemException("Nor Employee or Customer was found with given ID.");
        }

        /*
        public void updateLogInfo(int id, string loginName, string password)
        {
            throw new NotImplementedException();
        }
        */
        public void updateLogInfo(LogInfo logInfo)
        {
            LogInfoCtr liCtr = new LogInfoCtr();
            liCtr.update(logInfo.ID, logInfo.LoginName, logInfo.Password);
        }

        public void deleteLogInfo(int id)
        {
            LogInfoCtr liCtr = new LogInfoCtr();
            liCtr.delete(id);
        }

        #endregion

        #region Discount Groups

        public void addDiscountGroup(string name, decimal discount)
        {
            DiscountGroupCtr discoCtr = new DiscountGroupCtr();
            int newDiscoGrp = discoCtr.add(name, discount);
        }

        public DiscountGroup getDiscoutGroup(int id)
        {
            DiscountGroupCtr discoCtr = new DiscountGroupCtr();
            MDiscountGroup mDiscoGrp = discoCtr.get(id, false);
            DiscountGroup discoGrp = new DiscountGroup
            {
                ID = mDiscoGrp.ID,
                Name = mDiscoGrp.Name,
                Discount = mDiscoGrp.Discount
            };
            return discoGrp;
        }

        public List<DiscountGroup> getAllDiscountGroups()
        {
            DiscountGroupCtr discoCtr = new DiscountGroupCtr();
            List<MDiscountGroup> mDiscoGrps = discoCtr.getAll();
            List<DiscountGroup> discoGrps = new List<DiscountGroup>();
            foreach (MDiscountGroup mDiscoGrp in mDiscoGrps)
            {
                discoGrps.Add(new DiscountGroup
                {
                    ID = mDiscoGrp.ID,
                    Name = mDiscoGrp.Name,
                    Discount = mDiscoGrp.Discount
                });
            }
            return discoGrps;
        }

        /*
        public void updateDiscountGroup(int id, string name, decimal discount)
        {
            throw new NotImplementedException();
        }
        */
        public void updateDiscountGroup(DiscountGroup discountGroup)
        {
            DiscountGroupCtr discoCtr = new DiscountGroupCtr();
            discoCtr.update(discountGroup.ID, discountGroup.Name, (decimal) discountGroup.Discount);
        }

        public void deleteDiscountGroup(int id)
        {
            DiscountGroupCtr discoCtr = new DiscountGroupCtr();
            discoCtr.delete(id);
        }

        #endregion

        #endregion

        #region Booking

        public List<List<RouteStop>> getRoutes(int startSId, int endSIdint, DateTime tripStart, decimal batteryLimit)
        {
            List<List<RouteStop>> paths = new List<List<RouteStop>>();
            List<List<PathStop>> routes = new List<List<PathStop>>();
            PathFindCtr pfCtr = new PathFindCtr();
            routes = pfCtr.findRoutes(startSId, endSIdint, tripStart, batteryLimit);
            if (routes != null)
            {
                foreach (List<PathStop> r in routes)
                {
                    List<RouteStop> x = new List<RouteStop>();
                    for (int i = 0; i < r.Count; i++)
                    {
                        //translate path to route
                        RouteStop rs = new RouteStop();
                        rs.stationID = r[i].stationID;
                        Station s = new Station();
                        s.Id = r[i].station.Id;
                        s.Name = r[i].station.name;
                        s.Address = r[i].station.address;
                        rs.station = s;
                        if (i != 0)
                        {
                            rs.distance = r[i].distance + x[i - 1].distance;
                            rs.driveHour = r[i].driveHour + x[i - 1].driveHour;
                            rs.time = tripStart.AddHours(Convert.ToDouble(rs.driveHour));
                        }
                        else
                        {
                            rs.distance = r[i].distance;
                            rs.driveHour = r[i].driveHour;
                            rs.time = tripStart;
                        }
                        x.Add(rs);
                    }
                    paths.Add(x);
                }
            }
            
            
            return paths;
        }

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
                    bk.createDate = b.createDate.Value.ToString("MM/dd/yyyy HH:mm");
                    CustomerTest cust = new CustomerTest() { Id = b.customer.ID, name = b.customer.FName + " " + b.customer.LName };
                    bk.customer = cust;
                    bk.cId = b.customer.ID;
                    bk.payStatus = b.creaditCard;
                    bk.totalPrice = b.totalPrice.Value;
                    bk.tripStart = b.tripStart.Value.ToString("MM/dd/yyyy HH:mm");
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

        public void addBooking(Booking b)
        {
            MBooking bk = new MBooking();
            bk.cId = b.cId;
            bk.createDate = Convert.ToDateTime(b.createDate);
            bk.creaditCard = b.payStatus;
            bk.tripStart = Convert.ToDateTime(b.tripStart);
            bk.totalPrice = b.totalPrice;
            List<MBookingLine> bkls = new List<MBookingLine>();
            List<BookingLine> bls = b.bookinglines.ToList<BookingLine>();
            for (int i = 0; i < bls.Count; i++)
			{
                MBookingLine bl = new MBookingLine();
                bl.price = bls[i].price;
                bl.quantity = bls[i].quantity;
                bl.Station.Id = bls[i].station.Id;
                bl.time = bls[i].time;
                bl.BatteryType.id = bls[i].BatteryType.Id;
                bkls.Add(bl);
			}
            bk.bookinglines = bkls;
            BookingCtr bCtr = new BookingCtr();
            bCtr.addBooking(bk);
        }

        public void updateBooking(Booking b)
        {
            MBooking bk = new MBooking();
            bk.cId = b.cId;
            bk.createDate = Convert.ToDateTime(b.createDate);
            bk.creaditCard = b.payStatus;
            bk.totalPrice = b.totalPrice;
            BookingCtr bCtr = new BookingCtr();
            bCtr.updateBooking(bk);
        }

        public void deleteBooking(int bId)
        {
            BookingCtr bCtr = new BookingCtr();
            bCtr.deleteBooking(bId);
        }

        public decimal convertCapacityToDistance(decimal capacity)
        {
            return 350; //TODO convert capacity to distance with a formular
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

        #region Station
        public List<BookingLine> getBookingLinesForStation(int sId)
        {
            List<BookingLine> bls = new List<BookingLine>();
            BookingLineCtr blCtr = new BookingLineCtr();
            List<MBookingLine> mbls = blCtr.getBookingLinesForStation(sId, false);
            
            foreach (MBookingLine bl in mbls)
            {
                BookingLine b = new BookingLine();
                BatteryTypeTest bt = new BatteryTypeTest();
                b.BatteryType = bt;
                b.BatteryType.Id = bl.BatteryType.id;
                b.quantity = bl.quantity.Value;
                b.time = bl.time.Value;
                b.price = bl.price.Value;
                b.bId = bl.bId;
                bls.Add(b);
            }
            return bls;
        }

        public List<BookingLine> getBookingLinesForDateInStation(int sId, DateTime date)
        {
            List<BookingLine> bls = new List<BookingLine>();
            BookingLineCtr blCtr = new BookingLineCtr();
            List<MBookingLine> mbls = blCtr.getBookingLinesForDateInStation(sId, date, false);

            foreach (MBookingLine bl in mbls)
            {
                BookingLine b = new BookingLine();
                BatteryTypeTest bt = new BatteryTypeTest();
                b.BatteryType = bt;
                b.BatteryType.Id = bl.BatteryType.id;
                b.quantity = bl.quantity.Value;
                b.time = bl.time.Value;
                b.price = bl.price.Value;
                b.bId = bl.bId;
                bls.Add(b);
            }
            return bls;
        }
        #endregion
        #region Battery

        #region BatteryType
        public int addBatteryType(string name, string producer, decimal capacity, decimal exchangeCost)
        {
            BatteryTypeCtr tCtr = new BatteryTypeCtr();
            return tCtr.addNewRecord(name, producer, capacity, exchangeCost);
        }

        public BatteryType getBatteryType(int id)
        {
            BatteryTypeCtr tCtr = new BatteryTypeCtr();
            MBatteryType type = tCtr.getRecord(id, true);
            BatteryType bt = new BatteryType();
            if (type != null)
            {
                bt.ID = type.id;
                bt.name = type.name;
                bt.producer = type.producer;
                bt.capacity = (int)type.capacity;
                bt.exchangeCost = (int)type.exchangeCost;
            }
            return bt;
        }

        public void deleteBatteryType(int id)
        {
            BatteryTypeCtr tCtr = new BatteryTypeCtr();
            tCtr.deleteRecord(id);
        }

        public void updateBatteryType(int id, string name, string producer, decimal capacity, decimal exchangeCost)
        {
            BatteryTypeCtr tCtr = new BatteryTypeCtr();
            tCtr.updateRecord(id, name, producer, capacity, exchangeCost);
        }

        public List<BatteryType> getAllBatteryTypes()
        {
            BatteryTypeCtr tCtr = new BatteryTypeCtr();
            List<MBatteryType> types = tCtr.getAllRecord(true);
            List<BatteryType> bts = new List<BatteryType>();
            foreach (MBatteryType type in types)
            {
                BatteryType bt = new BatteryType();
                bt.ID = type.id;
                bt.name = type.name;
                bt.producer = type.producer;
                bt.capacity = (int)type.capacity;
                bt.exchangeCost = (int)type.exchangeCost;
                bts.Add(bt);
            }
            return bts;
        }

        public List<string> getAllInfoTypes()
        {
            BatteryTypeCtr tCtr = new BatteryTypeCtr();
            return tCtr.getAllInfo();
        }
        #endregion

        #region BatteryStorage

        public int addNewStorage(int btID, int sID, int storageNumber)
        {
            BatteryStorageCtr sCtr = new BatteryStorageCtr();
            return sCtr.addNewRecord(btID, sID, storageNumber);
        }

        public BatteryStorage getStorage(int id)
        {
            BatteryStorageCtr sCtr = new BatteryStorageCtr();
            PeriodCtr pCtr = new PeriodCtr();
            MBatteryStorage storage = sCtr.getRecord(id, true);
            BatteryStorage s = new BatteryStorage();
            if (storage != null)
            {
                s.ID = storage.id;
                s.typeID = storage.type.id;
                s.periods = getStoragePeriods(id);
                s.storageNumber = storage.storageNumber;

            }
            return s;
        }

        public void deleteStorage(int id)
        {
            BatteryStorageCtr sCtr = new BatteryStorageCtr();
            sCtr.deleteRecord(id);
        }

        public void deleteStorageByType(int btID)
        {
            BatteryStorageCtr bCtr = new BatteryStorageCtr();
            bCtr.deleteRecordByType(btID);
        }

        public void updateStorage(int id, int btid, int sID, int storageNumber)
        {
            BatteryStorageCtr sCtr = new BatteryStorageCtr();
            sCtr.updateRecord(id, btid, sID, storageNumber);
        }

        public List<BatteryStorage> getStationStorages(int sID)
        {
            BatteryStorageCtr sCtr = new BatteryStorageCtr();
            List<MBatteryStorage> storages = sCtr.getStationStorages(sID);
            List<BatteryStorage> bss = new List<BatteryStorage>();
            foreach (MBatteryStorage storage in storages)
            {
                BatteryStorage bs = new BatteryStorage();
                bs.ID = storage.id;
                bs.typeID = storage.type.id;
                bs.periods = getStoragePeriods(bs.ID);
                bs.storageNumber = storage.storageNumber;
                bss.Add(bs);
            }
            return bss;
        }

        public List<BatteryStorage> getAllStorages()
        {
            BatteryStorageCtr sCtr = new BatteryStorageCtr();
            List<MBatteryStorage> storages = sCtr.getAllRecord(true);
            List<BatteryStorage> bss = new List<BatteryStorage>();
            foreach (MBatteryStorage storage in storages)
            {
                BatteryStorage bs = new BatteryStorage();
                bs.ID = storage.id;
                bs.typeID = storage.type.id;
                bs.periods = getStoragePeriods(bs.ID);
                bs.storageNumber = storage.storageNumber;

                bss.Add(bs);
            }
            return bss;
        }

        public List<string> getAllStorageInfo()
        {
            BatteryStorageCtr sCtr = new BatteryStorageCtr();
            return sCtr.getAllInfo();
        }


        #endregion

        #region Period

        public Period getPeriod(int bsID, DateTime time)
        {
            PeriodCtr pCtr = new PeriodCtr();
            MPeriod period = pCtr.getRecord(bsID, time, true);
            Period p = new Period();
            if (period != null)
            {
                p.bsID = bsID;
                p.availNumber = period.initBatteryNumber;
                p.bookedNumber = period.bookedBatteryNumber;
                p.time = period.time;
            }
            return p;
        }


        public List<Period> getAllPeriods()
        {
            PeriodCtr pCtr = new PeriodCtr();
            List<MPeriod> periods = pCtr.getAllRecord(true);
            List<Period> ps = new List<Period>();
            foreach (MPeriod period in periods)
            {
                Period p = new Period();
                p.availNumber = period.initBatteryNumber;
                p.bookedNumber = period.bookedBatteryNumber;
                p.time = period.time;
                ps.Add(p);
            }
            return ps;
        }

        public List<Period> getStoragePeriods(int bsID)
        {
            PeriodCtr pCtr = new PeriodCtr();
            List<MPeriod> periods = pCtr.getStoragePeriods(bsID);
            List<Period> ps = new List<Period>();
            foreach (MPeriod period in periods)
            {
                Period p = new Period();
                p.availNumber = period.initBatteryNumber;
                p.bookedNumber = period.bookedBatteryNumber;
                p.time = period.time;
                ps.Add(p);
            }
            return ps;
        }

        #endregion
        #endregion







       
    }
}
