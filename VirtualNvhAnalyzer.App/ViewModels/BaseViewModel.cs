using VirtualNvhAnalyzer.Core.Common.Commands;
using VirtualNvhAnalyzer.Core.Models;
using VirtualNvhAnalyzer.Infrastructure.Configuration.ViewModels;

namespace VirtualNvhAnalyzer.App.ViewModels
{
    public class BaseViewModel : AbstractBaseViewModel
    {
        private Dictionary<string, Func<BaseViewModel>> _viewModelFactories;
        private Dictionary<string, Func<INamedCommand>> _namedCommandFactories;
        private List<ViewModelConfig> _configs;

        public BaseViewModel(Dictionary<string, Func<BaseViewModel>> viewModels, Dictionary<string, Func<INamedCommand>> commands, List<ViewModelConfig> configs)
        {
            _viewModelFactories = viewModels;
            _namedCommandFactories = commands;
            _configs = configs;
        }
        
        public Dictionary<string, BaseViewModel> ViewModels =>
            _viewModelFactories.ToDictionary(kvp => kvp.Key, kvp => kvp.Value());

        public Dictionary<string, INamedCommand> Commands =>
        _namedCommandFactories.ToDictionary(kvp => kvp.Key, kvp => kvp.Value());
    }
}
