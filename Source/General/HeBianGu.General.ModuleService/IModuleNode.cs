using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.General.ModuleService
{
    public interface IModuleNode : IModuleContainer
    {
        List<IModuleNode> Children { get; set; }

        IModuleNode Parent { get; set; }

        void Click();

        void Checked();

        void Unchecked();

        List<IFunctionNode> Functions { get; set; } 
    }

}
