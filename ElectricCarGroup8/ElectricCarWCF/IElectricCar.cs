using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ElectricCarWCF
{
    [ServiceContract]
    public partial interface IElectricCar
    {
        #region People

        [OperationContract]
        string getME();

        #endregion 

        #region Routes

        #endregion

        #region Batteries

        #endregion
    }
}
