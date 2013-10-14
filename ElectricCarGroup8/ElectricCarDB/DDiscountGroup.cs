using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarDB
{
    class DDiscountGroup : IDDiscountGroup
    {
        public int addNewRecord(string name, int discount)
        {
            throw new NotImplementedException();
        }

        public ElectricCarModelLayer.MDiscountGroup getRecord(int id, bool retrieveAssociation)
        {
            throw new NotImplementedException();
        }

        public void deleteRecord(int id, bool retrieveAssociation)
        {
            throw new NotImplementedException();
        }

        public void updateRecord(int id, string name, int discount)
        {
            throw new NotImplementedException();
        }

        public List<ElectricCarModelLayer.MDiscountGroup> getAllRecord()
        {
            throw new NotImplementedException();
        }

        public List<string> getAllInfo()
        {
            throw new NotImplementedException();
        }
    }
}
