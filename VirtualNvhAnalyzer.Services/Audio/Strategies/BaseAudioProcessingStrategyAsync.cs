using System.Numerics;
using MathNet.Numerics.IntegralTransforms;
using NAudio.Wave;
using VirtualNvhAnalyzer.Core.Common.Options;
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

        public async Task<IEnumerable<float>> ToPulseCodeModulationAsync()
        {
            if (_audioFileReader == null)
                throw new InvalidOperationException("Audio file not loaded.");

            var options = _audioSettings.ProcessingOptions;


            return await Task.Run(() =>
            {
                var sampleProvider = _audioFileReader.ToSampleProvider();

                List<float> samples = new List<float>();

                float[] buffer = AudioProcessingCalculations.CreatePulseCodeModulationBuffer(AudioFileInfo, options);
                int read;

                do
                {
                    read = sampleProvider.Read(buffer, 0, buffer.Length);
                    for (int i = 0; i < read; i++)
                    {
                        samples.Add(buffer[i]);
                    }
                } while (read > 0);

                return (IEnumerable<float>)samples;
            });
        }

        public async Task<IEnumerable<Complex>> ToFastFourierTransformationAsync()
        {
            if (_audioFileReader == null)
                throw new InvalidOperationException("Audio file not loaded.");

            var pcmSamples = await ToPulseCodeModulationAsync();

            var options = _audioSettings.ProcessingOptions;
            int fftSize = options.FftSize > 0 
                ? options.FftSize :
                AudioProcessingCalculations.CalculateFftSize(
                    AudioFileInfo.SampleRate, 
                    options.FftSizeFractionOfSampleRate);

            float[] samplesForFft = new float[fftSize];
            var pcmArray = pcmSamples as float[] ?? pcmSamples.ToArray();
            Array.Copy(pcmArray, samplesForFft, Math.Min(pcmArray.Length, fftSize));

            samplesForFft = AudioProcessingCalculations.ApplyHanningWindow(samplesForFft);

            Complex[] fftBuffer = samplesForFft.Select(sample => new Complex(sample, 0)).ToArray();

            return await Task.Run(() =>
            {
                Fourier.Forward(fftBuffer, FourierOptions.Matlab);
                return (IEnumerable<Complex>)fftBuffer;
            });
        }
    }
}
