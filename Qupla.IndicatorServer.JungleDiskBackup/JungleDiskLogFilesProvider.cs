using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Qupla.IndicatorServer.JungleDiskBackup
{
    public class JungleDiskLogFilesProvider : IJungleDiskLogFilesProvider
    {
        private readonly IFileSystem _fileSystem;
        private readonly ILogFilePath _logFilePath;

        public JungleDiskLogFilesProvider(IFileSystem fileSystem, ILogFilePath logFilePath)
        {
            _fileSystem = fileSystem;
            _logFilePath = logFilePath;
        }

        public string GetLatestLogFile()
        {
            var allLogFiles = GetAllLogFiles();
            if (!allLogFiles.Any())
            {
                return null;
            }
            var latestLogFileFirst = allLogFiles.OrderByDescending(f => new FileInfo(f).LastWriteTime);
            var latestLogFile = latestLogFileFirst.First();
            return latestLogFile;
        }

        public IEnumerable<string> GetAllLogFiles()
        {
            var logFileRegex = new Regex(@"\\backup-\d{10}\.csv$");
            var logFilePath = GetLogFilePath();
            var files = _fileSystem.GetFiles(logFilePath);
            var logFiles = from f in files where logFileRegex.Match(f).Success select f;
            return logFiles;
        }

        public string GetLogFilePath()
        {
            return _logFilePath.Path;
        }
    }
}