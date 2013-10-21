using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;
using System.Transactions;
using System.Data;
using System.Data.Objects;
using System.Data.Entity;

namespace ElectricCarDB
{
    public class DCustomer : IDCustomer
    {
        public int addNewRecord(MDiscountGroup discountGroup, string fName, string lName, string address, string country, string phone, string email, ICollection<MLogInfo> logInfos, string payStatus)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                // TODO: transaction scope
                try
                {
                    int newId = context.Bookings.Count() + 1;
                    context.People.Add(new Customer()
                    {
                        Id = newId,
                        fName = fName,
                        lname = lName,
                        address = address,
                        country = country,
                        phone = phone,
                        email = email,
                        LoginInfoes = DLogInfo.buildLogInfos(logInfos),
                        // TODO: change to enum
                        pType = "Customer",
                        payStatus = null,
                        // TODO: not considering putting the ICollection<Booking> Bookings on Customer record creation
                        // TODO: dgId or DiscountGroup
                        dgId = discountGroup.ID,
                        DiscoutGroup = DDiscountGroup.buildDiscountGroup(discountGroup),
                    });
                    context.SaveChanges();
                    return newId;
                }
                catch (Exception)
                {
                    throw new System.NullReferenceException("Cannot add new Customer. ");
                }
            }
        }

        public MCustomer getRecord(int id, bool retrieveAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    Customer cust = (Customer) context.People.Find(id);
                    MCustomer mcust = buildMCustomer(cust);
                    if (retrieveAssociation)
                    {
                        // TODO: retrieveAssociation part
                    }
                    return mcust;
                }
                catch(Exception)
                {
                    throw new System.NullReferenceException("Cannot find Customer with id: " + id);
                }
            }
            throw new NotImplementedException();
        }

        public void deleteRecord(int id)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                // TODO: transaction scope
                try
                {
                    Person cust = context.People.Find(id);
                    if (cust != null)
                    {
                        // TODO: do I need to delete each separately, or going to be done all together, cascade
                        context.Entry(cust).State = EntityState.Deleted;
                        // context.Entry(cust.LoginInfo) = EntityState.Deleted;
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    throw new System.NullReferenceException("Cannot delete Customer record ");
                }
            }
            throw new NotImplementedException();
        }

        public void updateRecord(int id, MDiscountGroup discountGroup, string fName, string lName, string address, string country, string phone, string email, ICollection<MLogInfo> logInfos, string payStatus)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    bool success = false;
                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            /*
                            deleteAllBookingLineForBooking(bookingid);
                            foreach (MBookingLine bl in bls)
                            {
                                updateRecord(bl.Booking.Id, bl.BatteryType.id, bl.quantity.Value, bl.price.Value);
                            }
                             */
                            scope.Complete();
                            success = true;
                        }
                        catch (Exception)
                        {

                            throw new SystemException("Cannot update Customer record");
                        }
                    }

                    if (success)
                    {
                        // Reset the context since the operation succeeded.
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new SystemException("Cannot update Customer record");
                    }
                }
                catch (Exception)
                {
                }
            }
            throw new NotImplementedException();
        }

        public List<MCustomer> getAllRecord()
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                }
                catch (Exception)
                {
                }
            }
            throw new NotImplementedException();
        }

        public List<string> getAllInfo()
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                }
                catch (Exception)
                {
                }
            }
            throw new NotImplementedException();
        }

        public static MCustomer buildMCustomer(Customer customer)
        {
            // TODO: payStatus is undefined in DB schema, using placeholder "daco"
            return new MCustomer(DDiscountGroup.buildMDiscountGroup(customer.DiscoutGroup), customer.Id,
                customer.fName, customer.lname, customer.address, customer.country, customer.phone, customer.email,
                DLogInfo.buildMlogInfos(customer.LoginInfoes), "daco");
        }
    }

    /*
    public enum PType
    {
        Customer = { string "Customer"},
        Employee = string "Employee"
    }
    */
}
