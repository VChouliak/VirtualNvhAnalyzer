using VirtualNvhAnalyzer.Core.Models;
using VirtualNvhAnalyzer.Infrastructure.Configuration;
using VirtualNvhAnalyzer.Services.Audio.Strategies;

namespace VirtualNvhAnalyzer.Services.Tests.Audio.Strategies
{
    public class TestWavProcessingStrategy : BaseAudioProcessingStrategyAsync
    {
        public TestWavProcessingStrategy(AudioSettings audioSettings) : base(audioSettings)
        {
        }

        public override bool CanProcess(string filePath) =>  _audioSettings.SupportedFormats
            .Contains(Path.GetExtension(filePath));      

        public override Task<AudioFileInfo> ProcessAsync(string input) =>
            Task.FromResult(new AudioFileInfo("test.wav", 44100, 2, TimeSpan.FromSeconds(120)));
    }
}
