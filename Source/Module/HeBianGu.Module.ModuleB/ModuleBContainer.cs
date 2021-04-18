using HeBianGu.General.ModuleService;
using HeBianGu.Module.ModuleB.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.Module.ModuleB
{
    class ModuleBContainer : ModuleContainer
    {
        public ModuleBContainer(IUnityContainer container, string moduleName) : base(container, moduleName)
        {

        }

        public override void Load()
        {
            _regionManager.RequestNavigate("ModuleRegion", nameof(DefaultBView)); 
        }

        public override void Unload()
        {
            Debug.WriteLine("  Debug.WriteLine(Unload);");
        }
    }
}
