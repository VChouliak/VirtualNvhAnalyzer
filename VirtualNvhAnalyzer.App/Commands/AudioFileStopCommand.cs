
using VirtualNvhAnalyzer.App.ViewModels;

namespace VirtualNvhAnalyzer.App.Commands
{
    public class AudioFileStopCommand : AsyncCommand
    {
        public AudioFileStopCommand(AudioFileProcessingViewModel viewModel) : base(()=>viewModel.StopAudioAsync(), null, "AudioFileStop")
        {
        }
    }
}
