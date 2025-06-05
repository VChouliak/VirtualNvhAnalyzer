namespace VirtualNvhAnalyzer.App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(AudioImportContainerViewModel viewModel)
        {
            AudioImportContainerViewModel = viewModel;
        }

        public AudioImportContainerViewModel AudioImportContainerViewModel { get; }
    }
}
