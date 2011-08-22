using System.IO;
using System.Linq;
using Qupla.IndicatorServer.Server;

namespace Qupla.IndicatorServer.JungleDiskBackup
{
    public class JungleDiskBackupLogFileContentsUpdater : UpdaterOf<JungleDiskBackupLogFileContentsConfiguration>
    {
        private readonly IJungleDiskLogFilesProvider _jungleDiskLogFilesProvider;

        public JungleDiskBackupLogFileContentsUpdater() : this(new JungleDiskLogFilesProvider(new FileSystem(), new AutomaticLogFilePath(new FileSystem()))) {}

        public JungleDiskBackupLogFileContentsUpdater(IJungleDiskLogFilesProvider jungleDiskLogFilesProvider)
        {
            _jungleDiskLogFilesProvider = jungleDiskLogFilesProvider;
        }

        public IIndicatorState Update(JungleDiskBackupLogFileContentsConfiguration indicatorConfiguration)
        {
            var logFilePath = _jungleDiskLogFilesProvider.GetLogFilePath();
            var latestLogFile = _jungleDiskLogFilesProvider.GetLatestLogFile();
            if (latestLogFile == null)
            {
                return new BasicRedIndicatorState
                    {
                        Name = indicatorConfiguration.Name,
                        Message = string.Format("No log file found in {0}.", logFilePath)
                    };
            }
            var linesText = File.ReadAllLines(latestLogFile);
            var lines = from l in linesText
                        let cells = l.Split(',')
                        select new
                                   {
                                       Result = cells[7]
                                   };
            var allLinesHaveResultOk = lines.All(l => l.Result == "OK");
            var content = string.Join("\n", linesText);
            if (allLinesHaveResultOk)
            {
                return new BasicGreenIndicatorState
                           {
                               Name = indicatorConfiguration.Name,
                               Message = string.Format("All lines OK in file {0}.", latestLogFile),
                               Content = content
                           };
            }
            var linesNotOk = lines.Where((l, i) => l.Result != "OK").Select((l, i) => i);
            var linesNotOkMessage = string.Join(", ", linesNotOk);
            return new BasicRedIndicatorState
                        {
                            Name = indicatorConfiguration.Name,
                            Message = string.Format("All lines NOT OK in file {0}. Lines not OK: {1}", latestLogFile, linesNotOkMessage),
                            Content = content
                        };
        }
    }
}