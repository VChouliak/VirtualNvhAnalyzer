using VirtualNvhAnalyzer.Core.Models;

namespace VirtualNvhAnalyzer.Core.Interfaces.Audio
{
    public interface IAudioFileHandler : IAudioHandler<string, AudioFileInfo>
    {
        bool IsSupportedFormat(String filePath);
    }
}
