using VirtualNvhAnalyzer.Core.Common.Commands;

namespace VirtualNvhAnalyzer.App.Commands
{
    public class RelayCommand : RelayCommand<object?>
    {
        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
            : base(execute, canExecute)
        {
        }
        public RelayCommand(Action execute, Predicate<object?>? canExecute = null)
            : base(_ => execute(), canExecute)
        {
        }
    } 
}
