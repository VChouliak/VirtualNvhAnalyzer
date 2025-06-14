using System.Windows.Input;

namespace VirtualNvhAnalyzer.Core.Common.Commands
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object? parameter);     
    }
}
