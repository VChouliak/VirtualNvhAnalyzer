
using VirtualNvhAnalyzer.App.ViewModels;

namespace VirtualNvhAnalyzer.App.Commands
{
    public class ImportAudioCommand : AsyncCommand
    {
        public ImportAudioCommand(AudioImportViewModel viewModel)
            : base(() => Task.FromResult(()=>viewModel.ImportAudio()), null, "LoadAudio")
        {
        }
    }
}
