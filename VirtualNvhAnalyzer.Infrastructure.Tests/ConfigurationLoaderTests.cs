using VirtualNvhAnalyzer.Infrastructure.Configuration.Loaders;

namespace VirtualNvhAnalyzer.Infrastructure.Tests
{
    public class ConfigurationLoaderTests
    {
        [Fact]
        public void LoadAudioSettings_ShouldReturnValidAudioSettings()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(),
                "Configuration",
                "Files",
                "audioSettings.json");

            var audioSettingsLoader = new AudioSettingsLoader();

            var settings = audioSettingsLoader.Load(path);

            Assert.NotNull(settings);
            Assert.NotEmpty(settings.SupportedFormats);
            Assert.Contains(".wav", settings.SupportedFormats);
        }

    }
}
