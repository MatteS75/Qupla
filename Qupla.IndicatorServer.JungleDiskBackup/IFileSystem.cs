using System.Collections.Generic;

namespace Qupla.IndicatorServer.JungleDiskBackup
{
    public interface IFileSystem
    {
        string GetCommonApplicationDataPath();
        IEnumerable<string> GetFiles(string path);
    }
}