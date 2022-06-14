using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using SerpAnalysis.Core.BusinessServices;
using SerpAnalysis.Core.Interfaces;
using SerpAnalysis.wpf.ViewModels;

namespace SerpAnalysis.Test.Mock
{
    public class MockIoc
    {
        public static void InitialiseIoc()
        {
            var serviceProvider = ConfigureServices();
            Ioc.Default.ConfigureServices(serviceProvider);
        }




        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<IGoogleCrawler, GoogleCrawler>();
            services.AddTransient<ICrawlerIntegrationService>(provider => Test.Mock.MockCrawlerIntegrationService.GetMockCrawlerIntegrationService());
            services.AddTransient<MainViewModel, MainViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
