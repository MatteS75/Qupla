using System;
using System.IO;
using System.ServiceProcess;

namespace Qupla.IndicatorServer.WindowsService
{
    internal static class Program
    {
        private static void Main()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            ServiceSelfInstaller.StartOrInstallOrUninstall(
                Service.StaticServiceName,
                () =>
                    {
                        var servicesToRun = new ServiceBase[] {new Service()};
                        ServiceBase.Run(servicesToRun);
                    });
        }
    }
}