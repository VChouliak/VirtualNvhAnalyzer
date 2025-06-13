using VirtualNvhAnalyzer.App.Commands;
using VirtualNvhAnalyzer.App.ViewModels;

public class ImportAudioCommand : RelayCommand
{
    public ImportAudioCommand(AudioImportViewModel viewModel)
        : base(() => viewModel.ImportAudio(), null, "LoadAudio")
    {
    }
}
