using Microsoft.Extensions.DependencyInjection;
using VirtualNvhAnalyzer.App.Services.Mediator;
using VirtualNvhAnalyzer.App.Utilities;
using VirtualNvhAnalyzer.App.Utilities.Extensions;
using VirtualNvhAnalyzer.App.ViewModels;
using VirtualNvhAnalyzer.Core.Common.Commands;
using VirtualNvhAnalyzer.Infrastructure.Configuration.Loaders;
using VirtualNvhAnalyzer.Infrastructure.Configuration.ViewModels;

namespace VirtualNvhAnalyzer.App.Tests.Utitilities
{
    public class ViewModelAndCommandFactoryBuilderTests
    {
        [Fact]
        public void Should_Build_Valid_ViewModels_And_Commands_From_Configs()
        {
            // Arrange
            var services = new ServiceCollection();


            services.AddSingleton(new Dictionary<string, Func<BaseViewModel>>());
            services.AddSingleton(new Dictionary<string, Func<INamedCommand>>());
            services.AddSingleton(new List<ViewModelConfig>());  
            services.AddSingleton<IMediator, Mediator>();


            services.AddSingleton<ISettingsLoader<List<ViewModelConfig>>, ViewModelSettingsLoader>();          

            services.AddSingleton(provider =>
            {
                var loader = provider.GetRequiredService<ISettingsLoader<List<ViewModelConfig>>>();
                return loader.Load("Configuration/Files/viewmodels.json");
            });
           
            services.RegisterViewModelsAndCommands();

            var provider = services.BuildServiceProvider();

            var configs = provider.GetRequiredService<List<ViewModelConfig>>();


            // Act
            var (viewModels, commands) = ViewModelAndCommandFactoryBuilder.BuildViewModelsAndCommands(provider, configs);

            // Assert
            Assert.True(viewModels.ContainsKey("AudioImport"));
            Assert.True(commands.ContainsKey(nameof(ImportAudioAsyncCommand).Replace("Command","")));
            

            var vmInstance = viewModels["AudioImport"]();
            var importAudioCmdInstance = commands[nameof(ImportAudioAsyncCommand).Replace("Command", "")]();
            

            Assert.NotNull(vmInstance);
            Assert.NotNull(importAudioCmdInstance);
               

            Assert.IsType<AudioImportViewModel>(vmInstance);
            Assert.IsType<ImportAudioAsyncCommand>(importAudioCmdInstance);
            
        }
       
    }
}
