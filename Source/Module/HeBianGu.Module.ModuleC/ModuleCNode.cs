using HeBianGu.General.ModuleService;
using HeBianGu.Module.ModuleC.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.Module.ModuleC
{
    class ModuleCNode : ModuleNode
    {
        public ModuleCNode(IUnityContainer container, string moduleName) : base(container, moduleName)
        {
            this.OrderIndex = 1;
        }

        public ModuleCNode(IUnityContainer container, IModuleNode parent, string moduleName) : base(container, parent, moduleName)
        {
            this.OrderIndex = 1;
        }

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
            _regionManager.RequestNavigate("ModuleRegion", nameof(DefaultCView));
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
}
