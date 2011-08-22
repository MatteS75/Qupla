namespace Qupla.IndicatorServer.JungleDiskBackup
{
    public class ExplicitLogFilePath : ILogFilePath
    {
        private readonly string _path;

        public ExplicitLogFilePath(string path)
        {
            _path = path;
        }

        public string Path
        {
            get { return _path; }
        }
    }
}