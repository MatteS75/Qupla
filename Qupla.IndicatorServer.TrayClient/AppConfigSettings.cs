using System.Configuration;

namespace Qupla.IndicatorServer.TrayClient
{
    public class AppConfigSettings : ISettings
    {
        public string HostName
        {
            get { return ConfigurationManager.AppSettings["hostName"]; }
        }
    }
}