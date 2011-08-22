using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Qupla.IndicatorServer.TrayClient
{
    public class Client : IClient
    {
        private readonly ISettings _settings;

        public Client(ISettings settings)
        {
            _settings = settings;
        }

        public IEnumerable<IndicatorState> GetIndicatorStates()
        {
            var urlString = string.Format("http://{0}:7571/IndicatorStates.json", _settings.HostName);
            var request = WebRequest.CreateDefault(new Uri(urlString));
            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            var streamReader = new StreamReader(stream);
            var jsonTextReader = new JsonTextReader(streamReader);
            var serializer = new JsonSerializer();
            return serializer.Deserialize<IEnumerable<IndicatorState>>(jsonTextReader);
        }
    }
}