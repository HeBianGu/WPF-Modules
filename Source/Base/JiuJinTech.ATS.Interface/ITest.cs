using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using HeBianGu.General.WcfService;


namespace JiuJinTech.ATS.Interface
{
    [ServiceContract(CallbackContract = typeof(ITestCallBack))]
    public interface ITest
    {
        /// <summary> 用来检测服务端是否连接成功 </summary>
        [OperationContract]
        CallResult Ping(string ip);

        /// <summary> 运行测试项，目前通过json传递数据 </summary>
        [OperationContract]
        CallResult Run(string json);

    }
}
