using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.General.DataBase.Identify;
using HeBianGu.General.ModuleService;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Module.Identify.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Module.Identify
{
    /// <summary> 说明</summary>
    internal class UserViewModel : ModuleCollectionViewModel<IRoleRespository, IUserRespository, hi_dd_user>
    {

        #region - 属性 -


        private ObservableCollection<hi_dd_role> _roles = new ObservableCollection<hi_dd_role>();
        /// <summary> 角色列表  </summary>
        public ObservableCollection<hi_dd_role> Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                RaisePropertyChanged("Roles");
            }
        }

        #endregion

        #region - 命令 -

        /// <summary> 加载方法 </summary>
        protected override async void Loaded(object obj)
        {
            this.Roles = await MessageService.ShowWaittingResultMessge(() =>
            {
                var finds = this.Service1.GetListAsync().Result;

                return new ObservableCollection<hi_dd_role>(finds);
            });

            this.Refresh();
        }

        async void Refresh()
        {
            this.Collection = await MessageService.ShowWaittingResultMessge(() =>
            {
                var finds = this.Service.GetListAsync("Role").Result;

                var selects = finds.Select(l => new SelectViewModel<hi_dd_user>(l));

                return new ObservableCollection<SelectViewModel<hi_dd_user>>(selects);
            });

            this.LogService.Info("加载完成");

            MessageService.ShowSnackMessage("加载完成");
        }

        /// <summary> 添加 </summary>
        protected override async void Add(object obj)
        {
            this.AddItem = new hi_dd_user() { RoleID = this.Roles.FirstOrDefault().ID };

            UserItemDialog dialog = new UserItemDialog();

            dialog.Sumit = () =>
              {
                  var result = this.AddItem.ModelState(out List<string> errors);

                  if (result)
                  {
                      this.Service.InsertAsync(this.AddItem).Wait();

                      this.Collection.Add(new SelectViewModel<hi_dd_user>(this.AddItem));

                      MessageService.CloseLayer();

                      this.Refresh();
                  }
                  else
                  {
                      MessageService.ShowSnackMessageWithNotice(errors?.FirstOrDefault());
                  } 
              };

            MessageService.ShowLayer(dialog);
        }

        #endregion


        #region - 方法 -

        protected override async void RelayMethod(object obj)
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

    public class UserPropertyTemplateSelector : PropertyGridTemplateSelector
    {
        public DataTemplate RoleSelecterTempate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ObjectPropertyItem objectItem)
            {
                if (objectItem.PropertyInfo.Name == "RoleID") return RoleSelecterTempate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
