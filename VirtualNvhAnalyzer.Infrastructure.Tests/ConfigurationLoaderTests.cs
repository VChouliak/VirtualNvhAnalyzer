using VirtualNvhAnalyzer.Core.Common.Options;
using VirtualNvhAnalyzer.Core.Models;
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

        [Fact]
        public void LoadAudioSettings_ShouldReturnValidProcessingOptions()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(),
                "Configuration",
                "Files",
                "audioSettings.json");

            var audioSettingsLoader = new AudioSettingsLoader();

            var settings = audioSettingsLoader.Load(path);

            Assert.NotNull(settings);
            Assert.NotNull(settings.ProcessingOptions);

            var options = settings.ProcessingOptions;

            // BufferDuration must be > 0
            Assert.True(options.BufferDurationMs > 0);
            Assert.True(options.BufferDuration.TotalMilliseconds > 0);

            // FFT size can be zero (implemented in AudioProcessingOptionFactory)
            Assert.True(options.FftSize >= 0);

            // FFT Fraction should be between 0 and 1
            Assert.InRange(options.FftSizeFractionOfSampleRate, 0.0, 1.0);
        }

        [Fact]
        public void AudioProcessingOptionsFactory_ShouldCalculateBufferAndFftSize()
        {
            // Example-FileInfo: 44.1 kHz Stereo, 10 Seconds
            var fileInfo = new AudioFileInfo("example.wav", 44100, 2, TimeSpan.FromSeconds(10));

            var options = new AudioProcessingOptions
            {
                BufferDurationMs = 20,
                FftSizeFractionOfSampleRate = 0.1
            };

            // Buffer
            var pcmBuffer = AudioProcessingCalculations.CreatePulseCodeModulationBuffer(fileInfo, options);
            Assert.NotNull(pcmBuffer);
            Assert.True(pcmBuffer.Length > 0);

            // FFT Size
            int fftSize = AudioProcessingCalculations.CalculateFftSize(fileInfo.SampleRate, options.FftSizeFractionOfSampleRate);
            Assert.True(fftSize > 0);
            // Muss Potenz von 2 sein
            Assert.True((fftSize & (fftSize - 1)) == 0);
        }

    }
}
