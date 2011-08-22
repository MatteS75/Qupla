using log4net;

namespace Qupla.IndicatorServer.Server
{
    public class IndicatorConfigurationRepository : Repository<IIndicatorConfiguration>
    {
        public IndicatorConfigurationRepository(ILog log) : base(FileName, log) { }

        public static string FileName = "IndicatorConfiguration.json";

        protected override string Sample
        {
            get
            {
                return @"{
  ""$type"": ""Qupla.IndicatorServer.Server.IIndicatorConfiguration[], Qupla.IndicatorServer.Server"",
  ""$values"": [
    {
      ""$type"": ""Qupla.IndicatorServer.JungleDiskBackup.JungleDiskBackupLogFileExistsConfiguration, Qupla.IndicatorServer.JungleDiskBackup"",
      ""BackupEvery"": ""01:00:00"",
      ""Name"": ""JungleDisk Backup Log File Exists""
    },
    {
        ...
    }
  ]
}";
            }
        }
    }
}