using System.IO;
using System.Net;
using System.Text;
using log4net;

namespace Qupla.IndicatorServer.Server
{
    public class WebServer
    {
        private readonly ILog _log;
        private readonly IWebServerSettings _settings;
        private readonly HttpServer _httpServer;

        public WebServer(ILog log, IWebServerSettings settings)
        {
            _log = log;
            _settings = settings;
            _httpServer = new HttpServer(new[] {settings.Url}, ProcessRequest);
        }

        public void Start()
        {
            _log.InfoFormat("Starting web server on url {0}", _settings.Url);
            _httpServer.Start();
            _log.Info("Web server started");
        }

        public void Stop()
        {
            _log.Info("Stopping web server");
            _httpServer.Stop();
            _log.Info("Web server stopped");
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            var request = context.Request;
            _log.DebugFormat("Received web request with url {0}", request.Url);
            var response = context.Response;
            if (request.Url.PathAndQuery == string.Format("/{0}", IndicatorStateRepository.FileName))
            {
                RespondWithIndicatorStates(request, response);
            }
            else
            {
                RespondWithNotFound(request, response);
            }
        }

        private void RespondWithIndicatorStates(HttpListenerRequest request, HttpListenerResponse response)
        {
            _log.DebugFormat("Responded with contents for url {0}", request.Url);
            var stream = response.OutputStream;
            var file = File.ReadAllBytes(IndicatorStateRepository.FileName);
            response.ContentLength64 = file.Length;
            stream.Write(file, 0, file.Length);
        }

        private void RespondWithNotFound(HttpListenerRequest request, HttpListenerResponse response)
        {
            _log.DebugFormat("Responded with 404 Not Found for url {0}", request.Url);
            response.StatusCode = 404;
            response.StatusDescription = "Not Found";
            response.OutputStream.Close();
        }
    }
}
