using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using Unity;

namespace HeBianGu.App.ModuleMenu
{
    class DataSourceLocator
    {
        public DataSourceLocator()
        {
            IUnityContainer unity = ServiceLocator.Current.GetInstance<IUnityContainer>();

            ShellViewModel = unity.Resolve<ShellViewModel>();
        }

        public ShellViewModel ShellViewModel { get; set; }
    }
}
