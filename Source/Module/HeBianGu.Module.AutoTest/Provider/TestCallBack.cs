using JiuJinTech.ATS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Module.AutoTest
{
    class TestCallBack : ITestCallBack
    {
        public void OnCallback()
        {
            Console.WriteLine("OnCallback");
        }

        public void OnCallbackMessage(string message)
        {
            Console.WriteLine(message);

            this.CallBack?.Invoke(message);
        }

        public void OnCallbackData(double k, double v)
        {
            this.CallBackData?.Invoke(k,v);
        }

        public event Action<string> CallBack;
        public event Action<double, double> CallBackData;
    }
}
