namespace VirtualNvhAnalyzer.Core.Interfaces.Audio.Services
{
    public interface IMediaPlayerAsync
    {
        Task PlayAsync();
        Task PauseAsync();
        Task StopAsync();
    }
}
