using Microsoft.Extensions.Configuration;
using VirtualNvhAnalyzer.Infrastructure.Configuration;

namespace VirtualNvhAnalyzer.Infrastructure
{
    public static class ConfigurationLoader
    {
        private static string SupportedFormatsSection = "SupportedFormats";

        public static AudioSettings LoadAudioSettings(string jsonPath = "ConfigFiles/audioSettings.json")
        {            
            var infrastructurePath = Path.GetDirectoryName(typeof(ConfigurationLoader).Assembly.Location);

            var config = new ConfigurationBuilder()
                .SetBasePath(infrastructurePath!)
                .AddJsonFile(jsonPath, optional: false, reloadOnChange: true)
                .Build();

            var audioSettings = new AudioSettings();
            config.GetSection(SupportedFormatsSection).Bind(audioSettings.SupportedFormats);

            return audioSettings;
        }
    }
}
