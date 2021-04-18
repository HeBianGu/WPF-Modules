using HeBianGu.General.ModuleService;
using HeBianGu.Module.ModuleD.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.Module.ModuleD
{
    class ModuleDNode : ModuleNode
    {
        public ModuleDNode(IUnityContainer container, string moduleName) : base(container, moduleName)
        {
            this.OrderIndex = 2;
        }

        public ModuleDNode(IUnityContainer container, IModuleNode parent, string moduleName) : base(container, parent, moduleName)
        {
            this.OrderIndex = 2;
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
            _regionManager.RequestNavigate("ModuleRegion", nameof(DefaultDView));
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
