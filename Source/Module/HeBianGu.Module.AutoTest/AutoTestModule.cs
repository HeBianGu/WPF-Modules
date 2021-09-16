using HeBianGu.General.DataBase.Logger;
using HeBianGu.General.ModuleService;
using HeBianGu.Module.AutoTest.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.Module.AutoTest
{
    //  Do ：OnDemand = true 按需加载，调用LoadModule时加载
    [Module(ModuleName = nameof(AutoTestModule), OnDemand = false)]
    public class AutoTestModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer container = containerRegistry.GetContainer();

            IModuleNode root = new ModuleParent(container, "自动测试") { OrderIndex = 0 };

            IModuleNode info = new ProcessModuleNode(container, root, "测试流程") { Icon = "\xe6c9" };
            info.Functions.Add(new DefaultFunctionNode(containerRegistry, "操作", typeof(ProcessToolBar)));

            root.Children.Add(info);

            IModuleNode error = new ResultModuleNode(container, root, "测试结果") { Icon = "\xe6c9" };
            error.Functions.Add(new DefaultFunctionNode(containerRegistry, "操作", typeof(ResultToolBar)));
            root.Children.Add(error);

            IModuleNode debug = new CalibrationModuleNode(container, root, "数据校准") { Icon = "\xe6c9" };
            debug.Functions.Add(new DefaultFunctionNode(containerRegistry, "操作", typeof(CalibrationBar)));
            root.Children.Add(debug);

            //  Do ：注册代理和工具功能
            container.RegisterInstance(typeof(IModuleNode), this.GetType().Name, root);



            //  Do ：第一步 注册
            containerRegistry.RegisterForNavigation<ProcessView>();

            containerRegistry.RegisterForNavigation<ResultView>();

            containerRegistry.RegisterForNavigation<CalibrationView>();

            //containerRegistry.RegisterForNavigation<FatalLogView>();

            //containerRegistry.RegisterForNavigation<WarnLogView>();

            //containerRegistry.RegisterForNavigation<ModuleCEditer>();

            //  Do ：注册服务
            container.RegisterType<IInfoRespository, InfoRespository>();
            container.RegisterType<IErrorRespository, ErrorRespository>();
        }
    }


}
