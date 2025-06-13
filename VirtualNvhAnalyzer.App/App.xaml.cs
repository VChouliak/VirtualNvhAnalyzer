using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VirtualNvhAnalyzer.App.Utilities;
using VirtualNvhAnalyzer.App.Utilities.Extensions;
using VirtualNvhAnalyzer.Core.Interfaces.Audio.Services;
using VirtualNvhAnalyzer.Infrastructure.Configuration;
using VirtualNvhAnalyzer.Infrastructure.Configuration.Loaders;
using VirtualNvhAnalyzer.Infrastructure.Configuration.ViewModels;
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
                   
                    services.AddSingleton<ISettingsLoader<AudioSettings>, AudioSettingsLoader>();
                    services.AddSingleton<ISettingsLoader<List<ViewModelConfig>>, ViewModelSettingsLoader>();                    

                    services.AddSingleton(provider =>
                    {
                        var loader = provider.GetRequiredService<ISettingsLoader<List<ViewModelConfig>>>();
                        return loader.Load("Configuration/Files/viewmodels.json");
                    });                             
                   
                    services.AddSingleton(provider =>
                    {
                        var layoutConfigs = provider.GetRequiredService<List<ViewModelConfig>>();
                        return ViewModelAndCommandFactoryBuilder.BuildViewModelsAndCommands(provider, layoutConfigs).Item1; // ViewModels
                    });

                    services.AddSingleton(provider =>
                    {
                        var layoutConfigs = provider.GetRequiredService<List<ViewModelConfig>>();
                        return ViewModelAndCommandFactoryBuilder.BuildViewModelsAndCommands(provider, layoutConfigs).Item2; // Commands
                    });
                    services.AddSingleton<MainWindow>();
                    services.RegisterViewModelsAndCommands();
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
