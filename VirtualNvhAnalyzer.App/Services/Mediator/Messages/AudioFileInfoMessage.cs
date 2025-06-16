using VirtualNvhAnalyzer.Core.Models;

namespace VirtualNvhAnalyzer.App.Services.Mediator.Messages
{
    public class AudioFileInfoMessage
    {
        public AudioFileInfoMessage(AudioFileInfo info)
        {
            _audioFileInfo = info;
        }

        private AudioFileInfo _audioFileInfo;

        public AudioFileInfo AudioFileInfo
        {
            get { return _audioFileInfo; }
            set { _audioFileInfo = value; }
        }

    }
}
