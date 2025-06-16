using VirtualNvhAnalyzer.Core.Interfaces.Audio.Strategies;

namespace VirtualNvhAnalyzer.App.Services.Mediator.Messages
{
    public class AudioProcessingStrategySelectedMessage
    {       

        public AudioProcessingStrategySelectedMessage(IAudioProcessingStrategy selectdStrategy)
        {
            _selectedStrategy = selectdStrategy;
        }

        private IAudioProcessingStrategy _selectedStrategy;

        public IAudioProcessingStrategy SelectedStrategy
        {
            get { return _selectedStrategy; }
            set { _selectedStrategy = value; }
        }

    }
}
