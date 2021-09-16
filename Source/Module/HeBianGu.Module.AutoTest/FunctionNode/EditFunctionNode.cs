using HeBianGu.General.ModuleService;
using HeBianGu.Module.AutoTest.Views;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.Module.AutoTest
{
    internal class EditFunctionNode : FunctionNode
    {
        public EditFunctionNode(IUnityContainer container, string moduleName) : base(container, moduleName)
        {
            this.OrderIndex = 1;
        }

        //public ModuleNavigatorNode(IUnityContainer container, IModuleNode parent, string moduleName) : base(container, parent, moduleName)
        //{
        //    this.OrderIndex = 1;
        //}

        public override void Load()
        {
            _regionManager.RequestNavigate("ToolBarRegion", nameof(ModuleCEditer));
        }

        public override void Unload()
        {
            //throw new NotImplementedException();
        }
    }
}
