using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualNvhAnalyzer.Core.Interfaces.Audio
{
    public interface IAudioHandler<TInput, TOutput>
    {
        public TOutput ProcessAudio(TInput input);
    }
}
