using System;

namespace Qupla.IndicatorServer.Server
{
    public interface ITimerSettings
    {
        TimeSpan Interval { get; }
    }
}