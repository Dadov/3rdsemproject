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
    public class DDiscountGroup : IDDiscountGroup
    {
        public int addNewRecord(string name, Nullable<decimal> discount)
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
                                max = context.DiscoutGroups.Max(dg => dg.Id);
                            } catch {
                                max = 0;
                            }
                            newId = max + 1;
                            context.DiscoutGroups.Add(new DiscoutGroup()
                            {
                                Id = newId,
                                name = name,
                                dgRate = discount
                            });
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            throw new SystemException("Cannot add new Discount Group record " + 
                                " with an error " + e.Message);
                        }
                    }
                    transaction.Complete();
                    return newId;
                }
                catch (TransactionAbortedException e)
                {
                    throw new SystemException("Cannot finish transaction for adding Discount Group " +
                       " with an error " + e.Message);
                }
            }
        }

        public MDiscountGroup getRecord(int id, bool retrieveAssociation)
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                try
                {
                    DiscoutGroup dg = context.DiscoutGroups.Find(id);
                    MDiscountGroup mdg = buildMDiscountGroup(dg);
                    if (retrieveAssociation)
                    {
                        // TODO: solve with entity framework somehow, or manually
                    }
                    return mdg;
                }
                catch(Exception e)
                {
                    throw new System.NullReferenceException("Cannot get discount grup with id: " + id + 
                        " with an error " + e.Message);
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
                            DiscoutGroup dg = context.DiscoutGroups.Find(id);
                            if (dg != null)
                            {
                                context.Entry(dg).State = EntityState.Deleted;
                                context.SaveChanges();
                            }
                        }
                        catch (Exception e)
                        {
                            throw new SystemException("Cannot delete Discount Group " + id + " record " +
                                " with an error " + e.Message);
                        }
                    }
                    transaction.Complete();
                }
                catch (TransactionAbortedException e)
                {
                    throw new SystemException("Cannot finish transaction for deleting Discount Group " +
                       " with an error " + e.Message);
                }
            }
        }

        public void updateRecord(int id, string name, Nullable<decimal> discount)
        {
            using (TransactionScope transaction = new TransactionScope((TransactionScopeOption.Required)))
            {
                try
                {
                    using (ElectricCarEntities context = new ElectricCarEntities())
                    {
                        try
                        {
                            DiscoutGroup dg = context.DiscoutGroups.Find(id);
                            dg.name = name;
                            dg.dgRate = discount;
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            throw new SystemException("Cannot update Discount Group " + id + " record " +
                                " with an error " + e.Message);
                        }
                    }
                    transaction.Complete();
                }
                catch (TransactionAbortedException e)
                {
                    throw new SystemException("Cannot finish transaction for updating Discount Group " +
                       " with an error " + e.Message);
                }
            }
        }

        public List<MDiscountGroup> getAllRecord()
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                List<MDiscountGroup> discountGroups = new List<MDiscountGroup>();
                try 
                {
                    foreach(DiscoutGroup dg in context.DiscoutGroups)
                    {
                        discountGroups.Add(DDiscountGroup.buildMDiscountGroup(dg));
                    }
                    return discountGroups;
                }
                catch (Exception e)
                {
                    throw new SystemException("Cannot get all Discount Groups " +
                       " with an error " + e.Message);
                }
            }        
        }

        public List<string> getAllInfo()
        {
            using (ElectricCarEntities context = new ElectricCarEntities())
            {
                List<string> discountGroups = new List<string>();
                try 
                {
                    foreach(DiscoutGroup dg in context.DiscoutGroups)
                    {
                        discountGroups.Add(dg.ToString());
                    }
                    return discountGroups;
                }
                catch (Exception e)
                {
                    throw new SystemException("Cannot get all Discount Groups info " +
                       " with an error " + e.Message);
                }
            } 
        }

        public static MDiscountGroup buildMDiscountGroup(DiscoutGroup discountGroup)
        {
            return new MDiscountGroup(discountGroup.Id, discountGroup.name, discountGroup.dgRate);
        }

        public static DiscoutGroup buildDiscountGroup(MDiscountGroup mDiscountGroup)
        {
            return new DiscoutGroup()
            {
                Id = mDiscountGroup.ID,
                name = mDiscountGroup.Name,
                dgRate = mDiscountGroup.Discount
            };
        }
    }
}
