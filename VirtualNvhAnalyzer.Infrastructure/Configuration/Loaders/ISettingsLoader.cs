namespace VirtualNvhAnalyzer.Infrastructure.Configuration.Loaders
{
    public interface ISettingsLoader<T>
    {
        T Load(string path);
    }
}
