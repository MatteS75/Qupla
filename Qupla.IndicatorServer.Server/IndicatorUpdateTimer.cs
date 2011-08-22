using System;
using System.Threading;
using log4net;

namespace Qupla.IndicatorServer.Server
{
    public class IndicatorUpdateTimer
    {
        private readonly ILog _log;
        private readonly IIndicatorUpdateCoordinator _indicatorUpdateCoordinator;
        private readonly ITimerSettings _settings;
        private Timer _timer;

        public IndicatorUpdateTimer(ILog log, IIndicatorUpdateCoordinator indicatorUpdateCoordinator, ITimerSettings settings)
        {
            _log = log;
            _indicatorUpdateCoordinator = indicatorUpdateCoordinator;
            _settings = settings;
        }

        public void Start()
        {
            _log.Info("Starting indicator update timer");
            _timer = new Timer(delegate
                {
                    try
                    {
                        _log.Debug("Updating indicators (Tick)");
                        _indicatorUpdateCoordinator.Update();
                    }
                    catch (Exception ex)
                    {
                        _log.Fatal("Exception in timer: " + ex);
                        StopWithoutWaitForTickToComplete();
                    }
                },
                null,
                TimeSpan.FromSeconds(1),
                _settings.Interval
            );
            _log.Info("Indicator update timer started");
        }

        private void StopWithoutWaitForTickToComplete()
        {
            _log.Info("Stopping indicator update timer");
            _timer.Dispose();
            _timer = null;
            _log.Info("Indicator update timer stopped");
        }

        public void Stop()
        {
            _log.Info("Stopping indicator update timer");
            if (_timer != null)
            {
                var waitHandle = new ManualResetEvent(false);
                _timer.Dispose(waitHandle);
                _timer = null;
                waitHandle.WaitOne();
            }
            _log.Info("Indicator update timer stopped");
        }
    }
}
