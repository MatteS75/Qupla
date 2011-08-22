using System.Collections.Generic;

namespace Qupla.IndicatorServer.TrayClient
{
    public interface IClient
    {
        IEnumerable<IndicatorState> GetIndicatorStates();
    }
}