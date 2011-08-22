using System;
using Qupla.IndicatorServer.Server;

namespace Qupla.IndicatorServer.JungleDiskBackup
{
    public class JungleDiskBackupLogFileExistsConfiguration : BaseIndicatorConfiguration
    {
        public TimeSpan BackupEvery { get; set; }
    }
}