using VirtualNvhAnalyzer.Core.Models;

namespace VirtualNvhAnalyzer.Infrastructure.Configuration.ViewModels
{
    public class ViewModelConfig : AbstractBaseViewModel
    {
        public string Key { get; set; } = null!;
        public string ViewModel { get; set; } = null!;
        public string View { get; set; } = null!;
        public List<string> Commands { get; set; } = new();
        public List<string> Children { get; set; } = new();
    }
}
