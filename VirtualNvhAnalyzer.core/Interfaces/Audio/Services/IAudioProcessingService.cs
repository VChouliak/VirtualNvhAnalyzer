using VirtualNvhAnalyzer.Core.Interfaces.Audio.Strategies;

namespace VirtualNvhAnalyzer.Core.Interfaces.Audio.Services
{
    public interface IAudioProcessingService : IProcessingServiceAsync<string, IAudioProcessingStrategy>
    {
    }
}
