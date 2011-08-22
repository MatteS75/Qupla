using log4net;

namespace Qupla.IndicatorServer.Server
{
    public class IndicatorStateRepository : Repository<IIndicatorState>
    {
        public IndicatorStateRepository(ILog log) : base(FileName, log) { }

        public static string FileName = "IndicatorStates.json";

        protected override string Sample
        {
            get { return @""; }
        }
    }
}