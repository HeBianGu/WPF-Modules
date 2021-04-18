﻿using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.App.ModuleList
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {


        #region Override

        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
          

        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }

        #endregion




        #region Startup Checking



        /// <summary>
        /// 重写应用启动
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            //应用名 

            Process thisProc = Process.GetCurrentProcess();

            //互斥量创建成功标志
            bool createdNew;

            //创建互斥量
            Mutex mutex = new Mutex(true, thisProc.ProcessName, out createdNew);

            if (!createdNew)
            {
                //app is already running! Exiting the application.
                Application.Current.Shutdown();
            }

            base.OnStartup(e);
        }

        #endregion

        #region Exit

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            //清理内存，退出所有线程
            Environment.Exit(Environment.ExitCode);
        }

        #endregion
    }
}
