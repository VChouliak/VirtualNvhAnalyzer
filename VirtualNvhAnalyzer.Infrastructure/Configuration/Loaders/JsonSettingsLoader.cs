using System.Text.Json;

namespace VirtualNvhAnalyzer.Infrastructure.Configuration.Loaders
{
    public abstract class JsonSettingsLoader<T> : ISettingsLoader<T>
    {
        private readonly JsonSerializerOptions _serializeOptions;

        protected JsonSettingsLoader(JsonSerializerOptions? serializeOptions = null)
        {
            _serializeOptions = serializeOptions ?? new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        public T Load(string path)
        {
            var infrastructurePath = Path.GetDirectoryName(typeof(JsonSettingsLoader<T>).Assembly.Location);
            var fullPath = Path.Combine(infrastructurePath!, path);

            var json = File.ReadAllText(fullPath);

            return JsonSerializer.Deserialize<T>(json, _serializeOptions)
                   ?? throw new InvalidOperationException($"Failed to deserialize JSON from {fullPath}");
        }
    }
}
