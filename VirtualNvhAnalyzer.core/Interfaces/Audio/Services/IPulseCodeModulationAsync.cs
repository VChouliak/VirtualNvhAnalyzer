namespace VirtualNvhAnalyzer.Core.Interfaces.Audio.Services
{
    public interface IPulseCodeModulationAsync
    {
        public Task<IEnumerable<float>> ToPulseCodeModulationAsync();
    }

    public interface IPulseCodeModulationAsync<T>
    {
        public Task<IEnumerable<float>> ToPulseCodeModulationAsync(T input);
    }
}
