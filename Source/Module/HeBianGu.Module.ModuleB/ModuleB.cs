using HeBianGu.General.ModuleService;
using HeBianGu.Module.ModuleB.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.Module.ModuleB
{
    //  Do ：OnDemand = true 按需加载，调用LoadModule时加载
    [Module(ModuleName = nameof(ModuleB), OnDemand = false)]
    public class ModuleB : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer container = containerRegistry.GetContainer();

            IModuleContainer operate = new ModuleBContainer(container, this.GetType().Name);

            container.RegisterInstance(typeof(IModuleContainer), this.GetType().Name, operate);

            containerRegistry.RegisterForNavigation<DefaultBView>();
        }
    }


}
