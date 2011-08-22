using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Qupla.IndicatorServer.JungleDiskBackup
{
    public interface IJungleDiskLogFilesProvider
    {
        string GetLatestLogFile();
        string GetLogFilePath();
    }
}