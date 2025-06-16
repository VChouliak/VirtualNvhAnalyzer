using VirtualNvhAnalyzer.App.Services.Mediator;
using VirtualNvhAnalyzer.App.Services.Mediator.Messages;
using VirtualNvhAnalyzer.Core.Common.Commands;
using VirtualNvhAnalyzer.Core.Interfaces.Audio.Services;
using VirtualNvhAnalyzer.Core.Interfaces.Audio.Strategies;
using VirtualNvhAnalyzer.Infrastructure.Configuration.ViewModels;

namespace VirtualNvhAnalyzer.App.ViewModels
{
    public class AudioFileProcessingViewModel : BaseViewModel
    {
        private IMediator _mediator;
        private readonly IAudioProcessingService _audioProcessingService;
        private IAudioProcessingStrategy _selectedStrategy;

        public AudioFileProcessingViewModel(Dictionary<string, Func<BaseViewModel>> viewModels, Dictionary<string, Func<INamedCommand>> commands, List<ViewModelConfig> configs, IMediator mediator, IAudioProcessingService audioProcessingService) : base(viewModels, commands, configs)
        {
            _mediator = mediator;
            _mediator.Subscribe<AudioImportedMessage>((Message) => Path = Message.FileName);
            _audioProcessingService = audioProcessingService;

        }
      
        private string _path;

        public string Path
        {
            get => _path;
            set
            {
                if (_path != value)
                {
                    _path = value;
                    OnPropertyChanged();
                    _selectedStrategy = null; // Reset, wenn neue Datei gewählt wird
                }
            }
        }

        public async Task EnsureStrategyInitializedAsync()
        {
            if (_selectedStrategy == null && !string.IsNullOrEmpty(Path))
            {
                _selectedStrategy = await _audioProcessingService.ProcessAsync(Path);
            }
        }


        public async Task PlayAudioAsync()
        {
            await EnsureStrategyInitializedAsync();
            if (_selectedStrategy != null)
                await _selectedStrategy.PlayAsync();
        }

        public async Task PauseAudioAsync()
        {
            await EnsureStrategyInitializedAsync();
            if (_selectedStrategy != null)
                await _selectedStrategy.PauseAsync();
        }

        public async Task StopAudioAsync()
        {
            await EnsureStrategyInitializedAsync();
            if (_selectedStrategy != null)
                await _selectedStrategy.StopAsync();
        }
    }
}
