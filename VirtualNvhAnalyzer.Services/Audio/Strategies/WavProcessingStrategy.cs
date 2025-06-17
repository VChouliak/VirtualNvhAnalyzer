using NAudio.Wave;
using VirtualNvhAnalyzer.Core.Models;
using VirtualNvhAnalyzer.Infrastructure.Configuration;

namespace VirtualNvhAnalyzer.Services.Audio.Strategies
{
    public class WavProcessingStrategy : BaseAudioProcessingStrategyAsync
    {
        public WavProcessingStrategy(AudioSettings audioSettings) : base(audioSettings)
        {
        }
        //TODO: adjust logic

        public override bool CanProcess(string filePath) => File.Exists(filePath) && filePath.EndsWith(".wav", StringComparison.OrdinalIgnoreCase);     
    }
}
