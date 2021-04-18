using HeBianGu.General.DataBase.Identify;
using HeBianGu.General.ModuleService;
using HeBianGu.Module.Identify.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.Module.Identify
{
    //  Do ：OnDemand = true 按需加载，调用LoadModule时加载
    [Module(ModuleName = nameof(IdentifyModule), OnDemand = false)]
    public class IdentifyModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer container = containerRegistry.GetContainer();

            IModuleNode root = new ModuleParent(container, "身份认证") { OrderIndex = 99 };

            IModuleNode user = new DefaultModuleNode(containerRegistry, root, "用户",typeof(UserView)) { Icon = "\xe6c9" };
            user.Functions.Add(new DefaultFunctionNode(containerRegistry, "操作", typeof(UserToolBar)));
            root.Children.Add(user);

            IModuleNode role = new DefaultModuleNode(containerRegistry, root, "角色", typeof(RoleView)) { Icon = "\xe6c9" };
            role.Functions.Add(new DefaultFunctionNode(containerRegistry, "操作", typeof(RoleToolBar)));
            root.Children.Add(role);

            IModuleNode author = new DefaultModuleNode(containerRegistry, root, "权限", typeof(AuthorView)) { Icon = "\xe6c9" };
            author.Functions.Add(new DefaultFunctionNode(containerRegistry, "操作", typeof(UserToolBar)));
            root.Children.Add(author);

            IModuleNode log = new DefaultModuleNode(containerRegistry, root, "日志", typeof(LogView)) { Icon = "\xe6c9" };
            log.Functions.Add(new DefaultFunctionNode(containerRegistry, "操作", typeof(UserToolBar)));
            root.Children.Add(log);

            //  Do ：注册代理和工具功能
            container.RegisterInstance(typeof(IModuleNode), this.GetType().Name, root); 

            //  Do ：注册服务
            container.RegisterType<IUserRespository, UserRespository>();
            container.RegisterType<IAuthorRespository, AuthorRespository>();
            container.RegisterType<IRoleRespository, RoleRespository>();
            container.RegisterType<ILogRespository, LogRespository>();

            //container.RegisterType<ILogService, LogService>();

        }
    }


}
