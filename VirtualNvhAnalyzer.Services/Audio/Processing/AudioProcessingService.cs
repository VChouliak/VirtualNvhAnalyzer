using VirtualNvhAnalyzer.Core.Interfaces.Audio.Services;
using VirtualNvhAnalyzer.Core.Interfaces.Audio.Strategies;

namespace VirtualNvhAnalyzer.Services.Audio.Processing
{
    public class AudioProcessingService : IAudioProcessingService
    {
        private readonly IEnumerable<IAudioProcessingStrategy> _strategies;
        private IAudioProcessingStrategy? _selectedStrategy;

        public AudioProcessingService(IEnumerable<IAudioProcessingStrategy> strategies)
        {
            _strategies = strategies;
        }

        public async Task<IAudioProcessingStrategy> ProcessAsync(string input)
        {
            _selectedStrategy = _strategies.FirstOrDefault(s => s.CanProcess(input));

            if (_selectedStrategy == null)
            {
                throw new NotSupportedException($"No processing strategy found for file: {input}");
            }

            await _selectedStrategy.ProcessAsync(input);

            return _selectedStrategy;
        }               
    }
}
