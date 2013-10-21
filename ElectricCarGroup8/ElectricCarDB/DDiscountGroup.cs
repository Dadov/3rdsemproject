using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricCarModelLayer;

namespace ElectricCarDB
{
    public class DDiscountGroup : IDDiscountGroup
    {
        public int addNewRecord(string name, Nullable<decimal> discount)
        {
            throw new NotImplementedException();
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
                catch(Exception)
                {
                    throw new System.NullReferenceException("Can not find discount grup with id: " + id);
                }
            }
        }

        public void deleteRecord(int id, bool retrieveAssociation)
        {
            throw new NotImplementedException();
        }

        public void updateRecord(int id, string name, Nullable<decimal> discount)
        {
            throw new NotImplementedException();
        }

        public List<MDiscountGroup> getAllRecord()
        {
            throw new NotImplementedException();
        }

        public List<string> getAllInfo()
        {
            throw new NotImplementedException();
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
