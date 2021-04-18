using HeBianGu.Base.WpfBase;
using HeBianGu.General.ModuleService;
using HeBianGu.General.WpfControlLib;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections;
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
using System.Windows.Data;
using System.Windows.Input;
using Unity;

namespace HeBianGu.App.ShellOffice
{
    internal class ShellViewModel : BindableBase
    {
        IUnityContainer _container;

        IRegionManager _regionManager;

        IEventAggregator eventAggregator;

        IModuleManager _moduleManager;


        private ObservableCollection<IFunctionNode> _functions = new ObservableCollection<IFunctionNode>();
        /// <summary> 说明  </summary>
        public ObservableCollection<IFunctionNode> Functions
        {
            get { return _functions; }
            set
            {
                _functions = value;
                RaisePropertyChanged("Functions");
            }
        }


        private IFunctionNode _selectedFunction;
        /// <summary> 说明  </summary>
        public IFunctionNode SelectedFunction
        {
            get { return _selectedFunction; }
            set
            {
                _selectedFunction = value;
                RaisePropertyChanged("SelectedFunction");
            }
        }



        private ObservableCollection<IModuleNode> _modules = new ObservableCollection<IModuleNode>();
        /// <summary> 说明  </summary>
        public ObservableCollection<IModuleNode> Modules
        {
            get { return _modules; }
            set
            {
                _modules = value;
                RaisePropertyChanged("Modules");
            }
        }


        private IModuleNode _selectedModuleNode;
        /// <summary> 说明  </summary>
        public IModuleNode SelectedModuleNode
        {
            get { return _selectedModuleNode; }
            set
            {
                _selectedModuleNode = value;
                RaisePropertyChanged("SelectedModuleNode");
            }
        }


        private bool _titleVisible = true;
        /// <summary> 抬头是否可见  </summary>
        public bool TitleVisble
        {
            get { return _titleVisible; }
            set
            {
                _titleVisible = value;
                RaisePropertyChanged("TitleVisble");
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
                var modules= _container.ResolveAll<IModuleNode>()?.OrderBy(l => l.OrderIndex)?.ToObservable();

                foreach (var item in modules)
                {
                    if(item is ModuleParent parent)
                    {
                        //  Do ：如果包含子节点，将子节点添加
                        this.Modules.AddRange(parent.Children);
                    }
                    else
                    {
                        this.Modules.Add(item);
                    }
                }

                Application.Current.Dispatcher.Invoke(() =>
                {
                    //  Do ：分组
                    ICollectionView vw = CollectionViewSource.GetDefaultView(this.Modules);
                    vw.GroupDescriptions.Clear();
                    vw.GroupDescriptions.Add(new PropertyGroupDescription("ParentName"));
                });


                this.Functions.Add(new NavigatorFunctionNode(_container, "模块列表"));

                this.SelectedFunction = this.Functions.FirstOrDefault();

                this.SelectedFunction.Load();

            });

            SelectionChangedCommand = new DelegateCommand<object>(l =>
            {
                if (this.SelectedFunction == null) return;

                this.SelectedFunction.Load(); 

                this.TitleVisble = true;
            });

            SelectionChangedModuleCommand = new DelegateCommand<object>(l =>
            {
                //  Do ：清理上一模块数据
                IList removes = l as IList;

                foreach (var last in removes)
                {
                    if (last is IModuleNode node)
                    {
                        this.SelectedModuleNode.Unload();

                        foreach (var fun in node.Functions)
                        {
                            this.Functions.Remove(fun);
                        }
                    }
                }

                //  Do ：加载当前模块数据
                if (this.SelectedModuleNode == null) return;

                this.SelectedModuleNode.Click();

                this.SelectedModuleNode.Load();

                if (this.SelectedModuleNode.Functions != null)
                {
                    this.Functions.AddRange(this.SelectedModuleNode.Functions);
                }

                //  Do ：跳转到工具页面
                if (this.Functions.Count > 1)
                {
                    this.SelectedFunction = this.Functions[1];
                }
            });

        }
        void OnSumit()
        {
            MessageBox.Show("sss");
        }

        public ICommand SumitCommand { get; }

        public ICommand LoadedCommand { get; }

        public ICommand SelectionChangedCommand { get; }

        public ICommand SelectionChangedModuleCommand { get; }
    }
}
