namespace Qupla.IndicatorServer.Server
{
    public class WebServerSettings : IWebServerSettings
    {
        public string Url
        {
            get
            {
                return "http://+:7571/";
            }
        }
    }
}