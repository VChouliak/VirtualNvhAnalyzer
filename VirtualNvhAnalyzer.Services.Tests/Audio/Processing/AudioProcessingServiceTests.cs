using Moq;
using VirtualNvhAnalyzer.Core.Models;
using VirtualNvhAnalyzer.Services.Audio.Strategies;
using VirtualNvhAnalyzer.Services.Audio.Processing;
using VirtualNvhAnalyzer.Core.Common.Interfaces;
using VirtualNvhAnalyzer.Services.Tests.Audio.Strategies;

namespace VirtualNvhAnalyzer.Services.Tests.Audio.Processing
{
    public class AudioProcessingServiceTests
    {
        [Fact]
        public async Task ProcessAsync_UsesCorrectStrategy()
        {
            var strategies = new List<IAudioProcessingStrategy>
            {
                new TestWavProcessingStrategy(),
               
            };
            var service = new AudioProcessingService((IEnumerable<IAudioProcessingStrategy>)strategies);

            var inputPath = "dummy.wav"; // kein echtes File nötig!

            // Act
            var result = await service.ProcessAsync(inputPath);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("test.wav", result.FileName);
            Assert.Equal(44100, result.SampleRate);
        }

        [Fact]
        public async Task ProcessAsync_ThrowsWhenNoStrategyFound()
        {
            // Arrange
            var mockStrategy = new Mock<IAudioProcessingStrategy>();
            mockStrategy.Setup(s => s.CanProcess("file.unknown")).Returns(false);

            var service = new AudioProcessingService(new[] { mockStrategy.Object });

            // Act & Assert
            await Assert.ThrowsAsync<NotSupportedException>(() => service.ProcessAsync("file.unknown"));
        }
    }
}
