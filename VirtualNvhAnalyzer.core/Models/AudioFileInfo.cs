namespace VirtualNvhAnalyzer.Core.Models
{
    public class AudioFileInfo
    {
        public string FileName { get; set; }
        public int SampleRate { get; set; }
        public int Channels { get; set; }
        public TimeSpan Duration { get; set; }
    }
}