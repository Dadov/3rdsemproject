using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ElectricCarWCF
{
    [ServiceContract]
    interface IElectricCar
    {
        [OperationContract]
        string test(int id);
    }
}
