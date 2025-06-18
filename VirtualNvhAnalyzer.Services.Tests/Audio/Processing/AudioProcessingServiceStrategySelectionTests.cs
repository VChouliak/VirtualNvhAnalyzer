using Moq;
using VirtualNvhAnalyzer.Core.Interfaces.Audio.Strategies;
using VirtualNvhAnalyzer.Services.Audio.Processing;

namespace VirtualNvhAnalyzer.Services.Tests.Audio.Processing
{
    public class AudioProcessingServiceStrategySelectionTests
    {
        [Fact]
        public async Task ProcessAsync_UsesWavStrategy_ForWavFile()
        {
            // Arrange
            var wavStrategy = new Mock<IAudioProcessingStrategy>();
            wavStrategy.Setup(s => s.CanProcess("file.wav")).Returns(true);
            wavStrategy.Setup(s => s.ProcessAsync("file.wav")).Returns(Task.CompletedTask);

            var mp3Strategy = new Mock<IAudioProcessingStrategy>();
            mp3Strategy.Setup(s => s.CanProcess("file.wav")).Returns(false);

            var service = new AudioProcessingService(new[] { wavStrategy.Object, mp3Strategy.Object });

            // Act
            await service.ProcessAsync("file.wav");

            // Assert
            wavStrategy.Verify(s => s.ProcessAsync("file.wav"), Times.Once);
            mp3Strategy.Verify(s => s.ProcessAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task ProcessAsync_UsesMp3Strategy_ForMp3File()
        {
            // Arrange
            var wavStrategy = new Mock<IAudioProcessingStrategy>();
            wavStrategy.Setup(s => s.CanProcess("file.mp3")).Returns(false);

            var mp3Strategy = new Mock<IAudioProcessingStrategy>();
            mp3Strategy.Setup(s => s.CanProcess("file.mp3")).Returns(true);
            mp3Strategy.Setup(s => s.ProcessAsync("file.mp3")).Returns(Task.CompletedTask);

            var service = new AudioProcessingService(new[] { wavStrategy.Object, mp3Strategy.Object });

            // Act
            await service.ProcessAsync("file.mp3");

            // Assert
            mp3Strategy.Verify(s => s.ProcessAsync("file.mp3"), Times.Once);
            wavStrategy.Verify(s => s.ProcessAsync(It.IsAny<string>()), Times.Never);
        }
    }
}
