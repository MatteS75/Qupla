using System;

namespace Qupla.IndicatorServer.Server
{
    public interface IIndicatorState
    {
        string Name { get; set; }
        DateTime UpdateTime { get; set; }
        string Color { get; set; }
        string Message { get; set; }
        string Content { get; set; }
    }
}