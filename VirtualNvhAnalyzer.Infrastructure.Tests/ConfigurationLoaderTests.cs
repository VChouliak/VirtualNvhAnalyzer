namespace VirtualNvhAnalyzer.Infrastructure.Tests
{
    public class ConfigurationLoaderTests
    {
        [Fact]
        public void LoadAudioSettings_ShouldReturnValidAudioSettings()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(),
                "ConfigFiles",
                "audioSettings.json");
        
            var settings = ConfigurationLoader.LoadAudioSettings(path);

            Assert.NotNull(settings);
            Assert.NotEmpty(settings.SupportedFormats);
            Assert.Contains(".wav", settings.SupportedFormats);
        }

    }
}
