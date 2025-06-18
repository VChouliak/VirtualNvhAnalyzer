using VirtualNvhAnalyzer.Core.Common.Commands;

namespace VirtualNvhAnalyzer.App.Commands
{
    public class AsyncCommand : NamedAsyncCommand<object?>
    {

        public AsyncCommand(Func<object?, Task> execute, Predicate<object?>? canExecute = null)
            : base(execute, canExecute)
        {
        }
        public AsyncCommand(Func<object?, Task> execute, Predicate<object?>? canExecute = null, string name = "")
          : base(execute, canExecute, name)
        {
        }
        public AsyncCommand(Func<Task> execute, Predicate<object?>? canExecute = null)
            : base(_ => execute(), canExecute)
        {
        }

        public AsyncCommand(Func<Task> execute, Predicate<object?>? canExecute = null, string name = "")
           : base(_ => execute(), canExecute,name)
        {
        }      
    }
}
