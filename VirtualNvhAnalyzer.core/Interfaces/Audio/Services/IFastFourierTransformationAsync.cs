using System.Numerics;

namespace VirtualNvhAnalyzer.Core.Interfaces.Audio.Services
{
    public interface IFastFourierTransformationAsync
    {
        Task<IEnumerable<Complex>> ToFastFourierTransformationAsync();
    }

    public interface IFastFourierTransformationAsync<T>
    {
        Task<IEnumerable<Complex>> ToFastFourierTransformationAsync(T input);
    }

}
