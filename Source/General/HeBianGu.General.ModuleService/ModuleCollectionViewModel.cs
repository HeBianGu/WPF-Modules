using CommonServiceLocator;
using HeBianGu.Base.WpfBase;
using HeBianGu.Common.DataBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.ModuleService
{
    public abstract class ModuleCollectionViewModel<T> : ObservableSourceViewModel<T> where T : new()
    {

    }


    public abstract class ModuleLogCollectionViewModel<T> : ModuleCollectionViewModel<T> where T : new()
    {
        public ILogService LogService { get; set; } = ServiceLocator.Current.GetInstance<ILogService>();
    }

    /// <summary> 带有仓储库和实体的ViewModel集合基类 </summary>
    public abstract class ModuleCollectionViewModel<S, T> : ModuleLogCollectionViewModel<T> where T : GuidEntityBase, new() where S : IRepository<T>
    {
        public S Service { get; set; } = ServiceLocator.Current.GetInstance<S>();

        /// <summary> 添加 </summary>
        protected override async void Add(object obj)
        {
            this.AddItem = new T();

            bool r = await PropertyGrid.ShowObject(this.AddItem, null, "新增");

            if (!r) return;

            await this.Service.InsertAsync(this.AddItem);

            this.Collection.Add(new SelectViewModel<T>(this.AddItem));
        }

        /// <summary> 删除 </summary>
        protected override async void Delete(object obj)
        {
            var selects = this.Collection.Where(l => l.Selected)?.ToList();

            if (selects == null || selects.Count() == 0)
            {
                MessageService.ShowSnackMessage($"至少选择一条数据");
                return;
            }

            bool r = MessageWindow.ShowDialog("删除数据将无法恢复，确认删除?");

            if (!r) return;

            var result = await MessageService.ShowStringProgress(l =>
            {

                for (int i = 0; i < selects.Count(); i++)
                {
                    var item = selects[i];

                    this.Service.DeleteAsync(item.Model, false).Wait();

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.Collection.Remove(item);
                    });

                    l.MessageStr = $"正在提交当前页第{i}份数据,共{selects.Count()}份";

                    Thread.Sleep(10);
                }

                this.Service.SaveAsync().Wait();

                return true;
            });

            if (result)
            {
                MessageService.ShowSnackMessage($"删除成功,总计{selects.Count()}条");

                this.LogService.Info($"删除成功,总计{selects.Count()}条");
            }
            else
            {
                MessageService.ShowSnackMessage($"删除失败");

                this.LogService.Info($"删除失败");
            }
        }

        /// <summary> 编辑 </summary>
        protected override void Edit(object obj)
        {

        }

        /// <summary> 查看 </summary>
        protected override void Detail(object obj)
        {

        }

        /// <summary> 删除选中项 </summary>
        protected override void DeleteItem(object obj)
        {
            if (obj == null) return;

            bool r = MessageWindow.ShowDialog("删除数据将无法恢复，确认删除?");

            if (!r) return;

            if (obj is SelectViewModel<T> s)
            {
                this.Collection.Remove(s);

                this.Service.DeleteAsync(s.Model);

                MessageService.ShowSnackMessage("删除成功");

                this.LogService.Info("删除成功");
            }
        }

        /// <summary> 加载方法 </summary>
        protected override async void Loaded(object obj)
        {
            this.Collection = await MessageService.ShowWaittingResultMessge(() =>
            {
                var finds = this.Service.GetListAsync().Result;

                var selects = finds.Select(l => new SelectViewModel<T>(l));

                return new ObservableCollection<SelectViewModel<T>>(selects);
            });

            this.LogService.Info("加载完成");

            MessageService.ShowSnackMessage("加载完成");
        }
    }


    public abstract class ModuleCollectionViewModel<S1, S, T> : ModuleCollectionViewModel<S,T> where T : GuidEntityBase, new() where S : IRepository<T>
    {
        public S1 Service1 { get; set; } = ServiceLocator.Current.GetInstance<S1>();
    }
}
