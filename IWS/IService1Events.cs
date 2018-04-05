using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace iws
{
    public interface IService1Events
    {
        [OperationContract(IsOneWay = true)]
        void GetInfoDone(string station, string cityName, List<string> res);

        [OperationContract(IsOneWay = true)]
        void GetInfoFinished();
    }
}
