using System.Windows.Forms;
using System.Windows.Input;
using VirtualNvhAnalyzer.Core.Common.Commands;
using VirtualNvhAnalyzer.Infrastructure.Configuration.ViewModels;

namespace VirtualNvhAnalyzer.App.ViewModels
{
    public class AudioImportViewModel : BaseViewModel
    {
        private string? _selectedFileName;

        public AudioImportViewModel(Dictionary<string, Func<BaseViewModel>> viewModels, Dictionary<string, Func<INamedCommand>> commands, List<ViewModelConfig> configs) 
            : base(viewModels, commands, configs)
        {
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


        public void ImportAudio()
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
