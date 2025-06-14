using VirtualNvhAnalyzer.App.Services.Mediator;
using VirtualNvhAnalyzer.Core.Common.Commands;
using VirtualNvhAnalyzer.Infrastructure.Configuration.ViewModels;

namespace VirtualNvhAnalyzer.App.ViewModels
{
    public class AudioImportContainerViewModel : BaseViewModel
    {
        private readonly IMediator _mediator;

        public AudioImportContainerViewModel(Dictionary<string, Func<BaseViewModel>> viewModels, Dictionary<string, Func<INamedCommand>> commands, List<ViewModelConfig> configs, IMediator mediator) 
            : base(viewModels, commands, configs)
        {
            _mediator = mediator;
        }     
    }
}
