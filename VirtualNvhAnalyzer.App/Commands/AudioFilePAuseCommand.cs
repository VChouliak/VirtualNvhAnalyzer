using VirtualNvhAnalyzer.App.ViewModels;

namespace VirtualNvhAnalyzer.App.Commands
{
    public class AudioFilePauseCommand : AsyncCommand
    {
        public AudioFilePauseCommand(AudioFileProcessingViewModel viewModel) : base(() => viewModel.PauseAudioAsync(), null, "AudioFilePause")
        {

        }
    }
}
