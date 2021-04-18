using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.General.ModuleService
{
    /// <summary> 工具栏 </summary>
    public abstract class FunctionNode : IFunctionNode
    {
        public string Name { get; set; } = "操作";

        public int OrderIndex { get; set; }

        protected IUnityContainer _container;

        protected IRegionManager _regionManager;

        protected IEventAggregator _eventAggregator;


        public FunctionNode(IUnityContainer container, string moduleName)
        {
            _container = container;

            Name = moduleName;

            _regionManager = container.Resolve<IRegionManager>();

            _eventAggregator = container.Resolve<IEventAggregator>();
        }
        public abstract void Load();

        public abstract void Unload();
    }

    /// <summary> 默认工具功能节点 </summary>
    public class DefaultFunctionNode : FunctionNode
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="containerRegistry"> IOC </param>
        /// <param name="moduleName"> 工具栏名称 </param>
        /// <param name="type"> 注册页面类型 </param>
        public DefaultFunctionNode(IContainerRegistry containerRegistry, string moduleName,Type type) : base(containerRegistry.GetContainer(), moduleName)
        {
            this.OrderIndex = 1;

            this.Source = type.FullName;

            //  Do ：注册页面
            containerRegistry.RegisterForNavigation(type, type.FullName);
        }

        //public ModuleNavigatorNode(IUnityContainer container, IModuleNode parent, string moduleName) : base(container, parent, moduleName)
        //{
        //    this.OrderIndex = 1;
        //}

        public string Source { get; set; }

        public override void Load()
        {
            _regionManager.RequestNavigate("ToolBarRegion", this.Source);
        }

        public override void Unload()
        {
            //throw new NotImplementedException();
        }
    }
}
