using NAudio.Wave;
using VirtualNvhAnalyzer.Core.Models;

namespace VirtualNvhAnalyzer.Services.Audio.Strategies
{
    public class WavProcessingStrategy : IAudioProcessingStrategy
    {
        public bool CanProcess(string filePath) => File.Exists(filePath) && filePath.EndsWith(".wav", StringComparison.OrdinalIgnoreCase);

        public async Task<AudioFileInfo> ProcessAsync(string input)
        {
            return await Task.Run(() =>
            {
                using var reader = new WaveFileReader(input);
                var waveFormat = reader.WaveFormat;
               
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
