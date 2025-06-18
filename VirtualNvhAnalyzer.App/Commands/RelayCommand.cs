using VirtualNvhAnalyzer.Core.Common.Commands;

namespace VirtualNvhAnalyzer.App.Commands
{
    public class RelayCommand : NamedRelayCommand<object?>
    {

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
            : base(execute, canExecute)
        {
        }
        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute, string name)
           : base(execute, canExecute, name)
        {
        }
        public RelayCommand(Action execute, Predicate<object?>? canExecute = null)
            : base(_ => execute(), canExecute)
        {
        }
        public RelayCommand(Action execute, Predicate<object?>? canExecute, string name)
            : base(_ => execute(), canExecute, name)
        {
        }       
    } 
}
