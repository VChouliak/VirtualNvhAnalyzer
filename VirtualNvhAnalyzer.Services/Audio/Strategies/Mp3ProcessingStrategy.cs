using NAudio.Wave;
using VirtualNvhAnalyzer.Core.Models;

namespace VirtualNvhAnalyzer.Services.Audio.Strategies
{
    internal class Mp3ProcessingStrategy : IAudioProcessingStrategy
    {
        public bool CanProcess(string filePath) => File.Exists(filePath) && filePath.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase);

        public async Task<AudioFileInfo> ProcessAsync(string input)
        {
            return await Task.Run(() =>
            {
                using var reader = new Mp3FileReader(input);
                var waveFormat = reader.Mp3WaveFormat;

                return new AudioFileInfo(
                    fileName: Path.GetFileName(input),
                    sampleRate: waveFormat.SampleRate,
                    channels: waveFormat.Channels,
                    duration: reader.TotalTime
                );
            });
        }
    }
}
