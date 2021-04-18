using HeBianGu.General.ModuleService;
using HeBianGu.Module.ModuleC.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.Module.ModuleC
{
    //  Do ：OnDemand = true 按需加载，调用LoadModule时加载
    [Module(ModuleName = nameof(ModuleC), OnDemand = false)]
    public class ModuleC : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer container = containerRegistry.GetContainer();

            IModuleNode root = new ModuleCNode(container, "任务管理") { Icon= "\xe6c9" };

            //  Do ：注册代理和工具功能
            container.RegisterInstance(typeof(IModuleNode), this.GetType().Name, root);

            root.Functions.Add(new EditFunctionNode(container, "插入"));

            //  Do ：第一步 注册
            containerRegistry.RegisterForNavigation<DefaultCView>();

            containerRegistry.RegisterForNavigation<ModuleCEditer>();
        }
    }


}
