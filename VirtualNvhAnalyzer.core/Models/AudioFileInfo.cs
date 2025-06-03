using System.Text.Json.Serialization;

namespace VirtualNvhAnalyzer.Core.Models
{
    public class AudioFileInfo
    {
        public string FileName { get;}
        public int SampleRate { get;}
        public int Channels { get;}
        public TimeSpan Duration { get;}

        [JsonConstructor]
        public AudioFileInfo(string fileName, int sampleRate, int channels, TimeSpan duration)
        {
            FileName = fileName;
            SampleRate = sampleRate;
            Channels = channels;
            Duration = duration;
        }
    }
}