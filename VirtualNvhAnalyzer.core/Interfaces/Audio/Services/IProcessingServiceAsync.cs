namespace VirtualNvhAnalyzer.Core.Interfaces.Audio.Services
{
    public interface IProcessingServiceAsync<TInput, TOutput>
    {
        public Task<TOutput> ProcessAsync(TInput input);
    }

    public interface IProcessingServiceAsync<TInput>
    {
        public Task ProcessAsync(TInput input);
    }
}
