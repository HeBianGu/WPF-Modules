using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.ModuleService
{
    public interface IModuleContainer
    {
        string Name { get; set; }

        int OrderIndex { get; set; }

        void Load();

        void Unload();
    }


}
