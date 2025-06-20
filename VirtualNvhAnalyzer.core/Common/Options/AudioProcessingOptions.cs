namespace VirtualNvhAnalyzer.Core.Common.Options
{
    public class AudioProcessingOptions
    {
        public int BufferDurationMs { get; set; } = 20;

        public int FftSize { get; set; } = 0;

        public double FftSizeFractionOfSampleRate { get; set; } = 0.1;

        public TimeSpan BufferDuration => TimeSpan.FromMilliseconds(BufferDurationMs);

    }
}
