using System.Windows.Forms;
using System.Windows.Input;
using VirtualNvhAnalyzer.App.Commands;

namespace VirtualNvhAnalyzer.App.ViewModels
{
    public class AudioImportViewModel : BaseViewModel
    {
        private string? _selectedFileName;

        public AudioImportViewModel()
        {
            ImportAudioCommand = new RelayCommand(ImportAudio);
        }

        public string? SelectedFileName
        {
            get => _selectedFileName;
            set
            {
                _selectedFileName = value;
                OnPropertyChanged();
            }
        }

        public ICommand ImportAudioCommand { get; }


        private void ImportAudio()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select Audio File",
                Filter = "Audio Files|*.wav;*.mp3;*.flac;*.aac;*.ogg",
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedFileName = openFileDialog.FileName;
            }
        }

    }
}
