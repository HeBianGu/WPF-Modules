﻿using HeBianGu.App.ShellOffice.View;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.DataBase.Logger;
using HeBianGu.General.ModuleService;
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

namespace HeBianGu.App.ShellOffice
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : HPrismApplication
    { 
        protected override Window CreateShell()
        {  
            return Container.Resolve<ShellWindow>();
        }

        protected override void InitializeShell(Window shell)
        {
            StartWindow startWindow = new StartWindow();

            Task.Run(() =>
            {
                Thread.Sleep(5000);

                this.Dispatcher.Invoke(() =>
                {
                    startWindow.Close();
                });
            });

            startWindow.ShowDialog();

            base.InitializeShell(shell);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //  Do ：注册用于导航的页面
            containerRegistry.RegisterForNavigation<ModuleNavigator>();

            containerRegistry.Register<IDebugRespository, DebugRespository>();
            containerRegistry.Register<IErrorRespository, ErrorRespository>();
            containerRegistry.Register<IFatalRespository, FatalRespository>();
            containerRegistry.Register<IInfoRespository, InfoRespository>();
            containerRegistry.Register<IWarnRespository, WarnRespository>();
            containerRegistry.Register<IDBLogService,DBLogService>();

            containerRegistry.Register<ILogService, AssemblyDomain>();

        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }

        /// <summary>
        /// 重写应用启动
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }


        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            //清理内存，退出所有线程
            Environment.Exit(Environment.ExitCode);
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator(); 
        }
    } 

    public partial class App
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            //  Do ：注册本地化配置读写服务
            services.AddSingleton<IThemeLocalizeService, LocalizeService>();

            //  Do ：注入领域模型服务
            services.AddSingleton<IAssemblyDomain, AssemblyDomain>();

            ////  Do ：注册日志服务
            //services.AddSingleton<ILogService, AssemblyDomain>();

        }

        protected override void Configure(IApplicationBuilder app)
        {
            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = (Color)ColorConverter.ConvertFromString("#FF009933");
                //l.ForegroundColor = (Color)ColorConverter.ConvertFromString("#727272");

                //l.SmallFontSize = 15D;
                //l.LargeFontSize = 18D;
                //l.FontSize = FontSize.Small;

                //l.ItemHeight = 30;

                //l.RowHeight = 32;

                //l.ItemWidth = 120;
                l.ItemCornerRadius = 2;

                //l.AnimalSpeed = 5000;

                //l.AccentColorSelectType = 0;

                //l.IsUseAnimal = false;

                //l.ThemeType = ThemeType.Light;

                //l.Language = Language.Chinese;

                l.AccentBrushType = AccentBrushType.SolidColorBrush;
            });
        }
    }
}
