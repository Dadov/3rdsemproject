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
    public class DEmployee : IDEmployee
    {
        public int addNewRecord(string fName, string lName, string address, string country, string phone, string email, int sId, EmployeePosition position)
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
                                max = context.People.Max(emp => emp.Id);
                            } catch {
                                max = 0;
                            }
                            newId = max + 1;
                            context.People.Add(new Employee()
                            {
                                Id = newId,
                                fName = fName,
                                lname = lName,
                                address = address,
                                country = country,
                                phone = phone,
                                email = email,
                                // together and after employee creation add logInfo and include returned 
                                // person Id in there
                                // LoginInfoes = DLogInfo.buildLogInfos(logInfos),
                                pType = PType.Employee.ToString(),
                                sId = sId,
                                position = position.ToString(),
                            });
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            throw new SystemException("Cannot add new Employee record " +
                                 " with an error " + e.Message);
                        }
                    }
                    transaction.Complete();
                    return newId;
                }
                catch (TransactionAbortedException e)
                {
                    throw new SystemException("Cannot finish transaction for adding an Employee " +
                        " with an error " + e.Message);
                }
            }
        }

        public MEmployee getRecord(int id, bool retrieveAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    Employee emp = (Employee)context.People.Find(id);
                    MEmployee memp = buildMEmployee(emp);
                    if (retrieveAssociation)
                    {
                        // TODO: retrieveAssociation part
                    }
                    return memp;
                }
                catch (Exception e)
                {
                    throw new System.NullReferenceException("Cannot get Employee with id: " + id
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
                            Person emp = context.People.Find(id);
                            IEnumerable<LoginInfo> logInfos = context.LoginInfoes.Where(pid => pid.pId == emp.Id);
                            if (emp != null)
                            {
                                context.Entry(emp).State = EntityState.Deleted;
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
                            throw new SystemException("Cannot delete Employee " + id + " record "
                                + " with message " + e.Message);
                        }
                    }
                    transaction.Complete();
                }
                catch (TransactionAbortedException e)
                {
                    throw new SystemException("Cannot finish transaction for deleting Employee no. " + id +
                        " with an error " + e.Message);
                }
            }
        }

        public void updateRecord(int id, string fName, string lName, string address, string country, string phone, string email, ICollection<MLogInfo> logInfos, int sId, EmployeePosition position)
        {
            using (TransactionScope transaction = new TransactionScope((TransactionScopeOption.Required)))
            {
                try
                {
                    using (ElectricCarEntities context = new ElectricCarEntities())
                    {
                        try
                        {
                            Employee emp = (Employee) context.People.Find(id);
                            emp.fName = fName;
                            emp.lname = lName;
                            emp.address = address;
                            emp.country = country;
                            emp.phone = phone;
                            emp.email = email;
                            // to be or not to be, cos LoginInfoes is 'virtual' in Customer EF model
                            emp.LoginInfoes = DLogInfo.buildLogInfos(logInfos);
                            emp.position = position.ToString();
                            emp.sId = sId;
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            throw new SystemException("Cannot update Employee record " + id 
                                + " with message " + e.Message);
                        }
                    }
                    transaction.Complete();
                }
                catch (TransactionAbortedException e)
                {
                    throw new SystemException("Cannot finish transaction for updating Employee no. " + id +
                        " with an error " + e.Message);
                }
            }
        }

        public List<MEmployee> getAllRecord()
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                List<MEmployee> employees = new List<MEmployee>();
                try
                {
                    // have to make string representation of enum cos LINQ doesn't
                    // understand ToString() inside expression
                    string empString = PType.Employee.ToString();
                    foreach (Employee emp in context.People.Where(pt => pt.pType.Equals(empString)))
                    {
                        employees.Add(DEmployee.buildMEmployee(emp));
                    }
                    return employees;
                }
                catch (Exception e)
                {
                    throw new SystemException("Cannot get all Employees"
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
                    string empString = PType.Employee.ToString();
                    foreach (Employee emp in context.People.Where(pt => pt.pType.Equals(empString)))
                    {
                        info.Add(emp.ToString());
                    }
                    return info;
                }
                catch (Exception e)
                {
                    throw new SystemException("Cannot get all Employees info"
                        + " with message " + e.Message);
                }
            }
        }

        public static MEmployee buildMEmployee(Employee employee)
        {
            return new MEmployee(employee.Id, employee.fName, employee.lname, employee.address, employee.country,
                employee.phone, employee.email, DLogInfo.buildMlogInfos(employee.LoginInfoes),
                (PType)Enum.Parse(typeof(PType), employee.pType),
                (EmployeePosition)Enum.Parse(typeof(EmployeePosition), employee.position), employee.sId.Value);
                // employee.sId.Value to convert <Nullable>Int to Int    
        }
    }
}
