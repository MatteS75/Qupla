using System;

namespace Qupla.IndicatorServer.Server
{
    public class TimerSettings : ITimerSettings
    {
        public TimeSpan Interval
        {
            get { return TimeSpan.FromHours(1); }
        }
    }
}