using VirtualNvhAnalyzer.Core.Models;
using VirtualNvhAnalyzer.Services.Audio.Strategies;

namespace VirtualNvhAnalyzer.Services.Tests.Audio.Strategies
{
    public class TestWavProcessingStrategy : IAudioProcessingStrategy
    {
        public bool CanProcess(string input) => input.EndsWith(".wav");

        public Task<AudioFileInfo> ProcessAsync(string input) =>
            Task.FromResult(new AudioFileInfo("test.wav", 44100, 2, TimeSpan.FromSeconds(120)));
    }
}
