using VirtualNvhAnalyzer.App.ViewModels;

namespace VirtualNvhAnalyzer.App.Commands
{
    public class AudioFilePlayCommand : AsyncCommand
    {
        public AudioFilePlayCommand(AudioFileProcessingViewModel viewModel) : base(() => viewModel.PlayAudioAsync(), null, "AudioFilePlay")
        {           
        }

    }
}
