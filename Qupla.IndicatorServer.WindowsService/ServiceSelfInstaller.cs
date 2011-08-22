using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;

namespace Qupla.IndicatorServer.WindowsService
{
    public static class ServiceSelfInstaller
    {
        private static readonly string ExePath = Assembly.GetExecutingAssembly().Location;

        public static void StartOrInstallOrUninstall(string serviceName, Action startServiceCallback)
        {
            if (IsStarting(serviceName))
            {
                startServiceCallback.Invoke();
                return;
            }
            if (IsInstalled(serviceName))
            {
                Uninstall();
            }
            else
            {
                Install();
            }
        }
        public static bool IsInstalled(string serviceName)
        {
            return QueryService(serviceName).Any();
        }

        private static IEnumerable<ServiceController> QueryService(string serviceName)
        {
            return from s in ServiceController.GetServices() where s.ServiceName == serviceName select s;
        }

        public static bool IsStarting(string serviceName)
        {
            var services = QueryService(serviceName);
            if (services.Any())
            {
                var service = services.Single();
                return service.Status == ServiceControllerStatus.StartPending;
            }
            return false;
        }

        public static bool Install()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new[] { ExePath });
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool Uninstall()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new [] { "/u", ExePath });
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
