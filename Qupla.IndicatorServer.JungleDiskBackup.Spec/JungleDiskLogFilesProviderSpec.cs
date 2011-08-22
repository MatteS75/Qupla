using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SharpTestsEx;

namespace Qupla.IndicatorServer.JungleDiskBackup.Spec
{

    [TestFixture]
    public class JungleDiskLogFilesProviderSpec
    {

        [Test]
        public void ShouldGetLogFilePath()
        {
            var fileSystem = new Moq.Mock<IFileSystem>();
            fileSystem.Setup(x => x.GetCommonApplicationDataPath()).Returns(@"C:\Path");
            var target = new JungleDiskLogFilesProvider(fileSystem.Object, new AutomaticLogFilePath(fileSystem.Object));

            var result = target.GetLogFilePath();

            result.Should().Be(@"C:\Path\cache\logs");
        }

        [Test]
        public void ShouldGetAllLogFiles()
        {
            var fileSystem = new Moq.Mock<IFileSystem>();
            var logFiles = new[] { @"C:\Path\backup-1234567890.csv" };
            var nonLogFiles = new[] { @"C:\Path\no-match.csv" };
            fileSystem.Setup(x => x.GetCommonApplicationDataPath()).Returns(@"C:\Path");
            fileSystem.Setup(x => x.GetFiles(@"C:\Path\cache\logs")).Returns(logFiles.Union(nonLogFiles));
            var target = new JungleDiskLogFilesProvider(fileSystem.Object, new AutomaticLogFilePath(fileSystem.Object));

            var result = target.GetAllLogFiles();

            result.Should().Have.SameSequenceAs(logFiles);
        }
    }
}
