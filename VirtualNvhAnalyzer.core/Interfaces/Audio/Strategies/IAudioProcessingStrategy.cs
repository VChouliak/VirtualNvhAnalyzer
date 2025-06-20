using VirtualNvhAnalyzer.Core.Interfaces.Audio.Services;

namespace VirtualNvhAnalyzer.Core.Interfaces.Audio.Strategies
{
    public interface IAudioProcessingStrategy : IMediaPlayerAsync, IProcessingServiceAsync<string>, IPulseCodeModulationAsync, IFastFourierTransformationAsync
    {
        bool CanProcess(string filePath);
    }
}
