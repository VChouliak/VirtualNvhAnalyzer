using System.IO;
using VirtualNvhAnalyzer.App.Services.Mediator;
using VirtualNvhAnalyzer.App.Services.Mediator.Messages;
using VirtualNvhAnalyzer.Core.Common.Commands;
using VirtualNvhAnalyzer.Core.Interfaces.Audio.Services;
using VirtualNvhAnalyzer.Core.Interfaces.Audio.Strategies;
using VirtualNvhAnalyzer.Core.Models;
using VirtualNvhAnalyzer.Infrastructure.Configuration.ViewModels;
using VirtualNvhAnalyzer.Services.Audio.Strategies;

namespace VirtualNvhAnalyzer.App.ViewModels
{
    public class AudioFileProcessingViewModel : BaseViewModel
    {
        private IMediator _mediator;       
        private IAudioProcessingStrategy? _selectedStrategy;

        public AudioFileProcessingViewModel(Dictionary<string, Func<BaseViewModel>> viewModels, Dictionary<string, Func<INamedCommand>> commands, List<ViewModelConfig> configs, IMediator mediator) : base(viewModels, commands, configs)
        {
            _mediator = mediator;
            _mediator.Subscribe<AudioProcessingStrategySelectedMessage>((Message) => { _selectedStrategy = Message.SelectedStrategy; AudioFileInfo = ((BaseAudioProcessingStrategyAsync)_selectedStrategy).AudioFileInfo; FileDisplayName = Path.GetFileName(((BaseAudioProcessingStrategyAsync)_selectedStrategy).AudioFileInfo.FileName);});           

        }

        private AudioFileInfo? _audioFileInfo;

        public AudioFileInfo? AudioFileInfo
        {
            get { return _audioFileInfo; }
            set
            {
                _audioFileInfo = value;
                OnPropertyChanged();
            }
        }

        private string? _fileDisplayName;

        public string? FileDisplayName
        {
            get { return _fileDisplayName; }
            set
            {
                _fileDisplayName = value;
                OnPropertyChanged();
            }
        }



        public async Task PlayAudioAsync()
        {            
            if (_selectedStrategy != null)
                await _selectedStrategy.PlayAsync();
        }

        public async Task PauseAudioAsync()
        {            
            if (_selectedStrategy != null)
                await _selectedStrategy.PauseAsync();
        }

        public async Task StopAudioAsync()
        {           
            if (_selectedStrategy != null)
                await _selectedStrategy.StopAsync();
        }
    }
}
