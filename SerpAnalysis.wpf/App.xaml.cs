using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using NLog;
using NLog.Extensions.Logging;
using SerpAnalysis.Core.BusinessServices;
using SerpAnalysis.Core.Interfaces;
using SerpAnalysis.wpf.ViewModels;

namespace SerpAnalysis.wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("NLog.json", optional: true, reloadOnChange: true).Build();
            LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));


            Services = ConfigureServices();
            Ioc.Default.ConfigureServices(Services);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var w = new MainWindow();
            w.Show();
            LogManager.GetCurrentClassLogger().Trace("MainWindow Showed");
            base.OnStartup(e);
        }



        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<IGoogleCrawler, GoogleCrawler>();
            services.AddTransient<ICrawlerIntegrationService, CrawlerIntegrationService>();
            services.AddTransient<MainViewModel, MainViewModel>();

            return services.BuildServiceProvider();
        }

        public IServiceProvider Services { get; }


        public static ILogger GetLogger(string classname)
        {
            return LogManager.GetLogger(classname);
        }
    }
}
