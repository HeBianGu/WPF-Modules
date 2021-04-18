using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HeBianGu.General.ModuleService
{
    public abstract class ModuleContainer : IModuleContainer
    {
        public string Name { get; set; }

        public int OrderIndex { get; set; }

        protected IUnityContainer _container;

        protected IRegionManager _regionManager;

        protected IEventAggregator _eventAggregator;


        public ModuleContainer(IUnityContainer container, string moduleName)
        {
            _container = container;

            Name = moduleName;

            _regionManager = container.Resolve<IRegionManager>();

            _eventAggregator = container.Resolve<IEventAggregator>();
        }
        public abstract void Load();

        public abstract void Unload();
    }
}
