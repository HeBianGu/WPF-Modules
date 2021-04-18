using HeBianGu.General.ModuleService;
using HeBianGu.Module.ModuleA.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.Module.ModuleA
{
    class ModuleAContainer : ModuleContainer
    {
        public ModuleAContainer(IUnityContainer container, string moduleName):base(container, moduleName)
        {
 
        }

        public override void Load()
        {
            _regionManager.RequestNavigate("ModuleRegion", nameof(DefaultView));
        }

        public override void Unload()
        {
            Debug.WriteLine("  Debug.WriteLine(Unload);");
        }
    }
}
