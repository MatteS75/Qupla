using System.Text;
using Autofac;
using Module = Autofac.Module;

namespace Qupla.IndicatorServer.Server.Autofac
{
    public class ServerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<AutofacResolver>().As<IResolver>();
            builder.RegisterType<IndicatorConfigurationRepository>().As<Repository<IIndicatorConfiguration>>();
            builder.RegisterType<IndicatorStateRepository>().As<Repository<IIndicatorState>>();
            builder.RegisterType<IndicatorUpdateCoordinator>().As<IIndicatorUpdateCoordinator>();
            builder.RegisterType<IndicatorUpdateTimer>();
            builder.RegisterType<WebServer>();
            builder.RegisterType<TimerSettings>().As<ITimerSettings>();
            builder.RegisterType<WebServerSettings>().As<IWebServerSettings>();
        }
    }
}
