using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data;
using System.Data.Objects;
using System.Data.Entity;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    public class DCustomer : IDCustomer
    {
        // delete LoginInfoes from arguments
        public int addNewRecord(string fName, string lName, string address, string country, string phone, string email, MDiscountGroup discountGroup, string payStatus) 
        {
            using (TransactionScope transaction = new TransactionScope((TransactionScopeOption.Required))) 
            {
                try 
                {
                    int newId = -1;
                    using (ElectricCarEntities context = new ElectricCarEntities()) 
                    {
                        try
                        {
                            int max;
                            try {
                                max = context.People.Max(cust => cust.Id);
                            } catch {
                                max = 0;
                            }
                            newId = max + 1;
                            context.People.Add(new Customer()
                            {
                                Id = newId,
                                fName = fName,
                                lname = lName,
                                address = address,
                                country = country,
                                phone = phone,
                                email = email,
                                // together and after customer creation add logInfo and include returned 
                                // person Id in there
                                // LoginInfoes = DLogInfo.buildLogInfos(logInfos),
                                // TODO: change to enum
                                pType = PType.Customer.ToString(),
                                payStatus = null,
                                // TODO: not considering putting the ICollection<Booking> Bookings on Customer record creation
                                // TODO: dgId or DiscountGroup, using dgId
                                dgId = discountGroup.ID,
                                // DiscoutGroup = DDiscountGroup.buildDiscountGroup(discountGroup)
                            });
                            context.SaveChanges();
                        } 
                        catch (Exception e) 
                        {
                            throw new SystemException("Cannot add new Customer record " +
                                " with an error " + e.Message);
                        }
                    }
                    transaction.Complete();
                    return newId;
                }
                catch (TransactionAbortedException e) 
                {
                    throw new SystemException("Cannot finish transaction for adding Customer " +
                       " with an error " + e.Message);
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
                catch(Exception e)
                {
                    throw new System.NullReferenceException("Cannot get Customer with id: " + id 
                        + " with message " + e.Message);
                }
            }
        }

        public void deleteRecord(int id)
        {
            using (TransactionScope transaction = new TransactionScope((TransactionScopeOption.Required)))
            {
                try
                {
                    using (ElectricCarEntities context = new ElectricCarEntities())
                    {
                        try
                        {
                            Person cust = context.People.Find(id);
                            IEnumerable<LoginInfo> logInfos = context.LoginInfoes.Where(pid => pid.pId == cust.Id);
                            if (cust != null)
                            {
                                context.Entry(cust).State = EntityState.Deleted;
                                // delete all associated logInfos
                                foreach (LoginInfo logInfo in logInfos)
                                {
                                    context.Entry(logInfo).State = EntityState.Deleted;
                                }
                                context.SaveChanges();
                            }
                        }
                        catch (Exception e)
                        {
                            throw new SystemException("Cannot delete Customers " + id + " record "
                                + " with message " + e.Message);
                        }
                    }
                    transaction.Complete();
                }
                catch (TransactionAbortedException e)
                {
                    throw new SystemException("Cannot finish transaction for deleting Customer " +
                       " with an error " + e.Message);
                }
            }
        }

        public void updateRecord(int id, string fName, string lName, string address, string country, string phone, string email, ICollection<MLogInfo> logInfos, MDiscountGroup discountGroup, string payStatus)
        {
            using (TransactionScope transaction = new TransactionScope((TransactionScopeOption.Required)))
            {
                try
                {
                    using (ElectricCarEntities context = new ElectricCarEntities())
                    {
                        try
                        {
                            Customer cust = (Customer) context.People.Find(id);
                            cust.fName = fName;
                            cust.lname = lName;
                            cust.address = address;
                            cust.country = country;
                            cust.phone = phone;
                            cust.email = email;
                            // to be or not to be, cos LoginInfoes is 'virtual' in Customer EF model
                            cust.LoginInfoes = DLogInfo.buildLogInfos(logInfos, cust.LoginInfoes);
                            foreach (LoginInfo logInfo in cust.LoginInfoes)
                            {
                                logInfo.pId = cust.Id;
                                logInfo.Person = cust;
                            }
                            cust.dgId = discountGroup.ID;
                            cust.payStatus = payStatus;
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            throw new SystemException("Cannot update Customer record " + id
                                + " with message " + e.Message);
                        }
                    }
                    transaction.Complete();
                }
                catch (TransactionAbortedException e)
                {
                    throw new SystemException("Cannot finish transaction for updating Customer " +
                       " with an error " + e.Message);
                }
            }
        }

        public List<MCustomer> getAllRecord()
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                List<MCustomer> customers = new List<MCustomer>();
                try
                {
                    // have to make string representation of enum cos LINQ doesn't
                    // understand ToString() inside expression
                    string custString = PType.Customer.ToString();
                    foreach (Customer cust in context.People.Where(pt => pt.pType.Equals(custString)))
                    {
                        customers.Add(DCustomer.buildMCustomer(cust));
                    }
                    return customers;
                }
                catch (Exception e)
                {
                    throw new SystemException("Cannot get all Customers"
                        + " with message " + e.Message);
                }
            }
        }

        public List<string> getAllInfo()
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                List<string> info = new List<string>();
                try
                {   
                    // have to make string representation of enum cos LINQ doesn't
                    // understand ToString() inside expression
                    string custString = PType.Customer.ToString();
                    foreach (Customer cust in context.People.Where(pt => pt.pType.Equals(custString)))
                    {
                        info.Add(cust.ToString());
                    }
                    return info;
                }
                catch (Exception e)
                {
                    throw new SystemException("Cannot get all Customers info"
                        + " with message " + e.Message);
                }
            }
        }

        public static MCustomer buildMCustomer(Customer customer)
        {
            return new MCustomer(customer.Id, customer.fName, customer.lname, customer.address,
                customer.country, customer.phone, customer.email, DLogInfo.buildMlogInfos(customer.LoginInfoes),
                (PType) Enum.Parse(typeof(PType), customer.pType),
                DDiscountGroup.buildMDiscountGroup(customer.DiscoutGroup), customer.payStatus);
        }
    }
}
