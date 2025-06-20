using VirtualNvhAnalyzer.Core.Common.Options;

namespace VirtualNvhAnalyzer.Infrastructure.Configuration
{
    public class AudioSettings
    {
        public List<string> SupportedFormats { get; set; } = new ();
        public AudioProcessingOptions ProcessingOptions { get; set; } = new();
    }
}
