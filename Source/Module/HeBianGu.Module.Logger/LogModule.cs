using HeBianGu.General.DataBase.Logger;
using HeBianGu.General.ModuleService;
using HeBianGu.Module.Logger.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.Module.Logger
{
    //  Do ：OnDemand = true 按需加载，调用LoadModule时加载
    [Module(ModuleName = nameof(LogModule), OnDemand = false)]
    public class LogModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer container = containerRegistry.GetContainer();

            IModuleNode root = new ModuleParent(container, "日志管理") { OrderIndex = 100 };

            IModuleNode info = new DefaultModuleNode(containerRegistry, root, "运行", typeof(InfoLogView)) { Icon = "\xe6c9" };
            info.Functions.Add(new DefaultFunctionNode(containerRegistry, "操作", typeof(InfoLogToolBar)));
            root.Children.Add(info);

            IModuleNode error = new DefaultModuleNode(containerRegistry, root, "错误", typeof(ErrorLogView)) { Icon = "\xe6c9" };
            error.Functions.Add(new DefaultFunctionNode(containerRegistry, "操作", typeof(ErrorLogToolBar)));
            root.Children.Add(error);

            IModuleNode debug = new DefaultModuleNode(containerRegistry, root, "调试", typeof(DebugLogView)) { Icon = "\xe6c9" };
            debug.Functions.Add(new DefaultFunctionNode(containerRegistry, "操作", typeof(DebugLogToolBar)));
            root.Children.Add(debug);

            IModuleNode warn = new DefaultModuleNode(containerRegistry, root, "警告", typeof(WarnLogView)) { Icon = "\xe6c9" };
            warn.Functions.Add(new DefaultFunctionNode(containerRegistry, "操作", typeof(WarnLogToolBar)));
            root.Children.Add(warn);

            IModuleNode fatal = new DefaultModuleNode(containerRegistry, root, "严重", typeof(FatalLogView)) { Icon = "\xe6c9" };
            fatal.Functions.Add(new DefaultFunctionNode(containerRegistry, "操作", typeof(FatalLogToolBar)));
            root.Children.Add(fatal);

            //  Do ：注册代理和工具功能
            container.RegisterInstance(typeof(IModuleNode), this.GetType().Name, root);



            //  Do ：第一步 注册
            //containerRegistry.RegisterForNavigation<InfoLogView>();
            //containerRegistry.RegisterForNavigation<ErrorLogView>();
            //containerRegistry.RegisterForNavigation<DebugLogView>();
            //containerRegistry.RegisterForNavigation<FatalLogView>();
            //containerRegistry.RegisterForNavigation<WarnLogView>();

            //containerRegistry.RegisterForNavigation<InfoLogToolBar>();
            //containerRegistry.RegisterForNavigation<ErrorLogToolBar>();
            //containerRegistry.RegisterForNavigation<DebugLogToolBar>();
            //containerRegistry.RegisterForNavigation<WarnLogToolBar>();
            //containerRegistry.RegisterForNavigation<FatalLogToolBar>();

            //  Do ：注册服务
            container.RegisterType<IInfoRespository, InfoRespository>();
            container.RegisterType<IErrorRespository, ErrorRespository>();
        }
    }


}
