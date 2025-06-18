using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using VirtualNvhAnalyzer.Infrastructure.Configuration.ViewModels;

namespace VirtualNvhAnalyzer.Infrastructure.Converters
{
    public class ViewModelConfigConverter : JsonConverter<ViewModelConfig>
    {
        private static Dictionary<string, Type>? _viewModelConfigTypeMap;

        private static Dictionary<string, Type> GetVieModelSettingsTypeMap()
        {
            if (_viewModelConfigTypeMap != null)
                return _viewModelConfigTypeMap;

            _viewModelConfigTypeMap = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(t => typeof(ViewModelConfig).IsAssignableFrom(t) && !t.IsAbstract)               
                .ToDictionary(
                    t => t.Name,
                    t => t
                );

            return _viewModelConfigTypeMap;
        }
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(ViewModelConfig).IsAssignableFrom(typeToConvert);
        }

        public override ViewModelConfig? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var vieModel = doc.RootElement.GetProperty("ViewModel").GetString();

            var typeMap = GetVieModelSettingsTypeMap();
            var safeOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true                
            };
            if (vieModel != null && typeMap.TryGetValue(vieModel, out var configType))
            {

                return (ViewModelConfig?)JsonSerializer.Deserialize(doc.RootElement.GetRawText(), configType, safeOptions);
            }

            return JsonSerializer.Deserialize<ViewModelConfig>(doc.RootElement.GetRawText(), safeOptions);
        }

        public override void Write(Utf8JsonWriter writer, ViewModelConfig value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
        }
    }
}
