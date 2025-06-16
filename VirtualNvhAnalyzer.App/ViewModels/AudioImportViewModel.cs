using System.Windows.Forms;
using System.Windows.Input;
using VirtualNvhAnalyzer.App.Services.Mediator;
using VirtualNvhAnalyzer.App.Services.Mediator.Messages;
using VirtualNvhAnalyzer.Core.Common.Commands;
using VirtualNvhAnalyzer.Infrastructure.Configuration.ViewModels;

namespace VirtualNvhAnalyzer.App.ViewModels
{
    public class AudioImportViewModel : BaseViewModel
    {
        private string? _selectedFileName="Test";
        private IMediator _mediator;

        public AudioImportViewModel(Dictionary<string, Func<BaseViewModel>> viewModels, Dictionary<string, Func<INamedCommand>> commands, List<ViewModelConfig> configs, IMediator mediator)
            : base(viewModels, commands, configs)
        {
            _mediator = mediator;
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

        public Task ImportAudioAsync()
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
                _mediator.Publish(new AudioImportedMessage(SelectedFileName));
            }
            return Task.CompletedTask;
        }

    }
}
