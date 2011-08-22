using System;
using System.Collections.Generic;
using System.IO;

namespace Qupla.IndicatorServer.JungleDiskBackup
{
    public class FileSystem : IFileSystem
    {
        public string GetCommonApplicationDataPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        }

        public IEnumerable<string> GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}