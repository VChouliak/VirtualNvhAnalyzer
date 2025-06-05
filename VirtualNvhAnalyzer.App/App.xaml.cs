using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VirtualNvhAnalyzer.App.ViewModels;
using VirtualNvhAnalyzer.Core.Interfaces.Audio.Services;
using VirtualNvhAnalyzer.Services.Audio.Processing;
using VirtualNvhAnalyzer.Services.Audio.Strategies;

namespace VirtualNvhAnalyzer.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IAudioProcessingService, AudioProcessingService>();
                    services.AddSingleton<IAudioProcessingStrategy, WavProcessingStrategy>();
                    services.AddSingleton<IAudioProcessingStrategy, Mp3ProcessingStrategy>();

                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<AudioImportContainerViewModel>();
                    services.AddSingleton<AudioImportViewModel>();                   
                })
                .Build();
        }

        private async void App_Startup(object sender, StartupEventArgs e)
        {
            await AppHost.StartAsync();

            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            base.OnExit(e);
        }
    }

}
