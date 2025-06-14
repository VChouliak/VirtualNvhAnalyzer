using VirtualNvhAnalyzer.App.Commands;
using VirtualNvhAnalyzer.App.ViewModels;

public class ImportAudioAsyncCommand : AsyncCommand
{
    public ImportAudioAsyncCommand(AudioImportViewModel viewModel)
        : base(() => viewModel.ImportAudioAsync(), null, "ImportAudioAsync")
    {
    }
}
