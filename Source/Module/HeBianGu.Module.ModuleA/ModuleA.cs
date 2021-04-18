using HeBianGu.General.ModuleService;
using HeBianGu.Module.ModuleA.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.Module.ModuleA
{
    //  Do ：OnDemand = true 按需加载，调用LoadModule时加载
    [Module(ModuleName = nameof(ModuleA), OnDemand = false)]
    public class ModuleA : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
           
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer container = containerRegistry.GetContainer();

            IModuleContainer operate = new ModuleAContainer(container, this.GetType().Name);

            container.RegisterInstance(typeof(IModuleContainer), this.GetType().Name, operate);

            containerRegistry.RegisterForNavigation<DefaultView>();
        }
    }

   
}
