using System;

namespace Qupla.IndicatorServer.Server
{
    public class ExceptionIndicatorState : IndicatorState
    {
        public ExceptionIndicatorState()
        {
            Color = "DarkRed";
        }

        public ExceptionIndicatorState(string name, Exception exception) : this()
        {
            Name = name;
            Message = exception.Message;
            Content = exception.StackTrace;
        }
    }
}