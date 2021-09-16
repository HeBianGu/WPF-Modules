using HeBianGu.Base.WpfBase;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.ShellOffice
{
    public interface IAssemblyDomain:ILogService
    {
        Tuple<string, string, bool> GetAccount(out string error);
        bool Login(string userName, string psd, bool IsSavePSD,out string error);
    }
}