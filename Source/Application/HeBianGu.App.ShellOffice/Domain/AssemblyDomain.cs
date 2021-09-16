using HeBianGu.Base.WpfBase;
using HeBianGu.Common.LocalConfig;
using HeBianGu.General.DataBase.Identify;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.App.ShellOffice
{
    public class AssemblyDomain : IAssemblyDomain
    {
        IUserRespository _user;
        public AssemblyDomain(IUserRespository user)
        {
            _user = user;
        }

        public Tuple<string, string, bool> GetAccount(out string error)
        {
            error = string.Empty;

            var finds = _user.GetListAsync().Result;

            var find = finds?.OrderBy(l => l.UDate)?.FirstOrDefault();

            if (find == null)
            {
                error = "用户不存在";
            }

            return Tuple.Create(find.Account, find.Password, true);
        }

        Random r = new Random();

        public bool Login(string account, string psd, bool IsSavePSD,out string error)
        {
            error = string.Empty;

            var find = _user.FirstOrDefaultAsync(l => l.Account == account).Result;

            if(find==null)
            {
                error = "账号不存在";
                return false;
            }

            if(find.Password!=psd)
            {
                error = "密码不正确";
                return false;
            }

            return true;


        }
        public void Debug(params string[] messages)
        {

        }

        public void Error(params Exception[] messages)
        {

        }

        public void Error(params string[] messages)
        {

        }

        public void Fatal(params string[] messages)
        {

        }

        public void Fatal(params Exception[] messages)
        {

        }

        public void Info(params string[] messages)
        {

        }

        public void Trace(params string[] messages)
        {

        }

        public void Warn(params string[] messages)
        {

        }
    }
}
