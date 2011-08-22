namespace Qupla.IndicatorServer.JungleDiskBackup
{
    public class AutomaticLogFilePath : ILogFilePath
    {
        private readonly IFileSystem _fileSystem;

        public AutomaticLogFilePath(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public string Path
        {
            get { return System.IO.Path.Combine(_fileSystem.GetCommonApplicationDataPath(), "JungleDisk\\cache\\logs"); }
        }
    }
}