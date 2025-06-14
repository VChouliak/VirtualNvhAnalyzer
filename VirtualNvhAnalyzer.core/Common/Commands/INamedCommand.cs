using System.Windows.Input;

namespace VirtualNvhAnalyzer.Core.Common.Commands
{
    public interface INamedCommand : ICommand
    {
        string Name { get; }
    }
}
