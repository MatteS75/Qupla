using System.Linq;
using Autofac;
using Autofac.Core;
using log4net;

namespace Qupla.IndicatorServer.Server.Autofac
{
    public class Log4NetModule : Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistry registry,
                                                              IComponentRegistration registration)
        {
            registration.Preparing += OnComponentPreparing;
        }

        private static void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            var t = e.Component.Activator.LimitType;
            e.Parameters = e.Parameters.Union(new[]
                                                  {
                                                      new ResolvedParameter((p, i) => p.ParameterType == typeof (ILog),
                                                                            (p, i) => LogManager.GetLogger(t))
                                                  });
        }
    }
}