using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BLESniffer.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider AppContainer { get; private set; }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            CreateContainer();
        }

        private void CreateContainer()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<Service.ManufacturerDataService>();

            AppContainer = services.BuildServiceProvider();
        }

    }
}
