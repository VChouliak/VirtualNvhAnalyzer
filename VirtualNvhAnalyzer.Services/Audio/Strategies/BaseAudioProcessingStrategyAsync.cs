using NAudio.Wave;
using VirtualNvhAnalyzer.Core.Interfaces.Audio.Strategies;
using VirtualNvhAnalyzer.Core.Models;
using VirtualNvhAnalyzer.Infrastructure.Configuration;

namespace VirtualNvhAnalyzer.Services.Audio.Strategies
{
    public abstract class BaseAudioProcessingStrategyAsync : IAudioProcessingStrategy
    {
        protected IWavePlayer? _wavePlayer;
        protected AudioFileReader? _audioFileReader;
        protected AudioSettings _audioSettings;

        protected BaseAudioProcessingStrategyAsync(AudioSettings audioSettings)
        {
            _audioSettings = audioSettings;
        }

        public virtual bool CanProcess(string filePath) => File.Exists(filePath) && _audioSettings.SupportedFormats
            .Contains(Path.GetExtension(filePath));

        public virtual async Task ProcessAsync(string input)
        {
            if (!CanProcess(input))
                throw new NotSupportedException($"File format not supported: {input}");

            var reader = await Task.Run(() => new AudioFileReader(input));

            _wavePlayer?.Stop();
            _audioFileReader?.Dispose();
            _wavePlayer?.Dispose();

            _wavePlayer = new WaveOutEvent();
            _wavePlayer.Init(reader);
            _audioFileReader = reader;
        }

        public AudioFileInfo AudioFileInfo
        {
            get
            {
                if (_audioFileReader == null)
                {
                    throw new InvalidOperationException("Audio file reader is not initialized.");
                }
                return new AudioFileInfo(
                    fileName: _audioFileReader.FileName,
                    sampleRate: _audioFileReader.WaveFormat.SampleRate,
                    channels: _audioFileReader.WaveFormat.Channels,
                    duration: _audioFileReader.TotalTime
                );
            }
        }

        public virtual Task PauseAsync()
        {
            _wavePlayer?.Pause();
            return Task.CompletedTask;
        }
        public virtual Task PlayAsync()
        {
            _wavePlayer?.Play();
            return Task.CompletedTask;
        }
        public virtual Task StopAsync()
        {
            if (_wavePlayer != null)
            {
                _wavePlayer.Stop();
            }

            if (_audioFileReader != null)
            {
                _audioFileReader.Position = 0;
            }

            return Task.CompletedTask;
        }
    }
}
