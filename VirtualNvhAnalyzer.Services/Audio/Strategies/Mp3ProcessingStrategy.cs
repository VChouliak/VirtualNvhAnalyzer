using NAudio.Wave;
using VirtualNvhAnalyzer.Core.Models;
using VirtualNvhAnalyzer.Infrastructure.Configuration;

namespace VirtualNvhAnalyzer.Services.Audio.Strategies
{
    public class Mp3ProcessingStrategy : BaseAudioProcessingStrategyAsync
    {
        public Mp3ProcessingStrategy(AudioSettings audioSettings) : base(audioSettings)
        {
        }
        //TODO: adjust logic
        public override bool CanProcess(string filePath) => File.Exists(filePath) && filePath.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase);        
    }
}
