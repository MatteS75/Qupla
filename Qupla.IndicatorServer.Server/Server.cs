using System;
using Autofac;
using Qupla.IndicatorServer.Server.Autofac;

namespace Qupla.IndicatorServer.Server
{
    public class Server
    {
        private static IComponentContext _container;
        private static IndicatorUpdateTimer _indicatorUpdateTimer;
        private static WebServer _webServer;

        public void Start()
        {
            ConfigureLogging();
            ConfigureContainer();

            _indicatorUpdateTimer = _container.Resolve<IndicatorUpdateTimer>();
            _webServer = _container.Resolve<WebServer>();

            _indicatorUpdateTimer.Start();
            _webServer.Start();
        }

        public void Stop()
        {
            _indicatorUpdateTimer.Stop();
            _webServer.Stop();
        }

        private static void ConfigureLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        private static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ServerModule());
            builder.RegisterModule(new AllUpdatersModule());
            builder.RegisterModule(new Log4NetModule());
            _container = builder.Build();
        }
    }
}