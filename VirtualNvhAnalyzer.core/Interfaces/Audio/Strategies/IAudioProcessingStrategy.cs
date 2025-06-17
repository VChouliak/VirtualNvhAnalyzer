using VirtualNvhAnalyzer.Core.Interfaces.Audio.Services;

namespace VirtualNvhAnalyzer.Core.Interfaces.Audio.Strategies
{
    public interface IAudioProcessingStrategy : IMediaPlayerAsync, IProcessingServiceAsync<string>
    {
        bool CanProcess(string filePath);
    }
}
