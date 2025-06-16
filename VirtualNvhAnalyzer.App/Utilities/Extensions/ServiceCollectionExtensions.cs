using Microsoft.Extensions.DependencyInjection;
using VirtualNvhAnalyzer.App.ViewModels;
using VirtualNvhAnalyzer.Core.Common.Commands;

namespace VirtualNvhAnalyzer.App.Utilities.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterViewModelsAndCommands(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var vmType in assemblies.SelectMany(a => a.GetTypes())
                .Where(t => typeof(BaseViewModel).IsAssignableFrom(t) && !t.IsAbstract && !t.IsGenericType))
            {
                services.AddSingleton(vmType);
            }

            foreach (var cmdType in assemblies.SelectMany(a => a.GetTypes())
                .Where(t => typeof(INamedCommand).IsAssignableFrom(t) && !t.IsAbstract && !t.IsGenericType))
            {
                services.AddSingleton(typeof(INamedCommand), cmdType);
                services.AddSingleton(cmdType);
            }
        }
    }
}
