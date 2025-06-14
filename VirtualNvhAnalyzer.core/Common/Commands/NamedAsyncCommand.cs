
namespace VirtualNvhAnalyzer.Core.Common.Commands
{
    public class NamedAsyncCommand<T> : INamedAsyncCommand
    {
        private readonly Func<T?, Task> _execute;
        private readonly Predicate<T?>? _canExecute;
        private bool _isExecuting;

        public string Name { get; }       

        public NamedAsyncCommand(Func<T?, Task> execute, Predicate<T?>? canExecute = null, string name = "")
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
            Name = name;
        }

        public bool CanExecute(object? parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke((T?)parameter) ?? true);
        }

        public async void Execute(object? parameter)
        {
            await ExecuteAsync(parameter);
        }

        public async Task ExecuteAsync(object? parameter)
        {
            if (!CanExecute(parameter))
                return;

            try
            {
                _isExecuting = true;
                RaiseCanExecuteChanged();
                await _execute((T?)parameter);
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
