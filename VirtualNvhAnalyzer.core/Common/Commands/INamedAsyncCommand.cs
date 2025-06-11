namespace VirtualNvhAnalyzer.Core.Common.Commands
{
    public interface INamedAsyncCommand : IAsyncCommand
    {
        public string Name { get; }
    }
}
