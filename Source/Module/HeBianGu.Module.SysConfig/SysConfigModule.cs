using HeBianGu.General.DataBase.SysConfig;
using HeBianGu.General.ModuleService;
using HeBianGu.Module.SysConfig.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.Module.SysConfig
{
    //  Do ：OnDemand = true 按需加载，调用LoadModule时加载
    [Module(ModuleName = nameof(SysConfigModule), OnDemand = false)]
    public class SysConfigModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer container = containerRegistry.GetContainer();

            IModuleNode root = new ModuleParent(container, "系统配置") { OrderIndex = 100 };

            IModuleNode info = new DefaultModuleNode(containerRegistry, root, "参数配置", typeof(SysConfigView)) { Icon = "\xe6c9" };
            info.Functions.Add(new DefaultFunctionNode(containerRegistry, "操作",typeof(SysConfigToolBar)));
            root.Children.Add(info);

            //  Do ：注册代理和工具功能
            container.RegisterInstance(typeof(IModuleNode), this.GetType().Name, root); 

            //  Do ：第一步 注册
            //containerRegistry.RegisterForNavigation<SysConfigView>();

            //containerRegistry.RegisterForNavigation<SysConfigToolBar>();

            //  Do ：注册服务
            container.RegisterType<IConfigRespository, ConfigRespository>(); 
        }
    }


}
