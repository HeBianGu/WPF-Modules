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
using System.Diagnostics;
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

namespace HeBianGu.App.ModuleMenu
{
    internal class ShellViewModel : BindableBase
    {
        IUnityContainer _container;

        IRegionManager _regionManager;

        IEventAggregator eventAggregator;

        IModuleManager _moduleManager;


        private ObservableCollection<IModuleNode> _collection = new ObservableCollection<IModuleNode>();
        /// <summary> 说明  </summary>
        public ObservableCollection<IModuleNode> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
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

                this.Collection = _container.ResolveAll<IModuleNode>()?.OrderBy(l=>l.OrderIndex)?.ToObservable();

                //this.SelectedModule = this.ModuleOperates?.FirstOrDefault();

                //this.SelectedModule.Load();

                var first = this.Collection?.FirstOrDefault();

                first.Load();

                first.Click();

            });

            SelectionChangedCommand = new DelegateCommand<object>(l =>
            {
                if(l is IModuleNode node)
                {
                    node.Click();

                    node.Load();
                }
                Debug.WriteLine("说明");
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
