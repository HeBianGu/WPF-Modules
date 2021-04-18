using HeBianGu.Base.WpfBase;
using HeBianGu.General.ModuleService;
using HeBianGu.General.WpfControlLib;
using Prism.Commands;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Unity;

namespace HeBianGu.App.ModuleList
{
    internal class ShellViewModel : BindableBase
    {
        IUnityContainer _container;

        IRegionManager _regionManager;

        IEventAggregator eventAggregator;

        IModuleManager _moduleManager;

        private List<IModuleContainer> _moduleOperate;
        /// <summary> 所有模块  </summary>
        public List<IModuleContainer> ModuleOperates
        {
            get { return _moduleOperate; }
            set
            {
                _moduleOperate = value;
                RaisePropertyChanged("ModuleOperates");
            }
        }


        private IModuleContainer _selectedModule;
        /// <summary> 说明  </summary>
        public IModuleContainer SelectedModule
        {
            get { return _selectedModule; }
            set
            {
                _selectedModule = value;
                RaisePropertyChanged("SelectedModule");
            }
        }

        public ShellViewModel(IUnityContainer unityContainer, IRegionManager regionManager, IModuleManager moduleManager)
        {
            _container = unityContainer;

            _moduleManager = moduleManager;

            _regionManager = regionManager;

            SumitCommand = new DelegateCommand(OnSumit);

            LoadedCommand = new DelegateCommand(() =>
            {
                //var catalog = _container.Resolve<IModuleCatalog>();

                //  var module = _container.ResolveAll<IModule>(); 

                this.ModuleOperates = _container.ResolveAll<IModuleContainer>()?.ToList();

                this.SelectedModule = this.ModuleOperates?.FirstOrDefault();

                this.SelectedModule.Load();
            });

            SelectionChangedCommand = new DelegateCommand(() =>
            {
                if (SelectedModule == null) return;

                this.SelectedModule.Load();
            });
        }

        void OnSumit()
        {
            MessageBox.Show("sss");
        }

        public ICommand SumitCommand { get; }

        public ICommand LoadedCommand { get; }

        public ICommand SelectionChangedCommand { get; }
    }
}
