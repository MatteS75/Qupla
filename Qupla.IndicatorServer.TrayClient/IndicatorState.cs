using System;

namespace Qupla.IndicatorServer.TrayClient
{
    public class IndicatorState
    {
        public DateTime RequestTime { get; set; }
        public string Name { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Color { get; set; }
        public string Message { get; set; }
        public string Content { get; set; }
    }
}