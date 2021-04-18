using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.General.ModuleService
{
    /// <summary> 模块显示 </summary>
    public abstract class ModuleNode : ModuleContainer, IModuleNode
    {
        public ModuleNode(IUnityContainer container, string moduleName) : base(container, moduleName)
        {

        }

        public ModuleNode(IUnityContainer container, IModuleNode parent, string moduleName) : base(container, moduleName)
        {
            this.Parent = parent;

            this.ParentName = parent.Name;
        }

        public List<IModuleNode> Children { get; set; } = new List<IModuleNode>();

        public IModuleNode Parent { get; set; }

        public string ParentName { get; set; } = "默认";

        public abstract void Checked();

        public abstract void Click();

        public abstract void Unchecked();

        public string Icon { get; set; }

        public List<IFunctionNode> Functions { get; set; } = new List<IFunctionNode>();
    }

    /// <summary> 默认模块节点 </summary>
    public class DefaultModuleNode : ModuleNode
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="containerRegistry"> ioc </param>
        /// <param name="moduleName"> 模块显示名称 </param>
        /// <param name="type"> 注册页面类型 </param>
        public DefaultModuleNode(IContainerRegistry containerRegistry, string moduleName, Type type) : base(containerRegistry.GetContainer(), moduleName)
        {
            this.OrderIndex = 100;

            this.Source = type.Name;

            //  Do ：注册页面
            containerRegistry.RegisterForNavigation(type, type.Name);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="containerRegistry"> ioc </param>
        /// <param name="parent"> 父节点 和分组名称 </param>
        /// <param name="moduleName"> 模块显示名称 </param>
        /// <param name="type"> 注册页面类型 </param>
        public DefaultModuleNode(IContainerRegistry containerRegistry, IModuleNode parent, string moduleName, Type type) : base(containerRegistry.GetContainer(), parent, moduleName)
        {
            this.OrderIndex = 100;

            this.Source = type.Name;

            //  Do ：注册页面
            containerRegistry.RegisterForNavigation(type, type.Name);
        }

        public string Source { get; set; }

        public override void Checked()
        {
            Debug.WriteLine("Click");
        }

        public override void Click()
        {
            Debug.WriteLine("Click");
        }

        public override void Load()
        {
            //  Do ：第二步 跳转
            _regionManager.RequestNavigate("ModuleRegion", this.Source);
        }

        public override void Unchecked()
        {
            Debug.WriteLine("Click");
        }

        public override void Unload()
        {
            Debug.WriteLine("  Debug.WriteLine(Unload);");
        }
    }

    /// <summary> 带有子模块的显示 </summary>
    public class ModuleParent: ModuleNode
    {
        public ModuleParent(IUnityContainer container, string moduleName) : base(container, moduleName)
        {

        }

        public override void Checked()
        {
           
        }

        public override void Click()
        {
            
        }

        public override void Load()
        {
            this.Children?.FirstOrDefault()?.Load();
        }

        public override void Unchecked()
        {
            
        }

        public override void Unload()
        {
            this.Children?.FirstOrDefault()?.Unload();
        }
    }
}
