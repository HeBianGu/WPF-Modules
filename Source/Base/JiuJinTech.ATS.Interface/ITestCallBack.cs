using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JiuJinTech.ATS.Interface
{ 
    public interface ITestCallBack
    {
        [OperationContract]
        void OnCallback();

        [OperationContract]
        void OnCallbackMessage(string message);

        [OperationContract]
        void OnCallbackData(double k,double v);

        event Action<string> CallBack;

        event Action<double,double> CallBackData;
    }
}
