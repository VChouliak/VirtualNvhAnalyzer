namespace VirtualNvhAnalyzer.Core.Common.Interfaces
{
    public interface IProcessingService<TInput, TOutput>
    {
        public Task<TOutput> ProcessAsync(TInput input);
    }
}
