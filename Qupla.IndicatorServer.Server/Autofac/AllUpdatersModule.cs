using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Qupla.IndicatorServer.Server.Autofac
{
    public class AllUpdatersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(GetAllAssembliesInBinFolder().ToArray())
                .Where(t => typeof(IDynamicallyLoadedUpdater).IsAssignableFrom(t))
                .As(t => t.GetInterfaces()
                             .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(UpdaterOf<>))
                             .Single()
                );
        }

        private static IEnumerable<Assembly> GetAllAssembliesInBinFolder()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var dllFiles = Directory.GetFiles(path, "*.dll");
            return from f in dllFiles select Assembly.LoadFrom(f);
        }

    }
}