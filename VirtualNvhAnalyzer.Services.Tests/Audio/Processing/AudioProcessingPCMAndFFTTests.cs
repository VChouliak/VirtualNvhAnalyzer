using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VirtualNvhAnalyzer.Core.Interfaces.Audio.Services;
using VirtualNvhAnalyzer.Core.Interfaces.Audio.Strategies;
using VirtualNvhAnalyzer.Infrastructure.Configuration;
using VirtualNvhAnalyzer.Infrastructure.Configuration.Loaders;
using VirtualNvhAnalyzer.Services.Audio.Processing;
using VirtualNvhAnalyzer.Services.Audio.Strategies;

namespace VirtualNvhAnalyzer.Services.Tests.Audio.Processing
{
    public class AudioProcessingPCMAndFFTTests
    {
        private readonly string settingsPath = Path.Combine(Directory.GetCurrentDirectory(),
                                               "Configuration",
                                               "Files",
                                               "audioSettings.json");

        private readonly string audioPath = Path.Combine(Directory.GetCurrentDirectory(),
                                            "Audio",
                                            "Files",
                                            "vibration.mp3");
        private readonly IHost _host;
        public AudioProcessingPCMAndFFTTests()
        {
            _host = Host.CreateDefaultBuilder()
               .ConfigureServices((context, services) =>
               {

                   // Einstellungen laden (du kannst sie fix eintragen oder MockLoader verwenden)
                   services.AddSingleton<ISettingsLoader<AudioSettings>, AudioSettingsLoader>();

                   services.AddSingleton<IAudioProcessingService, AudioProcessingService>();
                   services.AddSingleton<IAudioProcessingStrategy, WavProcessingStrategy>();
                   services.AddSingleton<IAudioProcessingStrategy, Mp3ProcessingStrategy>();

                   services.AddSingleton(provider =>
                   {
                       var loader = provider.GetRequiredService<ISettingsLoader<AudioSettings>>();
                       return loader.Load(settingsPath);
                   });
               })
               .Build();
        }

        [Fact]
        public async Task ToPulseCodeModulationAsync_WithRealFile_Works()
        {
            await _host.StartAsync();

            // Arrange
            var audioService = _host.Services.GetRequiredService<IAudioProcessingService>();           

            Assert.True(File.Exists(audioPath), $"Test-Datei nicht gefunden: {audioPath}");
            
            var strategy = await audioService.ProcessAsync(audioPath);

            // Act
            var samples = await strategy.ToPulseCodeModulationAsync();

            // Assert
            Assert.NotEmpty(samples);
            Assert.All(samples, s => Assert.InRange(s, -1.0f, 1.0f));

            Console.WriteLine($"Samples count: {samples.Count()}");
            await _host.StopAsync();
        }

        [Fact]
        public async Task ToFastFourierTransformationAsync_WithRealFile_Works()
        {         
            await _host.StartAsync();

            var processingService = _host.Services.GetRequiredService<IAudioProcessingService>();
            

            // Act
            var strategy = await processingService.ProcessAsync(audioPath);

            var fftResult = await strategy.ToFastFourierTransformationAsync();

            // Assert
            Assert.NotNull(fftResult);
            var fftArray = fftResult.ToArray();
            Assert.NotEmpty(fftArray);
            Assert.All(fftArray, complex =>
            {
                Assert.False(double.IsNaN(complex.Real));
                Assert.False(double.IsNaN(complex.Imaginary));
            });

            // Clean up
            await _host.StopAsync();
        }

    }
}

