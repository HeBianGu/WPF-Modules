﻿using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.ModuleService
{
    public abstract partial class HPrismApplication : PrismApplication
    {
        //protected override void InitializeShell(Window shell)
        //{
        //    //  Do ：兼容框架触发动画
        //    if (shell is WindowBase window)
        //    {
        //        window.Show();
        //    }

        //    base.InitializeShell(shell);
        //}
    }

    /// <summary>
    /// 兼容WpfBase框架的实现
    /// </summary>
    public abstract partial class HPrismApplication
    {
        public HPrismApplication()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            ServiceRegistry.Instance.Register<IServiceCollection, ServiceCollection>();
            ServiceRegistry.Instance.Register<IApplicationBuilder, ApplicationBuilder>();

            this.ConfigureServices(this.IServiceCollection);
        }

        /// <summary> 异步线程抛出没有补货的异常 </summary>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Exception item in e.Exception.InnerExceptions)
            {
                sb.AppendLine($@"异常类型：{item.GetType()}
异常内容：{item.Message}
来自：{item.Source}
{item.StackTrace}");
            }

            e.SetObserved();

            this.ILogger?.Error("Task Exception");
            this.ILogger?.Error(sb.ToString());

            Current.Dispatcher.Invoke(() => MessageWindow.ShowSumit(sb.ToString(), "系统任务异常", false, 5));
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            this.ILogger?.Info("系统启动");

            this.Configure(this.IApplicationBuilder);

            this.UseSingleInstance();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                this.IApplicationBuilder.SaveLocalTheme();

                this.ILogger?.Info("系统退出");
            }
            catch (Exception ex)
            {
                this.ILogger?.Error(ex);
            }
            finally
            {
                base.OnExit(e);
            }
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Current.Dispatcher.Invoke(() => MessageWindow.ShowSumit(e.Exception.Message, "系统异常", false, 5));

            e.Handled = true;

            this.ILogger?.Error("系统异常");
            this.ILogger?.Error(e.Exception);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception error = (Exception)e.ExceptionObject;

            Current.Dispatcher.Invoke(() => MessageBox.Show("当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员", "意外的操作"));


            this.ILogger?.Fatal("当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员", "意外的操作");
            this.ILogger?.Fatal(error);
        }

        protected string GetVersonInfo()
        {
            var version = Assembly.GetEntryAssembly().GetName().Version;

            return $"V{version.Major}.{version.Minor}.{version.Build}";
        }

        protected string GetWpfControlLibVersonInfo()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            return $"V{version.Major}.{version.Minor}.{version.Build}";
        }

        protected abstract void ConfigureServices(IServiceCollection services);

        protected abstract void Configure(IApplicationBuilder app);

        public ILogService ILogger
        {
            get
            {
                return ServiceRegistry.Instance.GetInstance<ILogService>();
            }
        }

        public IApplicationBuilder IApplicationBuilder
        {
            get
            {
                return ServiceRegistry.Instance.GetInstance<IApplicationBuilder>();
            }
        }

        public IServiceCollection IServiceCollection
        {
            get
            {
                return ServiceRegistry.Instance.GetInstance<IServiceCollection>();
            }
        }

        Mutex mutex;
        /// <summary> 只创建一个实例 </summary>
        public void UseSingleInstance()
        {
            Process thisProc = Process.GetCurrentProcess();

            //互斥量创建成功标志
            bool createdNew;

            //创建互斥量
            mutex = new Mutex(true, thisProc.ProcessName, out createdNew);

            if (!createdNew)
            {
                MessageWindow.ShowSumit("当前程序已经运行！");

                Application.Current.Shutdown();
            }
        }

    }
}
