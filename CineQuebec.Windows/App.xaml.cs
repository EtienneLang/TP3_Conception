using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CineQuebec.Windows
{
    
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Configure services
            var services = new ServiceCollection();
            services.AjoutDesServices(); // Ensure this method exists and adds services correctly

            // Build the service provider
            _serviceProvider = services.BuildServiceProvider();

            // Resolve your main window or other components
            var mainWindow = _serviceProvider.GetService<MainWindow>();
        }

    }
}