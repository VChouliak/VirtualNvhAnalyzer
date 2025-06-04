using VirtualNvhAnalyzer.Core.Interfaces.Audio.Services;
using VirtualNvhAnalyzer.Core.Models;
using VirtualNvhAnalyzer.Services.Audio.Strategies;

namespace VirtualNvhAnalyzer.Services.Audio.Processing
{
    public class AudioProcessingService : IAudioProcessingService
    {
        private readonly IEnumerable<IAudioProcessingStrategy> _strategies;

        public AudioProcessingService(IEnumerable<IAudioProcessingStrategy> strategies)
        {
            _strategies = strategies;
        }

        public async Task<AudioFileInfo> ProcessAsync(string input)
        {
            var strategy = _strategies.FirstOrDefault(s => s.CanProcess(input));

            if (strategy == null)
            {
                throw new NotSupportedException($"No processing strategy found for file: {input}");
            }

            return await strategy.ProcessAsync(input);
        }
    }
}
