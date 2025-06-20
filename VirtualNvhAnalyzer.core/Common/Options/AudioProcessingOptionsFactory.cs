using VirtualNvhAnalyzer.Core.Models;

namespace VirtualNvhAnalyzer.Core.Common.Options
{
    public class AudioProcessingOptionsFactory
    {
        private const double DEFAULT_SAMPLE_RATE_FRACTION = 0.1;
        private const int SMALLEST_POWER_OF_TWO = 1;

        public static float[] CreatePulseCodeModulationBuffer(AudioFileInfo audioFileInfo, AudioProcessingOptions options)
        {
            int sampleRate = audioFileInfo.SampleRate;
            int channels = audioFileInfo.Channels;
            double bufferDurationSeconds = options.BufferDuration.TotalSeconds;

            int framesPerBuffer = (int)(sampleRate * bufferDurationSeconds);
            int bufferSize = framesPerBuffer * channels;

            return new float[bufferSize];        
        }

        public static int CalculateFftSize(int sampleRate, double? fractionOfSampleRate = null)
        {
            double fraction = fractionOfSampleRate ?? DEFAULT_SAMPLE_RATE_FRACTION;

            int rawSize = (int)(sampleRate * fraction);

            int fftSize = SMALLEST_POWER_OF_TWO;

            while (fftSize < rawSize)
            {
                fftSize <<= 1; // Bit-Shift left to multiply by 2
            }

            return fftSize;
        }
    }
}
