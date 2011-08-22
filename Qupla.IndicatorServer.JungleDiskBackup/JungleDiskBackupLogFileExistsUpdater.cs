using System;
using System.IO;
using Qupla.IndicatorServer.Server;

namespace Qupla.IndicatorServer.JungleDiskBackup
{
    public class JungleDiskBackupLogFileExistsUpdater : UpdaterOf<JungleDiskBackupLogFileExistsConfiguration>
    {
        private readonly IJungleDiskLogFilesProvider _jungleDiskLogFilesProvider;

        public JungleDiskBackupLogFileExistsUpdater() : this(new JungleDiskLogFilesProvider(new FileSystem(), new AutomaticLogFilePath(new FileSystem()))) {}

        public JungleDiskBackupLogFileExistsUpdater(IJungleDiskLogFilesProvider jungleDiskLogFilesProvider)
        {
            _jungleDiskLogFilesProvider = jungleDiskLogFilesProvider;
        }

        public IIndicatorState Update(JungleDiskBackupLogFileExistsConfiguration indicatorConfiguration)
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
            var fileInfo = new FileInfo(latestLogFile);
            var earliestDate = DateTime.Now.Subtract(indicatorConfiguration.BackupEvery);
            if (fileInfo.LastWriteTime >= earliestDate)
            {
                return new BasicGreenIndicatorState
                {
                    Name = indicatorConfiguration.Name,
                    Message = string.Format("Log file {0} was last changed {1}.", latestLogFile, fileInfo.LastAccessTime)
                };
            }
            return new BasicGreenIndicatorState
            {
                Name = indicatorConfiguration.Name,
                Message = string.Format("Log file {0} was last changed {1}.", latestLogFile, fileInfo.LastAccessTime)
            };
        }
    }
}