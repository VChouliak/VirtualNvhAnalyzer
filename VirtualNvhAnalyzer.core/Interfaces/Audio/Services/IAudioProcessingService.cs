using VirtualNvhAnalyzer.Core.Common.Interfaces;
using VirtualNvhAnalyzer.Core.Models;

namespace VirtualNvhAnalyzer.Core.Interfaces.Audio.Services
{
    public interface IAudioProcessingService : IProcessingService<string, AudioFileInfo>
    {
    }
}
