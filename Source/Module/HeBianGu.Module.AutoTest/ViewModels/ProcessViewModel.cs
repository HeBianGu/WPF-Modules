using HeBianGu.Base.WpfBase;
using HeBianGu.General.DataBase.Logger;
using HeBianGu.General.ModuleService;
using HeBianGu.General.WpfControlLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Module.AutoTest
{
    /// <summary> 说明</summary>
    internal partial class ProcessViewModel : NotifyPropertyChanged
    {

        #region - 属性 -

        private ObservableCollection<string> _message = new ObservableCollection<string>();
        /// <summary> 说明  </summary>
        public ObservableCollection<string> Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }


        private bool _isBusy;
        /// <summary> 说明  </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }
        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        //IClientService service;
        protected override async void Loaded(object obj)
        {
            this.InfoWithTime("正在尝试连接WCF服务");
            this.InfoWithTime("IP:127.0.0.1");
            this.InfoWithTime("端口:7777");

            try
            {
                //service = new TcpService("127.0.0.1", "7777");

                //var ip = Dns.GetHostAddresses(Dns.GetHostName())?.FirstOrDefault(l => l.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                ////  Do ：尝试连接服务器
                ////var result = service.Call<ITest, CallResult>(l => l.Ping(ip.ToString()));
                //ITestCallBack callBack = new TestCallBack();

                //callBack.CallBack += l => this.InfoWithTime(l);

                //var result = service.DuplexCall<ITest, CallResult, ITestCallBack>(l => l.Ping(ip.ToString()), callBack);


                //if (result != null && result.Code)
                //{
                //    this.InfoWithTime("连接成功");
                //}
                //else
                //{
                //    this.InfoWithTime("连接成功,调用接口失败");
                //    this.InfoWithTime(result.Message);
                //}


                ////CallResult result= service.Call<ITest, CallResult>(l => l.Ping(ip.ToString()));

                //this.RefreshTestConfig();
            }
            catch (Exception ex)
            {
                this.InfoWithTime("连接失败", ex);
            }
        }

        protected override async void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Loaded")
            {
                this.InfoWithTime("正在尝试连接WCF服务");
                this.InfoWithTime("IP:127.0.0.1");
                this.InfoWithTime("端口:7777");

                try
                {
                    //service = new TcpService("127.0.0.1", "7777");

                    //var ip = Dns.GetHostAddresses(Dns.GetHostName())?.FirstOrDefault(l => l.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                    ////  Do ：尝试连接服务器
                    ////var result = service.Call<ITest, CallResult>(l => l.Ping(ip.ToString()));
                    //ITestCallBack callBack = new TestCallBack();

                    //callBack.CallBack += l => this.InfoWithTime(l);

                    //var result = service.DuplexCall<ITest, CallResult, ITestCallBack>(l => l.Ping(ip.ToString()), callBack);


                    //if (result != null && result.Code)
                    //{
                    //    this.InfoWithTime("连接成功");
                    //}
                    //else
                    //{
                    //    this.InfoWithTime("连接成功,调用接口失败");
                    //    this.InfoWithTime(result.Message);
                    //}


                    ////CallResult result= service.Call<ITest, CallResult>(l => l.Ping(ip.ToString()));

                    //this.RefreshTestConfig();
                }
                catch (Exception ex)
                {
                    this.InfoWithTime("连接失败", ex);
                }
            }
            //  Do：取消
            else if (command == "Run")
            {
                //  Do ：发送测试数据到服务端
                foreach (var cate in this.TestConfig.TestCategories)
                {
                    foreach (var item in cate.Items)
                    {
                        //if (!item.Selected) continue;

                        //var result = await this.RunItem(item);

                        ////service.Do<ITest>(l => l.Run(json));

                        //item.IsBuzy = false;

                        //if (result == null)
                        //{
                        //    this.InfoWithTime("调用服务失败"); continue;
                        //}

                        //if (!result.Code)
                        //{
                        //    this.InfoWithTime(result.Message); continue;
                        //}

                        //this.InfoWithTime($"发送成功:" + item.Model.Name);


                    }
                }

                this.InfoWithTime($"运行完成");
            }
        }


        void Info(string message)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                this.Message.Add(message);
            });
        }

        void InfoWithTime(string message)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                this.Message.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + message);
            });

        }

        void InfoWithTime(string message, Exception ex)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                this.Message.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + message + " " + ex);
            });
        }

        #endregion
    }

    partial class ProcessViewModel
    {
        public RelayCommand _RelayCommand;
        /// <summary>
        /// 修改工程测试项
        /// </summary>
        public RelayCommand RelayCommand
        {
            get
            {
                if (null == _RelayCommand)
                {
                    _RelayCommand = new RelayCommand(new Action<object>(RelayMethod));
                }
                return _RelayCommand;
            }
        }

        public RelayCommand _runItemCommand;
        /// <summary>
        /// 运行测试项
        /// </summary>
        public RelayCommand RunItemCommand
        {
            get
            {
                if (null == _runItemCommand)
                {
                    //_runItemCommand = new RelayCommand(new Action<object>(RunItem));
                }
                return _runItemCommand;
            }
        }


        //async Task<CallResult> RunItem(ItemViewModel item)
        //{
        //    string json = JsonConvert.SerializeObject(item.Model);

        //    ITestCallBack callBack = new TestCallBack();

        //    callBack.CallBack += l => this.InfoWithTime(l);

        //    item.xDatas.Clear();
        //    item.yDatas.Clear();
        //    item.DrawOnce = true;

        //    callBack.CallBackData += (l, k) =>
        //    {
        //        item.xDatas.Add(l);
        //        item.yDatas.Add(k);

        //        item.DrawOnce = true;
        //    };

        //    item.IsBuzy = true;

        //    item.IsShowChart = true;

        //    return await Task.Run(() =>
        //    {
        //        return service.DuplexCall<ITest, CallResult, ITestCallBack>(l => l.Run(json), callBack);
        //    });

        //}

        //async void RunItem(object obj)
        //{
        //    if (obj is ItemViewModel item)
        //    {

        //        var result = await this.RunItem(item);

        //        item.IsBuzy = false;

        //        if (result == null)
        //        {
        //            this.InfoWithTime("调用服务失败"); return;
        //        }

        //        if (!result.Code)
        //        {
        //            this.InfoWithTime(result.Message); return;
        //        }

        //        this.InfoWithTime($"发送成功:" + item.Model.Name);
        //    }
        //}
    }

    partial class ProcessViewModel
    {

        private TestConfigViewModel _testConfig;
        /// <summary> 说明  </summary>
        public TestConfigViewModel TestConfig
        {
            get { return _testConfig; }
            set
            {
                _testConfig = value;
                RaisePropertyChanged("TestConfig");
            }
        }


        AssemblyDomain _domain = new AssemblyDomain();


        //private ProjectViewModel _currentNew;
        ///// <summary> 说明  </summary>
        //public ProjectViewModel CurrentNew
        //{
        //    get { return _currentNew; }
        //    set
        //    {
        //        _currentNew = value;
        //        RaisePropertyChanged("CurrentNew");
        //    }
        //}


        async void RefreshTestConfig()
        {
            this.IsBusy = true;

            var result = await Task.Run(() =>
            {
                return _domain.LoadTestConfig();
            });

            this.TestConfig = new TestConfigViewModel(result);

            //try
            //{
            //    var ps = ServiceClient.GetInstance().GetTestProjects(Guid.NewGuid());

            //    if (ps.Count == 0)
            //    {
            //        this.CurrentNew = new ProjectViewModel(new TestProjectNew() { ID = Guid.NewGuid() });
            //    }
            //    else
            //    {
            //        this.CurrentNew = new ProjectViewModel(ps.FirstOrDefault());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.Info(ex.Message, ex);

            //    this.CurrentNew = new ProjectViewModel(new TestProjectNew() { ID = Guid.NewGuid() });
            //}

            this.IsBusy = false;

        }

        /// <summary>
        /// 保存工程信息
        /// </summary>
        /// <param name="obj"></param>
        async Task<bool> SaveProjectNew(object obj)
        {
            this.IsBusy = true;

            try
            {
                await Task.Run(() =>
                {
                    //var finds = ServiceClient.GetInstance().GetTestProjects(Guid.NewGuid());

                    //var find = finds?.FirstOrDefault(l => l.ProjectName == this.CurrentNew.Model.ProjectName);
                    ////  Do ：新增 
                    //if (find == null)
                    //{ 
                    //    foreach (var c in this.TestConfig.TestCategories)
                    //    {
                    //        foreach (var item in c.Items)
                    //        {

                    //        }
                    //    }

                    //    var selecteds = this.TestConfig.TestCategories.SelectMany(l => l.Items).Where(l => l.Selected);

                    //    this.CurrentNew.Model.TestItems.Clear();
                    //    ServiceClient.GetInstance().AddTestProject(this.CurrentNew.Model);
                    //    foreach (var item in selecteds)
                    //    {
                    //        TestItemNew @new = item.Convert();
                    //        @new.ProjectID = this.CurrentNew.Model.ID;
                    //        this.CurrentNew.Model.TestItems.Add(@new);
                    //        ServiceClient.GetInstance().ProjectAddTestItems(this.CurrentNew.Model, @new);
                    //    }


                    //}
                    //else
                    //{

                    //    var selecteds = this.TestConfig.TestCategories.SelectMany(l => l.Items).Where(l => l.Selected);

                    //    this.CurrentNew.Model.TestItems.Clear();

                    //    foreach (var item in selecteds)
                    //    {
                    //        TestItemNew @new = item.Convert();
                    //        @new.ProjectID = this.CurrentNew.Model.ID;
                    //        this.CurrentNew.Model.TestItems.Add(@new);
                    //    }

                    //    ServiceClient.GetInstance().ModifyTestProject(this.CurrentNew.Model);

                    //    //foreach (var item in this.CurrentProject.Model.TestItems)
                    //    //{
                    //    //    ServiceClient.GetInstance().RemoveAssociateTestItem(this.CurrentProject.Model, item);
                    //    //}

                    //    //ServiceClient.GetInstance().AssociateTestItems(this.CurrentProject.Model, this.CurrentProject.TestItems.Select(l => l.Model)?.ToList());

                    //    ////  Do ：保存到实体
                    //    //this.CurrentProject.Model.TestItems = this.CurrentProject.TestItems.Select(l => l.Model).ToList();

                    //    //ServiceClient.GetInstance().ModifyTestProject(this.CurrentProject.Model);
                    //}
                });

                this.InfoWithTime($"保存成功");

                //this.IsChanged = false;

                return true;
            }
            catch (Exception ex)
            {
                this.InfoWithTime("保存工程信息错误，详情请看日志", ex);

                return false;
            }
            finally
            {
                this.IsBusy = false;
            }

        }
    }

}
