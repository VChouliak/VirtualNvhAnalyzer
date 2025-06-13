using Microsoft.Extensions.DependencyInjection;
using VirtualNvhAnalyzer.App.ViewModels;
using VirtualNvhAnalyzer.Core.Common.Commands;
using VirtualNvhAnalyzer.Infrastructure.Configuration.ViewModels;

namespace VirtualNvhAnalyzer.App.Utilities
{
    public class ViewModelAndCommandFactoryBuilder
    {
        public static (Dictionary<string, Func<BaseViewModel>>, Dictionary<string, Func<INamedCommand>>) BuildViewModelsAndCommands(IServiceProvider provider, IEnumerable<ViewModelConfig> viewModelConfigs)
        {
            var viewModelDict = new Dictionary<string, Func<BaseViewModel>>();
            var commandDict = new Dictionary<string, Func<INamedCommand>>();

            foreach (var config in viewModelConfigs)
            {
                var key = config.Key;
                var viewModelName = config.ViewModel;
                var viewModel = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.Name == viewModelName && typeof(BaseViewModel).IsAssignableFrom(t));

                if (viewModel != null)
                {
                    viewModelDict[key] = () => (BaseViewModel)provider.GetRequiredService(viewModel);
                }

                foreach (var commandName in config.Commands)
                {
                    var commandType = AppDomain.CurrentDomain
                        .GetAssemblies()
                        .SelectMany(a => a.GetTypes())
                        .FirstOrDefault(t => t.Name == commandName && typeof(INamedCommand).IsAssignableFrom(t));
                    if (commandType != null)
                    {
                        commandDict[commandName] = () => (INamedCommand)provider.GetRequiredService(commandType);
                    }
                }
            }

            return (viewModelDict, commandDict);
        }
    }
}
