using VirtualNvhAnalyzer.Core.Common.Commands;
using VirtualNvhAnalyzer.Core.Models;
using VirtualNvhAnalyzer.Infrastructure.Configuration.ViewModels;

namespace VirtualNvhAnalyzer.App.ViewModels
{
    public class BaseViewModel : AbstractBaseViewModel
    {
        private Dictionary<string, Func<BaseViewModel>> _viewModels;
        private Dictionary<string, Func<INamedCommand>> _commands;
        private List<ViewModelConfig> _configs;

        public BaseViewModel(Dictionary<string, Func<BaseViewModel>> viewModels, Dictionary<string, Func<INamedCommand>> commands, List<ViewModelConfig> configs)
        {
            _viewModels = viewModels;
            _commands = commands;
            _configs = configs;
        }

        public Dictionary<string, Func<BaseViewModel>> ViewModels => _viewModels;
        public Dictionary<string, Func<INamedCommand>> Commands => _commands;
        public List<ViewModelConfig> Configs => _configs;
    }
}
