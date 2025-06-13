using VirtualNvhAnalyzer.Infrastructure.Configuration.Loaders;
using VirtualNvhAnalyzer.Infrastructure.Configuration.ViewModels;
using Xunit;

namespace VirtualNvhAnalyzer.Infrastructure.Tests
{
    public class ViewModelSettingsLoaderTests
    {
        [Fact]
        public void Should_Load_ViewModelConfigs_Correctly()
        {
            // Arrange
            var loader = new ViewModelSettingsLoader();
            string path = "Configuration/Files/viewmodels.json";

            // Act
            var configs = loader.Load(path);

            // Assert
            Assert.NotNull(configs);
            Assert.NotEmpty(configs);

            // Beispielhafte Prüfung
            var audioImport = configs.FirstOrDefault(c => c.Key == "AudioImport");
            Assert.NotNull(audioImport);
            Assert.Equal("AudioImportViewModel", audioImport.ViewModel);
            Assert.Contains("ImportAudioCommand", audioImport.Commands);
        }
    }
}
