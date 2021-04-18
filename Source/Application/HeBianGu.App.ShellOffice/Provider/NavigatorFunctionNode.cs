using HeBianGu.App.ShellOffice.View;
using HeBianGu.General.ModuleService;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.App.ShellOffice
{
    internal class NavigatorFunctionNode : FunctionNode
    {
        public NavigatorFunctionNode(IUnityContainer container, string moduleName) : base(container, moduleName)
        {
            this.OrderIndex = 1;
        }

        public override void Load()
        {
            _regionManager.RequestNavigate("ToolBarRegion", nameof(ModuleNavigator));
        }

        public override void Unload()
        {

        }
    }
}
