
namespace VirtualNvhAnalyzer.Core.Common.Commands
{
    public class NamedRelayCommand<T> : INamedCommand
    {
        public string Name { get; }

        private readonly Action<T?> _execute;
        private readonly Predicate<T?>? _canExecute;

        public NamedRelayCommand(Action<T?> execute, Predicate<T?>? canExecute, string name = "")
        {
            _execute = execute;
            _canExecute = canExecute;
            Name = name;
        }


        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => _canExecute?.Invoke((T?)parameter) ?? true;


        public void Execute(object? parameter) => _execute((T?)parameter);

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
