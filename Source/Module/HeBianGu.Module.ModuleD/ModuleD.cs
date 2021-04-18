using HeBianGu.General.ModuleService;
using HeBianGu.Module.ModuleD.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.Module.ModuleD
{
    //  Do ：OnDemand = true 按需加载，调用LoadModule时加载
    [Module(ModuleName = nameof(ModuleD), OnDemand = false)]
    public class ModuleD : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer container = containerRegistry.GetContainer();

            IModuleNode root = new ModuleDNode(container, "基础管理") { Icon = "\xe6c9" };

            container.RegisterInstance(typeof(IModuleNode), this.GetType().Name, root);

            root.Functions.Add(new EditFunctionNode(container, "操作"));

            root.Functions.Add(new EditFunctionNode(container, "查看"));

            containerRegistry.RegisterForNavigation<DefaultDView>(); 

            containerRegistry.RegisterForNavigation<ModuleDEditer>();
        }
    }


}
