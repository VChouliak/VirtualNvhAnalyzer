using VirtualNvhAnalyzer.Core.Common.Interfaces;
using VirtualNvhAnalyzer.Core.Models;

namespace VirtualNvhAnalyzer.Services.Audio.Strategies
{
    public interface IAudioProcessingStrategy : IProcessingService<string, AudioFileInfo>
    {
        bool CanProcess(string filePath);
    }
}
