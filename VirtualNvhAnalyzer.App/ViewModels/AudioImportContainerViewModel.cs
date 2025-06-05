namespace VirtualNvhAnalyzer.App.ViewModels
{
    public class AudioImportContainerViewModel : BaseViewModel
    {       

        public AudioImportContainerViewModel(AudioImportViewModel audioImportViewModel)
        {
            AudioImportViewModel = audioImportViewModel;
        }

        public AudioImportViewModel AudioImportViewModel { get; }
    }
}
