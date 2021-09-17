using CommonServiceLocator;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.ModuleService;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Unity;
using HeBianGu.General.LicService;
//using HeBianGu.Control.Lic;
using System.Windows.Data;

namespace HeBianGu.App.ShellOffice
{
    [ViewModel("Loyout")]
    class LoyoutViewModel : MvcViewModelBase
    {
        private ObservableCollection<License> _Licenses = new ObservableCollection<License>();
        /// <summary> 说明  </summary>
        public ObservableCollection<License> Licenses
        {
            get { return _Licenses; }
            set
            {
                _Licenses = value;
                RaisePropertyChanged("Licenses");
            }
        }

        private License _selectedLicense;
        /// <summary> 说明  </summary>
        public License SelectedLicense
        {
            get { return _selectedLicense; }
            set
            {
                _selectedLicense = value;
                RaisePropertyChanged("SelectedLicense");
            }
        }


        private ObservableCollection<ProjectNotify> _project = new ObservableCollection<ProjectNotify>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ProjectNotify> Projects
        {
            get { return _project; }
            set
            {
                _project = value;
                RaisePropertyChanged("Projects");
            }
        }


        private ProjectNotify _selectedProject;
        /// <summary> 说明  </summary>
        public ProjectNotify SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                RaisePropertyChanged("SelectedProject");
            }
        }




        protected override void Init()
        {
            //  Do ：许可
            IUnityContainer _container = ServiceLocator.Current.GetInstance<IUnityContainer>();

            var modules = _container.ResolveAll<IModuleNode>()?.OrderBy(l => l.OrderIndex)?.ToObservable();

            Licenses.Clear();

            LicenseService service = new LicenseService();

            foreach (var module in modules)
            {
                foreach (var item in module.Children)
                {
                    License license = new License();

                    license.ModuleName = item.Name;

                    //var option = service.IsVail(item.Name, out string err);

                    //if (option == null)
                    //{
                    //    license.State = "未激活";

                    //    license.Date = string.Empty;

                    //    Licenses.Add(license);
                    //}
                    //else
                    //{
                    //    license.State = "已激活";

                    //    license.Date = option.EndTime.ToString("yyyy-MM-dd HH:mm:ss");

                    //    Licenses.Add(license);
                    //}
                }

            }

            //  Do ：加载打开

            for (int i = 0; i < 20; i++)
            {
                var p = new Project()
                {
                    Name = "工程" + i,
                    Type = "类型",
                    Path = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)
                };
                var pn = new ProjectNotify(p);

                pn.Group = "更早";

                if (i < 5)
                {
                    pn.Group = "今天";
                }

                if (i < 2)
                {
                    pn.Group = "已固定";
                }

                this.Projects.Add(pn);
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                //Do ：分组
                ICollectionView vw = CollectionViewSource.GetDefaultView(this.Projects);
                vw.GroupDescriptions.Clear();
                vw.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
            });

            this.SelectedProject = this.Projects?.FirstOrDefault();
        }

        protected override void Loaded(string args)
        {

        }

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：对话消息
            if (command == "Sumit")
            {

            }

            //  Do：等待消息
            else if (command == "Button.Click.EditLic")
            {
                //if (this.SelectedLicense == null) return;

                //var r = LicWindow.ShowLic(this.SelectedLicense.ModuleName, out string error);

                //if (!r) return;

                //LicenseService service = new LicenseService();

                //var option = service.IsVail(this.SelectedLicense.ModuleName, out string err);

                //if (option == null)
                //{
                //    this.SelectedLicense.State = "未激活";

                //    this.SelectedLicense.Date = string.Empty;
                //}
                //else
                //{
                //    this.SelectedLicense.State = "已激活";

                //    this.SelectedLicense.Date = option.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
                //}
            }
        }

    }


    class Connect
    {
        [Required]
        [Display(Name = "连接地址")]
        public string IP { get; set; }

        [Required]
        [Display(Name = "连接地址")]
        public string Port { get; set; }
    }

    /// <summary> 说明</summary>
    internal class ProjectNotify : ModelViewModel<Project>
    {
        public ProjectNotify() : base(new Project())
        {

        }

        public ProjectNotify(Project model) : base(model)
        {

        }
        #region - 属性 -


        private string _group;
        /// <summary> 说明  </summary>
        public string Group
        {
            get { return _group; }
            set
            {
                _group = value;
                RaisePropertyChanged("Group");
            }
        }


        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }

        #endregion
    }


    class Project
    {
        [Required]
        [Display(Name = "工程名称")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "类型")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "地址")]
        public string Path { get; set; }

        [Display(Name = "编辑时间")]
        public DateTime EDate { get; set; } = DateTime.Now;

        [Display(Name = "编辑时间")]
        public DateTime CDate { get; set; } = DateTime.Now;
    }


    class License : NotifyPropertyChanged
    {

        private string _moduleName;
        /// <summary> 说明  </summary>
        public string ModuleName
        {
            get { return _moduleName; }
            set
            {
                _moduleName = value;
                RaisePropertyChanged("ModuleName");
            }
        }

        private string _state;
        /// <summary> 说明  </summary>
        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
                RaisePropertyChanged("Value");
            }
        }

        private string _date;
        /// <summary> 说明  </summary>
        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged("Date");
            }
        }
    }
}
