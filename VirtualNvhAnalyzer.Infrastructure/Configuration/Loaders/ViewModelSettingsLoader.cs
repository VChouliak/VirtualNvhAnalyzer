using System.Text.Json;
using VirtualNvhAnalyzer.Infrastructure.Configuration.ViewModels;
using VirtualNvhAnalyzer.Infrastructure.Converters;

namespace VirtualNvhAnalyzer.Infrastructure.Configuration.Loaders
{
    public class ViewModelSettingsLoader : JsonSettingsLoader<List<ViewModelConfig>>
    {
        public ViewModelSettingsLoader() : base(new JsonSerializerOptions
        {
            Converters = { new ViewModelConfigConverter() },
            PropertyNameCaseInsensitive = true,
        }){}
    }
}
