using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace iws
{
    [ServiceContract(CallbackContract = typeof(IService1Events))]
    public interface IService1
    {
        /**
         * Renvoit une liste contenant les informations en temps réel de la station et dans la ville passée en paramètre
         **/
        [OperationContract]
        List<string> GetInfo(string station, string cityName);

        [OperationContract]
        void SubscribeGetInfoDoneEvent();

        [OperationContract]
        void SubscribeGetInfoFinishedEvent();

        [OperationContract]
        List<string> GetStations(string cityName);

        [OperationContract]
        List<string> GetCities();
    }
}
