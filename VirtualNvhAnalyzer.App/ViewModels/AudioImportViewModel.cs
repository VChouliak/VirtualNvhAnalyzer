using System.Windows.Forms;
using System.Windows.Input;
using VirtualNvhAnalyzer.App.Services.Mediator;
using VirtualNvhAnalyzer.App.Services.Mediator.Messages;
using VirtualNvhAnalyzer.Core.Common.Commands;
using VirtualNvhAnalyzer.Core.Interfaces.Audio.Services;
using VirtualNvhAnalyzer.Core.Interfaces.Audio.Strategies;
using VirtualNvhAnalyzer.Infrastructure.Configuration.ViewModels;

namespace VirtualNvhAnalyzer.App.ViewModels
{
    public class AudioImportViewModel : BaseViewModel
    {
        private readonly IAudioProcessingService _audioProcessingService;
        private IAudioProcessingStrategy? _selectedStrategy;

        private string? _selectedFileName;
        private IMediator _mediator;

        public AudioImportViewModel(Dictionary<string, Func<BaseViewModel>> viewModels, Dictionary<string, Func<INamedCommand>> commands, List<ViewModelConfig> configs, IMediator mediator, IAudioProcessingService audioProcessingService)
            : base(viewModels, commands, configs)
        {
            _mediator = mediator;
            _audioProcessingService = audioProcessingService;
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

        public async Task ImportAudioAsync()
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
                _selectedStrategy = await _audioProcessingService.ProcessAsync(SelectedFileName);
                if (_selectedStrategy == null)
                {
                    MessageBox.Show("Unsupported audio format or processing error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _mediator.Publish(new AudioProcessingStrategySelectedMessage(_selectedStrategy));
            }           
        }

    }
}
