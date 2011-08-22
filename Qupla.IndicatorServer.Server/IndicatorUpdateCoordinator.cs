using System;
using System.Collections.Generic;
using System.Linq;
using log4net;

namespace Qupla.IndicatorServer.Server
{
    public class IndicatorUpdateCoordinator : IIndicatorUpdateCoordinator
    {
        private readonly ILog _log;
        private readonly Repository<IIndicatorConfiguration> _indicatorConfigurationRepository;
        private readonly Repository<IIndicatorState> _indicatorStateRepository;
        private readonly IResolver _resolver;

        public IndicatorUpdateCoordinator(ILog log, Repository<IIndicatorConfiguration> indicatorConfigurationRepository, Repository<IIndicatorState> indicatorStateRepository, IResolver resolver)
        {
            _log = log;
            _indicatorConfigurationRepository = indicatorConfigurationRepository;
            _indicatorStateRepository = indicatorStateRepository;
            _resolver = resolver;
        }

        private class InternalUpdater
        {
            public ILog Log;
            public IIndicatorConfiguration Configuration;
            public IResolver Resolver;

            public IIndicatorState Update()
            {
                var updater = ResolveUpdater();
                try
                {
                    var result = updater.GetType().GetMethod("Update").Invoke(updater, new[] { Configuration });
                    return (IIndicatorState)result;
                }
                catch (Exception e)
                {
                    Log.Error("Exception invoking updater", e);
                    return new ExceptionIndicatorState(Configuration.Name, e);
                }
            }

            private object ResolveUpdater()
            {
                var configurationType = Configuration.GetType();
                var updaterGenericType = typeof(UpdaterOf<>);
                var updaterType = updaterGenericType.MakeGenericType(configurationType);
                return Resolver.Resolve(updaterType);
            }
        }

        public void Update()
        {
            var indicators = _indicatorConfigurationRepository.LoadAll();
            var storedIndicatorStates = _indicatorStateRepository.LoadAll();
            var updatedIndicatorStates = Update(indicators, storedIndicatorStates);
            _indicatorStateRepository.SaveAll(updatedIndicatorStates);
        }

        private IEnumerable<IIndicatorState> Update(IEnumerable<IIndicatorConfiguration> indicatorConfigurations, IEnumerable<IIndicatorState> indicatorStates)
        {
            var updaters = from c in indicatorConfigurations select new InternalUpdater {Log = _log, Configuration = c, Resolver = _resolver};
            var newIndicatorStates = from u in updaters select u.Update();
            return newIndicatorStates.ToList();
        }
    }
}