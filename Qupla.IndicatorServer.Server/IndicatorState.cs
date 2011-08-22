using System;

namespace Qupla.IndicatorServer.Server
{
    public abstract class IndicatorState : IIndicatorState
    {
        protected IndicatorState()
        {
            UpdateTime = DateTime.Now;
        }

        public string Name { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Color { get; set; }
        public string Message { get; set; }
        public string Content { get; set; }
    }
}