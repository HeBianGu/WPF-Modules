using HeBianGu.General.ModuleService;
using HeBianGu.Module.AutoTest.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.Module.AutoTest
{
    class ProcessModuleNode : ModuleNode
    {
        public ProcessModuleNode(IUnityContainer container, string moduleName) : base(container, moduleName)
        {
            this.OrderIndex = 100;
        }

        public ProcessModuleNode(IUnityContainer container, IModuleNode parent, string moduleName) : base(container, parent, moduleName)
        {
            this.OrderIndex = 100;
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
            _regionManager.RequestNavigate("ModuleRegion", nameof(ProcessView));
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
