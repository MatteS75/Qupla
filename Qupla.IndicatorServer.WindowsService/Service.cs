using System.ServiceProcess;

namespace Qupla.IndicatorServer.WindowsService
{
    public partial class Service : ServiceBase
    {
        private Server.Server _server;
        public static string StaticServiceName = "IndicatorServer";

        public Service()
        {
            InitializeComponent();
            ServiceName = StaticServiceName;
        }

        protected override void OnStart(string[] args)
        {
            _server = new Server.Server();
            _server.Start();
        }

        protected override void OnStop()
        {
            _server.Stop();
            _server = null;
        }
    }
}