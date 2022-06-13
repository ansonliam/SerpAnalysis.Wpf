using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
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
            var Services = ConfigureServices();
            Ioc.Default.ConfigureServices(Services);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var w = new MainWindow();
            w.Show();
            base.OnStartup(e);
        }


        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<IGoogleCrawler, GoogleCrawler>();
            //services.AddTransient<ICrawlerIntegrationService>(provider => Test.Mock.MockCrawlerIntegrationService.GetMockCrawlerIntegrationService());
            services.AddTransient<ICrawlerIntegrationService, CrawlerIntegrationService>();
            services.AddTransient<MainViewModel, MainViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
