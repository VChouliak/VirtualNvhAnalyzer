using VirtualNvhAnalyzer.Core.Common.Commands;

namespace VirtualNvhAnalyzer.App.Commands
{
    public class AsyncCommand : AsyncCommand<object?>
    {
        public AsyncCommand(Func<object?, Task> execute, Predicate<object?>? canExecute = null)
            : base(execute, canExecute)
        {
        }
        public AsyncCommand(Func<Task> execute, Predicate<object?>? canExecute = null)
            : base(_ => execute(), canExecute)
        {
        }
    }


}
